using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class AverbacaoSeguradoraMdfeController : BaseController<AverbacaoSeguradoraMdfe>
    {
        #region Singleton
        private static AverbacaoSeguradoraMdfeController instance;
        private static IRepositorioAverbacaoSeguradoraMdfe repositorioAverbacaoSeguradoraMdfe;

        private AverbacaoSeguradoraMdfeController()
        { }

        static AverbacaoSeguradoraMdfeController()
        {
            instance = new AverbacaoSeguradoraMdfeController();
            repositorioT = RepositorioFactory<AverbacaoSeguradoraMdfe>.GetRepositorio();
            repositorioAverbacaoSeguradoraMdfe = (IRepositorioAverbacaoSeguradoraMdfe)repositorioT;
        }

        public static AverbacaoSeguradoraMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(AverbacaoSeguradoraMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
