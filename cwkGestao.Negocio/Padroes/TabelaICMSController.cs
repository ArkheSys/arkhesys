using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TabelaICMSController : BaseController<TabelaICMS>
	{
		#region Singleton
        private static TabelaICMSController instance;
        private static IRepositorioTabelaICMS repositorioTabelaICMS;

        private TabelaICMSController()
        { }
		
		static TabelaICMSController()
        {
            instance = new TabelaICMSController();
            repositorioT = RepositorioFactory<TabelaICMS>.GetRepositorio();
            repositorioTabelaICMS = (IRepositorioTabelaICMS)repositorioT;
        }
			
		public static TabelaICMSController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaICMS objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
