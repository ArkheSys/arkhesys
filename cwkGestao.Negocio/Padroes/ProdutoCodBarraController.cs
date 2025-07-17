using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProdutoCodBarraController : BaseController<ProdutoCodBarra>
	{
		#region Singleton
        private static ProdutoCodBarraController instance;
        private static IRepositorioProdutoCodBarra repositorioProdutoCodBarra;

        private ProdutoCodBarraController()
        { }
		
		static ProdutoCodBarraController()
        {
            instance = new ProdutoCodBarraController();
            repositorioT = RepositorioFactory<ProdutoCodBarra>.GetRepositorio();
            repositorioProdutoCodBarra = (IRepositorioProdutoCodBarra)repositorioT;
        }
			
		public static ProdutoCodBarraController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProdutoCodBarra objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
