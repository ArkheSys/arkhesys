using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class EntregaItemController : BaseController<EntregaItem>
	{
		#region Singleton
        private static EntregaItemController instance;
        private static IRepositorioEntregaItem repositorioEntregaItem;

        private EntregaItemController()
        { }
		
		static EntregaItemController()
        {
            instance = new EntregaItemController();
            repositorioT = RepositorioFactory<EntregaItem>.GetRepositorio();
            repositorioEntregaItem = (IRepositorioEntregaItem)repositorioT;
        }
			
		public static EntregaItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(EntregaItem objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
