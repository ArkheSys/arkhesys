using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class RepositorioCEST_Segmento : RepositorioAbstrato<CEST_Segmento>, IRepositorioCEST_Segmento
    {
        protected override string HqlLoadById()
        {
            return "select n from CEST_Segmento n "
                   + " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CEST_Segmento n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
        }
    }
}
