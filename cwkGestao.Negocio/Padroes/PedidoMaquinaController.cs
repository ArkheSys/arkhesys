using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PedidoMaquinaController : BaseController<PedidoMaquina>
    {
        #region Singleton

        private static readonly IRepositorioPedidoMaquina repositorioPedidoMaquina;

        private PedidoMaquinaController() { }

        static PedidoMaquinaController()
        {
            Instancia = new PedidoMaquinaController();
            repositorioT = RepositorioFactory<PedidoMaquina>.GetRepositorio();
            repositorioPedidoMaquina = (IRepositorioPedidoMaquina)repositorioT;
        }

        public static PedidoMaquinaController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(PedidoMaquina objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<PedidoMaquina> GetAll()
        {
            return repositorioPedidoMaquina.GetAll();
        }
    }
}