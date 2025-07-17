using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PortadorRepositorio : RepositorioAbstrato<Portador>, IRepositorioPortador
    {
        protected override string HqlLoadById()
        {
            return "from Portador where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Portador";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
