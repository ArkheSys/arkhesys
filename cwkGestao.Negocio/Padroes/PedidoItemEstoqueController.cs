using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PedidoItemEstoqueController : BaseController<PedidoItemEstoque>
	{
		#region Singleton
        private static PedidoItemEstoqueController instance;
        private static IRepositorioPedidoItemEstoque repositorioPedidoItemEstoque;

        private PedidoItemEstoqueController()
        { }
		
		static PedidoItemEstoqueController()
        {
            instance = new PedidoItemEstoqueController();
            repositorioT = RepositorioFactory<PedidoItemEstoque>.GetRepositorio();
            repositorioPedidoItemEstoque = (IRepositorioPedidoItemEstoque)repositorioT;
        }
			
		public static PedidoItemEstoqueController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PedidoItemEstoque objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
