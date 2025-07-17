using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PercursoMdfeRepositorio : RepositorioAbstrato<PercursoMdfe>, IRepositorioPercursoMdfe
    {
        protected override string HqlLoadById()
        {
            return "from PercursoMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PercursoMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
