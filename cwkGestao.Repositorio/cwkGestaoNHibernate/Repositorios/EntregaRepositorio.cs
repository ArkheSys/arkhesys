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
    partial class EntregaRepositorio : RepositorioAbstrato<Entrega>, IRepositorioEntrega
    {
        protected override string HqlLoadById()
        {
            return "from Entrega n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Entrega n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<pxyEntregas> GetPorAllPorIDPedido(int IDPedido)
        {
            using (var session = GetSession())
            {
                string sql = @"select p.ID IDPedido,
	                                  e.Sequencia, 
	                                  e.ID IDEntrega, 
	                                  e.Data, 
	                                  e.NumeroEntrega, 
	                                  p.Codigo CodigoPedido, 
	                                  e.QuantidadeEntrega QuantidadeEntregue,
		                              (select sum(Quantidade) from PedidoItem pit where pit.idpedido = p.id) QuantidadeEntregar,
		                              Null QuantidadeRestante,
	                                  e.Responsavel,
	                                  e.Solicitante
                                from Entrega e
                                join Pedido p on p.ID = e.IDPedido
                                where p.ID = :IDPedido
                            order by e.Sequencia";

                var query = session.CreateSQLQuery(sql).SetInt32("IDPedido", IDPedido);

                ConstructorInfo construtor = typeof(pxyEntregas).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(DateTime),
                typeof(string),
                typeof(int),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal?),
                typeof(string),
                typeof(string)
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyEntregas>();

                return retorno;
            }
        }
    }
}
