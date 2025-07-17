using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FollowUpRepositorio : RepositorioAbstrato<FollowUp>, IRepositorioFollowUp
    {
        protected override string HqlLoadById()
        {
            return "from FollowUp t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FollowUp t";
        }

        public IList<FollowUp> GetTodasFollowUp()
        {
            using (var session = GetSession())
            {
                string hql = "from FollowUp tn";
                IQuery query = session.CreateQuery(hql);
                return query.List<FollowUp>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
