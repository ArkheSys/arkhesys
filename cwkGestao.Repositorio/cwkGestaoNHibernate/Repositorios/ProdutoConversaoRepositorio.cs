using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoConversaoRepositorio : RepositorioAbstrato<ProdutoConversao>, IRepositorioProdutoConversao
    {
        protected override string HqlLoadById()
        {
            return "from ProdutoConversao n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ProdutoConversao n";
        }


        public IList<ProdutoConversao> GetByIdProduto(int IdProduto)
        {
            using (var session = GetSession())
            {
                string hql = "from ProdutoConversao p where p.Produto = :IdProduto";

                return session.CreateQuery(hql).SetInt32("IdProduto",IdProduto).List<ProdutoConversao>();
            }
        }

        public IList<ProdutoConversao> GetByIdConversaoUnidade(int IdConversaoUnidade)
        {
            using (var session = GetSession())
            {
                string hql = "from ProdutoConversao p where p.ConversaoUnidade = :IdConversaoUnidade";

                return session.CreateQuery(hql).SetInt32("IdConversaoUnidade", IdConversaoUnidade).List<ProdutoConversao>();
            }
        }

        public ProdutoConversao GetByIdConversaoUnidadeAndIdProduto(int IdConversaoUnidade, int IdProduto)
        {
            using (var session = GetSession())
            {
                string hql = "from ProdutoConversao p where p.ConversaoUnidade = :IdConversaoUnidade and p.Produto = :IdProduto";

                return session.CreateQuery(hql).SetInt32("IdConversaoUnidade", IdConversaoUnidade).SetInt32("IdProduto", IdProduto).UniqueResult<ProdutoConversao>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
