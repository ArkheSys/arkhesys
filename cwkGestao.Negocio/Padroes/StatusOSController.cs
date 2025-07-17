using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Padroes
{
    public class StatusOSController : BaseController<StatusOS>
    {
        #region Singleton
        private static StatusOSController instance;
        private static IRepositorioStatusOS repositorioStatusOS;

        protected  StatusOSController()
        { }

        static StatusOSController()
        {
            instance = new StatusOSController();
            repositorioT = RepositorioFactory<StatusOS>.GetRepositorio();
            repositorioStatusOS = (IRepositorioStatusOS)repositorioT;
        }

        public static StatusOSController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(StatusOS objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<string> GetAllDescricao()
        {
            return repositorioStatusOS.GetAllDescricao();
        }

        public StatusOS GetStatusTipoFechamento()
        {
            return repositorioStatusOS.GetStatusTipoFechamento();
        }

        public StatusOS GetStatusTipoFaturamento()
        {
            return repositorioStatusOS.GetStatusTipoFaturamento();
        }

        public StatusOS GetStatusTipoAgendamento()
        {
            return repositorioStatusOS.GetStatusTipoAgendamento();
        }
    }
}
