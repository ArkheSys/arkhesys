using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CotacaoRepositorio : RepositorioAbstrato<Cotacao>, IRepositorioCotacao
    {
        protected override string HqlLoadById()
        {
            return " from Cotacao c left join fetch c.CotacaoProdutos cp left join fetch cp.Produto p left join fetch cp.CotacaoProdutoPedidoItems cppi left join fetch cppi.PedidoItem pi left join fetch pi.Produto left join fetch pi.Pedido where c.ID = :id ";
        }
        
        protected override string hqlGetAll()
        {
            return "from Cotacao c";
        }

        public IList<Cotacao> GetCotacaoLista()
        {
            using (var session = GetSession())
            {
                string hql = " select c from Cotacao c " +
                             " left join fetch c.CotacaoProdutos cp " +
                             " left join fetch c.Filial fi " +
                             " where c.TipoCotacao = :tipoCotacao ";

                var result = session.CreateQuery(hql).SetEnum("tipoCotacao", TipoCotacao.Lista).List<Cotacao>();
                /*if (result.Count > 0)
                    return result[0];
                return null;*/
                return result;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
