using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CodigoImpostoController : BaseController<CodigoImposto>
	{
		#region Singleton
        private static CodigoImpostoController instance;
        private static IRepositorioCodigoImposto repositorioCodigoImposto;

        private CodigoImpostoController()
        { }
		
		static CodigoImpostoController()
        {
            instance = new CodigoImpostoController();
            repositorioT = RepositorioFactory<CodigoImposto>.GetRepositorio();
            repositorioCodigoImposto = (IRepositorioCodigoImposto)repositorioT;
        }
			
		public static CodigoImpostoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CodigoImposto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<CodigoImposto> GetAllPorTipoImposto(string p)
        {
            return repositorioCodigoImposto.GetAllPorTipoImposto(p);
        }
    }
}
