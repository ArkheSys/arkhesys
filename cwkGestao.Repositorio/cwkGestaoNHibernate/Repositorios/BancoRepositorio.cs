using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class BancoRepositorio : RepositorioAbstrato<Banco>, IRepositorioBanco
    {
        protected override string HqlLoadById()
        {
            return "from Banco where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Banco";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
