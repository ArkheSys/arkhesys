using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class LogAlteracaoParticipanteController : BaseController<LogAlteracaoParticipante>
	{
		#region Singleton
        private static LogAlteracaoParticipanteController instance;
        private static IRepositorioLogAlteracaoParticipante repositorioLogAlteracaoParticipante;

        private LogAlteracaoParticipanteController()
        { }
		
		static LogAlteracaoParticipanteController()
        {
            instance = new LogAlteracaoParticipanteController();
            repositorioT = RepositorioFactory<LogAlteracaoParticipante>.GetRepositorio();
            repositorioLogAlteracaoParticipante = (IRepositorioLogAlteracaoParticipante)repositorioT;
        }
			
		public static LogAlteracaoParticipanteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(LogAlteracaoParticipante objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<LogAlteracaoParticipante> LoadObjectByData(DateTime start, DateTime end)
        {
            return repositorioLogAlteracaoParticipante.LoadObjectByData(start, end);
        }
    }
}
