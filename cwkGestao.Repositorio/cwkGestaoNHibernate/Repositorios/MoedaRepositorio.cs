using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MoedaRepositorio : RepositorioAbstrato<Moeda>, IRepositorioMoeda
    {
        protected override string HqlLoadById()
        {
            return "from Moeda t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Moeda t";
        }

        public IList<Moeda> GetTodasMoedas()
        {
            using (var session = GetSession())
            {
                string hql = "from Moeda tn";
                IQuery query = session.CreateQuery(hql);
                return query.List<Moeda>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
