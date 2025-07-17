using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class EventosCorreioController : BaseController<EventosCorreio>
    {
        #region Singleton
        private static EventosCorreioController instance;
        private static IRepositorioEventosCorreio repositorioEventosCorreio;

        private EventosCorreioController()
        { }

        static EventosCorreioController()
        {
            instance = new EventosCorreioController();
            repositorioT = RepositorioFactory<EventosCorreio>.GetRepositorio();
            repositorioEventosCorreio = (IRepositorioEventosCorreio)repositorioT;
        }

        public static EventosCorreioController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(EventosCorreio objeto)
        {
            return new Dictionary<string, string>();
        }

        public virtual EventosCorreio GetEventoPorTipoEStatus(string tipo, int status)
        {
            return repositorioEventosCorreio.GetEventoPorTipoEStatus(tipo, status);
        }
    }
}
