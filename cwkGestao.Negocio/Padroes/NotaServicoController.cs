using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NotaServicoController : BaseController<NotaServico>
	{
		#region Singleton
        private static NotaServicoController instance;
        private static IRepositorioNotaServico repositorioPedido;

        private NotaServicoController()
        { }

        static NotaServicoController()
        {
            instance = new NotaServicoController();
            repositorioT = RepositorioFactory<NotaServico>.GetRepositorio();
            repositorioPedido = (IRepositorioNotaServico)repositorioT;
        }

        public static NotaServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaServico objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            return ret;
        }
	}
}
