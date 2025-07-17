using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SHLConfiguracaoRepositorio : RepositorioAbstrato<SHLConfiguracao>, IRepositorioSHLConfiguracao
    {
        protected override string HqlLoadById()
        {
            return "from SHLConfiguracao n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from SHLConfiguracao n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
