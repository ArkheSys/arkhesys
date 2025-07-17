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
	public class PedidoItemTrocaController : BaseController<PedidoItemTroca>
	{
		#region Singleton
        private static PedidoItemTrocaController instance;
        private static IRepositorioPedidoItemTroca repositorioPedidoItemTroca;

        private PedidoItemTrocaController()
        { }
		
		static PedidoItemTrocaController()
        {
            instance = new PedidoItemTrocaController();
            repositorioT = RepositorioFactory<PedidoItemTroca>.GetRepositorio();
            repositorioPedidoItemTroca = (IRepositorioPedidoItemTroca)repositorioT;
        }
			
		public static PedidoItemTrocaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PedidoItemTroca objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<PedidoItemTroca> GetItemPedidoTrocaPorIDPedidoItem(int idPedidoItem)
        {
            return repositorioPedidoItemTroca.LoadItemTrocaByIdPedidoItem(idPedidoItem);
        }

        public IList<pxyPedidoItemTroca> GetItensDoPedidoECodProduto(int IDPedido)
        {
            return repositorioPedidoItemTroca.GetItensDoPedidoECodProduto(IDPedido);
        }
        
	}
}
