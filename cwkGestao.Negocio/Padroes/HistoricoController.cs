using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class HistoricoController : BaseController<Historico>
	{
		#region Singleton
        private static HistoricoController instance;
        private static IRepositorioHistorico repositorioHistorico;

        private HistoricoController()
        { }
		
		static HistoricoController()
        {
            instance = new HistoricoController();
            repositorioT = RepositorioFactory<Historico>.GetRepositorio();
            repositorioHistorico = (IRepositorioHistorico)repositorioT;
        }
			
		public static HistoricoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Historico objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
