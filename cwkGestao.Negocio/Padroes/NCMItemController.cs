using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NCMItemController : BaseController<NCMItem>
	{
		#region Singleton
        private static NCMItemController instance;
        private static IRepositorioNCMItem repositorioNCMItem;

        private NCMItemController()
        { }

        static NCMItemController()
        {
            instance = new NCMItemController();
            repositorioT = RepositorioFactory<NCMItem>.GetRepositorio();
            repositorioNCMItem = (IRepositorioNCMItem)repositorioT;
        }

        public static NCMItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NCMItem objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            return ret;
        }
	}
}
