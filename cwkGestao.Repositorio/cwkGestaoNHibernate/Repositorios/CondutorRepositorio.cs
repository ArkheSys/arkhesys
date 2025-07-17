using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CondutorRepositorio : RepositorioAbstrato<Condutor>, IRepositorioCondutor
    {
        protected override string HqlLoadById()
        {
            return "from Condutor where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Condutor";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
