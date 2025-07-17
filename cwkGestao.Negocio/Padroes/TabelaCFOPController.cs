using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TabelaCFOPController : BaseController<TabelaCFOP>
	{
		#region Singleton
        private static TabelaCFOPController instance;
        private static IRepositorioTabelaCFOP repositorioTabelaCFOP;

        private TabelaCFOPController()
        { }
		
		static TabelaCFOPController()
        {
            instance = new TabelaCFOPController();
            repositorioT = RepositorioFactory<TabelaCFOP>.GetRepositorio();
            repositorioTabelaCFOP = (IRepositorioTabelaCFOP)repositorioT;
        }
			
		public static TabelaCFOPController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TabelaCFOP objeto)
        {
            return new Dictionary<string, string>();
        }

        public TabelaCFOP GetCFOPPorNumero(string codCFOP)
        {
            return repositorioTabelaCFOP.GetCFOPPorNumero(codCFOP);
        }
	}
}
