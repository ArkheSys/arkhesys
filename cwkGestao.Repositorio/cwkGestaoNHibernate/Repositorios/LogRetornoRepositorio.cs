using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class LogRetornoRepositorio : RepositorioAbstrato<LogRetorno>, IRepositorioLogRetorno
    {
        protected override string HqlLoadById()
        {
            return "from LogRetorno l where l.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from LogRetorno l";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
