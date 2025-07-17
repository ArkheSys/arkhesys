using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SeguradoraRepositorio : RepositorioAbstrato<Seguradora>, IRepositorioSeguradora
    {
        protected override string HqlLoadById()
        {
            return "from Seguradora where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Seguradora";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();
    }
}
