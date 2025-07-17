using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_MaquinasRepositorio : RepositorioAbstrato<Ofd_Maquinas>, IRepositorioOfd_Maquinas
    {

        protected override string HqlLoadById()
        {
            return "from Ofd_Maquinas m where m.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_Maquinas m";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
