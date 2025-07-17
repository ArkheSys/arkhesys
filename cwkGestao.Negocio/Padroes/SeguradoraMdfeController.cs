using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class SeguradoraMdfeController : BaseController<SeguradoraMdfe>
    {
        #region Singleton
        private static SeguradoraMdfeController instance;
        private static IRepositorioSeguradoraMdfe repositorioSeguradoraMdfe;

        private SeguradoraMdfeController()
        { }

        static SeguradoraMdfeController()
        {
            instance = new SeguradoraMdfeController();
            repositorioT = RepositorioFactory<SeguradoraMdfe>.GetRepositorio();
            repositorioSeguradoraMdfe = (IRepositorioSeguradoraMdfe)repositorioT;
        }

        public static SeguradoraMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(SeguradoraMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
