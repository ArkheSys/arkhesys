using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PlanoNegocioController : BaseController<PlanoNegocio>
	{
		#region Singleton
        private static PlanoNegocioController instance;
        private static IRepositorioPlanoNegocio repositorioPlanoNegocio;

        private PlanoNegocioController()
        { }
		
		static PlanoNegocioController()
        {
            instance = new PlanoNegocioController();
            repositorioT = RepositorioFactory<PlanoNegocio>.GetRepositorio();
            repositorioPlanoNegocio = (IRepositorioPlanoNegocio)repositorioT;
        }
			
		public static PlanoNegocioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PlanoNegocio objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
