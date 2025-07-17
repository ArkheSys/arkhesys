using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ResponsavelSeguroCargaMdfeRepositorio : RepositorioAbstrato<ResponsavelSeguroCargaMdfe>, IRepositorioResponsavelSeguroCargaMdfe
    {
        protected override string HqlLoadById()
        {
            return "from ResponsavelSeguroCargaMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ResponsavelSeguroCargaMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
