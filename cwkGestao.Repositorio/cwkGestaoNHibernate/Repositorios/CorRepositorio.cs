using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CorRepositorio : RepositorioAbstrato<Cor>, IRepositorioCor
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
