using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class OrdemProducaoHistoricoController : BaseController<OrdemProducaoHistorico>
	{
		#region Singleton
        private static OrdemProducaoHistoricoController instance;
        private static IRepositorioOrdemProducaoHistorico repositorioOrdemProducaoHistorico;

        private OrdemProducaoHistoricoController()
        { }
		
		static OrdemProducaoHistoricoController()
        {
            instance = new OrdemProducaoHistoricoController();
            repositorioT = RepositorioFactory<OrdemProducaoHistorico>.GetRepositorio();
            repositorioOrdemProducaoHistorico = (IRepositorioOrdemProducaoHistorico)repositorioT;
        }
			
		public static OrdemProducaoHistoricoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(OrdemProducaoHistorico objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
