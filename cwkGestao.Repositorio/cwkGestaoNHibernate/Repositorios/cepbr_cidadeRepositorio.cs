using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class cepbr_cidadeRepositorio : RepositorioAbstrato<cepbr_cidade>, IRepositoriocepbr_cidade
    {
        protected override string HqlLoadById()
        {
            return "from cepbr_cidade n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from cepbr_cidade n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
