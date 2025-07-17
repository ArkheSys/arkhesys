using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CondicaoFornecimentoRepositorio : RepositorioAbstrato<CondicaoFornecimento>, IRepositorioCondicaoFornecimento
    {
        protected override string HqlLoadById()
        {
            return "select t from CondicaoFornecimento t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CondicaoFornecimento t";
        }

        public IList<CondicaoFornecimento> GetTodasCondicaoFornecimento()
        {
            using (var session = GetSession())
            {
                string hql = "from CondicaoFornecimento tn";
                IQuery query = session.CreateQuery(hql);
                return query.List<CondicaoFornecimento>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
