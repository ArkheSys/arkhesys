using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoSKURepositorio : RepositorioAbstrato<ProdutoSKU>, IRepositorioProdutoSKU
    {
        protected override string HqlLoadById()
        {
            return "from ProdutoSKU n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"select new ProdutoSKU(produtoSKU.ID, produto, produtoSKU.SKU)                      
                     from ProdutoSKU produtoSKU
                     join produtoSKU.Produto produto ";
        }

        public IList<ProdutoSKU> GetSKUPorProduto(int id)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + "where produto.ID = :id";

                return session.CreateQuery(hql).SetInt32("id", id).List<ProdutoSKU>();
            }
        }

        public IList<ProdutoSKU> GetProdutoPorSKU(string sku)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + "where produtoSKU.SKU = :sku";

                return session.CreateQuery(hql).SetString("sku", sku).List<ProdutoSKU>();
            }
        }

        public Produto FindProdutoPorSKU(string sku)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "from ProdutoSKU sku where sku.SKU = :sku";
                    ProdutoSKU pSku = session.CreateQuery(hql).SetString("sku", sku).UniqueResult<ProdutoSKU>();
                    string hql2 = "from Produto p where p.ID = :id";
                    Produto p = session.CreateQuery(hql2).SetInt32("id", pSku.Produto.ID).UniqueResult<Produto>();
                    return p;
                }
                catch (Exception z)
                {
                    
                    throw z;
                }

            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
