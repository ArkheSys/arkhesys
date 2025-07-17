using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ficha_ProdutoController : BaseController<Ficha_Produto>
	{
		#region Singleton
        private static Ficha_ProdutoController instance;
        private static IRepositorioFicha_Produto repositorioFicha_Produto;

        private Ficha_ProdutoController()
        { }
		
		static Ficha_ProdutoController()
        {
            instance = new Ficha_ProdutoController();
            repositorioT = RepositorioFactory<Ficha_Produto>.GetRepositorio();
            repositorioFicha_Produto = (IRepositorioFicha_Produto)repositorioT;
        }
			
		public static Ficha_ProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ficha_Produto objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
