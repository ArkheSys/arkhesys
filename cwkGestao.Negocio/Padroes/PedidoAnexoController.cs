using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PedidoAnexoController : BaseController<PedidoAnexo>
    {
        #region Singleton

        private static readonly IRepositorioPedidoAnexo repositorioPedidoAnexo;

        private PedidoAnexoController() { }

        static PedidoAnexoController()
        {
            Instancia = new PedidoAnexoController();
            repositorioT = RepositorioFactory<PedidoAnexo>.GetRepositorio();
            repositorioPedidoAnexo = (IRepositorioPedidoAnexo)repositorioT;
        }

        public static PedidoAnexoController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(PedidoAnexo objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<PedidoAnexo> GetAll()
        {
            return repositorioPedidoAnexo.GetAll();
        }

    }
}