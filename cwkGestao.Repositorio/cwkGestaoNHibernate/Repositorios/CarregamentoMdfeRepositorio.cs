using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CarregamentoMdfeRepositorio : RepositorioAbstrato<CarregamentoMdfe>, IRepositorioCarregamentoMdfe
    {
        protected override string HqlLoadById()
        {
            return "from CarregamentoMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CarregamentoMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
