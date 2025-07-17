using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SincronismoPendenteSisECommerceRepositorio : RepositorioAbstrato<SincronismoPendenteSisECommerce>, IRepositorioSincronismoPendenteSisECommerce
    {
        protected override string HqlLoadById()
        {
            return "from SincronismoPendenteSisECommerce n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from SincronismoPendenteSisECommerce n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
