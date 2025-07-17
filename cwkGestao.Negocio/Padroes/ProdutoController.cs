using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

using cwkGestao.Integracao.Ecommerce;
using cwkGestao.Integracao.Ecommerce.Entidades;
using cwkGestao.Integracao.Ecommerce.Enums;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ProdutoController : BaseController<Produto>
    {
        #region Singleton

        private static readonly IRepositorioProduto repositorioProduto;

        private ProdutoController() { }

        static ProdutoController()
        {
            Instancia = new ProdutoController();
            repositorioT = RepositorioFactory<Produto>.GetRepositorio();
            repositorioProduto = (IRepositorioProduto)repositorioT;
        }

        public static ProdutoController Instancia { get; }

        #endregion

        public override IList<Produto> GetAll()
        {
            if (ConfiguracaoController.Instancia.GetConfiguracao().MostraProdutosInativos)
                return base.GetAll();
            return repositorioProduto.GetAllAtivos();
        }

        public IList<ProdutoMercadoLivre> GetCodigosMercadoLivre(int idProduto)
        {
            return repositorioProduto.GetCodigosMercadoLivre(idProduto);
        }

        public IList<Produto> GetInativos()
        {
            return repositorioProduto.GetAllInativos();
        }

        public override Dictionary<string, string> ValidaObjeto(Produto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Produto> FindByName(string p)
        {
            return repositorioProduto.FindByName(p);
        }

        public IList<Produto> ListaByBarra(string barra)
        {
            IList<Produto> barrasencontradas = repositorioProduto.FindByBarra(barra);
            return barrasencontradas;
        }

        public Produto FindByBarra(string p)
        {
            IList<Produto> barrasencontradas = repositorioProduto.FindByBarra(p);
            if (barrasencontradas.Count > 1)
                throw new ApplicationException("Mais de um produto com o código de barras informado");
            return barrasencontradas.FirstOrDefault();
        }

        public Produto FindByCodigoBarraOuCodigo(string codPesquisa, bool TelaPDV = false)
        {
            Produto objProduto = null;

            if (codPesquisa.Length >= 8)
            {
                // Trata como um código de barras padrão
                objProduto = FindByCodigoBarra(codPesquisa);

                // Se o código tem exatamente 13 dígitos e começa com '2', não precisa de mais verificações
                if (codPesquisa.Length == 13 && codPesquisa.StartsWith("2"))
                {
                    return objProduto;
                }
            }
            else if (codPesquisa.Length <= 7)
            {
                // Trata como um código interno
                objProduto = LoadProdutoByCodigo(codPesquisa);
            }

            // Se objProduto ainda for nulo, outras lógicas de busca podem ser aplicadas aqui

            return objProduto;
        }

        public Produto FindByDescricao(string pDescricao)
        {
            Produto objProduto = new Produto();

            objProduto = LoadProdutoByDescricao(pDescricao);

            //if ((objProduto == null) || (objProduto == new Produto()))
            //{
            //    objProduto = this.FindByCodigoBarra(pDescricao);
            //}

            return objProduto;
        }

        public Produto FindByCodigoBarras(string Barra)
        {
            return repositorioProduto.GetProdutoPorCodigoDeBarras(Barra);
        }

        public Produto FindByCodigoBarra(string barra)
        {
            var listaProduto = Instancia.GetAll();
            var produtoSelecionado = listaProduto.Where(i => i.Barra == barra).FirstOrDefault();

            if (produtoSelecionado != null) return produtoSelecionado;

            produtoSelecionado = Instancia.FindByBarra(barra);

            if (produtoSelecionado != null) return produtoSelecionado;

            return repositorioProduto.FindByCodigoBarra(barra);
        }

        public Produto FindByBarraObjCompleto(string p)
        {
            IList<Produto> barrasencontradas = repositorioProduto.FindByBarraObjCompleto(p);
            if (barrasencontradas.Count > 1)
                throw new ApplicationException("Mais de um produto com o código de barras informado");
            return barrasencontradas.FirstOrDefault();
        }

        public Hashtable GetProdutosImportados()
        {
            return repositorioProduto.GetProdutosImportados();
        }

        public Hashtable GetProdutosImportadosPorCodigo()
        {
            return repositorioProduto.GetProdutosImportadosPorCodigo();
        }

        private Configuracao config;

        public enum NCMCST
        {
            NCM = 0,
            CST = 1,
            CSOSN = 2
        }

        public int AtualizaProdutos(GrupoEstoque grupo, NCMCST opcao, string novoValor)
        {
            return repositorioProduto.AtualizaProdutos(grupo, opcao.ToString(), novoValor);
        }

        public List<string> verificaTabPreco(TabelaPreco tabela, List<Produto> produtos)
        {
            List<string> lista = new List<string>();
            foreach (Produto produto in produtos)
                if (repositorioProduto.GetTabelasPreco(produto).Count == 0)
                    lista.Add("Produto: " + produto.Codigo + " - " + produto.Nome);
            return lista;
        }

        public decimal getPrecoBase(Produto produto)
        {
            config = ConfiguracaoController.Instancia.GetConfiguracao();

            decimal preco = 0;
            decimal frete = 0;
            decimal ipi = 0;
            decimal custoex = 0;

            preco = produto.PrecoFornecedor - produto.PrecoFornecedor * produto.DescontoFornecedor / 100;
            frete = preco * produto.CustoFrete / 100;
            ipi = preco * produto.CustoIPI / 100;
            custoex = preco * produto.CustoExtra / 100;

            preco = preco + frete + ipi + custoex;

            if (config.PrecisaoPrecoProd)
                return preco;
            return Math.Round(preco, 2);
        }

        public decimal getPreco(Produto produto, TabelaPreco tabelaPreco, TipoPrecoType tipoPreco)
        {
            return getPreco(produto, tabelaPreco, tipoPreco, null);
        }

        public decimal getPreco(Produto produto, TabelaPreco tabelaPreco, TipoPrecoType tipoPreco, Condicao condicao)
        {
            return getPreco(produto.ID, produto.PrecoBase, tabelaPreco.ID, tipoPreco, condicao);
        }

        public decimal getPreco(int idproduto, decimal precobase, int idtabelaPreco, TipoPrecoType tipoPreco, Condicao condicao)
        {
            config = ConfiguracaoController.Instancia.GetConfiguracao();

            var objTabelaPrecoProduto = TabelaPrecoProdutoController.Instancia.GetByTabelaPrecoProduto(idtabelaPreco, idproduto);

            decimal preco = getPreco(precobase, objTabelaPrecoProduto.MargemLucro, objTabelaPrecoProduto.PAcrescimo, objTabelaPrecoProduto.PDesconto, tipoPreco);

            if (condicao != null && tipoPreco == TipoPrecoType.Normal)
                //preco = preco * (100 + condicao.Variacao) / 100;
                preco = condicao.CalculaVariacao(preco);

            if (config.PrecisaoPrecoProd)
                return preco;
            return Math.Round(preco, 2);
        }

        public decimal getPreco(decimal precobase, decimal margemLucro, decimal acrescimo, decimal desconto, TipoPrecoType tipoPreco)
        {
            config = ConfiguracaoController.Instancia.GetConfiguracao();

            decimal preco = 0;
            preco = Math.Round(precobase + precobase * margemLucro / 100, 2);
            switch (tipoPreco)
            {
                case TipoPrecoType.Mínimo:
                    preco -= Math.Round(preco * desconto / 100, 2);
                    break;
                case TipoPrecoType.Normal:

                    break;
                case TipoPrecoType.Máximo:
                    preco += Math.Round(preco * acrescimo / 100, 2);
                    break;
            }

            if (config.PrecisaoPrecoProd)
                return preco;
            return Math.Round(preco, 2);
        }

        public bool CodigoExiste(Produto produto, string codigo)
        {
            Produto candidato = repositorioProduto.GetProdutoByCodigo(codigo);
            return candidato == null || !produto.Equals(candidato);
        }

        public IList GetPrecosProduto(int idProduto)
        {
            Produto produto = repositorioProduto.LoadObjectById(idProduto);
            var lista = from tp in produto.TabelaPrecoProdutos
                        select new
                        {
                            Tabela = tp.TabelaPreco.Nome,
                            PrecoNormal = string.Format("{0:C2}", getPreco(produto.PrecoBase, tp.MargemLucro, tp.PAcrescimo, tp.PDesconto, TipoPrecoType.Normal)),
                            PrecoMinimo = string.Format("{0:C2}", getPreco(produto.PrecoBase, tp.MargemLucro, tp.PAcrescimo, tp.PDesconto, TipoPrecoType.Mínimo)),
                            PrecoMaximo = string.Format("{0:C2}", getPreco(produto.PrecoBase, tp.MargemLucro, tp.PAcrescimo, tp.PDesconto, TipoPrecoType.Máximo)),
                        };

            return lista.ToList();
        }

        public IList<pxPrecoProduto> GetPrecosProdutoParaCotacao(int idProduto)
        {
            Produto produto = repositorioProduto.LoadObjectById(idProduto);
            if (produto == null)
                return new List<pxPrecoProduto>();

            var lista = from tp in produto.TabelaPrecoProdutos
                        select new pxPrecoProduto
                        {
                            Tabela = tp.TabelaPreco.Nome,
                            PrecoNormal = getPreco(produto.PrecoBase, tp.MargemLucro, tp.PAcrescimo, tp.PDesconto, TipoPrecoType.Normal),
                            PrecoMinimo = getPreco(produto.PrecoBase, tp.MargemLucro, tp.PAcrescimo, tp.PDesconto, TipoPrecoType.Mínimo),
                            PrecoMaximo = getPreco(produto.PrecoBase, tp.MargemLucro, tp.PAcrescimo, tp.PDesconto, TipoPrecoType.Máximo),
                        };

            return lista.ToList();
        }

        public Produto LoadProdutoByCodigo(string codigo)
        {
            return repositorioProduto.GetProdutoByCodigo(codigo);
        }

        public Produto LoadProdutoByDescricao(string pDescricao)
        {
            return repositorioProduto.GetProdutoByDescricao(pDescricao);
        }

        public IList<Produto> LoadProdutoByCodigoString(string codigo)
        {
            return repositorioProduto.GetProdutoByCodigoString(codigo);
        }

        public TabelaPrecoProduto GetTabelaPrecoProduto(Produto produto, TabelaPreco tabelaPreco)
        {
            return repositorioProduto.GetTabelaPrecoProduto(tabelaPreco, produto);
        }

        public string GetProximoCodigoBarrasEAN13(IList<string> CodigosExistentes)
        {
            //      lógica antiga

            //public string GetProximoCodigoBarrasEAN13()
            //{
            //string ultimo = repositorioProduto.GetUltimoCodigoBarrasEAN13();

            //if (ultimo.Length < 13)
            //{
            //    var complemento = "0";
            //    while (ultimo.Length < 14)
            //    {
            //        ultimo = complemento + ultimo;
            //    }
            //}

            //if (ultimo.Length > 13)
            //{
            //    while (ultimo.Length > 14)
            //    {
            //        ultimo = ultimo.Substring(1, ultimo.Length); ;
            //    }
            //}

            //string proximo = "0000000000001";

            //if (!String.IsNullOrEmpty(ultimo))
            //    proximo = String.Format("{0:000000000000}", Convert.ToInt64(ultimo.Substring(0, 12)) + 1);

            // LÓGICA NOVA -- Gera códigos de barra únicos para todo o sistema, verificando
            // inclusive códigos gerados em tempo de execução

            IList<string> CodBarras = repositorioProduto.GetAllCodigoBarras();
            IList<string> AllCodes = new List<string>();

            foreach (string barra in CodBarras) AllCodes.Add(barra);

            if (CodigosExistentes != null)
                foreach (string item in CodigosExistentes)
                    AllCodes.Add(item);
            SortedDictionary<long, long> Codes = new SortedDictionary<long, long>();
            long cod;
            foreach (string item in AllCodes)
            {
                try
                {
                    cod = Convert.ToInt64(item);
                }
                catch
                {
                    continue;
                }

                if (!Codes.ContainsKey(cod) && cod < 999999999999) Codes.Add(cod, cod);
            }

            List<long> ListaChaves = Codes.Keys.ToList();
            bool flag = false;
            long numero = 1;

            if (ListaChaves.Count > 0 && ListaChaves.Last() < 999999999999)
            {
                //procura "buracos" na numeracao. caso nao ache, adiciona sequencialmente

                int j = 0;
                while (!flag && j + 1 < ListaChaves.Count)
                {
                    for (long i = ListaChaves[j]; i < ListaChaves[j + 1]; i++)
                        if (!Codes.ContainsKey(i))
                        {
                            flag = true;
                            numero = i;
                            break;
                        }

                    j = j + 1;
                }

                if (!flag) numero = ListaChaves.Last() + 1;
            }

            string proximo = string.Format("{0:000000000000}", numero);

            string valor = proximo + GerarDigitoEAN13(proximo);
            if (valor.Length == 14)
                valor = valor.Substring(1, valor.Length - 1);

            return valor;
        }

        public string GerarDigitoEAN13(string proximo)
        {
            int somaMultiplicacao = 0;
            int maiorMultiplo10;
            for (int i = 0; i < proximo.Length; i++)
            {
                var digito = Convert.ToInt32(proximo[i].ToString());
                somaMultiplicacao += digito * (i % 2 == 0 ? 1 : 3);
            }

            var aux = string.Format("{0:00}", somaMultiplicacao);

            if (somaMultiplicacao % 10 == 0)
                maiorMultiplo10 = somaMultiplicacao;
            else
                maiorMultiplo10 = Convert.ToInt32(Convert.ToInt32(aux.Substring(0, 1)) + 1 + "0");

            return (maiorMultiplo10 - somaMultiplicacao).ToString();
        }

        private string TratarZerosBarra(string barra)
        {
            barra = Regex.Replace(barra, "^0+(?!$)", "");

            while (barra.Length < 13)
                barra += "0";

            return barra;
        }

        public virtual string MaxCodigoIntegracao()
        {
            return repositorioProduto.MaxCodigoIntegracao();
        }

        public virtual Produto getProdutoByIDIntegracao(string id)
        {
            return repositorioProduto.getProdutoByIDIntegracao(id);
        }

        #region Relatórios

        public IList<object> GetRelatorio(TipoRelatorioProduto tipo, string inicial, string final, bool codigoStr, int Status)
        {
            return repositorioProduto.GetRelatorio(tipo, inicial, final, codigoStr, Status);
        }

        private readonly IRepositorioGrupoEstoque repositorioGruposEstoque = (IRepositorioGrupoEstoque)RepositorioFactory<GrupoEstoque>.GetRepositorio();

        public IList<object> GetRelatorioPorGrupoEstoque(GrupoEstoque grupoEstoque)
        {
            if (grupoEstoque == null)
                grupoEstoque = new GrupoEstoque { Classificacao = string.Empty };

            IList<Produto> produtos = repositorioProduto.GetRelatorioPorGrupoEstoque(grupoEstoque.Classificacao);
            IList<GrupoEstoque> gruposEstoque = repositorioGruposEstoque.GetGruposEstoquePorClassificacao(grupoEstoque.Classificacao);

            IList<object> retorno = new List<object>();
            string[] niveisGruposEstoque = new string[16];
            GrupoEstoque grupoEstoqueProd;
            foreach (Produto mov in produtos.OrderBy(m2 => m2.GrupoEstoque.Classificacao))
            {
                grupoEstoqueProd = gruposEstoque.Where(p => p.ID == mov.GrupoEstoque.ID).First();
                LimpaNiveisGruposEstoque(niveisGruposEstoque);
                short indiceFinal = Convert.ToInt16(niveisGruposEstoque.Length - 1);
                PreencheNiveisGruposEstoque(grupoEstoque.ID, grupoEstoqueProd, niveisGruposEstoque, ref indiceFinal);
                AdicionaRegistroRelatorioPorGrupoEstoque(retorno, niveisGruposEstoque, mov);
            }

            return retorno;
        }

        private void AdicionaRegistroRelatorioPorGrupoEstoque(IList<object> retorno, string[] niveisGrupoEstoque, Produto prod)
        {
            retorno.Add(new
            {
                prod.ID,
                prod.Codigo,
                prod.Nome,
                prod.NCM,
                Unidade = prod.Unidade.Sigla,
                prod.Barra,
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
                Nivel15 = niveisGrupoEstoque[15]
            });
        }

        public IList<object> GetListagemPrecos(GrupoEstoque grupoEstoque, TabelaPreco tabelaPreco)
        {
            if (grupoEstoque == null)
                grupoEstoque = new GrupoEstoque { Classificacao = string.Empty };

            IList<pxyListagemPreco> produtos = repositorioProduto.GetListagemPorGrupoEstoqueETabelaPreco(grupoEstoque.Nome, tabelaPreco);
            IList<GrupoEstoque> gruposEstoque = repositorioGruposEstoque.GetGruposEstoquePorClassificacao(grupoEstoque.Classificacao);
            IList<object> retorno = new List<object>();
            string[] niveisGrupoEstoque = new string[16];
            GrupoEstoque grupoEstoqueProd;
            foreach (var prod in produtos)
            {
                grupoEstoqueProd = gruposEstoque.Where(p => p.ID == prod.IDGrupoEstoque).First();
                LimpaNiveisGruposEstoque(niveisGrupoEstoque);
                short indiceFinal = Convert.ToInt16(niveisGrupoEstoque.Length - 1);
                PreencheNiveisGruposEstoque(grupoEstoque.ID, grupoEstoqueProd, niveisGrupoEstoque, ref indiceFinal);
                AdicionaRegistroListagemPrecos(retorno, niveisGrupoEstoque, prod);
            }

            return retorno;
        }

        public IList<object> GetListagemPrecosSemInativos(GrupoEstoque grupoEstoque, TabelaPreco tabelaPreco)
        {
            if (grupoEstoque == null)
                grupoEstoque = new GrupoEstoque { Classificacao = string.Empty };

            IList<pxyListagemPreco> produtos = repositorioProduto.GetListagemPorGrupoEstoqueETabelaPrecoSemInativos(grupoEstoque.Nome, tabelaPreco);
            IList<GrupoEstoque> gruposEstoque = repositorioGruposEstoque.GetAll();
            IList<object> retorno = new List<object>();
            string[] niveisGrupoEstoque = new string[16];
            GrupoEstoque grupoEstoqueProd;
            foreach (var prod in produtos)
            {
                grupoEstoqueProd = gruposEstoque.Where(p => p.Codigo == prod.IDGrupoEstoque).First();
                LimpaNiveisGruposEstoque(niveisGrupoEstoque);
                short indiceFinal = Convert.ToInt16(niveisGrupoEstoque.Length - 1);
                PreencheNiveisGruposEstoque(grupoEstoque.ID, grupoEstoqueProd, niveisGrupoEstoque, ref indiceFinal);
                AdicionaRegistroListagemPrecos(retorno, niveisGrupoEstoque, prod);
            }

            return retorno;
        }

        private void AdicionaRegistroListagemPrecos(IList<object> retorno, string[] niveisGrupoEstoque, pxyListagemPreco prod)
        {
            retorno.Add(new
            {
                CodigoProduto = prod.Codigo,
                prod.NomeProduto,
                prod.Unidade,
                prod.Preco,
                prod.NomeGrupo,
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
            });
        }

        private void LimpaNiveisGruposEstoque(string[] niveisGruposEstoque)
        {
            for (int i = 0; i < niveisGruposEstoque.Length; i++) niveisGruposEstoque[i] = string.Empty;
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
            {
                throw new Exception("O relatório não suporta a quantidade de grupos estoque utilizada.");
            }
        }

        public IList<ImpressaoEtiquetaProdutoView> GetImpressaoEtiquetaProduto()
        {
            return repositorioProduto.GetImpressaoEtiquetaProduto(1);
        }

        #endregion

        public decimal BuscaCustoMedio(int idFilial, int idProduto, DateTime data)
        {
            return repositorioProduto.BuscaCustoMedio(idFilial, idProduto, data);
        }

        public decimal CalculaCustoMedio(int idFilial, int idProduto, DateTime data, decimal quantidade, decimal custoUnitario, int idNota)
        {
            return repositorioProduto.CalculaCustoMedio(idFilial, idProduto, data, quantidade, custoUnitario, idNota);
        }

        public decimal GetQtdEstoqueFisico(int IdLocalEstoque, int idProduto)
        {
            return repositorioProduto.GetQtdEstoqueFisico(IdLocalEstoque, idProduto);
        }

        public IList<pxyEstoque> GetListaEstoque(int idProduto)
        {
            return GetListaEstoque(idProduto, 0, 0);
        }

        public IList<pxyEstoque> GetListaEstoque(int idProduto, int idNota, int idPedido)
        {
            return repositorioProduto.GetListaEstoque(idProduto, idNota, idPedido);
        }

        internal IList<pxyEstoqueProduto> GetListaEstoque(string filiais, GrupoEstoque grupoEstoque, string situacao, int? IDLocalEstoque, int IdFornecedor)
        {
            return repositorioProduto.GetListaEstoque(filiais, grupoEstoque, situacao, IDLocalEstoque, IdFornecedor);
        }

        internal IList<pxyEstoqueProduto> GetListaEstoque(string filiais, Produto produto, int? IDLocalEstoque)
        {
            return repositorioProduto.GetListaEstoque(filiais, produto, IDLocalEstoque);
        }

        public string GetIDIntegracaoPorID(int id)
        {
            return repositorioProduto.GetIDIntegracaoPorID(id);
        }

        public IList<pxyListagemPreco> GetProdutoTabelaPreco(TabelaPreco tabelaPreco)
        {
            IList<pxyListagemPreco> produtos = repositorioProduto.GetListagemPorGrupoEstoqueETabelaPrecoSemInativos("%%", tabelaPreco);
            return produtos;
        }

        public decimal GetMargemLucroTabelaPreco(TabelaPreco tabelaPreco, Produto produto)
        {
            return repositorioProduto.GetMargemLucroTabelaPreco(tabelaPreco, produto);
        }

        public IList<Grupo1> GetGrupos1Produto()
        {
            return repositorioProduto.GetGrupos1Produto();
        }

        public IList<Produto> GetProdutosPorGrupo1(int IDGrupo)
        {
            return repositorioProduto.GetProdutosPorGrupo1(IDGrupo);
        }

        public IList<Produto> GetProdutosFiltroDelivery(string Descricao)
        {
            return repositorioProduto.GetProdutosFiltroDelivery(Descricao);
        }

        public Produto GetProdutoByID(int ID)
        {
            return repositorioProduto.GetProdutoByID(ID);
        }

        public Produto GetProduto(int IDProduto)
        {
            return repositorioProduto.GetProdutoByCodigo(IDProduto);
        }

        public IList<Produto> CodigoDeBarrasExistente(string p, int IDProduto)
        {
            return repositorioProduto.CodigoDeBarrasExistente(p, IDProduto);
        }

        public void AtualizarEanProduto(int ID, string Barra)
        {
            repositorioProduto.AtualizarEanProduto(ID, Barra);
        }

        public IList<Produto> GetTodosProdutos()
        {
            return base.GetAll();
        }
    }
}