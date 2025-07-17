using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class VeiculoTracaoMdfeRepositorio : RepositorioAbstrato<VeiculoTracaoMdfe>, IRepositorioVeiculoTracaoMdfe
    {
        protected override string HqlLoadById()
        {
            return "from VeiculoTracaoMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from VeiculoTracaoMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
