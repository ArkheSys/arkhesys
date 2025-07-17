using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PedidoItemServicoController : BaseController<PedidoItemServico>
	{
		#region Singleton
        private static PedidoItemServicoController instance;
        private static IRepositorioPedidoItemServico repositorioPedidoItemServico;

        private PedidoItemServicoController()
        { }
		
		static PedidoItemServicoController()
        {
            instance = new PedidoItemServicoController();
            repositorioT = RepositorioFactory<PedidoItemServico>.GetRepositorio();
            repositorioPedidoItemServico = (IRepositorioPedidoItemServico)repositorioT;
        }
			
		public static PedidoItemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PedidoItemServico objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
