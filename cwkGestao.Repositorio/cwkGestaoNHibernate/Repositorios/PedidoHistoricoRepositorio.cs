using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoHistoricoRepositorio : RepositorioAbstrato<PedidoHistorico>, IRepositorioPedidoHistorico
    {
        protected override string HqlLoadById()
        {
            return " from PedidoHistorico u";
        }

        protected override string hqlGetAll()
        {
            return " select distinct ph from PedidoHistorico as ph";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
