using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public class ProdutoFCIController : BaseController<ProdutoFCI>
	{
		#region Singleton
        private static ProdutoFCIController instance;
        private static IRepositorioProdutoFCI repositorioProdutoFCI;

        private ProdutoFCIController()
        { }
		
		static ProdutoFCIController()
        {
            instance = new ProdutoFCIController();
            repositorioT = RepositorioFactory<ProdutoFCI>.GetRepositorio();
            repositorioProdutoFCI = (IRepositorioProdutoFCI)repositorioT;
        }
			
		public static ProdutoFCIController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProdutoFCI objeto)
        {
            return new Dictionary<string, string>();
        }
        
        public IList<pxyProdutoFCI> GetAllPXYProdutoFCIPorIDProduto(int IDProduto)
        {
            return repositorioProdutoFCI.GetAllPXYProdutoFCIPorIDProduto(IDProduto);
        }

        public IList<ProdutoFCI> GetFCIByIdProduto(int IdProduto)
        {
            return repositorioProdutoFCI.GetFCIByIdProduto(IdProduto);
        }
	}
}
