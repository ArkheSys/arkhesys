using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class LogAlteracaoParticipanteRepositorio : RepositorioAbstrato<LogAlteracaoParticipante>, IRepositorioLogAlteracaoParticipante
    {
        protected override string HqlLoadById()
        {
            return "from LogAlteracaoParticipante n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from LogAlteracaoParticipante n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<LogAlteracaoParticipante> LoadObjectByData(DateTime dataInicio, DateTime dataFim)
        {
            using (var session = GetSession())
            {
                string hql = " from LogAlteracaoParticipante log";

                hql += " where log.DataAlteracao >= DATEADD(day, 1, :dataInicio) " +
                       " and log.DataAlteracao < :dataFim";


                var query = session.CreateQuery(hql).SetDateTime("dataInicio", dataInicio).SetDateTime("dataFim", dataFim).List<LogAlteracaoParticipante>();

                return query;
            }
        }
    }
}
