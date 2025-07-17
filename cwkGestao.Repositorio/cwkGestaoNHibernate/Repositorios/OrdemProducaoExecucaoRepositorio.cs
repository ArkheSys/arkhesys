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
    partial class OrdemProducaoExecucaoRepositorio : RepositorioAbstrato<OrdemProducaoExecucao>, IRepositorioOrdemProducaoExecucao
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public IList<object> GetRelatorioApontamento(DateTime dataInicial, DateTime dataFinal, OrdemProducao op, Pessoa funcionario, Servico servico)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();

                hql.AppendLine("select o.Codigo as OP");
                hql.AppendLine(", e.DataInicio");
                hql.AppendLine(", e.DataTermino");
                hql.AppendLine(", s.Nome as Servico");
                hql.AppendLine(", p.Nome as Funcionario");
                hql.AppendLine(", e.Quantidade as TempoHoras");
                hql.AppendLine("from OrdemProducaoExecucao e");
                hql.AppendLine("join e.OrdemProducao o");
                hql.AppendLine("join e.Servico s");
                hql.AppendLine("left join e.Funcionario pf");
                hql.AppendLine("left join pf.Pessoa p");
                hql.AppendLine("where e.DataInicio >= :dataInicial");
                hql.AppendLine("and e.DataInicio < :dataFinal");

                if (op != null)
                    hql.AppendLine("and o.ID = " + op.ID);

                if (funcionario != null)
                    hql.AppendLine("and p.ID = " + funcionario.ID);

                if (servico != null)
                    hql.AppendLine("and s.ID = " + servico.ID);

                hql.AppendLine("order by e.DataInicio, e.DataTermino");

                IQuery query = session.CreateQuery(hql.ToString())
                                      .SetDateTime("dataInicial", dataInicial.Date)
                                      .SetDateTime("dataFinal", dataFinal.Date.AddDays(1));

                ConstructorInfo construtor = typeof(pxyApontamentoHoras).GetConstructor(new Type[] {
                typeof(int),
                typeof(DateTime),
                typeof(DateTime),
                typeof(string),
                typeof(string),
                typeof(decimal)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public IList<OrdemProducaoExecucao> GetApontamentos(PessoaFuncionario pessoaFuncionario)
        {
            using (var session = GetSession())
            {
                string hql = "from OrdemProducaoExecucao ope where ope.Funcionario = :func ";
                IQuery query = session.CreateQuery(hql).SetEntity("func", pessoaFuncionario);
                var resultados = query.List<OrdemProducaoExecucao>();
                return resultados;

            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
