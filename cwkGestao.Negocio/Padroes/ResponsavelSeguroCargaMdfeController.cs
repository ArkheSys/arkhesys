using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ResponsavelSeguroCargaMdfeController : BaseController<ResponsavelSeguroCargaMdfe>
    {
        #region Singleton
        private static ResponsavelSeguroCargaMdfeController instance;
        private static IRepositorioResponsavelSeguroCargaMdfe repositorioResponsavelSeguroCargaMdfe;

        private ResponsavelSeguroCargaMdfeController()
        { }

        static ResponsavelSeguroCargaMdfeController()
        {
            instance = new ResponsavelSeguroCargaMdfeController();
            repositorioT = RepositorioFactory<ResponsavelSeguroCargaMdfe>.GetRepositorio();
            repositorioResponsavelSeguroCargaMdfe = (IRepositorioResponsavelSeguroCargaMdfe)repositorioT;
        }

        public static ResponsavelSeguroCargaMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ResponsavelSeguroCargaMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
