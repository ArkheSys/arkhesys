using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoItemRepositorio : RepositorioAbstrato<PedidoItem>, IRepositorioPedidoItem
    {
        protected override string HqlLoadById()
        {
            return "from PedidoItem pi" +
                  " left join fetch pi.Pedido " +
                  " where pi.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PedidoItem pi" +
                  " left join fetch pi.Pedido";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<pxyEntregaPedidoItem> GetAllPorIDPedido(int IDPedido)
        {
            //IList<Pedido> result = new List<Pedido>();
            using (var session = GetSession())
            {
                string sql = @" select 
	                                pit.ID, 
	                                pit.ProdutoNome, 
	                                pit.Unidade, 
	                                pit.Quantidade,
	                                ISNULL(SUM(EI.QuantidadeEntrega),0) QuantidadeEntrega,
	                                PIT.Quantidade - ISNULL(SUM(EI.QuantidadeEntrega),0) Saldo
                                from Pedido p
                                join PedidoItem pit on pit.IDPedido = p.ID 
                                left join EntregaItem ei on ei.IDPedidoItem = pit.ID
                                where p.ID = :IDPedido
                                group by pit.ID, pit.ProdutoNome, pit.Unidade, pit.Quantidade
                                ";
                          
                var query = session.CreateSQLQuery(sql).SetInt32("IDPedido", IDPedido);

                ConstructorInfo construtor = typeof(pxyEntregaPedidoItem).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(decimal),
                typeof(decimal?),
                typeof(decimal),
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyEntregaPedidoItem>();

                return retorno;
            }
        }
    }
}
