using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class GrupoEstoqueRepositorio : RepositorioAbstrato<GrupoEstoque>, IRepositorioGrupoEstoque
    {
        protected override string HqlLoadById()
        {
            return "from GrupoEstoque g"
                    + " left join fetch g.GrupoEstoquePai"
                    + " where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from GrupoEstoque g";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                return session.CreateQuery("select max(g.Codigo) from GrupoEstoque g").UniqueResult<int>() + 1;
            }
        }

        public IList<GrupoEstoque> GetGruposEstoquePorClassificacao(string classificacao)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from GrupoEstoque p");
                hql.AppendLine("left join fetch p.GrupoEstoquePai");
                hql.AppendLine("where p.Classificacao like :classificacao");
                IQuery query = session.CreateQuery(hql.ToString()).SetString("classificacao", classificacao + "%");
                return query.List<GrupoEstoque>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
