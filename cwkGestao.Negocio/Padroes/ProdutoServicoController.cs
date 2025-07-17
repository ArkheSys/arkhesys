using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ProdutoServicoController : BaseController<ProdutoServico>
	{
		#region Singleton
        private static ProdutoServicoController instance;
        private static IRepositorioProdutoServico repositorioProdutoServico;

        private ProdutoServicoController()
        { }
		
		static ProdutoServicoController()
        {
            instance = new ProdutoServicoController();
            repositorioT = RepositorioFactory<ProdutoServico>.GetRepositorio();
            repositorioProdutoServico = (IRepositorioProdutoServico)repositorioT;
        }
			
		public static ProdutoServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ProdutoServico objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Produto> GetAllProdutoPorIDServico(int idServico)
        {
            return repositorioProdutoServico.GetAllProdutoPorIDServico(idServico);
        }

        public ProdutoServico GetSelecionadoPorIDProdutoEIDServico(int idProduto, int idServico)
        {
            return repositorioProdutoServico.GetSelecionadoPorIDProdutoEIDServico(idProduto, idServico);
        }
	}
}
