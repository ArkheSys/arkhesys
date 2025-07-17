using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class FichaTecnicaController : BaseController<FichaTecnica>
	{
		#region Singleton
        private static FichaTecnicaController instance;
        private static IRepositorioFichaTecnica repositorioFichaTecnica;

        private FichaTecnicaController()
        { }
		
		static FichaTecnicaController()
        {
            instance = new FichaTecnicaController();
            repositorioT = RepositorioFactory<FichaTecnica>.GetRepositorio();
            repositorioFichaTecnica = (IRepositorioFichaTecnica)repositorioT;
        }
			
		public static FichaTecnicaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FichaTecnica objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
