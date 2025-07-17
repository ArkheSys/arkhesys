using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CteIdentificacaoRepositorio : RepositorioAbstrato<CteIdentificacao>, IRepositorioCteIdentificacao
    {
        protected override string HqlLoadById()
        {
            return "from CteIdentificacao n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CteIdentificacao n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}