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
    partial class FluxoCaixaRepositorio : RepositorioAbstrato<FluxoCaixa>, IRepositorioFluxoCaixa
    {
        protected override string hqlGetAll()
        {
            return "from FluxoCaixa c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from FluxoCaixa c where c.ID = :id";
        }

        public FluxoCaixa GetFluxoDeCaixa(int IDFluxoCaixa)
        {
            string hql = "from FluxoCaixa c where c.ID = :ID";

            using (var session = GetSession())
            {
                return session.CreateQuery(hql).SetInt32("ID", IDFluxoCaixa).List<FluxoCaixa>().FirstOrDefault();
            }
        }

        public FluxoCaixa GetByUsuario(int IDUsuario)
        {
            string hql = "from FluxoCaixa c where c.IDUsuario = :IDUsuario and c.Aberto = 1 order by c.DataAbertura desc";

            using (var session = GetSession())
            {
                try
                {
                    return session.CreateQuery(hql).SetInt32("IDUsuario", IDUsuario).List<FluxoCaixa>().FirstOrDefault();
                }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                {
                    return session.CreateQuery(hql).SetInt32("IDUsuario", IDUsuario).List<FluxoCaixa>().FirstOrDefault();
                }
            }
        }

        public IList<pxFluxoCaixa> GetFluxosCaixa()
        {
            using (var session = GetSession())
            {

                IQuery query = session.CreateSQLQuery(@"SELECT FluxoCaixa.ID,
                                                               cw_usuario.Login,
                                                               FluxoCaixa.DataAbertura,
                                                        	   FluxoCaixa.DataFechamento,
                                                        	   FluxoCaixa.ValorCaixa AS ValorAbertura,
                                                        	   FluxoCaixa.ValorFechamento,
                                                        	   FluxoCaixa.Observacao
                                                          FROM FluxoCaixa
                                                            inner join cw_usuario on (cw_usuario.id = FluxoCaixa.IDUsuario)
                                                        ORDER BY FluxoCaixa.ID DESC");

                ConstructorInfo construtor = typeof(pxFluxoCaixa).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(string),
                                                    typeof(DateTime?),
                                                    typeof(DateTime?),
                                                    typeof(decimal),
                                                    typeof(decimal),
                                                    typeof(string)
                                                    });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxFluxoCaixa>();
            }
        }
    }
}
