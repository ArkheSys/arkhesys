using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ContratanteMdfeController : BaseController<ContratanteMdfe>
    {
        #region Singleton
        private static ContratanteMdfeController instance;
        private static IRepositorioContratanteMdfe repositorioContratanteMdfe;

        private ContratanteMdfeController()
        { }

        static ContratanteMdfeController()
        {
            instance = new ContratanteMdfeController();
            repositorioT = RepositorioFactory<ContratanteMdfe>.GetRepositorio();
            repositorioContratanteMdfe = (IRepositorioContratanteMdfe)repositorioT;
        }

        public static ContratanteMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ContratanteMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
