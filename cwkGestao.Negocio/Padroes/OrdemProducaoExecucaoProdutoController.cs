using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class OrdemProducaoExecucaoProdutoController : BaseController<OrdemProducaoExecucaoProduto>
	{
		#region Singleton
        private static OrdemProducaoExecucaoProdutoController instance;
        private static IRepositorioOrdemProducaoExecucaoProduto repositorioOrdemProducaoExecucaoProduto;

        private OrdemProducaoExecucaoProdutoController()
        { }
		
		static OrdemProducaoExecucaoProdutoController()
        {
            instance = new OrdemProducaoExecucaoProdutoController();
            repositorioT = RepositorioFactory<OrdemProducaoExecucaoProduto>.GetRepositorio();
            repositorioOrdemProducaoExecucaoProduto = (IRepositorioOrdemProducaoExecucaoProduto)repositorioT;
        }
			
		public static OrdemProducaoExecucaoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(OrdemProducaoExecucaoProduto objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
