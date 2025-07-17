using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoMaquinaRepositorio : RepositorioAbstrato<PedidoMaquina>, IRepositorioPedidoMaquina
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from PedidoMaquina p where p.ID = :id";
        }

        public override PedidoMaquina LoadObjectById(int id)
        {
            return GetSession().Get<PedidoMaquina>(id);
        }

        protected override string hqlGetAll()
        {
            return "select distinct p from PedidoMaquina p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
