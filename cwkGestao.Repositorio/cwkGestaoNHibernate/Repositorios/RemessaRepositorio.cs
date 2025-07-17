using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class RemessaRepositorio : RepositorioAbstrato<Remessa>, IRepositorioRemessa
    {
        protected override string HqlLoadById()
        {
            return "from Remessa r where r.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Remessa";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
