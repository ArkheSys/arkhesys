using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CteSeguradoraRepositorio : RepositorioAbstrato<CteSeguradora>, IRepositorioCteSeguradora
    {
        protected override string HqlLoadById()
        {
            return "from CteSeguradora n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CteSeguradora n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
