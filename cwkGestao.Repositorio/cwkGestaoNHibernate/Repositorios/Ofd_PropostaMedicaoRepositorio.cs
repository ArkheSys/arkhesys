using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System.Reflection;
using NHibernate.Transform;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaMedicaoRepositorio : RepositorioAbstrato<Ofd_PropostaMedicao>, IRepositorioOfd_PropostaMedicao
    {
        protected override string HqlLoadById()
        {
            return @"from Ofd_PropostaMedicao n 
                    join fetch n.Proposta p 
                    join fetch n.Maquina m
                    join fetch n.Operador1 o1
                    left join fetch n.Operador2 o2
                    left join fetch n.Perfuracoes where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_PropostaMedicao n";
        }

        public int[] GetMedicoesNaoFaturadasByProposta(Ofd_Proposta ofd_Proposta)
        {
            //and pm.Nota is null
            using (var session = GetSession())
            {
                var hql = "select pm.Medicao from Ofd_PropostaMedicao pm where pm.Proposta.ID = :idProposta";
                return session.CreateQuery(hql).SetInt32("idProposta", ofd_Proposta.ID).List<int>().ToArray();
            }
        }

        public Ofd_PropostaMedicao GetByPropostaENumero(Ofd_Proposta proposta, int numero)
        {
            using (var session = GetSession())
            {
                var hql = @"from Ofd_PropostaMedicao n 
                    join fetch n.Proposta p 
                    join fetch n.Maquina m
                    join fetch n.Operador1 o1
                    left join fetch n.Operador2 o2
                    left join fetch n.Perfuracoes 
                    where n.Proposta.ID = :idProposta and n.Medicao = :numero";

                var result = session.CreateQuery(hql)
                             .SetInt32("idProposta", proposta.ID)
                             .SetInt32("numero", numero)
                             .List<Ofd_PropostaMedicao>().FirstOrDefault();
                return result;

            }
        }

        public IList<Ofd_PropostaMedicao> GetLstByPropostaENumero(Ofd_Proposta proposta, int numero)
        {
            using (var session = GetSession())
            {
                var hql = @"from Ofd_PropostaMedicao n 
                    join fetch n.Proposta p 
                    join fetch n.Maquina m
                    join fetch n.Operador1 o1
                    left join fetch n.Operador2 o2
                    
                    where n.Proposta.ID = :idProposta and n.Medicao = :numero";

                var result = session.CreateQuery(hql)
                             .SetInt32("idProposta", proposta.ID)
                             .SetInt32("numero", numero)
                             .List<Ofd_PropostaMedicao>();
                return result;

            }
        }

        public int[] GetMedicoesFaturadasByProposta(Ofd_Proposta ofd_Proposta)
        {
            using (var session = GetSession())
            {
                var hql = "select pm.Medicao from Ofd_PropostaMedicao pm " +
                          "where pm.Proposta.ID = :idProposta " +
                          "group by pm.Medicao";
                var retorno = session.CreateQuery(hql).SetInt32("idProposta", ofd_Proposta.ID).List<int>().ToArray();
                return retorno;
            }
        }

        public IList<Ofd_PropostaMedicao> GetByObra(Ofd_Obra obra)
        {
            using (var session = GetSession())
            {
                var hql = @"from Ofd_PropostaMedicao n 
                    join fetch n.Proposta p
                    join fetch p.Obra o 
                    join fetch n.Maquina m
                    join fetch n.Operador1 o1
                    left join fetch n.Operador2 o2
                    where o.ID = :idObra";
                return session.CreateQuery(hql)
                              .SetInt32("idObra", obra.ID)
                              .List<Ofd_PropostaMedicao>();
            }
        }

        public IList<Ofd_PropostaMedicao> GetByRelatorioMes(DateTime mes)
        {
            using (var session = GetSession())
            {
                var hql = @"from Ofd_PropostaMedicao n 
                    join fetch n.Proposta p
                    join fetch p.Obra o 
                    join fetch n.Maquina m
                    join fetch n.Operador1 o1
                    left join fetch n.Operador2 o2
                    where n.DataRelatorio >= :inicioMes and n.DataRelatorio < :fimMes";
                return session.CreateQuery(hql)
                              .SetDateTime("inicioMes", mes)
                              .SetDateTime("fimMes", mes.AddMonths(1))
                              .List<Ofd_PropostaMedicao>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
