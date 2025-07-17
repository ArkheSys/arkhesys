using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class MdfeController : BaseController<Mdfe>
    {
        #region Singleton
        private static MdfeController instance;
        private static IRepositorioMdfe repositorioMdfe;

        private MdfeController()
        { }

        static MdfeController()
        {
            instance = new MdfeController();
            repositorioT = RepositorioFactory<Mdfe>.GetRepositorio();
            repositorioMdfe = (IRepositorioMdfe)repositorioT;
        }

        public static MdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Mdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
