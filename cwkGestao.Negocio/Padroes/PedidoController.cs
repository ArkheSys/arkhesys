using System;
using System.Collections.Generic;
using System.Linq;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PedidoController : BaseController<Pedido>
    {
        #region Singleton

        private static readonly IRepositorioPedido repositorioPedido;

        private PedidoController() { }

        static PedidoController()
        {
            Instancia = new PedidoController();
            repositorioT = RepositorioFactory<Pedido>.GetRepositorio();
            repositorioPedido = (IRepositorioPedido)repositorioT;
        }

        public static PedidoController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Pedido objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(Pedido objeto, Acao acao)
        {
            SetaCamposIncAltUsuario(objeto, acao);

            foreach (var item in objeto.Items)
                item.PrazoEntrega = new DateTime(1900, 01, 01);

            if (acao == Acao.Excluir)
                RegisterDeleteIntoTransaction(objeto);

            #region Modo Antigo

            /*
             * 
            var atualizaEstoque = ControleEstoque.AtualizaEstoque.GetAtualizaEstoque(objeto);
            atualizaEstoque.Atualizar(acao); 
            */

            #endregion

            if (acao == Acao.Incluir && objeto.Dt.Year < 2000)
                objeto.Dt = DateTime.Now;

            objeto.AltData = DateTime.Now;

            if (acao != Acao.Excluir)
            {
                // Pedido de Compra - 16/03/23
                IList<Pedido> pedidosCompra = new List<Pedido>(); // EstoqueController.Instancia.VerificarEstoqueNegativo(objeto);

                if (acao == Acao.Incluir)
                {
                    objeto.Codigo = CodigoDisponivelPedido();
                    if (!ConfiguracaoController.Instancia.GetConfiguracao().ReiniciarNumeracaoPedido)
                        if (NumeroComplementoExiste(objeto))
                            objeto.Numero = repositorioPedido.NumeroDisponivelPorComplemento(objeto.Filial, objeto.TipoPedido, objeto.ComplementoNumero);
                }

                RegisterNewIntoTransaction(objeto);

                // Pedido de Compra - 16/03/23
                int maxCodigo = objeto.Codigo + 1;
                foreach (Pedido pedido in pedidosCompra)
                {
                    pedido.Codigo = maxCodigo;
                    RegisterNewIntoTransaction(pedido);
                    maxCodigo = maxCodigo + 1;
                }
            }

            CommitUnitOfWorkTransaction();

            return new Dictionary<string, string>();
        }

        public void SalvarDelivery(Pedido objeto, Acao acao)
        {
            SetaCamposIncAltUsuario(objeto, acao);
            RegisterNewIntoTransaction(objeto);
            CommitUnitOfWorkTransaction();
        }

        public bool GetPercFatProdNota(int codigoPedido)
        {
            return repositorioPedido.GetPercFatProdNota(codigoPedido);
        }

        private bool NumeroComplementoExiste(Pedido objeto)
        {
            return repositorioPedido.NumeroComplementoExiste(objeto);
        }

        public IList<Pedido> LoadAllByID(List<int> ids)
        {
            return repositorioPedido.LoadAllByID(ids);
        }

        public Pedido LoadObjectByIdMercadoLivre(int id)
        {
            return repositorioPedido.LoadObjectByIdMercadoLivre(id);
        }

        public decimal[] getTotalPorID(int id)
        {
            return repositorioPedido.getTotalPorID(id);
        }

        public IList<Pedido> GetAll(InOutType inOutType, DateTime? Inicio, DateTime? Fim, params TipoPedidoType[] tipoPedido)
        {
            return repositorioPedido.GetAll(inOutType, Inicio, Fim, tipoPedido);
        }

        public IList<Pedido> GetAllPedidos(InOutType inOutType, DateTime? Inicio, DateTime? Fim, TipoPedidoType tipoPedido)
        {
            return repositorioPedido.GetAllPedidos(inOutType, Inicio, Fim, tipoPedido);
        }

        public IList<PedidoItem> GetProdutoVendaPorClienteNoPeriodo(Pessoa pessoa, Produto produto, DateTime dateTime, DateTime dateTime_4, InOutType inOut)
        {
            return repositorioPedido.GetProdutoVendaPorClienteNoPeriodo(pessoa, produto, dateTime, dateTime_4, inOut);
        }

        public IList<Pedido> GetByCliente(Pessoa cliente, int maxResults)
        {
            return repositorioPedido.GetByCliente(cliente, maxResults);
        }

        public IList<Pedido> GetByPCP_Lote(int IDPCP_lote)
        {
            return repositorioPedido.GetByPCP_Lote(IDPCP_lote);
        }

        public IList<Pedido> GetByCliente(Pessoa cliente, DateTime inicio, DateTime fim)
        {
            return repositorioPedido.GetByCliente(cliente, inicio, fim);
        }

        public IList<Pedido> GetByAllParameters(Pessoa cliente, int maxResults, DateTime inicio, DateTime fim, TipoPedido tipoPedido, Filial filial)
        {
            return repositorioPedido.GetByAllParameters(cliente, maxResults, inicio, fim, tipoPedido, filial);
        }

        public IList<int> GetByTipo(TipoPedido tipopedido)
        {
            return repositorioPedido.GetBytipo(tipopedido);
        }

        public Pedido GetByCodigo(int p)
        {
            return repositorioPedido.GetByCodigo(p);
        }

        public HashSet<int> GetHashIdsIntegracao()
        {
            return repositorioPedido.GetHashIdsIntegracao();
        }

        public HashSet<int> GetHashIdsIntegracaoPendente()
        {
            return repositorioPedido.GetHashIdsIntegracaoPendente();
        }

        public Pedido LoadPedidoImpressaoById(int id)
        {
            return repositorioPedido.LoadPedidoImpressaoById(id);
        }

        public int MaxNumero(Filial filial, TipoPedido tipoPedido)
        {
            return repositorioPedido.MaxNumero(filial, tipoPedido);
        }

        public int CodigoDisponivelPedido()
        {
            return repositorioPedido.CodigoDisponivelPedido();
        }

        public IList<Pedido> GetPedidosAbertos(Pessoa pessoa)
        {
            return repositorioPedido.GetPedidosAbertos(pessoa);
        }

        public IList<Pedido> GetPedidosAbertos(Pessoa pessoa, Filial filial, TipoPedido tipoPedido, DateTime inicio, DateTime fim)
        {
            return repositorioPedido.GetPedidosAbertos(pessoa, filial, tipoPedido, inicio, fim);
        }

        public IList<pxyBaixaRequisicao> GetItensRequisicaoBaixar(Pedido requisicao)
        {
            return (from i in requisicao.Items
                    where i.Quantidade + i.QtdCancelada > i.QtdRetirada
                    select new pxyBaixaRequisicao
                    {
                        Selecionado = false,
                        PedidoItem = i
                    }).ToList();
        }

        public int MaxCodigoIntegracao()
        {
            return repositorioPedido.MaxCodigoIntegracao();
        }

        public IList<object> getListaPedidos(List<int> Empresas, string pStatus, List<int> Vendedores, List<int> TipoOrcamentos, DateTime DataInicial, DateTime DataFinal)
        {
            return repositorioPedido.getListaPedidos(Empresas, pStatus, Vendedores, TipoOrcamentos, DataInicial, DataFinal);
        }

        public IList<pxyImpressaoEtiquetaExpedicao> GetListaCodRastreamento()
        {
            return repositorioPedido.GetListaCodRastreamento();
        }

        public IList<pxyImpressaoEtiquetaExpedicao> GetListaCodRastreamento(int pedido)
        {
            return repositorioPedido.GetListaCodRastreamento(pedido);
        }

        public IList<Pedido> GetPedidosFaturados()
        {
            return repositorioPedido.GetPedidosFaturados();
        }

        public IList<Pedido> GetPedidosVendedor(int idVendedor)
        {
            return repositorioPedido.GetPedidosVendedor(idVendedor);
        }

        public IList<Pedido> GetPedidosParaAprovar()
        {
            IList<Pedido> pedidos = repositorioPedido.GetPedidosParaAprovar();

            //foreach (var pedido in pedidos)
            //{
            //    if ((pedido.UsuarioAprovacao == null) && (pedido.dtAprovacao != null))
            //    {
            //        pedido.dtAprovacao = null;
            //    }
            //    if ((pedido.UsuarioRevogacao == null) && (pedido.dtRevogacao != null))
            //    {
            //        pedido.dtRevogacao = null;
            //    }

            //    PedidoController.Instancia.Salvar(pedido, Acao.Alterar);
            //}

            //Foi comentado esse foreach por não se encontrar utilidade nele e o mesmo está fazendo a tela perder desempenho.

            return pedidos;
        }

        public IList<PedidoItem> GetItensDoPedido(int IDPedido)
        {
            return repositorioPedido.GetItensDoPedido(IDPedido);
        }

        public IList<Pedido> GetAllPorIdSISeCommerce(int p)
        {
            return repositorioPedido.GetAllPorIdSISeCommerce(p);
        }

        public IList<Pedido> GetAllEnviadosSHL()
        {
            return repositorioPedido.GetAllEnviadosSHL();
        }

        public NotaItemPedidoItem LoadNotaItemPedidoItemByIdNota(int id)
        {
            return repositorioPedido.LoadNotaItemPedidoItemByIdNota(id);
        }

        public IList<Pedido> GetPedidosSHLNaoFinalizados()
        {
            return repositorioPedido.GetAllEnviadosSHLNaoFinalizados();
        }

        public IList<Pedido> GetPedidosAbertosLimiteCredito(Pessoa pessoa)
        {
            return repositorioPedido.GetPedidosAbertosLimiteCredito(pessoa);
        }

        public decimal GetValorTotalPedidosAbertosLimiteCredito(Pessoa pessoa)
        {
            return repositorioPedido.GetValorTotalPedidosAbertosLimiteCredito(pessoa);
        }

        public IList<pxyEntregaPedido> GetAllParaEntregas(int idpessoa, int Entrega, int StatusEntrega, DateTime DataInicial, DateTime DataFinal)
        {
            return repositorioPedido.GetAllParaEntregas(idpessoa, Entrega, StatusEntrega, DataInicial, DataFinal);
        }

        public IList<NotaItemPedidoItem> LoadNotaItemsPedidoItems(int idnota)
        {
            return repositorioPedido.getListaNotaItemPedidoItem(idnota);
        }

        public object GetByClienteAberto(Pessoa cliente, int maxResults)
        {
            return repositorioPedido.GetByClienteAberto(cliente, maxResults);
        }

        public IList<Pedido> GerarPedidoCompra(Filial filial, LocalEstoque localEstoque, IList<pxyEstoque> produtosEstoqueNegativo)
        {
            Pedido pedidoCompra;
            Produto produto;
            IList<Pedido> pedidosCompras = new List<Pedido>();

            int maxCodigo = MaxCodigo();

            Condicao objCondicao = CondicaoController.Instancia.GetAVista(1);
            TipoPedido objTipoPedido = TipoPedidoController.Instancia.GetAll(InOutType.Entrada, TipoPedidoType.Pedido).FirstOrDefault();
            TabelaPreco objTabelaPreco = TabelaPrecoController.Instancia.hqlGetAllAtiva().FirstOrDefault();

            foreach (var produtoGroup in produtosEstoqueNegativo.GroupBy(s => s.Produto.Fornecedor.ID))
            {
                produto = new Produto();
                produto = produtoGroup.Select(e => e.Produto).FirstOrDefault();

                pedidoCompra = new Pedido();

                pedidoCompra.Codigo = maxCodigo++;

                pedidoCompra.Ent_Sai = 1;
                pedidoCompra.Filial = filial;
                pedidoCompra.Condicao = objCondicao;
                pedidoCompra.TipoPedido = objTipoPedido;
                pedidoCompra.TabelaPreco = objTabelaPreco;
                pedidoCompra.LocalEstoquePedido = localEstoque;
                pedidoCompra.Numero = MaxNumero(filial, pedidoCompra.TipoPedido);

                #region Preenchendo dados do Fornecedor

                pedidoCompra.Pessoa = produtoGroup.Select(e => e.Produto.Fornecedor).FirstOrDefault();
                pedidoCompra.PessoaNome = pedidoCompra.Pessoa.Nome;
                pedidoCompra.PessoaCNPJCPF = pedidoCompra.Pessoa.CNPJ_CPF;
                pedidoCompra.PessoaInscRG = pedidoCompra.Pessoa.Inscricao;
                pedidoCompra.PessoaEndereco = pedidoCompra.Pessoa.EnderecoPrincipal().Endereco;
                pedidoCompra.PessoaBairro = pedidoCompra.Pessoa.EnderecoPrincipal().Bairro;
                pedidoCompra.PessoaCidade = pedidoCompra.Pessoa.EnderecoPrincipal().Cidade.Nome;
                pedidoCompra.PessoaTelefone = pedidoCompra.Pessoa.EnderecoPrincipal().Telefone;

                #endregion

                int sequencia = 1;

                foreach (var item in produtoGroup.Select(e => e))
                {
                    PedidoItem pedidoItem = new PedidoItem();
                    pedidoItem.Sequencia = sequencia++;
                    pedidoItem.Valor = item.Produto.UltimoCusto;
                    pedidoItem.ValorProduto = item.Produto.UltimoCusto;
                    pedidoItem.ValorCalculado = item.Produto.UltimoCusto;
                    pedidoItem.Unidade = item.Produto.Unidade.Sigla;
                    pedidoItem.ProdutoNome = item.Produto.Nome;
                    pedidoItem.ProdutoDescReduzida = item.Produto.DescReduzida;
                    pedidoItem.Dt = DateTime.Now;
                    pedidoItem.Produto = item.Produto;
                    pedidoItem.Pedido = pedidoCompra;
                    pedidoItem.Quantidade = Math.Abs((item.EstoqueFisico ?? 0) - (item.EstoqueMin ?? 0) - (item.QtdPedido ?? 0));
                    pedidoItem.Total = pedidoItem.Quantidade * pedidoItem.Valor;

                    pedidoCompra.Items.Add(pedidoItem);
                }

                pedidoCompra.TotalPedido = pedidoCompra.Items.Sum(pi => pi.Total);
                pedidoCompra.Parcelas = CondicaoController.Instancia.GerarParcelas(pedidoCompra);
                pedidosCompras.Add(pedidoCompra);
            }

            return pedidosCompras;
        }

        public IList<Pedido> GetPedidoPorNumero(int Numero)
        {
            return repositorioPedido.GetPedidoPorNumero(Numero);
        }

        public IList<Pedido> GetPedidosDelivery()
        {
            return repositorioPedido.GetPedidosDelivery();
        }

        public FrenteCaixaNota GetFrenteCaixaNotaPorPedido(int IDPedido)
        {
            return repositorioPedido.GetFrenteCaixaNotaPorPedido(IDPedido);
        }

        public IList<pxyPedidoFrenteCaixaCarregar> GetListaPedidosFrenteCaixaParaCarregar(bool Reservas = false, int? IDFluxoCaixa = null)
        {
            return repositorioPedido.GetListaPedidosFrenteCaixaParaCarregar(Reservas, IDFluxoCaixa);
        }

        public IList<pxPedidosDashBoard> GetPedidosParaDashBoard()
        {
            return repositorioPedido.GetPedidosParaDashBoard();
        }
    }
}