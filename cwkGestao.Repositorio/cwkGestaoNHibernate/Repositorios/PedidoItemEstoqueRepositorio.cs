using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoItemEstoqueRepositorio : RepositorioAbstrato<PedidoItemEstoque>, IRepositorioPedidoItemEstoque
    {
        protected override string HqlLoadById()
        {
            return "from PedidoItemEstoque n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PedidoItemEstoque n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
