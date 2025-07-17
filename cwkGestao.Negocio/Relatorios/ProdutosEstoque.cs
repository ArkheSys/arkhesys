using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cwkGestao.Negocio.Relatorios
{
    public class ProdutosEstoque
    {
        private ProdutoController produtoController = ProdutoController.Instancia;
        private GrupoEstoqueController grupoEstoqueController = GrupoEstoqueController.Instancia;

        private string filiais;

        public ProdutosEstoque(string filiais)
        {
            this.filiais = filiais;
        }

        public IList<object> GerarPorGrupoEstoque(GrupoEstoque grupoEstoque, string situacao, bool bCusto, int IndiceEstoque = 0,
            int? IDLocalEstoque = null, int IdFornecedor = 0)
        {
            if (grupoEstoque == null)
                grupoEstoque = new GrupoEstoque() { Classificacao = String.Empty };

            IList<pxyEstoqueProduto> estoques = produtoController.GetListaEstoque(filiais, grupoEstoque, situacao, IDLocalEstoque, IdFornecedor);
            IList<GrupoEstoque> gruposEstoque = grupoEstoqueController.GetGruposEstoquePorClassificacao(grupoEstoque.Classificacao);

            return Gerar(estoques, gruposEstoque, grupoEstoque, bCusto, IndiceEstoque);
        }

        public IList<object> GerarPorProduto(Produto produto, bool bCusto, int IndiceEstoque = 0, int? IDLocalEstoque = null)
        {
            IList<pxyEstoqueProduto> estoques = produtoController.GetListaEstoque(filiais, produto, IDLocalEstoque);
            IList<GrupoEstoque> gruposEstoque = grupoEstoqueController.GetGruposEstoquePorClassificacao(produto.GrupoEstoque.Classificacao);

            return Gerar(estoques, gruposEstoque, produto.GrupoEstoque, bCusto, IndiceEstoque);
        }

        public IList<object> GerarPorProdutoEGrupoEstoque(Produto produto, GrupoEstoque grupoEstoque, bool bCusto, int? IDLocalEstoque)
        {
            IList<pxyEstoqueProduto> estoques = produtoController.GetListaEstoque(filiais, produto, IDLocalEstoque);
            IList<GrupoEstoque> gruposEstoque = grupoEstoqueController.GetGruposEstoquePorClassificacao(grupoEstoque.Classificacao);

            return Gerar(estoques, gruposEstoque, produto.GrupoEstoque, bCusto);
        }

        private IList<object> Gerar(IList<pxyEstoqueProduto> estoques, IList<GrupoEstoque> gruposEstoque, GrupoEstoque grupoEstoquePai, bool bCusto, int IndiceEstoque = 0)
        {
            IList<object> retorno = new List<object>();
            string[] niveisGruposEstoque = new string[16];
            GrupoEstoque grupoEstoqueProd;
            foreach (pxyEstoqueProduto mov in estoques.OrderBy(m2 => m2.ClassificacaoGrupoEstoque).OrderBy(o => o.Produto))
            {
                if ((IndiceEstoque == 1 && mov.EstoqueFisico < 0) || (IndiceEstoque == 2 && mov.EstoqueFisico >= 0))
                    continue;

                // Fazer Tratamento de Local de Estoque

                grupoEstoqueProd = gruposEstoque.Where(p => p.ID == mov.IDGrupoEstoque).First();
                LimpaNiveisGruposEstoque(niveisGruposEstoque);
                short indiceFinal = Convert.ToInt16(niveisGruposEstoque.Length - 1);
                PreencheNiveisGruposEstoque(grupoEstoquePai.ID, grupoEstoqueProd, niveisGruposEstoque, ref indiceFinal);
                AdicionaRegistro(retorno, niveisGruposEstoque, mov, bCusto);
            }
            return retorno;
        }

        private void LimpaNiveisGruposEstoque(string[] niveisGruposEstoque)
        {
            for (int i = 0; i < niveisGruposEstoque.Length; i++)
            {
                niveisGruposEstoque[i] = String.Empty;
            }
        }

        private void PreencheNiveisGruposEstoque(int idPaiDeTodos, GrupoEstoque grupoEstoque, string[] niveisGruposEstoque, ref short indiceFinal)
        {
            if (grupoEstoque.ID != idPaiDeTodos && grupoEstoque.GrupoEstoquePai != null)
                PreencheNiveisGruposEstoque(idPaiDeTodos, grupoEstoque.GrupoEstoquePai, niveisGruposEstoque, ref indiceFinal);
            if (indiceFinal >= 0)
            {
                if (grupoEstoque.ID == idPaiDeTodos || grupoEstoque.GrupoEstoquePai != null || idPaiDeTodos == 0)
                    niveisGruposEstoque[indiceFinal--] = grupoEstoque.Classificacao + " - " + grupoEstoque.Nome;
            }
            else
                throw new Exception("O relatório não suporta a quantidade de grupos estoque utilizada.");
        }

        private static void AdicionaRegistro(IList<object> retorno, string[] niveisGrupoEstoque, pxyEstoqueProduto estoque, bool bCusto)
        {
            if (bCusto)
            {
                decimal precoProduto = estoque.Custo * (estoque.Saldo ?? 0);
                precoProduto = precoProduto < 0 ? 0 : precoProduto;
 
                retorno.Add(new
                {
                    Produto = estoque.Produto,
                    Unidade = estoque.Unidade,
                    Fisico = estoque.EstoqueFisico,
                    Reserva = estoque.QtdPedido,
                    Efetivo = estoque.Saldo,
                    Custo = estoque.Custo,
                    CustoTotal = precoProduto,
                    GrupoEstoque = estoque.NomeGrupoEstoque,
                    IDGrupoEstoque = estoque.IDGrupoEstoque,
                    Filial = estoque.Filial,
                    CodigoProduto = estoque.CodigoProduto
                });
            }
            else
            {
                retorno.Add(new
                {
                    Filial = estoque.Filial,
                    Produto = estoque.Produto,
                    Unidade = estoque.Unidade,
                    Fisico = estoque.EstoqueFisico,
                    Reserva = estoque.QtdPedido,
                    Efetivo = estoque.Saldo,
                    EstoqueMinimo = estoque.EstoqueMin,
                    Nivel0 = niveisGrupoEstoque[0],
                    Nivel1 = niveisGrupoEstoque[1],
                    Nivel2 = niveisGrupoEstoque[2],
                    Nivel3 = niveisGrupoEstoque[3],
                    Nivel4 = niveisGrupoEstoque[4],
                    Nivel5 = niveisGrupoEstoque[5],
                    Nivel6 = niveisGrupoEstoque[6],
                    Nivel7 = niveisGrupoEstoque[7],
                    Nivel8 = niveisGrupoEstoque[8],
                    Nivel9 = niveisGrupoEstoque[9],
                    Nivel10 = niveisGrupoEstoque[10],
                    Nivel11 = niveisGrupoEstoque[11],
                    Nivel12 = niveisGrupoEstoque[12],
                    Nivel13 = niveisGrupoEstoque[13],
                    Nivel14 = niveisGrupoEstoque[14],
                    Nivel15 = niveisGrupoEstoque[15],
                    Fornecedor = estoque.Fornecedor,
                    LocalEstoque = estoque.LocalEstoque
                });
            }
        }
    }
}
