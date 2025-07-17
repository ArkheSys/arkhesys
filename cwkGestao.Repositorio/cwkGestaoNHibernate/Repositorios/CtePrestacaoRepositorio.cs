using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CtePrestacaoRepositorio : RepositorioAbstrato<CtePrestacao>, IRepositorioCtePrestacao
    {
        protected override string HqlLoadById()
        {
            return "from CtePrestacao c where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CtePrestacao c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
