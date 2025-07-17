using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TabelaFaixaFatSimplesController : BaseController<TabelaFaixaFatSimples>
	{
		#region Singleton
        private static TabelaFaixaFatSimplesController instance;
        private static IRepositorioTabelaFaixaFatSimples repositorioTabelaFaixaFatSimples;

        private TabelaFaixaFatSimplesController()
        { }
		
		static TabelaFaixaFatSimplesController()
        {
            instance = new TabelaFaixaFatSimplesController();
            repositorioT = RepositorioFactory<TabelaFaixaFatSimples>.GetRepositorio();
            repositorioTabelaFaixaFatSimples = (IRepositorioTabelaFaixaFatSimples)repositorioT;
        }
			
		public static TabelaFaixaFatSimplesController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaFaixaFatSimples objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
