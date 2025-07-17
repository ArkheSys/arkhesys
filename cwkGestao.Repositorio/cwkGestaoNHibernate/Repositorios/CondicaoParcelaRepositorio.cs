using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CondicaoParcelaRepositorio : RepositorioAbstrato<CondicaoParcela>, IRepositorioCondicaoParcela
    {
        protected override string HqlLoadById()
        {
            return " from CondicaoParcela cp where cp.ID = :id ";
        }

        protected override string hqlGetAll()
        {
            return " from CondicaoParcela ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
