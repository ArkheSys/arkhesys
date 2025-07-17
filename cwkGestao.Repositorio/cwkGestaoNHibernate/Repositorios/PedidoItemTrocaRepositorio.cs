using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoItemTrocaRepositorio : RepositorioAbstrato<PedidoItemTroca>, IRepositorioPedidoItemTroca
    {
        protected override string HqlLoadById()
        {
            return "from PedidoItemTroca n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PedidoItemTroca n";
        }

        public IList<PedidoItemTroca> LoadItemTrocaByIdPedidoItem(int idPedidoItem)
        {
            using (var session = GetSession())
            {
                string hql = " from PedidoItemTroca p" +
                             " where p.idPedidoItem = " + idPedidoItem;

                var query = session.CreateQuery(hql).List<PedidoItemTroca>();

                return query;
            }
        }

        public IList<pxyPedidoItemTroca> GetItensDoPedidoECodProduto(int IDPedido)
        {
            using (var session = GetSession())
            {
                string hql = @" SELECT p.ID, pr.Codigo, p.ProdutoNome, p.Valor, p.Quantidade FROM PedidoItem p
                               inner join Produto pr on p.IDProduto = pr.ID
                               where p.IDPedido = :IDPedido";
                IQuery query = session.CreateSQLQuery(hql).SetInt32("IDPedido", IDPedido);

                ConstructorInfo construtor = typeof(pxyPedidoItemTroca).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyPedidoItemTroca>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
