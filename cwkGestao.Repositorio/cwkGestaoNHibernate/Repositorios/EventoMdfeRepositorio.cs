using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class EventoMdfeRepositorio : RepositorioAbstrato<EventoMdfe>, IRepositorioEventoMdfe
    {
        protected override string HqlLoadById()
        {
            return "from EventoMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from EventoMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
