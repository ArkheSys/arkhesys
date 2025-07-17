using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class TabelaPrecoProdutoController : BaseController<TabelaPrecoProduto>
	{
		#region Singleton
        private static TabelaPrecoProdutoController instance;
        private static IRepositorioTabelaPrecoProduto repositorioTabelaPrecoProduto;

        private TabelaPrecoProdutoController()
        { }
		
		static TabelaPrecoProdutoController()
        {
            instance = new TabelaPrecoProdutoController();
            repositorioT = RepositorioFactory<TabelaPrecoProduto>.GetRepositorio();
            repositorioTabelaPrecoProduto = (IRepositorioTabelaPrecoProduto)repositorioT;
        }
			
		public static TabelaPrecoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaPrecoProduto objeto)
        {
            return new Dictionary<string, string>();
        }

        public TabelaPrecoProduto GetByTabelaPrecoProduto(int idtabelapreco, int idproduto)
        {
            return repositorioTabelaPrecoProduto.GetByTabelaPrecoProduto(idtabelapreco, idproduto);
        }
	}
}
