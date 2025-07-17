using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PlanoContaController : BaseController<PlanoConta>
	{
		#region Singleton
        private static PlanoContaController instance;
        private static IRepositorioPlanoConta repositorioPlanoConta;

        private PlanoContaController()
        { }
		
		static PlanoContaController()
        {
            instance = new PlanoContaController();
            repositorioT = RepositorioFactory<PlanoConta>.GetRepositorio();
            repositorioPlanoConta = (IRepositorioPlanoConta)repositorioT;
        }
			
		public static PlanoContaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PlanoConta objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<PlanoConta> GetPlanoContasPorClassificacao(string classificacao)
        {
            return repositorioPlanoConta.GetPlanoContasPorClassificacao(classificacao);
        }
	}
}
