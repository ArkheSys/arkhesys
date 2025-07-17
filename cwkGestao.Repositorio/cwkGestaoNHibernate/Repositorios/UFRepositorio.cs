using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class UFRepositorio : RepositorioAbstrato<UF>, IRepositorioUF
    {

        protected override string HqlLoadById()
        {
            return " from UF u" +
                   " left join fetch u.Pais " +
                   " where u.ID = :id";

        }

        protected override string hqlGetAll()
        {
            return " select distinct uf from UF as uf" +
                   " left join fetch uf.Pais ";
                   
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
