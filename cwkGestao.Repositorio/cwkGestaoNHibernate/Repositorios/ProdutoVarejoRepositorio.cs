using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoVarejoRepositorio : RepositorioAbstrato<ProdutoVarejo>, IRepositorioProdutoVarejo
    {
        public IList<ProdutoVarejo> GetPorProduto(int IDProduto)
        {
            using (var session = GetSession())
            {
                string hql = "from ProdutoVarejo c where c.IDProduto = :IDProduto";
                var query = session.CreateQuery(hql).SetInt32("IDProduto", IDProduto);
                return query.List<ProdutoVarejo>();
            }
        }

        protected override string hqlGetAll()
        {
            return "from ProdutoVarejo c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException("Não está implementado");
        }

        protected override string HqlLoadById()
        {
            return "from ProdutoVarejo c where c.ID = :id";
        }
    }
}
