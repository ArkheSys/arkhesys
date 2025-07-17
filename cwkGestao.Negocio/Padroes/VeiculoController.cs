using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class VeiculoController : BaseController<Veiculo>
    {
        #region Singleton
        private static VeiculoController instance;
        private static IRepositorioVeiculo repositorioVeiculo;

        private VeiculoController()
        { }

        static VeiculoController()
        {
            instance = new VeiculoController();
            repositorioT = RepositorioFactory<Veiculo>.GetRepositorio();
            repositorioVeiculo = (IRepositorioVeiculo)repositorioT;
        }

        public static VeiculoController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Veiculo objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
