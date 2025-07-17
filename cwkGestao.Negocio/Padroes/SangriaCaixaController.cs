using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;

namespace cwkGestao.Negocio.Padroes
{
    public partial class SangriaCaixaController : BaseController<SangriaCaixa>
    {
        #region Singleton

        private static SangriaCaixaController instance;
        private static IRepositorioSangriaCaixa repositorioSangriaCaixa;

        private SangriaCaixaController()
        { }

        static SangriaCaixaController()
        {
            instance = new SangriaCaixaController();
            repositorioT = RepositorioFactory<SangriaCaixa>.GetRepositorio();
            repositorioSangriaCaixa = (IRepositorioSangriaCaixa)repositorioT;
        }

        public static SangriaCaixaController Instancia
        {
            get
            {
                return instance;
            }
        }

        public override Dictionary<string, string> ValidaObjeto(SangriaCaixa objeto)
        {
            return new Dictionary<string, string>();
        }
        #endregion

        public decimal GetSaldoAtualVendasComSangriaESuprimentos(int IDUsuario, int IDFluxoCaixa)
        {
            return repositorioSangriaCaixa.GetSangriaVendasComSangriaESuprimentos(IDUsuario, IDFluxoCaixa);
        }

        public List<SangriaCaixa> GetSangriasPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioSangriaCaixa.GetSangriasPorFluxoCaixa(IDFluxoCaixa);
        }

        public List<FrenteCaixaNota> GetNotasPorFluxoCaixa(int IDFluxoCaixa)
        {
            return repositorioSangriaCaixa.GetNotasPorFluxoCaixa(IDFluxoCaixa);
        }

        public IList<pxFrenteCaixaNota> GetRateiosPorFormaDePagamento(int IDFluxoCaixa)
        {
            return repositorioSangriaCaixa.GetRateiosPorFormaDePagamento(IDFluxoCaixa);
        }

        public pxyDetalhesFechamento GetDetalhesFechamento(int IDFluxoCaixa)
        {
            return repositorioSangriaCaixa.GetDetalhesFechamento(IDFluxoCaixa);
        }

        public IList<pxSangriaCaixa> GetSangrias(DateTime dtInicio, DateTime dtFinal)
        {
            return repositorioSangriaCaixa.GetSangrias(dtInicio, dtFinal);
        }

        public SangriaCaixa GetSangria(int IDSangriaCaixa)
        {
            return repositorioSangriaCaixa.GetSangria(IDSangriaCaixa);
        }
    }
}
