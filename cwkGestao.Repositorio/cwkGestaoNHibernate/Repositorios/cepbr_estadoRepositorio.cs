using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class cepbr_estadoRepositorio : RepositorioAbstrato<cepbr_estado>, IRepositoriocepbr_estado
    {
        protected override string HqlLoadById()
        {
            return "from cepbr_estado n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from cepbr_estado n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
