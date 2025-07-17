using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ContratanteMdfeRepositorio : RepositorioAbstrato<ContratanteMdfe>, IRepositorioContratanteMdfe
    {
        protected override string HqlLoadById()
        {
            return "from ContratanteMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ContratanteMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
