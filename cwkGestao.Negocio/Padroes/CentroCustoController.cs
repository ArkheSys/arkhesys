using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CentroCustoController : BaseController<CentroCusto>
	{
		#region Singleton
        private static CentroCustoController instance;
        private static IRepositorioCentroCusto repositorioCentroCusto;

        private CentroCustoController()
        { }
		
		static CentroCustoController()
        {
            instance = new CentroCustoController();
            repositorioT = RepositorioFactory<CentroCusto>.GetRepositorio();
            repositorioCentroCusto = (IRepositorioCentroCusto)repositorioT;
        }
			
		public static CentroCustoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CentroCusto objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
