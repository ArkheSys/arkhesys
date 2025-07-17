using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    internal partial class OSAgendamentoRepositorio : RepositorioAbstrato<OSAgendamento>, IRepositorioOSAgendamento
    {
        protected override string HqlLoadById()
        {
            return "from OSAgendamento os where os.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from OSAgendamento";
        }

        public IList<Modelo.OSAgendamento> GetAgendamentosDia(DateTime dia)
        {
            using (var session = GetSession())
            {
                string hql = "select oa from OSAgendamento oa " +
                             "left join fetch oa.OSOrdemServico os " +
                                 "left join fetch os.Equipamento " +
                                 "left join fetch os.Cliente " +

                             "left join fetch oa.Funcionario " +
                             "where day(oa.Inicio) = day(:dia)";

                return session.CreateQuery(hql).SetDateTime("dia", dia).List<OSAgendamento>();
            }
        }

        public IList<OSAgendamento> GetAgendamentosPeriodo(DateTime periodoInicial, DateTime periodoFinal)
        {
            using (var session = GetSession())
            {
                string hql = "select oa from OSAgendamento oa " +
                             "left join fetch oa.OSOrdemServico os " +
                                 "left join fetch os.Equipamento " +
                                 "left join fetch os.Cliente " +

                             "left join fetch oa.Funcionario " +
                             "where day(:periodoInicial) <= day(oa.Inicio) and day(oa.Inicio) <= day(:periodoFinal)";

                return session.CreateQuery(hql).SetDateTime("periodoInicial", periodoInicial).SetDateTime("periodoFinal", periodoFinal).List<OSAgendamento>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
