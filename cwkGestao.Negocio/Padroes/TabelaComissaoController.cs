using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TabelaComissaoController : BaseController<TabelaComissao>
	{
		#region Singleton
        private static TabelaComissaoController instance;
        private static IRepositorioTabelaComissao repositorioTabelaComissao;

        private TabelaComissaoController()
        { }
		
		static TabelaComissaoController()
        {
            instance = new TabelaComissaoController();
            repositorioT = RepositorioFactory<TabelaComissao>.GetRepositorio();
            repositorioTabelaComissao = (IRepositorioTabelaComissao)repositorioT;
        }
			
		public static TabelaComissaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaComissao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
