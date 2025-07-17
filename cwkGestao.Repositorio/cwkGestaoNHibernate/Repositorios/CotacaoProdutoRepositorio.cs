using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CotacaoProdutoRepositorio : RepositorioAbstrato<CotacaoProduto>, IRepositorioCotacaoProduto
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public IList<cwkGestao.Modelo.CotacaoProduto> GetByCotacao(cwkGestao.Modelo.Cotacao cotacao)
        {
            using (var session = GetSession())
            {
                string hql = "from CotacaoProduto cp where cp.Cotacao.ID = " + cotacao.ID +
                             " left join fetch cp.CotacaoProdutoPedidoItems " +
                             " left join fetch cp.Produto " +
                             " left join fetch cp.Cotacao ";
                return session.CreateQuery(hql).List<CotacaoProduto>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
