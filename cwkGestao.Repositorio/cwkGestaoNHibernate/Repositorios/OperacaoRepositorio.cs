using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OperacaoRepositorio : RepositorioAbstrato<Operacao>, IRepositorioOperacao
    {
        protected override string HqlLoadById()
        {
            return "from Operacao op where op.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Operacao";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
