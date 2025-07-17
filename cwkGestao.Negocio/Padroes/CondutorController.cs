using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class CondutorController : BaseController<Condutor>
    {
        #region Singleton
        private static CondutorController instance;
        private static IRepositorioCondutor repositorioCondutor;

        private CondutorController()
        { }

        static CondutorController()
        {
            instance = new CondutorController();
            repositorioT = RepositorioFactory<Condutor>.GetRepositorio();
            repositorioCondutor = (IRepositorioCondutor)repositorioT;
        }

        public static CondutorController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Condutor objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
