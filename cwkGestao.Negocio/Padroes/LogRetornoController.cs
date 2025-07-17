using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public partial class LogRetornoController : BaseController<LogRetorno>
	{
		#region Singleton
        private static LogRetornoController instance;
        private static IRepositorioLogRetorno repositorioLogRetorno;

        private LogRetornoController()
        { }

        static LogRetornoController()
        {
            instance = new LogRetornoController();
            repositorioT = RepositorioFactory<LogRetorno>.GetRepositorio();
            repositorioLogRetorno = (IRepositorioLogRetorno)repositorioT;
        }

        public static LogRetornoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(LogRetorno objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
