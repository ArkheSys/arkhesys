using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class LogRetornoDetalheController : BaseController<LogRetornoDetalhe>
	{
		#region Singleton
        private static LogRetornoDetalheController instance;
        private static IRepositorioLogRetornoDetalhe repositorioLogRetornoDetalhe;

        private LogRetornoDetalheController()
        { }

        static LogRetornoDetalheController()
        {
            instance = new LogRetornoDetalheController();
            repositorioT = RepositorioFactory<LogRetornoDetalhe>.GetRepositorio();
            repositorioLogRetornoDetalhe = (IRepositorioLogRetornoDetalhe)repositorioT;
        }

        public static LogRetornoDetalheController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(LogRetornoDetalhe objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<LogRetornoDetalhe> HqlLoadByIdLogRetorno(int idLogRetorno) 
        {
            return repositorioLogRetornoDetalhe.HqlLoadByIdLogRetorno(idLogRetorno);
        }
	}
}
