using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class UnidadeRepositorio : RepositorioAbstrato<Unidade>, IRepositorioUnidade
    {
        protected override string HqlLoadById()
        {
            return "from Unidade u where u.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Unidade u";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
