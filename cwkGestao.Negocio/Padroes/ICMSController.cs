using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ICMSController : BaseController<ICMS>
	{
		#region Singleton
        private static ICMSController instance;
        private static IRepositorioICMS repositorioICMS;

        private ICMSController()
        { }
		
		static ICMSController()
        {
            instance = new ICMSController();
            repositorioT = RepositorioFactory<ICMS>.GetRepositorio();
            repositorioICMS = (IRepositorioICMS)repositorioT;
        }
			
		public static ICMSController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ICMS objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
