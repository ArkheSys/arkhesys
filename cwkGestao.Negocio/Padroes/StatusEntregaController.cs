using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class StatusEntregaController : BaseController<StatusEntrega>
	{
		#region Singleton
        private static StatusEntregaController instance;
        private static IRepositorioStatusEntrega repositorioStatusEntrega;

        private StatusEntregaController()
        { }
		
		static StatusEntregaController()
        {
            instance = new StatusEntregaController();
            repositorioT = RepositorioFactory<StatusEntrega>.GetRepositorio();
            repositorioStatusEntrega = (IRepositorioStatusEntrega)repositorioT;
        }
			
		public static StatusEntregaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(StatusEntrega objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
