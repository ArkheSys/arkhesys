using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ServicoResponsavelController : BaseController<ServicoResponsavel>
    {
        #region Singleton
        private static ServicoResponsavelController instance;
        private static IRepositorioServicoResponsavel repositorioServicoResponsavel;

        private ServicoResponsavelController()
        { }

        static ServicoResponsavelController()
        {
            instance = new ServicoResponsavelController();
            repositorioT = RepositorioFactory<ServicoResponsavel>.GetRepositorio();
            repositorioServicoResponsavel = (IRepositorioServicoResponsavel)repositorioT;
        }

        public static ServicoResponsavelController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ServicoResponsavel objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
