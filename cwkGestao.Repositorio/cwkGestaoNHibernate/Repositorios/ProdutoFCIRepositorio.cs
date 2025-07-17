using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System.Reflection;
using NHibernate.Transform;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoFCIRepositorio : RepositorioAbstrato<ProdutoFCI>, IRepositorioProdutoFCI
    {
        protected override string HqlLoadById()
        {
            return "from ProdutoFCI n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ProdutoFCI n";
        }

        public IList<pxyProdutoFCI> GetAllPXYProdutoFCIPorIDProduto(int IDProduto)
        {
            using (var session = GetSession())
            {
                string hql = @" select pFCI.ID, pFCI.FCI, n.Dt, pFCI.IDProduto, pFCI.bAtivo from NotaItem ni
                                inner join ProdutoFCI pFCI on pFCI.ID = ni.IDProdutoFCI
                                inner join Nota n on n.ID = ni.IDNota 
                                where ni.IDProduto = :IDProduto and n.Ent_Sai = 1";
                IQuery query = session.CreateSQLQuery(hql).SetInt32("IDProduto", IDProduto);

                ConstructorInfo construtor = typeof(pxyProdutoFCI).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(DateTime),
                typeof(int),
                typeof(bool),
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyProdutoFCI>();
                return retorno;
            }
        }

        public IList<ProdutoFCI> GetFCIByIdProduto(int IdProduto)
        {
            using (var session = GetSession())
            {
                string hql = @" from ProdutoFCI pFCI 
                                where pFCI.Produto.ID = :IdProduto and pFCI.bAtivo = 1";
                return session.CreateQuery(hql).SetInt32("IdProduto", IdProduto).List<ProdutoFCI>();
                
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
