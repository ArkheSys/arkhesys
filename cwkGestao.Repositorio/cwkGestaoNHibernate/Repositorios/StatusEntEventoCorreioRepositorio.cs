using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class StatusEntEventoCorreioRepositorio : RepositorioAbstrato<StatusEntEventoCorreio>, IRepositorioStatusEntEventoCorreio
    {
        protected override string HqlLoadById()
        {
            return "from StatusEntEventoCorreio n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from StatusEntEventoCorreio n";
        }

        public StatusEntEventoCorreio GetStatusEntEventoCorreioPorEvento(int id)
        {
            using (var session = GetSession())
            {
                string hql = "FROM StatusEntEventoCorreio s WHERE s.EventosCorreio.ID = :id";
                return session.CreateQuery(hql).SetParameter("id", id).UniqueResult<StatusEntEventoCorreio>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
