using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProdutoConversaoController : BaseController<ProdutoConversao>
	{
		#region Singleton
        private static ProdutoConversaoController instance;
        private static IRepositorioProdutoConversao repositorioProdutoConversao;

        private ProdutoConversaoController()
        { }
		
		static ProdutoConversaoController()
        {
            instance = new ProdutoConversaoController();
            repositorioT = RepositorioFactory<ProdutoConversao>.GetRepositorio();
            repositorioProdutoConversao = (IRepositorioProdutoConversao)repositorioT;
        }
			
		public static ProdutoConversaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProdutoConversao objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ProdutoConversao> GetByIdProduto(int IdProduto)
        {
            return repositorioProdutoConversao.GetByIdProduto(IdProduto);
        }

        public IList<ProdutoConversao> GetByIdConversaoUnidade(int IdConversaoUnidade)
        {
            return repositorioProdutoConversao.GetByIdConversaoUnidade(IdConversaoUnidade);
        }

        public ProdutoConversao GetByIdConversaoUnidadeAndIdProduto(int IdConversaoUnidade, int IdProduto)
        {
            return repositorioProdutoConversao.GetByIdConversaoUnidadeAndIdProduto(IdConversaoUnidade, IdProduto);
        }
	}
}
