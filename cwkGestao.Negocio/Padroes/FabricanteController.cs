using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class FabricanteController : BaseController<Fabricante>
    {
        #region Singleton

        private static readonly IRepositorioFabricante repositorioFabricante;

        private FabricanteController() { }

        static FabricanteController()
        {
            Instancia = new FabricanteController();
            repositorioT = RepositorioFactory<Fabricante>.GetRepositorio();
            repositorioFabricante = (IRepositorioFabricante)repositorioT;
        }

        public static FabricanteController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Fabricante objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<Fabricante> GetAll()
        {
            return repositorioFabricante.GetAll();
        }

    }
}