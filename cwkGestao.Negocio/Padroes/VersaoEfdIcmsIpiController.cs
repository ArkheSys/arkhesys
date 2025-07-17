using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class VersaoEfdIcmsIpiController : BaseController<VersaoEfdIcmsIpi>
	{
		#region Singleton
        private static VersaoEfdIcmsIpiController instance;
        private static IRepositorioVersaoEfdIcmsIpi repositorioVersaoEfdIcmsIpi;

        private VersaoEfdIcmsIpiController()
        { }
		
		static VersaoEfdIcmsIpiController()
        {
            instance = new VersaoEfdIcmsIpiController();
            repositorioT = RepositorioFactory<VersaoEfdIcmsIpi>.GetRepositorio();
            repositorioVersaoEfdIcmsIpi = (IRepositorioVersaoEfdIcmsIpi)repositorioT;
        }
			
		public static VersaoEfdIcmsIpiController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(VersaoEfdIcmsIpi objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
