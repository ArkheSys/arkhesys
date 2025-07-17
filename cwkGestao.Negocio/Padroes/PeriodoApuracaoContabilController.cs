using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PeriodoApuracaoContabilController : BaseController<PeriodoApuracaoContabil>
	{
		#region Singleton
        private static PeriodoApuracaoContabilController instance;
        private static IRepositorioPeriodoApuracaoContabil repositorioPeriodoApuracaoContabil;

        private PeriodoApuracaoContabilController()
        { }
		
		static PeriodoApuracaoContabilController()
        {
            instance = new PeriodoApuracaoContabilController();
            repositorioT = RepositorioFactory<PeriodoApuracaoContabil>.GetRepositorio();
            repositorioPeriodoApuracaoContabil = (IRepositorioPeriodoApuracaoContabil)repositorioT;
        }
			
		public static PeriodoApuracaoContabilController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PeriodoApuracaoContabil objeto)
        {
            return new Dictionary<string, string>();
        }

        public PeriodoApuracaoContabil GetAllByPeriod(DateTime start, DateTime end)
        {
            return repositorioPeriodoApuracaoContabil.GetAllByPeriod(start, end);
        }
	}
}
