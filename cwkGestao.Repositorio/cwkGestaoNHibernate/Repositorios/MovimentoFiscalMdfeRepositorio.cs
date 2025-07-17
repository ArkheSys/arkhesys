using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MovimentoFiscalMdfeRepositorio : RepositorioAbstrato<MovimentoFiscalMdfe>, IRepositorioMovimentoFiscalMdfe
    {
        protected override string HqlLoadById()
        {
            return "from MovimentoFiscalMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from MovimentoFiscalMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
