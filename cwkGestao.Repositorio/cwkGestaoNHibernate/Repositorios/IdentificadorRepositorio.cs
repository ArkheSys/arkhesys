using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class IdentificadorRepositorio : RepositorioAbstrato<Identificador>, IRepositorioIdentificador
    {
        protected override string HqlLoadById()
        {
            return "from Identificador n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Identificador n";
        }

        public int GetProximaSequenciaAno(int ano)
        {
            using (var session = GetSession())
            {
                var hql = "select MAX(i.Sequencia) from Identificador i where i.Ano = :ano";
                return session.CreateQuery(hql)
                              .SetParameter("ano", ano)
                              .UniqueResult<int>() + 1;
            }
        }

        public Identificador getByAnoSequencia(int ano, int sequencia)
        {
            using (var session = GetSession())
            {
                var hql = "from Identificador i where i.Ano = :ano and i.Sequencia = :sequencia";
                return session.CreateQuery(hql)
                              .SetParameter("ano", ano)
                              .SetParameter("sequencia", sequencia)
                              .UniqueResult<Identificador>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
