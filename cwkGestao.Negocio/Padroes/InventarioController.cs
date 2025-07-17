using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class InventarioController : BaseController<Inventario>
	{
		#region Singleton
        private static InventarioController instance;
        private static IRepositorioInventario repositorioInventario;

        private InventarioController()
        { }
		
		static InventarioController()
        {
            instance = new InventarioController();
            repositorioT = RepositorioFactory<Inventario>.GetRepositorio();
            repositorioInventario = (IRepositorioInventario)repositorioT;
        }
			
		public static InventarioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Inventario objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
