using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio
{
    public partial class ChequeHistoricoController : BaseController<ChequeHistorico>
    {
        #region Singleton
        private static ChequeHistoricoController instance;
        private static IRepositorioChequeHistorico repositorioChequeHistorico;

        private ChequeHistoricoController()
        { }

        static ChequeHistoricoController()
        {
            instance = new ChequeHistoricoController();
            repositorioT = RepositorioFactory<ChequeHistorico>.GetRepositorio();
            repositorioChequeHistorico = (IRepositorioChequeHistorico)repositorioT;
        }

        public static ChequeHistoricoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(ChequeHistorico objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
