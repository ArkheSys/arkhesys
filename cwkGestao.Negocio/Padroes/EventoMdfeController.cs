using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class EventoMdfeController : BaseController<EventoMdfe>
    {
        #region Singleton
        private static EventoMdfeController instance;
        private static IRepositorioEventoMdfe repositorioEventoMdfe;

        private EventoMdfeController()
        { }

        static EventoMdfeController()
        {
            instance = new EventoMdfeController();
            repositorioT = RepositorioFactory<EventoMdfe>.GetRepositorio();
            repositorioEventoMdfe = (IRepositorioEventoMdfe)repositorioT;
        }

        public static EventoMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(EventoMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
