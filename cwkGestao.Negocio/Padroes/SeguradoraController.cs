using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class SeguradoraController : BaseController<Seguradora>
    {
        #region Singleton
        private static SeguradoraController instance;
        private static IRepositorioSeguradora repositorioSeguradora;

        private SeguradoraController()
        { }

        static SeguradoraController()
        {
            instance = new SeguradoraController();
            repositorioT = RepositorioFactory<Seguradora>.GetRepositorio();
            repositorioSeguradora = (IRepositorioSeguradora)repositorioT;
        }

        public static SeguradoraController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Seguradora objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
