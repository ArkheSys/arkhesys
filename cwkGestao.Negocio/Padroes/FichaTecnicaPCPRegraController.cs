using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FichaTecnicaPCPRegraController : BaseController<FichaTecnicaPCPRegra>
	{
		#region Singleton
        private static FichaTecnicaPCPRegraController instance;
        private static IRepositorioFichaTecnicaPCPRegra repositorioFichaTecnicaPCPRegra;

        private FichaTecnicaPCPRegraController()
        { }
		
		static FichaTecnicaPCPRegraController()
        {
            instance = new FichaTecnicaPCPRegraController();
            repositorioT = RepositorioFactory<FichaTecnicaPCPRegra>.GetRepositorio();
            repositorioFichaTecnicaPCPRegra = (IRepositorioFichaTecnicaPCPRegra)repositorioT;
        }
			
		public static FichaTecnicaPCPRegraController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FichaTecnicaPCPRegra objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
