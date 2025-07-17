using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class MaquinaController : BaseController<Maquina>
    {
        #region Singleton

        private static readonly IRepositorioMaquina repositorioMaquina;

        private MaquinaController() { }

        static MaquinaController()
        {
            Instancia = new MaquinaController();
            repositorioT = RepositorioFactory<Maquina>.GetRepositorio();
            repositorioMaquina = (IRepositorioMaquina)repositorioT;
        }

        public static MaquinaController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Maquina objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<Maquina> GetAll()
        {
            return repositorioMaquina.GetAll();
        }

    }
}