using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class RemessaController : BaseController<Remessa>
	{
		#region Singleton
        private static RemessaController instance;
        private static IRepositorioRemessa repositorioRemessa;

        private RemessaController()
        { }
		
		static RemessaController()
        {
            instance = new RemessaController();
            repositorioT = RepositorioFactory<Remessa>.GetRepositorio();
            repositorioRemessa = (IRepositorioRemessa)repositorioT;
        }
			
		public static RemessaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Remessa objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
