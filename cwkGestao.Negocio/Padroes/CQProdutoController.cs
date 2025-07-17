using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CQProdutoController : BaseController<CQProduto>
	{
		#region Singleton
        private static CQProdutoController instance;
        private static IRepositorioCQProduto repositorioCQProduto;

        private CQProdutoController()
        { }
		
		static CQProdutoController()
        {
            instance = new CQProdutoController();
            repositorioT = RepositorioFactory<CQProduto>.GetRepositorio();
            repositorioCQProduto = (IRepositorioCQProduto)repositorioT;
        }
			
		public static CQProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CQProduto objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
