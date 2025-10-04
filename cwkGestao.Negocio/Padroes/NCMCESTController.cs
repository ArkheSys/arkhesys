using System;
using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
namespace cwkGestao.Negocio
{
    public class NCMCESTController : BaseController<NCMCEST>
    {
        #region Singleton
        private static NCMCESTController instance;

        private NCMCESTController() { }

        static NCMCESTController()
        {
            instance = new NCMCESTController();
            repositorioT = RepositorioFactory<NCMCEST>.GetRepositorio();
        }

        public static NCMCESTController Instancia
        {
            get { return instance; }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(NCMCEST objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}