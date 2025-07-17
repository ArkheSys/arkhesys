using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class VeiculoTracaoMdfeController : BaseController<VeiculoTracaoMdfe>
    {
        #region Singleton
        private static VeiculoTracaoMdfeController instance;
        private static IRepositorioVeiculoTracaoMdfe repositorioVeiculoTracaoMdfe;

        private VeiculoTracaoMdfeController()
        { }

        static VeiculoTracaoMdfeController()
        {
            instance = new VeiculoTracaoMdfeController();
            repositorioT = RepositorioFactory<VeiculoTracaoMdfe>.GetRepositorio();
            repositorioVeiculoTracaoMdfe = (IRepositorioVeiculoTracaoMdfe)repositorioT;
        }

        public static VeiculoTracaoMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(VeiculoTracaoMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
