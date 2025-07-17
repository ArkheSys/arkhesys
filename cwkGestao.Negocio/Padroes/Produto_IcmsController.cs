using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Produto_IcmsController : BaseController<Produto_Icms>
	{
		#region Singleton
        private static Produto_IcmsController instance;
        private static IRepositorioProduto_Icms repositorioProduto_Icms;

        private Produto_IcmsController()
        { }
		
		static Produto_IcmsController()
        {
            instance = new Produto_IcmsController();
            repositorioT = RepositorioFactory<Produto_Icms>.GetRepositorio();
            repositorioProduto_Icms = (IRepositorioProduto_Icms)repositorioT;
        }
			
		public static Produto_IcmsController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Produto_Icms objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
