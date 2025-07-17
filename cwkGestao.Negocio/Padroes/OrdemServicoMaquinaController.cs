using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class OrdemServicoMaquinaController : BaseController<OrdemServicoMaquina>
    {
        #region Singleton

        private static readonly IRepositorioOrdemServicoMaquina repositorioOrdemServicoMaquina;

        private OrdemServicoMaquinaController() { }

        static OrdemServicoMaquinaController()
        {
            Instancia = new OrdemServicoMaquinaController();
            repositorioT = RepositorioFactory<OrdemServicoMaquina>.GetRepositorio();
            repositorioOrdemServicoMaquina = (IRepositorioOrdemServicoMaquina)repositorioT;
        }

        public static OrdemServicoMaquinaController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(OrdemServicoMaquina objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<OrdemServicoMaquina> GetAll()
        {
            return repositorioOrdemServicoMaquina.GetAll();
        }
    }
}