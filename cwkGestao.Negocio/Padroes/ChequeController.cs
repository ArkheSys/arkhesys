using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public partial class ChequeController : BaseController<Cheque>
    {
        #region Singleton
        private static ChequeController instance;
        private static IRepositorioCheque repositorioCheque;

        private ChequeController()
        { }

        static ChequeController()
        {
            instance = new ChequeController();
            repositorioT = RepositorioFactory<Cheque>.GetRepositorio();
            repositorioCheque = (IRepositorioCheque)repositorioT;
        }

        public static ChequeController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(Cheque objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ChequeHistorico> GetHistorico(Cheque cheque)
        {
            return repositorioCheque.GetHistorico(cheque);
        }

        public IList<Cheque> GetChequesNaoUtilizados()
        {
            return repositorioCheque.GetChequesNaoUtilizados();
        }

        public IList<pxyChequeSaldoRestante> GetChequesComSaldoRestante(int IDMovCaixaExcluido, int EntSai)
        {
            return repositorioCheque.GetChequesComSaldoRestante(IDMovCaixaExcluido, EntSai);
        }

        public IList<pxyChequeSaldoRestante> GetChequesComSaldoRestante()
        {
            return repositorioCheque.GetChequesComSaldoRestante();
        }
    }
}
