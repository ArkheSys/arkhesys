using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaOrcamentoServicosRepositorio : RepositorioAbstrato<Ofd_PropostaOrcamentoServicos>, IRepositorioOfd_PropostaOrcamentoServicos
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_PropostaOrcamentoServicos n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_PropostaOrcamentoServicos n";
        }

        public IList<Modelo.Ofd_PropostaOrcamentoServicos> GetOfdServicosPorIDOfd_Proposta(int IDOfd_Proposta)
        {
            using (var session = GetSession())
            {
                string hql = "select os from Ofd_PropostaOrcamentoServicos os " +
                             "left join fetch os.Servico s " +
                             "left join fetch os.Ofd_Proposta p " +
                             " where p.ID = :IDOfd_Proposta";
                var retorno = session.CreateQuery(hql).SetInt32("IDOfd_Proposta", IDOfd_Proposta).List<Modelo.Ofd_PropostaOrcamentoServicos>();
                return retorno;
            }
        }
        public Modelo.Ofd_PropostaOrcamentoServicos GetOfdServicoPorIDServico(int IDServico)
        {
            using (var session = GetSession())
            {
                string hql = "select os from Ofd_PropostaOrcamentoServicos os " +
                             "left join fetch os.Servico s " +
                             "left join fetch os.Ofd_Proposta p " +
                             " where s.ID = :IDServico";
                var retorno = session.CreateQuery(hql).SetInt32("IDServico", IDServico).List<Modelo.Ofd_PropostaOrcamentoServicos>().FirstOrDefault();
                return retorno;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
