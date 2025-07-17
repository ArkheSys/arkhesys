using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ExpedicaoHistoricoController : BaseController<ExpedicaoHistorico>
	{
		#region Singleton
        private static ExpedicaoHistoricoController instance;
        private static IRepositorioExpedicaoHistorico repositorioExpedicaoHistorico;

        private ExpedicaoHistoricoController()
        { }
		
		static ExpedicaoHistoricoController()
        {
            instance = new ExpedicaoHistoricoController();
            repositorioT = RepositorioFactory<ExpedicaoHistorico>.GetRepositorio();
            repositorioExpedicaoHistorico = (IRepositorioExpedicaoHistorico)repositorioT;
        }
			
		public static ExpedicaoHistoricoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ExpedicaoHistorico objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ExpedicaoHistorico> GetHistoricosPorCliente(DateTime dtInicio, DateTime dtFinal, int idCliente)
        {
            return repositorioExpedicaoHistorico.GetHistoricosPorCliente(dtInicio,dtFinal,idCliente);
        }

        public IList<ExpedicaoHistorico> GetHistoricos(DateTime dtInicio, DateTime dtFinal, int idEmpresa)
        {
            return repositorioExpedicaoHistorico.GetHistoricos(dtInicio, dtFinal, idEmpresa);
        }

        public IList<ExpedicaoHistorico> GetHistoricosPorExpedicaoRastreamento(int id)
        {
            return repositorioExpedicaoHistorico.GetHistoricosPorExpedicaoRastreamento(id);
        }
	}
}
