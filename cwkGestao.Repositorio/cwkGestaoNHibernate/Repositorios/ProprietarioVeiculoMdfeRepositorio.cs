using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProprietarioVeiculoMdfeRepositorio : RepositorioAbstrato<ProprietarioVeiculoMdfe>, IRepositorioProprietarioVeiculoMdfe
    {
        protected override string HqlLoadById()
        {
            return "from ProprietarioVeiculoMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ProprietarioVeiculoMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
