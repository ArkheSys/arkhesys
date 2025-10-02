using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class CESTController : BaseController<CEST>
    {
        #region Singleton
        private static CESTController instance;
        private static IRepositorioCEST repositorioCEST;

        private CESTController()
        { }

        static CESTController()
        {
            instance = new CESTController();
            repositorioT = RepositorioFactory<CEST>.GetRepositorio();
            repositorioCEST = (IRepositorioCEST)repositorioT;
        }

        public static CESTController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CEST objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            return ret;
        }

        public CEST GetByCEST(string Codigo)
        {
            //return repositorioCEST.GetByCEST(Codigo);
            return ((IRepositorioCEST)repositorioT).GetByCEST(Codigo);
        }
    }
}
