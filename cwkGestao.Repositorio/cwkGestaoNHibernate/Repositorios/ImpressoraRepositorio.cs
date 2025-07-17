using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ImpressoraRepositorio : RepositorioAbstrato<Impressora>, IRepositorioImpressora
    {
        protected override string HqlLoadById()
        {
            return "from Impressora i where i.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Impressora i";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
