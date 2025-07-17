using System;
using System.Collections.Generic;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MovimentoRepositorio : RepositorioAbstrato<Movimento>, IRepositorioMovimento
    {
        protected override string HqlLoadById()
        {
            return "select m from Movimento m    " +
                   "     left join fetch m.Rateio_Movs rm    " +
                   "      left join fetch m.FormaPagamentoMovimentos fpm " +
                   "     left join fetch fpm.FormaPagamento    fpppp" +
                   " where m.ID = :id  ";
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public IList<Movimento> GetRelatorioPorPlanoContas(DateTime dataInicial, DateTime dataFinal, string classificacao, string idsEmpresas, bool filtrarPorDataCompetencia)
        {
            dataFinal = dataFinal.AddHours(23).AddMinutes(59);
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from Movimento m");
                hql.AppendLine("join fetch m.PlanoConta p");
                hql.AppendLine("left join fetch m.Documento d");
                hql.AppendLine("left join fetch d.Pessoa");
                hql.AppendLine("left join fetch d.Historico");
                hql.AppendLine("left join fetch m.MovCaixa mc");
                hql.AppendLine("left join fetch mc.Historico");
                hql.AppendLine("where p.Classificacao like :classificacao");
                hql.AppendLine("and m.Dt >=  :dataInicial AND m.Dt <= :dataFinal");
                if (filtrarPorDataCompetencia)
                {
                    hql.AppendLine("and (m.Operacao = 1)");
                }
                else
                {
                    hql.AppendLine("and (m.Operacao in (2, 3) or (m.Operacao = 1 and m.Origem = 3))");
                }

                hql.AppendLine("and (d.Filial.ID in " + idsEmpresas);
                hql.AppendLine("or mc.Filial.ID in " + idsEmpresas + ")");
                IQuery query = session.CreateQuery(hql.ToString()).SetString("classificacao", classificacao + "%");
                query = query.SetDateTime("dataInicial", dataInicial).SetDateTime("dataFinal", dataFinal.AddHours(23).AddMinutes(59).AddSeconds(59));

                return query.List<Movimento>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public int GetNovoCodigoMovimento()
        {
            using (var session = GetSession()) return session.CreateSQLQuery("SELECT NEXT VALUE FOR [SEQ_CODIGO_MOVIMENTO]").UniqueResult<int>();
        }
    }
}
