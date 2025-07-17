using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ICMS_InterestadualRepositorio : RepositorioAbstrato<ICMS_Interestadual>, IRepositorioICMS_Interestadual
    {
        protected override string HqlLoadById()
        {
            return "from ICMS_Interestadual icms left join fetch icms.UFOrigem left join fetch icms.UFDestino where icms.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ICMS_Interestadual icms left join fetch icms.UFOrigem left join fetch icms.UFDestino ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
