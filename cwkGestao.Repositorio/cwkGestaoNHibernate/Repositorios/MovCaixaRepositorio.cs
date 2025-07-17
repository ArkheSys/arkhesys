using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MovCaixaRepositorio : RepositorioAbstrato<MovCaixa>, IRepositorioMovCaixa
    {
        protected override string HqlLoadById()
        {
            return @"select m from MovCaixa m 
                     left join fetch m.Filial
                     left join fetch m.Banco
                     left join fetch m.Historico
                     left join fetch m.FormaPagamento
                     left join fetch m.Movimentos mov
                     left join fetch mov.PlanoConta
                     left join fetch mov.Documento
                     where m.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"select distinct new MovCaixa(m.ID, m.Codigo, mov.Origem, fil.Nome, b.Nome, m.Dt, m.DtConciliacao, m.NumDocumento, m.Valor
                            , m.ComplementoHist, fp.Nome, m.Ent_Sai, m.IncData, m.IncUsuario, m.AltData, m.AltUsuario, m.IDFluxoCaixa) 
                            from MovCaixa m
                            left join m.Movimentos mov
                            left join m.FormaPagamento fp
                            left join m.Banco b 
                            left join m.Filial fil ";
        }

        public IList<pxyMovCaixaPorFluxoCaixa> GetMovimentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var sessao = GetSession())
            {
                var sql = @"With MovimentosRecebimento as 
                              (
                                select MovCaixa.IDFluxoCaixa,
                                concat('[',Documento.Parcela, '/', Documento.QtParcela, '] - ', Pessoa.Nome) Descricao,
       	                        MovCaixa.Valor as Valor,  Documento.ID
                                from MovCaixa
       	                        inner join Movimento on (MovCaixa.ID = Movimento.IDMovCaixa)
       	                        inner join Documento on (Movimento.IDDocumento = Documento.ID)
       	                        inner join Pessoa on (Documento.IDPessoa = Pessoa.ID)
       	                        inner join FormaPagamento on (MovCaixa.IDFormaPagamento = FormaPagamento.ID)
                                where MovCaixa.IDFluxoCaixa = :idFluxoCaixa
                                and MovCaixa.Ent_Sai = :mvcaixa_Entrada
                                group by MovCaixa.IDFluxoCaixa, Pessoa.Nome, Documento.QtParcela, Documento.Parcela, MovCaixa.ID, Movcaixa.Valor, Documento.ID
                              )
                          Select IDFluxoCaixa, Descricao, SUM(Valor) as Valor From MovimentosRecebimento
                         Group By IDFluxoCaixa, Descricao, ID";
                IQuery query = sessao.CreateSQLQuery(sql).SetInt32("idFluxoCaixa", IDFluxoCaixa).SetEnum("mvcaixa_Entrada", Ent_SaiMovCaixa.Entrada);
                ConstructorInfo construtor = typeof(pxyMovCaixaPorFluxoCaixa).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(string),
                    typeof(decimal),
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyMovCaixaPorFluxoCaixa>();
            }
        }

        public IList<pxySaldoBanco> GetSaldoBancos(IList<Modelo.Banco> bancos, DateTime data)
        {
            using (var sessao = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select  b.ID, b.Nome");
                hql.AppendLine(", SUM(case when m.Ent_Sai = :mvcaixa_Entrada then m.Valor else -m.Valor end) as saldo");
                hql.AppendLine("from MovCaixa m");
                hql.AppendLine("join m.Banco b");
                hql.AppendLine("where m.Dt < :data");
                if (bancos.Count > 0)
                {
                    hql.Append("and b.ID in (");
                    for (int i = 0; i < bancos.Count; i++)
                    {
                        if (i > 0)
                            hql.Append(", ");
                        hql.Append(bancos[i].ID);
                    }
                    hql.AppendLine(")");
                }
                hql.AppendLine("group by b.ID, b.Nome");
                hql.AppendLine("order by b.Nome");

                IQuery query = sessao.CreateQuery(hql.ToString())
                    .SetDateTime("data", data.AddDays(1))
                    .SetEnum("mvcaixa_Entrada", Modelo.Ent_SaiMovCaixa.Entrada);
                ConstructorInfo construtor = typeof(pxySaldoBanco).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(decimal),
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxySaldoBanco>();
            }
        }

        public IList<Modelo.MovCaixa> GetPorBancoEData(Modelo.Banco banco, DateTime dataInicial, DateTime dataFinal)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select m from MovCaixa m");
                hql.AppendLine("join fetch m.Banco b");
                hql.AppendLine("join fetch m.Filial f");
                hql.AppendLine("where m.Dt >= :dataInicial and m.Dt <= :dataFinal");
                if (banco != null)
                    hql.AppendLine("and b.ID = :idbanco");
                hql.Append("ORDER BY m.Dt");

                IQuery query = session.CreateQuery(hql.ToString())
                                      .SetDateTime("dataInicial", dataInicial.Date)
                                      .SetDateTime("dataFinal", dataFinal.Date.AddHours(23).AddMinutes(59).AddSeconds(59));
                if (banco != null)
                    query = query.SetInt32("idbanco", banco.ID);

                return query.List<Modelo.MovCaixa>().OrderBy(x => x.Dt).ToList();
            }
        }

        public IList<pxyMovimentoCaixa> GetRelatorioDeMovimentoCaixa(Modelo.Banco banco, DateTime dataInicial, DateTime dataFinal)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select (case when m.Origem = 1 then 'F' else 'C' end) as Origem, ");
                hql.AppendLine("d.NumDocumento as Doc, ");
                hql.AppendLine("p.Nome as Pessoa, ");
                hql.AppendLine("mc.ComplementoHist as Historico, ");
                hql.AppendLine("pc.Nome as PlanoConta, ");
                hql.AppendLine("fp.Nome as FormaPagamento, ");
                hql.AppendLine("mc.Ent_Sai as Ent_Sai, ");
                hql.AppendLine("pc.DebCre as Deb_Cre, ");
                hql.AppendLine("d.Tipo as Tipo_Doc, ");
                hql.AppendLine("m.Tipo as Tipo_Mov, ");
                hql.AppendLine("m.Valor as Valor ");
                hql.AppendLine("from Movimento m");
                hql.AppendLine("join m.MovCaixa mc");
                hql.AppendLine("left join m.Documento d");
                hql.AppendLine("left join m.PlanoConta pc");
                hql.AppendLine("left join mc.Banco b");
                hql.AppendLine("join mc.FormaPagamento fp");
                hql.AppendLine("left join d.Pessoa p");
                hql.AppendLine("where mc.Dt >= :dataInicial and mc.Dt < :dataFinal");
                if (banco != null)
                    hql.AppendLine("and b.ID = :idbanco");

                IQuery query = session.CreateQuery(hql.ToString())
                                      .SetDateTime("dataInicial", dataInicial.Date)
                                      .SetDateTime("dataFinal", dataFinal.Date.AddDays(1));
                if (banco != null)
                    query = query.SetInt32("idbanco", banco.ID);

                ConstructorInfo construtor = typeof(pxyMovimentoCaixa).GetConstructor(new Type[] {
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(Ent_SaiMovCaixa),
                typeof(DebCreType),
                typeof(TipoDocumentoType),
                typeof(TipoMovimentoType),
                typeof(decimal),
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyMovimentoCaixa>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public int GetNovoCodigoMovimento()
        {
            using (var session = GetSession()) return session.CreateSQLQuery("SELECT NEXT VALUE FOR [SEQ_CODIGO_MOVCAIXA]").UniqueResult<int>();
        }
    }
}
