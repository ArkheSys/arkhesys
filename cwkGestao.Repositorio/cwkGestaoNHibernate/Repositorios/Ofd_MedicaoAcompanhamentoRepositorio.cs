using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_MedicaoRepositorioAcompanhamento : RepositorioAbstrato<Ofd_MedicaoAcompanhamento>, IRepositorioOfd_MedicaoAcompanhamento
    {

        public IList<Ofd_MedicaoAcompanhamento> GetByMedicao(int IDMedicao)
        {
            using (var session = GetSession())
            {
                string hql = @"from Ofd_MedicaoAcompanhamento m 
                             inner join fetch m.Ofd_Medicao mof
                             where mof.ID = " + IDMedicao;
                var query = session.CreateQuery(hql);
                var Lista = query.List<Ofd_MedicaoAcompanhamento>();
                return Lista;
            }
        }

        protected override string HqlLoadById()
        {
            return @"from Ofd_MedicaoAcompanhamento where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"from Ofd_MedicaoAcompanhamento";
        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
