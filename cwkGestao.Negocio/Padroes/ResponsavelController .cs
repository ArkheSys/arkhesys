using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ResponsavelController : BaseController<Responsavel>
    {
        #region Singleton

        private static readonly IRepositorioResponsavel repositorioResponsavel;

        private ResponsavelController() { }

        static ResponsavelController()
        {
            Instancia = new ResponsavelController();
            repositorioT = RepositorioFactory<Responsavel>.GetRepositorio();
            repositorioResponsavel = (IRepositorioResponsavel)repositorioT;
        }

        public static ResponsavelController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Responsavel objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<Responsavel> GetAll()
        {
            return repositorioResponsavel.GetAll();
        }

    }
}