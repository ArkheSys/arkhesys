using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CtePrestacaoComplementoRepositorio : RepositorioAbstrato<CtePrestacaoComplemento>, IRepositorioCtePrestacaoComplemento
    {
        protected override string HqlLoadById()
        {
            return "from CtePrestacaoComplemento c where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CtePrestacaoComplemento c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
