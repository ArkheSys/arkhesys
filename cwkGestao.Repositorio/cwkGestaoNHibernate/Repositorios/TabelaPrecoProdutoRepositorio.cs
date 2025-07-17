using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TabelaPrecoProdutoRepositorio : RepositorioAbstrato<TabelaPrecoProduto>, IRepositorioTabelaPrecoProduto
    {
        protected override string HqlLoadById()
        {
            return "from TabelaPrecoProduto where ID = :id";
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public TabelaPrecoProduto GetByTabelaPrecoProduto(int idtabelapreco, int idproduto)
        {
            using (var session = GetSession())
            {
                string hql = @" from TabelaPrecoProduto t
                              where t.TabelaPreco.ID = :idtabelapreco
                              and t.Produto.ID = :idproduto";

                var result =
                    session.CreateQuery(hql).SetInt32("idtabelapreco", idtabelapreco).SetInt32("idproduto", idproduto).
                        UniqueResult();

                if (result != null)
                {
                    return result as TabelaPrecoProduto;
                }
                else
                {
                    return new TabelaPrecoProduto();
                }
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
