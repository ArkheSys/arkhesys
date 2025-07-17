using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ServicoResponsavelRepositorio : RepositorioAbstrato<ServicoResponsavel>, IRepositorioServicoResponsavel
    {
        protected override string HqlLoadById()
        {
            return "from ServicoResponsavel t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ServicoResponsavel t";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
