using System;
using System.Collections.Generic;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;

using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class InventarioProdutoRepositorio : RepositorioAbstrato<InventarioProduto>, IRepositorioInventarioProduto
    {
        protected override string HqlLoadById()
        {
            return "from InventarioProduto n left join fetch n.Produto where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from InventarioProduto n left join fetch n.Produto";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<pxInventarioProduto> GetProdutosPorInventario(int IDInventario)
        {
            using (var session = GetSession())
            {
                var query = session.CreateSQLQuery(@"SELECT Produto.Nome as Produto,
                                                               InventarioProduto.Quantidade,
                                                        	   InventarioProduto.Valor,
                                                        	   InventarioProduto.Total
                                                          FROM InventarioProduto
                                                             INNER JOIN Produto on (InventarioProduto.IDProduto = Produto.ID)
                                                        WHERE InventarioProduto.IDInventario = :IDInventario
                                                     ORDER BY Produto.Nome").SetInt32("IDInventario", IDInventario);

                var construtor = typeof(pxInventarioProduto).GetConstructor(new Type[] { typeof(string), typeof(decimal), typeof(decimal), typeof(decimal) });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxInventarioProduto>();
            }
        }
    }
}
