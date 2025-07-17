using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NotaServicoItemController : BaseController<NotaServicoItem>
	{
		#region Singleton
        private static NotaServicoItemController instance;
        private static IRepositorioNotaServicoItem repositorioPedido;

        private NotaServicoItemController()
        { }

        static NotaServicoItemController()
        {
            instance = new NotaServicoItemController();
            repositorioT = RepositorioFactory<NotaServicoItem>.GetRepositorio();
            repositorioPedido = (IRepositorioNotaServicoItem)repositorioT;
        }

        public static NotaServicoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaServicoItem objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
