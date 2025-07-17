using System.Collections.Generic;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
    public class FechamentoCaixaRateioController: BaseController<FechamentoCaixaRateio>
	{
		#region Singleton
        private static FechamentoCaixaRateioController instance;
        private static IRepositorioFechamentoCaixaRateio repositorioFechamentoCaixaRateio;

        private FechamentoCaixaRateioController()
        { }
		
		static FechamentoCaixaRateioController()
        {
            instance = new FechamentoCaixaRateioController();
            repositorioT = RepositorioFactory<FechamentoCaixaRateio>.GetRepositorio();
            repositorioFechamentoCaixaRateio = (IRepositorioFechamentoCaixaRateio)repositorioT;
        }
			
		public static FechamentoCaixaRateioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(FechamentoCaixaRateio objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxFechamentoCaixaRateio> GetRateiosPorFluxoDeCaixa(int IDFluxoCaixa)
        {
            return repositorioFechamentoCaixaRateio.GetRateiosPorFluxoDeCaixa(IDFluxoCaixa);
        }
    }
}
