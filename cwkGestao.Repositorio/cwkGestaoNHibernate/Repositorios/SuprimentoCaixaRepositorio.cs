using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SuprimentoCaixaRepositorio : RepositorioAbstrato<SuprimentoCaixa>, IRepositorioSuprimentoCaixa
    {
        protected override string hqlGetAll()
        {
            return "from SuprimentoCaixa c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from SuprimentoCaixa c where c.ID = :id";
        }

        public decimal GetSuprimentoVendasComSuprimento(int IDUsuario, int IDFluxoCaixa)
        {
            string hql = @"SELECT DISTINCT (FC.VALORCAIXA + COALESCE((SELECT SUM(COALESCE(TOTALPAGO, 0)) FROM FRENTECAIXANOTA FCN WHERE FCN.IDFLUXOCAIXA = FC.ID), 0)) - 
                                  COALESCE((SELECT SUM(COALESCE(VALOR, 0)) FROM SuprimentoCaixa SC WHERE SC.IDFLUXOCAIXA = FC.ID), 0)
                           FROM FLUXOCAIXA AS FC
                              LEFT JOIN FRENTECAIXANOTA AS FCN ON FC.ID = FCN.IDFLUXOCAIXA
                              LEFT JOIN SuprimentoCaixa AS SG ON FC.ID = SG.IDFLUXOCAIXA
                             WHERE FC.IDUsuario = :IDUsuario
                               AND FC.ID = :IDFluxoCaixa";

            using (var session = GetSession())
            {
                return session.CreateSQLQuery(hql).SetInt32("IDUsuario", IDUsuario).SetInt32("IDFluxoCaixa", IDFluxoCaixa).UniqueResult<decimal>();
            }
        }

        public List<SuprimentoCaixa> GetSuprimentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(@"from SuprimentoCaixa c where c.IDFluxoCaixa = :IDFluxoCaixa").SetInt32("IDFluxoCaixa", IDFluxoCaixa).List<SuprimentoCaixa>().ToList();
            }
        }

        public SuprimentoCaixa GetSuprimento(int IDSuprimentoCaixa)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(@"from SuprimentoCaixa c where c.ID = :IDSuprimentoCaixa").SetInt32("IDSuprimentoCaixa", IDSuprimentoCaixa).List<SuprimentoCaixa>().FirstOrDefault();
            }
        }

        public IList<pxSuprimentoCaixa> GetSuprimentos(DateTime dtInicio, DateTime dtFinal)
        {
            using (var session = GetSession())
            {
                IQuery query = session.CreateSQLQuery(@"select DISTINCT sc.ID as IDSuprimentoCaixa,
                                                            cw.Nome as Usuario,
                                                            fc.DataAbertura,
                                                            fc.DataFechamento,
                                                            sc.DataSuprimento,
                                                            sc.Valor as ValorSuprimento,
                                                            sc.Observacao
                                                       from SuprimentoCaixa sc
                                                          inner join FluxoCaixa fc on sc.IDFluxoCaixa = fc.ID
                                                     	  inner join cw_usuario cw on fc.IDUsuario = cw.id

                                                        order by sc.DataSuprimento desc");
                //.SetDateTime("dtInicio", dtInicio)
                //.SetDateTime("dtFinal", dtFinal);

                ConstructorInfo construtor = typeof(pxSuprimentoCaixa).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(string),
                                                    typeof(DateTime?),
                                                    typeof(DateTime?),
                                                    typeof(DateTime?),
                                                    typeof(decimal),
                                                    typeof(string)
                                                    });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxSuprimentoCaixa>();
            }
        }
    }
}
