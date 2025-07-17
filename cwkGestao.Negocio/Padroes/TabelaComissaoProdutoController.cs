using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TabelaComissaoProdutoController : BaseController<TabelaComissaoProduto>
	{
		#region Singleton
        private static TabelaComissaoProdutoController instance;
        private static IRepositorioTabelaComissaoProduto repositorioTabelaComissaoProduto;

        private TabelaComissaoProdutoController()
        { }
		
		static TabelaComissaoProdutoController()
        {
            instance = new TabelaComissaoProdutoController();
            repositorioT = RepositorioFactory<TabelaComissaoProduto>.GetRepositorio();
            repositorioTabelaComissaoProduto = (IRepositorioTabelaComissaoProduto)repositorioT;
        }
			
		public static TabelaComissaoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaComissaoProduto objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
