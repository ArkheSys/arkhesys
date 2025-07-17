using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class StatusOSRepositorio : RepositorioAbstrato<StatusOS>, IRepositorioStatusOS
    {

        protected override string HqlLoadById()
        {
            return "from StatusOS s where s.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from StatusOS s";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetAllDescricao()
        {
            using (var session = GetSession())
            {
                string hql = "Select s.Descricao from StatusOS s";

                IList<string> status = session.CreateQuery(hql).List<string>();

                return status;
            }
        }

        public StatusOS GetStatusTipoFechamento()
        {
            using (var session = GetSession())
            {
                string hql = "from StatusOS s where s.BFechado = 1";
                StatusOS statusOS = session.CreateQuery(hql).List<StatusOS>().FirstOrDefault();

                return statusOS;
            }
        }


        public StatusOS GetStatusTipoAgendamento()
        {
            using (var session = GetSession())
            {
                string hql = "from StatusOS s where s.BAgendar = 1";
                StatusOS statusOS = session.CreateQuery(hql).List<StatusOS>().FirstOrDefault();

                return statusOS;
            }
        }

        public StatusOS GetStatusTipoFaturamento()
        {
            using (var session = GetSession())
            {
                string hql = "from StatusOS s where s.BFaturar = True";
                StatusOS statusOS = session.CreateQuery(hql).List<StatusOS>().FirstOrDefault();

                return statusOS;
            }
        }
    }
}
