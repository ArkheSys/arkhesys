using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class VeiculoRepositorio : RepositorioAbstrato<Veiculo>, IRepositorioVeiculo
    {
        protected override string HqlLoadById()
        {
            return "from Veiculo where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Veiculo";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
