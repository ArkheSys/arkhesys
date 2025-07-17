using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PercursoMdfeController : BaseController<PercursoMdfe>
    {
        #region Singleton
        private static PercursoMdfeController instance;
        private static IRepositorioPercursoMdfe repositorioPercursoMdfe;

        private PercursoMdfeController()
        { }

        static PercursoMdfeController()
        {
            instance = new PercursoMdfeController();
            repositorioT = RepositorioFactory<PercursoMdfe>.GetRepositorio();
            repositorioPercursoMdfe = (IRepositorioPercursoMdfe)repositorioT;
        }

        public static PercursoMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(PercursoMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
