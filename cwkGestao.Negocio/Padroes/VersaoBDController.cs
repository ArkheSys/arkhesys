using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class VersaoBDController : BaseController<VersaoBD>
	{
		#region Singleton
        private static VersaoBDController instance;
        private static IRepositorioVersaoBD repositorioVersaoBD;

        private VersaoBDController()
        { }
		
		static VersaoBDController()
        {
            instance = new VersaoBDController();
            repositorioT = RepositorioFactory<VersaoBD>.GetRepositorio();
            repositorioVersaoBD = (IRepositorioVersaoBD)repositorioT;
        }
			
		public static VersaoBDController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(VersaoBD objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
