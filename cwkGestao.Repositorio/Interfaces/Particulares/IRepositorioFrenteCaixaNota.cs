using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFrenteCaixaNota : IRepositorio<FrenteCaixaNota>
    {
        IList<pxPedidoParcelaFrenteCaixa> GetPagamentosPorFluxoCaixa(int IDFluxoCaixa);
        IList<pxProdutosPedidoFrenteCaixa> GetProdutosPorFluxoCaixa(int IDFluxoCaixa);
        IList<pxClientesEPagamentosPedidoFrenteCaixa> GetClientesEPagamentosPorFluxoCaixa(int IDFluxoCaixa);
        IList<FrenteCaixaNotaItem> GetItensPorFrenteCaixaNotaItem(int IDFrenteCaixaNotaItem);
        
        IList<FrenteCaixaNotaItem> GetItensCancelados(int IDFrenteCaixaNota);
        bool SeItensCancelados(int idFrenteCaixaNota);

        IList<pxyItemFrenteCaixaPorFilial> GetItensPorFilial(int? IDTabelaPreco, DateTime? DataInicio, DateTime? DataFim, string CodigoInicial, string CodigoFinal, string OperadorInicial, string OperadorFinal);

        IList<pxyFrenteCaixaNotaItemCancelado> GetVendasCanceladasPDV(int? IDFilial, DateTime? DataInicio, DateTime? DataFim);
    }
}