using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ContratoProdutoController : BaseController<ContratoProduto>
	{
		#region Singleton
        private static ContratoProdutoController instance;
        private static IRepositorioContratoProduto repositorioContratoProduto;

        private ContratoProdutoController()
        { }
		
		static ContratoProdutoController()
        {
            instance = new ContratoProdutoController();
            repositorioT = RepositorioFactory<ContratoProduto>.GetRepositorio();
            repositorioContratoProduto = (IRepositorioContratoProduto)repositorioT;
        }
			
		public static ContratoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ContratoProduto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ContratoProduto> GetItensContratoParaFaturar(DateTime vencimento, out DateTime referencia)
        {
            referencia = vencimento.AddMonths(-1);
            Int32 diavcto = vencimento.Day;

            return repositorioContratoProduto.GetItensContratoParaFaturar(referencia, diavcto);
        }
	}
}
