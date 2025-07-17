using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SincronismoPendenteSHLController : BaseController<SincronismoPendenteSHL>
	{
		#region Singleton
        private static SincronismoPendenteSHLController instance;
        private static IRepositorioSincronismoPendenteSHL repositorioSincronismoPendenteSHL;

        private SincronismoPendenteSHLController()
        { }
		
		static SincronismoPendenteSHLController()
        {
            instance = new SincronismoPendenteSHLController();
            repositorioT = RepositorioFactory<SincronismoPendenteSHL>.GetRepositorio();
            repositorioSincronismoPendenteSHL = (IRepositorioSincronismoPendenteSHL)repositorioT;
        }
			
		public static SincronismoPendenteSHLController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(SincronismoPendenteSHL objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
