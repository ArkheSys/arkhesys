using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
    public class FinanceiroPlanoContasController : BaseController<FinanceiroPlanoContas>
    {
      
        #region Singleton
        private static FinanceiroPlanoContasController instance;
        //private static IRepositorioPlanoConta repositorioPlanoConta;

        private FinanceiroPlanoContasController()
        { }

        static FinanceiroPlanoContasController()
        {
            instance = new FinanceiroPlanoContasController();
            //repositorioT = RepositorioFactory<FinanceiroPlanoContas>.GetRepositorio();
            //repositorioPlanoConta = (IRepositorioPlanoConta)repositorioT;
        }

        public static FinanceiroPlanoContasController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
        public override Dictionary<string, string> ValidaObjeto(FinanceiroPlanoContas objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
