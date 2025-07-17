using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CwkPedidoEmpresaRepositorio : RepositorioAbstrato<CwkPedidoEmpresa>, IRepositorioCwkPedidoEmpresa
    {
        protected override string HqlLoadById()
        {
            return "from CwkPedidoEmpresa where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CwkPedidoEmpresa";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public int UltimoNumeroSerie(int pIdSistema, int pIdPedido)
        {
            using (var session = GetSession())
            {
                string hql = "select max(cpe.NumeroSerie) from CwkPedidoEmpresa cpe where cpe.Pedido.Sistema.ID = :idsistema and cpe.Pedido.id != :pIdPedido";
                var result = session.CreateQuery(hql).SetInt32("idsistema", pIdSistema).SetInt32("pIdPedido", pIdPedido).UniqueResult<int?>();
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    return 0;
            }
        }
    }
}
