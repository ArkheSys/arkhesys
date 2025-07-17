using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PCP_LoteRepositorio : RepositorioAbstrato<PCP_Lote>, IRepositorioPCP_Lote
    {
        protected override string HqlLoadById()
        {
            return "from PCP_Lote n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PCP_Lote n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
