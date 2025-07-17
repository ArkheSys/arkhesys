using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NCMItemRepositorio : RepositorioAbstrato<NCMItem>, IRepositorioNCMItem
    {
        protected override string HqlLoadById()
        {
            return "select n from NCMItem n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from NCMItem n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
