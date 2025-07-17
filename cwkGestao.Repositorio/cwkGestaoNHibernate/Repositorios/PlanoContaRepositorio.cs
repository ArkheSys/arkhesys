using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PlanoContaRepositorio : RepositorioAbstrato<PlanoConta>, IRepositorioPlanoConta
    {
        protected override string HqlLoadById()
        {
            return "from PlanoConta p"
                    + " left join fetch p.PlanoContaPai"
                    + " where p.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PlanoConta p";
        }

        public IList<PlanoConta> GetPlanoContasPorClassificacao(string classificacao)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from PlanoConta p");
                hql.AppendLine("left join fetch p.PlanoContaPai");
                hql.AppendLine("left join fetch p.PlanoContaFilhos");
                hql.AppendLine("where p.Classificacao like :classificacao");
                IQuery query = session.CreateQuery(hql.ToString()).SetString("classificacao", classificacao + "%");
                return query.List<PlanoConta>();

            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
