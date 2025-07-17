using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PromocaoRepositorio : RepositorioAbstrato<Promocao>, IRepositorioPromocao
    {
        protected override string HqlLoadById()
        {
            return "from Promocao n left join fetch n.Itens where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Promocao n";
        }

        public override Promocao LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(HqlLoadById()).SetInt32("id", id).UniqueResult<Promocao>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}