using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class NfeReferenciadaMdfeController : BaseController<NfeReferenciadaMdfe>
    {
        #region Singleton
        private static NfeReferenciadaMdfeController instance;
        private static IRepositorioNfeReferenciadaMdfe repositorioNfeReferenciadaMdfe;

        private NfeReferenciadaMdfeController()
        { }

        static NfeReferenciadaMdfeController()
        {
            instance = new NfeReferenciadaMdfeController();
            repositorioT = RepositorioFactory<NfeReferenciadaMdfe>.GetRepositorio();
            repositorioNfeReferenciadaMdfe = (IRepositorioNfeReferenciadaMdfe)repositorioT;
        }

        public static NfeReferenciadaMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(NfeReferenciadaMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
