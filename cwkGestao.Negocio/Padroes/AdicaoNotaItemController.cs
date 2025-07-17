using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class AdicaoNotaItemController : BaseController<AdicaoNotaItem>
	{
		#region Singleton
        private static AdicaoNotaItemController instance;
        private static IRepositorioAdicaoNotaItem repositorioAdicaoNotaItem;

        private AdicaoNotaItemController()
        { }

        static AdicaoNotaItemController()
        {
            instance = new AdicaoNotaItemController();
            repositorioT = RepositorioFactory<AdicaoNotaItem>.GetRepositorio();
            repositorioAdicaoNotaItem = (IRepositorioAdicaoNotaItem)repositorioT;
        }

        public static AdicaoNotaItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(AdicaoNotaItem objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            return ret;
        }
	}
}
