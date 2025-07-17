using System;
using System.Collections.Generic;
using System.Reflection;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CotacaoMoedaRepositorio : RepositorioAbstrato<CotacaoMoeda>, IRepositorioCotacaoMoeda
    {
        protected override string HqlLoadById()
        {
            return "select t from CotacaoMoeda t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CotacaoMoeda t";
        }

        public IList<CotacaoMoeda> GetTodasCotacaoMoedas()
        {
            using (var session = GetSession())
            {
                string hql = "from CotacaoMoeda tn";
                IQuery query = session.CreateQuery(hql);
                return query.List<CotacaoMoeda>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }

        public IList<pxCotacaoMoeda> GetCotacoesMoeda()
        {
            using (var session = GetSession())
            {

                IQuery query = session.CreateSQLQuery(@"SELECT DISTINCT MOEDA.ID,
                                                               MOEDA.DESCRICAO AS MOEDA,
                                                        	   MAX(COTACAOMOEDA.DATACOTACAO) AS DATACOTACAO,
                                                        	   MAX(COTACAOMOEDA.VALORCOTACAO) AS VALORCOTACAO
                                                          FROM COTACAOMOEDA
                                                             INNER JOIN MOEDA ON (COTACAOMOEDA.IDMOEDA = MOEDA.ID)
                                                          WHERE COTACAOMOEDA.HABILITACOTACAO = 1
                                                        GROUP BY MOEDA.ID, MOEDA.DESCRICAO");

                ConstructorInfo construtor = typeof(pxCotacaoMoeda).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(string),
                                                    typeof(DateTime),
                                                    typeof(decimal),
                                                    });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxCotacaoMoeda>();
            }
        }
    }
}
