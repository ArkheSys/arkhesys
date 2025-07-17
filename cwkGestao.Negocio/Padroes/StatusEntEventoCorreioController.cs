using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class StatusEntEventoCorreioController : BaseController<StatusEntEventoCorreio>
	{
		#region Singleton
        private static StatusEntEventoCorreioController instance;
        private static IRepositorioStatusEntEventoCorreio repositorioStatusEntEventoCorreio;

        private StatusEntEventoCorreioController()
        { }
		
		static StatusEntEventoCorreioController()
        {
            instance = new StatusEntEventoCorreioController();
            repositorioT = RepositorioFactory<StatusEntEventoCorreio>.GetRepositorio();
            repositorioStatusEntEventoCorreio = (IRepositorioStatusEntEventoCorreio)repositorioT;
        }
			
		public static StatusEntEventoCorreioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(StatusEntEventoCorreio objeto)
        {
            return new Dictionary<string, string>();
        }

        public virtual StatusEntEventoCorreio GetStatusEntEventoCorreioPorEvento(int id)
        {
            return repositorioStatusEntEventoCorreio.GetStatusEntEventoCorreioPorEvento(id);
        }
	}
}
