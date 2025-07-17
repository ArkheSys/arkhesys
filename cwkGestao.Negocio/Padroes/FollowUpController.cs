using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class FollowUpController : BaseController<FollowUp>
    {
        #region Singleton
        private static FollowUpController instance;
        private static IRepositorioFollowUp repositorioFollowUp;

        private FollowUpController()
        { }

        static FollowUpController()
        {
            instance = new FollowUpController();
            repositorioT = RepositorioFactory<FollowUp>.GetRepositorio();
            repositorioFollowUp = (IRepositorioFollowUp)repositorioT;
        }

        public static FollowUpController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(FollowUp objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
