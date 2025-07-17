using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PeriodoApuracaoContabilRepositorio : RepositorioAbstrato<PeriodoApuracaoContabil>, IRepositorioPeriodoApuracaoContabil
    {
        protected override string HqlLoadById()
        {
            return "from PeriodoApuracaoContabil n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PeriodoApuracaoContabil n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public PeriodoApuracaoContabil GetAllByPeriod(DateTime start, DateTime end)
        {
            using (var session = GetSession())
            {
                string hql = " from PeriodoApuracaoContabil p" +
                         " where :start >= p.Inicio " +
                         " and :end <= p.Fim";

                var query = session.CreateQuery(hql).SetDateTime("start", start).SetDateTime("end", end).List<PeriodoApuracaoContabil>().FirstOrDefault();

                return query;
            }        
        }
    }
}
