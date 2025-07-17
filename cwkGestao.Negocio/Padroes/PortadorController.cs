using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PortadorController : BaseController<Portador>
	{
		#region Singleton
        private static PortadorController instance;
        private static IRepositorioPortador repositorioPortador;

        private PortadorController()
        { }
		
		static PortadorController()
        {
            instance = new PortadorController();
            repositorioT = RepositorioFactory<Portador>.GetRepositorio();
            repositorioPortador = (IRepositorioPortador)repositorioT;
        }
			
		public static PortadorController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Portador objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
