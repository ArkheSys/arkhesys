using System;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CteDocumentosInformadosRepositorio : RepositorioAbstrato<CteDocumentosInformados>, IRepositorioCteDocumentosInformados
    {
        protected override string HqlLoadById()
        {
            return "from CteDocumentosInformados ac where ac.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CteDocumentosInformados ";
        }
        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
