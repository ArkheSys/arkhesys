using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class StatusEntregaRepositorio : RepositorioAbstrato<StatusEntrega>, IRepositorioStatusEntrega
    {
        protected override string HqlLoadById()
        {
            return "from StatusEntrega n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from StatusEntrega n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
