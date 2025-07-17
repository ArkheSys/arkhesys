using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ExpedicaoItemController : BaseController<ExpedicaoItem>
	{
		#region Singleton
        private static ExpedicaoItemController instance;
        private static IRepositorioExpedicaoItem repositorioExpedicaoItem;

        private ExpedicaoItemController()
        { }
		
		static ExpedicaoItemController()
        {
            instance = new ExpedicaoItemController();
            repositorioT = RepositorioFactory<ExpedicaoItem>.GetRepositorio();
            repositorioExpedicaoItem = (IRepositorioExpedicaoItem)repositorioT;
        }
			
		public static ExpedicaoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ExpedicaoItem objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
