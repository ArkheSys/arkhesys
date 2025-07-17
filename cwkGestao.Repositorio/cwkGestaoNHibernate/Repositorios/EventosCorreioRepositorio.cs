using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class EventosCorreioRepositorio : RepositorioAbstrato<EventosCorreio>, IRepositorioEventosCorreio
    {
        protected override string HqlLoadById()
        {
            return "from EventosCorreio n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from EventosCorreio n";
        }

        public EventosCorreio GetEventoPorTipoEStatus(string tipo, int status)
        {
            using (var session = GetSession())
            {
                string hql = "from EventosCorreio e where e.Tipo like '%" + tipo + "%' and e.Status = :status";
                return session.CreateQuery(hql).SetParameter("status", status).UniqueResult<EventosCorreio>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}