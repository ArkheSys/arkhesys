using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_OcorrenciasRepositorio : RepositorioAbstrato<Ofd_Ocorrencias>, IRepositorioOfd_Ocorrencias
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_Ocorrencias n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_Ocorrencias n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
