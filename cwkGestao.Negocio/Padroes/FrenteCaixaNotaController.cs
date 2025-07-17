using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public partial class FrenteCaixaNotaController : BaseController<FrenteCaixaNota>
    {
        #region Singleton
        private static FrenteCaixaNotaController instance;
        private static IRepositorioFrenteCaixaNota repositorioFrenteCaixaNota;

        private FrenteCaixaNotaController()
        { }

        static FrenteCaixaNotaController()
        {
            instance = new FrenteCaixaNotaController();
            repositorioT = RepositorioFactory<FrenteCaixaNota>.GetRepositorio();
            repositorioFrenteCaixaNota = (IRepositorioFrenteCaixaNota)repositorioT;
        }

        public static FrenteCaixaNotaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(FrenteCaixaNota objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            return ret;
        }


        public static void AtualizaFrenteCaixa(Nota objNota, ref FrenteCaixaNota objFrenteCaixaNota)
        {
            objFrenteCaixaNota.Data = objNota.Dt;
            objFrenteCaixaNota.Pessoa = objFrenteCaixaNota.Pessoa ?? objNota.Pessoa;
            objFrenteCaixaNota.Vendedor = objFrenteCaixaNota.Vendedor ?? objNota.Vendedor.Pessoa;
            objFrenteCaixaNota.Nota = objNota;
        }

        public IList<pxPedidoParcelaFrenteCaixa> GetPagamentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioFrenteCaixaNota.GetPagamentosPorFluxoCaixa(IDFluxoCaixa);
        }

        public IList<pxProdutosPedidoFrenteCaixa> GetProdutosPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioFrenteCaixaNota.GetProdutosPorFluxoCaixa(IDFluxoCaixa);
        }
        public IList<pxClientesEPagamentosPedidoFrenteCaixa> GetClientesEPagamentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioFrenteCaixaNota.GetClientesEPagamentosPorFluxoCaixa(IDFluxoCaixa);
        }

        public IList<FrenteCaixaNotaItem> GetItensPorFrenteCaixaNotaItem(int IDFrenteCaixaNotaItem)
        {
            return repositorioFrenteCaixaNota.GetItensPorFrenteCaixaNotaItem(IDFrenteCaixaNotaItem);
        }

        public IList<FrenteCaixaNotaItem> GetItensCancelados(int IDFrenteCaixaNota)
        {
            return repositorioFrenteCaixaNota.GetItensCancelados(IDFrenteCaixaNota);
        }

        public bool SeItensCancelados(int idFrenteCaixaNota)
        {
            return repositorioFrenteCaixaNota.SeItensCancelados(idFrenteCaixaNota);
        }

        public IList<pxyItemFrenteCaixaPorFilial> GetItensPorFilial(int? IDTabelaPreco, DateTime? DataInicio, DateTime? DataFim, string CodigoInicial, string CodigoFinal, string OperadorInicial, string OperadorFinal)
        {
            return repositorioFrenteCaixaNota.GetItensPorFilial(IDTabelaPreco, DataInicio, DataFim, CodigoInicial, CodigoFinal, OperadorInicial, OperadorFinal);
        }

        public IList<pxyFrenteCaixaNotaItemCancelado> GetVendasCanceladasPDV(int? IDFilial, DateTime? DataInicio, DateTime? DataFim)
        {
            return repositorioFrenteCaixaNota.GetVendasCanceladasPDV(IDFilial, DataInicio, DataFim);
        }
    }
}
