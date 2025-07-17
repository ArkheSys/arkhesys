using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class LogExportacaoNotaRepositorio : RepositorioAbstrato<LogExportacaoNota>, IRepositorioLogExportacaoNota
    {
        protected override string hqlGetAll()
        {
            return "from LogExportacaoNota n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from LogExportacaoNota c where c.ID = :id";
        }
    }
}
