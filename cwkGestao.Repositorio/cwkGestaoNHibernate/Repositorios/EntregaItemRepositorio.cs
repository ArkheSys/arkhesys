using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class EntregaItemRepositorio : RepositorioAbstrato<EntregaItem>, IRepositorioEntregaItem
    {
        protected override string HqlLoadById()
        {
            return "from EntregaItem n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from EntregaItem n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
