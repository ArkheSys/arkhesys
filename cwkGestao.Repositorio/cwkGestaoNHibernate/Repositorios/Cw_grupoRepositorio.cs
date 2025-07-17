using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Cw_grupoRepositorio : RepositorioAbstrato<Cw_grupo>, IRepositorioCw_grupo
    {
        protected override string HqlLoadById()
        {
            return "from Cw_grupo n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Cw_grupo n";
        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
