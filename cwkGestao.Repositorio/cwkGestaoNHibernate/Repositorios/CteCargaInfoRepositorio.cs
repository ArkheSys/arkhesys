using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CteCargaInfoRepositorio : RepositorioAbstrato<CteCargaInfo>, IRepositorioCteCargaInfo
    {
        protected override string HqlLoadById()
        {
            return "from CteCargaInfo n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CteCargaInfo n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
