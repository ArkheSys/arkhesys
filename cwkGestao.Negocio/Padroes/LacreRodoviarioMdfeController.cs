using System.Collections.Generic;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class LacreRodoviarioMdfeController : BaseController<LacreRodoviarioMdfe>
    {
        #region Singleton
        private static LacreRodoviarioMdfeController instance;
        private static IRepositorioLacreRodoviarioMdfe repositorioLacreRodoviarioMdfe;

        private LacreRodoviarioMdfeController()
        { }

        static LacreRodoviarioMdfeController()
        {
            instance = new LacreRodoviarioMdfeController();
            repositorioT = RepositorioFactory<LacreRodoviarioMdfe>.GetRepositorio();
            repositorioLacreRodoviarioMdfe = (IRepositorioLacreRodoviarioMdfe)repositorioT;
        }

        public static LacreRodoviarioMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(LacreRodoviarioMdfe objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<LacreRodoviarioMdfe> GetLacresPorMDFe(int IDMDFe)
        {
            return repositorioLacreRodoviarioMdfe.GetLacresPorMDFe(IDMDFe);
        }
    }
}
