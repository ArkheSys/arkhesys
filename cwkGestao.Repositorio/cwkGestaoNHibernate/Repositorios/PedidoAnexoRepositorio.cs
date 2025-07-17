using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoAnexoRepositorio : RepositorioAbstrato<PedidoAnexo>, IRepositorioPedidoAnexo
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from PedidoAnexo p where p.ID = :id";
        }

        public override PedidoAnexo LoadObjectById(int id)
        {
            return GetSession().Get<PedidoAnexo>(id);
        }

        protected override string hqlGetAll()
        {
            return "select distinct p from PedidoAnexo p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
