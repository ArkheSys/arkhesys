using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_MedicaoServicosRepositorio : RepositorioAbstrato<Ofd_MedicaoServicos>, IRepositorioOfd_MedicaoServicos
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_MedicaoServicos n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_MedicaoServicos n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
