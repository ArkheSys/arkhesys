using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TabelaCOFINSRepositorio : RepositorioAbstrato<TabelaCOFINS>, IRepositorioTabelaCOFINS
    {
        protected override string hqlGetAll()
        {
            return "from TabelaCOFINS i";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from TabelaCOFINS i where i.ID = :id";
        }
    }
}
