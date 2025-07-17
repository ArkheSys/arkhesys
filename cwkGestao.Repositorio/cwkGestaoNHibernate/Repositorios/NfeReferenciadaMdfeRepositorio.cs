using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NfeReferenciadaMdfeRepositorio : RepositorioAbstrato<NfeReferenciadaMdfe>, IRepositorioNfeReferenciadaMdfe
    {
        protected override string HqlLoadById()
        {
            return "from NfeReferenciadaMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from NfeReferenciadaMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
