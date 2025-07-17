using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public partial class PedidoItemController : BaseController<PedidoItem>
	{
		#region Singleton
        private static PedidoItemController instance;
        private static IRepositorioPedidoItem repositorioPedidoItem;

        private PedidoItemController()
        { }
		
		static PedidoItemController()
        {
            instance = new PedidoItemController();
            repositorioT = RepositorioFactory<PedidoItem>.GetRepositorio();
            repositorioPedidoItem = (IRepositorioPedidoItem)repositorioT;
        }
			
		public static PedidoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PedidoItem objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxyEntregaPedidoItem> GetAllPorIDPedido(int IDPedido)
        {
            return repositorioPedidoItem.GetAllPorIDPedido(IDPedido);
        }
	}
}
