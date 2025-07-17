using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PromocaoProdutoRepositorio : RepositorioAbstrato<PromocaoProduto>, IRepositorioPromocaoProduto
    {
        protected override string HqlLoadById()
        {
            return "from PromocaoProduto n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PromocaoProduto n";
        }

        public override PromocaoProduto LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(HqlLoadById()).SetInt32("id", id).UniqueResult<PromocaoProduto>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}