using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CotacaoProdutoController : BaseController<CotacaoProduto>
	{
		#region Singleton
        private static CotacaoProdutoController instance;
        private static IRepositorioCotacaoProduto repositorioCotacaoProduto;

        private CotacaoProdutoController()
        { }
		
		static CotacaoProdutoController()
        {
            instance = new CotacaoProdutoController();
            repositorioT = RepositorioFactory<CotacaoProduto>.GetRepositorio();
            repositorioCotacaoProduto = (IRepositorioCotacaoProduto)repositorioT;
        }
			
		public static CotacaoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CotacaoProduto objeto)
		{
		    return new Dictionary<string, string>();
		}

        public IList<CotacaoProduto> GetByCotacao(Cotacao cotacao)
        {
            return repositorioCotacaoProduto.GetByCotacao(cotacao);
        }
    }
}
