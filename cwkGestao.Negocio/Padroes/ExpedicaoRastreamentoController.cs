using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ExpedicaoRastreamentoController : BaseController<ExpedicaoRastreamento>
	{
		#region Singleton
        private static ExpedicaoRastreamentoController instance;
        private static IRepositorioExpedicaoRastreamento repositorioexpedicaorastreamento;

        private ExpedicaoRastreamentoController()
        { }
		
		static ExpedicaoRastreamentoController()
        {
            instance = new ExpedicaoRastreamentoController();
            repositorioT = RepositorioFactory<ExpedicaoRastreamento>.GetRepositorio();
            repositorioexpedicaorastreamento = (IRepositorioExpedicaoRastreamento)repositorioT;
        }
			
		public static ExpedicaoRastreamentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ExpedicaoRastreamento objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoPorPedido(int id)
        {
            return repositorioexpedicaorastreamento.GetExpedicaoRastreamentoPorPedido(id);
        }

        public IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoPorPLP(PLP plp)
        {
            return repositorioexpedicaorastreamento.GetExpedicaoRastreamentoPorPLP(plp);
        }

        public ExpedicaoRastreamento GetExpedicaoRastreamentoPorRastreamento(int idRastreamento)
        {
            return repositorioexpedicaorastreamento.GetExpedicaoRastreamentoByRastreamento(idRastreamento);
        }

        public IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoNaoCompletas(int pIdPedido)
        {
            return repositorioexpedicaorastreamento.GetExpedicaoRastreamentoNaoCompletas(pIdPedido);
        }
	}
}
