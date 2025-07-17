using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Integracao.Magento.Utils;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;

namespace cwkGestao.Integracao.Magento.Importacao
{
    public class ImportadorProdutos
    {
        private readonly MagentoConfiguracao configuracao;
        private readonly Buscador importador;
        private Filial filial;
        private Hashtable produtosMagentoCadastrados;
        private string proximoCodigoDisponivel;
        private Produto produtoGestao;

        public ImportadorProdutos(MagentoConfiguracao configuracao, Buscador importador, Filial filial)
        {
            this.configuracao = configuracao;
            this.importador = importador;
            this.filial = filial;

            produtosMagentoCadastrados = ProdutoController.Instancia.GetProdutosImportados();
        }

        public void Importar()
        {
            foreach (var ProdutoMagento in GetProdutosMagento())
            {
                produtoGestao = BuscarProduto(ProdutoMagento);

                if (produtoGestao == null)
                    IncluirProdutoInexistente(ProdutoMagento);
                else
                    AlterarProdutoExistente(produtoGestao, ProdutoMagento);
            }
        }

        public Produto Importar(string id)
        {
            catalogProductReturnEntity produtoMagento = importador.ImportarProduto(id);
            produtoGestao = BuscarProduto(produtoMagento);

            if (produtoGestao == null)
                if (!produtosMagentoCadastrados.ContainsKey(produtoMagento.product_id))
                    return IncluirProdutoInexistente(produtoMagento);


            return produtoGestao;            
        }

        private Produto BuscarProduto(catalogProductReturnEntity produtoMagento)
        {
            int id = Convert.ToInt32(produtoMagento.product_id);

            if (produtosMagentoCadastrados.Contains(id))
                return (Produto)produtosMagentoCadastrados[id];

            return null;
        }

        private IEnumerable<catalogProductReturnEntity> GetProdutosMagento()
        {
            return importador.ImportarProdutos();
        }

        private void AlterarProdutoExistente(Produto produto, catalogProductReturnEntity produtoMagento)
        {
            produto.Nome = produtoMagento.name;
            produto.DescReduzida = produtoMagento.short_description;
            produto.DescLonga1 = produto.Nome;

            ProdutoController.Instancia.Salvar(produto, Acao.Alterar);
        }

        private Produto IncluirProdutoInexistente(catalogProductReturnEntity produtoMagento)
        {
            proximoCodigoDisponivel = ProdutoController.Instancia.MaxCodigoIntegracao();
#pragma warning disable CS0219 // A variável "auxPreco" é atribuída, mas seu valor nunca é usado
            string auxPreco = "";
#pragma warning restore CS0219 // A variável "auxPreco" é atribuída, mas seu valor nunca é usado

            Produto produto = new Produto();

            produto.Codigo = proximoCodigoDisponivel;
            produto.PrecoBase = produtoMagento.price.ValorMagentoToDecimal();
            produto.IDIntegracao = Convert.ToInt32(produtoMagento.product_id);
            produto.Nome = produtoMagento.name;
            produto.DescReduzida = produtoMagento.short_description.Replace("\r\n"," ");
            produto.DescLonga1 = produto.Nome;
            produto.Unidade = configuracao.Unidade;
            produto.Fornecedor = configuracao.Fornecedor;
            produto.GrupoEstoque = configuracao.GrupoEstoque;
            produto.InfAdicionais = produtoMagento.short_description.Replace("\r\n", " ");

            TabelaPrecoProduto tabelaPreco = new TabelaPrecoProduto();

            tabelaPreco.TabelaPreco = configuracao.TabelaPreco;
            tabelaPreco.Produto = produto;
            tabelaPreco.Codigo = 1;
            produto.TabelaPrecoProdutos.Add(tabelaPreco);

            Estoque estoque = new Estoque();

            estoque.Produto = produto;
            estoque.LocalEstoque = configuracao.LocalEstoque;

            foreach (var ProdutoEstoqueMagento in importador.ImportarProdutoEstoque(produtoMagento.product_id))
            {
                if (ProdutoEstoqueMagento.qty == "0")
                    continue;

                estoque.Qtd_Estoque_Fisico = Convert.ToDecimal(ProdutoEstoqueMagento.qty);
                estoque.Qtd_Estoque_Min = Convert.ToDecimal(ProdutoEstoqueMagento.qty);
            }

            produto.Estoques.Add(estoque);

            ProdutoController.Instancia.Salvar(produto, Acao.Incluir);

            return produto;
        }
    }
}
