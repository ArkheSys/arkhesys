using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ServicoOSRepositorio : RepositorioAbstrato<ServicoOS>, IRepositorioServicoOS
    {
        protected override string HqlLoadById()
        {
            return "from ServicoOS where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ServicoOS";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
