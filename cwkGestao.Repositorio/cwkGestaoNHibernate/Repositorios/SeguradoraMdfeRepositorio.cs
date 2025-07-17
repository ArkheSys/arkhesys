using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SeguradoraMdfeRepositorio : RepositorioAbstrato<SeguradoraMdfe>, IRepositorioSeguradoraMdfe
    {
        protected override string HqlLoadById()
        {
            return "from SeguradoraMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from SeguradoraMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
