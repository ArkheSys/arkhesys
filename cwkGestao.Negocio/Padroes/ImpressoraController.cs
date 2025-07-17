using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ImpressoraController : BaseController<Impressora>
	{
		#region Singleton
        private static ImpressoraController instance;
        private static IRepositorioImpressora repositorioImpressora;

        private ImpressoraController()
        { }
		
		static ImpressoraController()
        {
            instance = new ImpressoraController();
            repositorioT = RepositorioFactory<Impressora>.GetRepositorio();
            repositorioImpressora = (IRepositorioImpressora)repositorioT;
        }
			
		public static ImpressoraController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Impressora objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
