using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class StatusContatoController : BaseController<StatusContato>
	{
		#region Singleton
        private static StatusContatoController instance;
        private static IRepositorioStatusContato repositorioStatusContato;

        private StatusContatoController()
        { }
		
		static StatusContatoController()
        {
            instance = new StatusContatoController();
            repositorioT = RepositorioFactory<StatusContato>.GetRepositorio();
            repositorioStatusContato = (IRepositorioStatusContato)repositorioT;
        }
			
		public static StatusContatoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(StatusContato objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
