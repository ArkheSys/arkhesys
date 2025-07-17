using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ImpostoController : BaseController<Imposto>
	{
		#region Singleton
        private static ImpostoController instance;
        private static IRepositorioImposto repositorioImposto;

        private ImpostoController()
        { }
		
		static ImpostoController()
        {
            instance = new ImpostoController();
            repositorioT = RepositorioFactory<Imposto>.GetRepositorio();
            repositorioImposto = (IRepositorioImposto)repositorioT;
        }
			
		public static ImpostoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Imposto objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
