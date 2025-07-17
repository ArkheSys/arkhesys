using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SincronismoPendenteSisECommerceController : BaseController<SincronismoPendenteSisECommerce>
	{
		#region Singleton
        private static SincronismoPendenteSisECommerceController instance;
        private static IRepositorioSincronismoPendenteSisECommerce repositorioSincronismoPendenteSisECommerce;

        private SincronismoPendenteSisECommerceController()
        { }
		
		static SincronismoPendenteSisECommerceController()
        {
            instance = new SincronismoPendenteSisECommerceController();
            repositorioT = RepositorioFactory<SincronismoPendenteSisECommerce>.GetRepositorio();
            repositorioSincronismoPendenteSisECommerce = (IRepositorioSincronismoPendenteSisECommerce)repositorioT;
        }
			
		public static SincronismoPendenteSisECommerceController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(SincronismoPendenteSisECommerce objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
