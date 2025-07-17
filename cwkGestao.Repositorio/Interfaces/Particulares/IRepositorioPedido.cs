using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPedido : IRepositorio<Pedido>
    {
        Pedido LoadPedidoImpressaoById(int id);

        int MaxNumero(Filial filial, TipoPedido tipoPedido);

        IList<Pedido> GetAll(InOutType inOutType, DateTime? Inicio, DateTime? Fim, TipoPedidoType[] tipoPedido);
        IList<Pedido> GetAllPedidos(InOutType inOutType, DateTime? Inicio, DateTime? Fim, TipoPedidoType tipoPedido);

        IList<PedidoItem> GetProdutoVendaPorClienteNoPeriodo(Pessoa pessoa, Produto produto, DateTime dateTime, DateTime dateTime_4, InOutType inOut);

        IList<Pedido> GetByCliente(Pessoa cliente, int maxResults);

        int CodigoDisponivelPedido();
        IList<Pedido> GetPedidosAbertos(Pessoa pessoa);

        IList<Pedido> GetPedidoPorNumero(int Numero);

        IList<Pedido> GetPedidosDelivery();

        FrenteCaixaNota GetFrenteCaixaNotaPorPedido(int IDPedido);

        IList<Pedido> GetByCliente(Pessoa cliente, DateTime inicio, DateTime fim);

        IList<Pedido> GetByPCP_Lote(int IDPCP_lote);

        IList<Pedido> GetByAllParameters(Pessoa cliente, int maxResults, DateTime inicio, DateTime fim, TipoPedido tipoPedido, Filial filial);

        decimal[] getTotalPorID(int id);

        Pedido GetByCodigo(int p);
        IList<Pedido> GetPedidosAbertos(Pessoa pessoa, Filial filial, TipoPedido tipoPedido, DateTime inicio, DateTime fim);

        IList<Pedido> GetByTipoPedido(Pessoa pessoa, Filial filial, TipoPedido tipoPedido);

        bool GetPercFatProdNota(int codigoPedido);

        bool NumeroComplementoExiste(Pedido objeto);

        int NumeroDisponivelPorComplemento(Filial filial, TipoPedido tipoPedido, string complementoNumero);

        HashSet<int> GetHashIdsIntegracao();

        HashSet<int> GetHashIdsIntegracaoPendente();

        int MaxCodigoIntegracao();

        IList<object> getListaPedidos(List<int> Empresas, string pStatus, List<int> Vendedores, List<int> TipoOrcamentos, DateTime DataInicial, DateTime DataFinal);

        IList<pxyImpressaoEtiquetaExpedicao> GetListaCodRastreamento();
        IList<pxyImpressaoEtiquetaExpedicao> GetListaCodRastreamento(int pedido);

        IList<Pedido> GetPedidosFaturados();

        IList<Pedido> GetPedidosVendedor(int idVendedor);

        IList<Pedido> GetPedidosParaAprovar();

        IList<PedidoItem> GetItensDoPedido(int IDPedido);

        IList<Pedido> LoadAllByID(List<int> ids);

        Pedido LoadObjectByIdMercadoLivre(int id);

        IList<Pedido> GetAllPorIdSISeCommerce(int p);

        IList<Pedido> GetAllEnviadosSHL();
        IList<Pedido> GetAllEnviadosSHLNaoFinalizados();
        NotaItemPedidoItem LoadNotaItemPedidoItemByIdNota(int id);
        IList<Pedido> GetPedidosAbertosLimiteCredito(Pessoa pessoa);
        decimal GetValorTotalPedidosAbertosLimiteCredito(Pessoa pessoa);
        IList<pxyEntregaPedido> GetAllParaEntregas(int idpessoa, int Entrega, int StatusEntrega, DateTime DataInicial, DateTime DataFinal);

        IList<NotaItemPedidoItem> getListaNotaItemPedidoItem(int idnota);

        IList<Pedido> GetByClienteAberto(Pessoa cliente, int maxResults);

        IList<int> GetBytipo(TipoPedido tipopedido);

        IList<pxyPedidoFrenteCaixaCarregar> GetListaPedidosFrenteCaixaParaCarregar(bool Reservas = false, int? IDFluxoCaixa = null);

        IList<pxPedidosDashBoard> GetPedidosParaDashBoard();
    }
}