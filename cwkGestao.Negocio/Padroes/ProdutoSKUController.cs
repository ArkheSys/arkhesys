using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProdutoSKUController : BaseController<ProdutoSKU>
	{
		#region Singleton
        private static ProdutoSKUController instance;
        private static IRepositorioProdutoSKU repositorioProdutoSKU;

        private ProdutoSKUController()
        { }
		
		static ProdutoSKUController()
        {
            instance = new ProdutoSKUController();
            repositorioT = RepositorioFactory<ProdutoSKU>.GetRepositorio();
            repositorioProdutoSKU = (IRepositorioProdutoSKU)repositorioT;
        }
			
		public static ProdutoSKUController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProdutoSKU objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ProdutoSKU> GetSKUPorProduto(int id)
        {
            return repositorioProdutoSKU.GetSKUPorProduto(id);
        }

        public IList<ProdutoSKU> GetProdutoPorSKU(string sku)
        {
            return repositorioProdutoSKU.GetProdutoPorSKU(sku);
        }
        public Produto FindProdutoPorSKU(string sku)
        {
            return repositorioProdutoSKU.FindProdutoPorSKU(sku);
        }
	}
}
