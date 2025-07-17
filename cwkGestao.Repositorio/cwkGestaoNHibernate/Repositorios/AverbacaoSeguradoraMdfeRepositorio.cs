using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class AverbacaoSeguradoraMdfeRepositorio : RepositorioAbstrato<AverbacaoSeguradoraMdfe>, IRepositorioAverbacaoSeguradoraMdfe
    {
        protected override string HqlLoadById()
        {
            return "from AverbacaoSeguradoraMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from AverbacaoSeguradoraMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
