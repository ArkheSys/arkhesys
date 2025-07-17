using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FeriadoRepositorio : RepositorioAbstrato<Feriado>, IRepositorioFeriado
    {
        protected override string HqlLoadById()
        {
            return "from Feriado t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Feriado t";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
