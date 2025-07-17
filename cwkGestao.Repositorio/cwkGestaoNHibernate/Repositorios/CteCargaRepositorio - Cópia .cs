using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CteCargaRepositorio : RepositorioAbstrato<CteCarga>, IRepositorioCteCarga
    {
        protected override string HqlLoadById()
        {
            return "from CteCarga c"
                    + " where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CteCarga c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
