using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FichaTecnicaPCPRegraRepositorio : RepositorioAbstrato<FichaTecnicaPCPRegra>, IRepositorioFichaTecnicaPCPRegra
    {
        protected override string HqlLoadById()
        {
            return "from FichaTecnicaPCPRegra n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FichaTecnicaPCPRegra n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
