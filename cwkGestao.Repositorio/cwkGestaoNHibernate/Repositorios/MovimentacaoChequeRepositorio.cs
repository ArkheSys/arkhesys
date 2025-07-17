using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MovimentacaoChequeRepositorio : RepositorioAbstrato<MovimentacaoCheque>, IRepositorioMovimentacaoCheque
    {
        protected override string HqlLoadById()
        {
            return "from MovimentacaoCheque c where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from MovimentacaoCheque c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
