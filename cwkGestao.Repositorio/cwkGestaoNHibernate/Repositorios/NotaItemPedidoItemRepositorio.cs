using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NotaItemPedidoItemRepositorio : RepositorioAbstrato<NotaItemPedidoItem>, IRepositorioNotaItemPedidoItem
    {
        protected override string HqlLoadById()
        {
            return "from NotaItemPedidoItem n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from NotaItemPedidoItem n";
        }

        public decimal GetQtdRetirada(int IDPedidoItem, int IDNotaItem)
        {
            try
            {
                using (var session = GetSession())
                {
                    string sql = @"select sum(nipi.QtdRetiradaPedido) from NotaItemPedidoItem nipi
                    where nipi.IDPedidoItem = :IDPedidoItem
                    and nipi.IDNotaItem != :IDNotaItem";
                    decimal soma = session.CreateSQLQuery(sql).SetInt32("IDNotaItem", IDNotaItem).SetInt32("IDPedidoItem", IDPedidoItem).UniqueResult<decimal>();
                    return soma;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IList<NotaItemPedidoItem> GetAllNiPiPorIDPedido(int idPedido)
        {
            IList<NotaItemPedidoItem> result = new List<NotaItemPedidoItem>();
            try
            {
                using (var session = GetSession())
                {
                    string hql = "from NotaItemPedidoItem nipi join fetch nipi.PedidoItem pei join fetch pei.Pedido p where p.ID = :idPedido and nipi.NotaItem is not null";
                    result = session.CreateQuery(hql).SetInt32("idPedido", idPedido).List<NotaItemPedidoItem>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }

        public IList<NotaItemPedidoItem> GetAllNiPiPorIDNota(int idNota)
        {
            IList<NotaItemPedidoItem> result = new List<NotaItemPedidoItem>();
            try
            {
                using (var session = GetSession())
                {
                    string hql = @" from NotaItemPedidoItem nipi
                                    join fetch nipi.NotaItem ni 
                                    join fetch ni.Nota n 
                                    where n.ID = :idNota";
                    result = session.CreateQuery(hql).SetInt32("idNota", idNota).List<NotaItemPedidoItem>();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return result;
        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
