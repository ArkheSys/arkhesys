using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_FaturaLigacaoRepositorio : RepositorioAbstrato<Tel_FaturaLigacao>, IRepositorioTel_FaturaLigacao
    {
        protected override string HqlLoadById()
        {
            return "from Tel_FaturaLigacao g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_FaturaLigacao g";
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetLigacoes(int IDFatura)
        {
            using (var session = GetSession())
            {
                string select = @"SELECT f.ID, t.Sequencia, l.TempoSegundos, t.ValorMinuto,t.Total,
                                         l.Origem, l.Destino, l.DataHora, l.TipoUso, l.DescricaoUso,
                                         l.Cidade, l.UF, tl.ResumoFatura, tl.LigacaoLocal  
                                FROM Tel_FaturaLigacao t
                                JOIN t.Fatura f
                                LEFT JOIN t.Ligacao l
                                LEFT JOIN t.TipoLigacao tl
                                WHERE t.Fatura.ID = :IDFatura";

                ConstructorInfo construtor = typeof(pxyTel_FaturaLigacao).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int),
                    typeof(int),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(string),
                    typeof(string),
                    typeof(DateTime),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(bool)
                });

                return session.CreateQuery(select).SetInt32("IDFatura", IDFatura).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyTel_FaturaLigacao>();
            }
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetByGrupoEReferencia(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(GetQueryPxyLigacoes("WHERE c.GrupoCliente.ID = :idGrupoCliente AND f.Referencia = :referencia"))
                              .SetInt32("idGrupoCliente", grupoCliente.ID)
                              .SetDateTime("referencia", referencia)
                              .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorInfoPxyLigacoes()))
                              .List<pxyTel_FaturaLigacao>();
            }
        }

        public string GetQueryPxyLigacoes(string where)
        {
            return @"SELECT f.ID, t.Sequencia, l.TempoSegundos, t.ValorMinuto,t.Total,
                                         l.Origem, l.Destino, l.DataHora, l.TipoUso, l.DescricaoUso,
                                         l.Cidade, l.UF, tl.ResumoFatura, tl.LigacaoLocal 
                                FROM Tel_FaturaLigacao t
                                JOIN t.Fatura f
                                JOIN f.Cliente c
                                JOIN t.Ligacao l
                                JOIN t.TipoLigacao tl " + where;
        }

        public ConstructorInfo GetConstructorInfoPxyLigacoes()
        {
            return typeof(pxyTel_FaturaLigacao).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int),
                    typeof(int),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(string),
                    typeof(string),
                    typeof(DateTime),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(bool)
                });
        }

        public IList<cwkGestao.Modelo.Proxy.pxyTel_FaturaLigacao> GetByFaturas(IList<int> idsFaturas)
        {
            using (var session = GetSession())
            {
                StringBuilder where = new StringBuilder(" WHERE f.ID in (");
                for (int i = 0; i < idsFaturas.Count(); i++)
                {
                    if (i > 0)
                        where.Append(", ");
                    where.Append(idsFaturas[i]);
                }
                where.Append(")");

                return session.CreateQuery(GetQueryPxyLigacoes(where.ToString()))
                              .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorInfoPxyLigacoes()))
                              .List<pxyTel_FaturaLigacao>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Tel_Ligacao> GetAllLigacoesPorIDArquivo(int id)
        {
            IList<Tel_Ligacao> result = new List<Tel_Ligacao>();

            using (var session = GetSession())
            {
                string sql = @"
                    SELECT tl.[ID]
                          ,tl.[TipoUso]
                          ,tl.[DescricaoUso]
                          ,tl.[Origem]
                          ,tl.[Destino]
                          ,tl.[Cidade]
                          ,tl.[UF]
                          ,tl.[DataHora]
                          ,tl.[TempoSegundos]
                          ,tl.[Horario]
                          ,tl.[ValorComImposto],
                    case when tf.Id IS null then 0
                    else 1
                    end as GeradoFatura

                    from Tel_ligacao tl
                    left join Tel_ArquivoLigacao ta on ta.id = tl.IdTel_ArquivoLigacao
                    left join Tel_FaturaLigacao tf on tf.IdTel_Ligacao = tl.ID
                    where ta.Id = :id
                    ";

                ConstructorInfo construtor = typeof(Tel_Ligacao).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(DateTime),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(int)
                    
                });
                result = session.CreateSQLQuery(sql).SetInt32("id", id).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<Tel_Ligacao>();
            }
            return result;
        }
    }
}
