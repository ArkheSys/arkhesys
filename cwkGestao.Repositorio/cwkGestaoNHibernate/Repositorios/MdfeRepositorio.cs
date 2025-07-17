using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MdfeRepositorio : RepositorioAbstrato<Mdfe>, IRepositorioMdfe
    {
        protected override string HqlLoadById()
        {
            return "from Mdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Mdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
