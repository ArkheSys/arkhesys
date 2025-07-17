using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CotacaoProdutoPedidoItemController : BaseController<CotacaoProdutoPedidoItem>
	{
		#region Singleton
        private static CotacaoProdutoPedidoItemController instance;
        private static IRepositorioCotacaoProdutoPedidoItem repositorioCotacaoProdutoPedidoItem;

        private CotacaoProdutoPedidoItemController()
        { }
		
		static CotacaoProdutoPedidoItemController()
        {
            instance = new CotacaoProdutoPedidoItemController();
            repositorioT = RepositorioFactory<CotacaoProdutoPedidoItem>.GetRepositorio();
            repositorioCotacaoProdutoPedidoItem = (IRepositorioCotacaoProdutoPedidoItem)repositorioT;
        }
			
		public static CotacaoProdutoPedidoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CotacaoProdutoPedidoItem objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
