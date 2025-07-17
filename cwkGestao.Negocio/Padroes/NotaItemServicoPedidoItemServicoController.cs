using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NotaItemServicoPedidoItemServicoController : BaseController<NotaItemServicoPedidoItemServico>
	{
		#region Singleton
        private static NotaItemServicoPedidoItemServicoController instance;
        private static IRepositorioNotaItemServicoPedidoItemServico repositorioNotaItemServicoPedidoItemServico;

        private NotaItemServicoPedidoItemServicoController()
        { }
		
		static NotaItemServicoPedidoItemServicoController()
        {
            instance = new NotaItemServicoPedidoItemServicoController();
            repositorioT = RepositorioFactory<NotaItemServicoPedidoItemServico>.GetRepositorio();
            repositorioNotaItemServicoPedidoItemServico = (IRepositorioNotaItemServicoPedidoItemServico)repositorioT;
        }
			
		public static NotaItemServicoPedidoItemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaItemServicoPedidoItemServico objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
