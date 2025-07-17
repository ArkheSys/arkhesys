using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FormatoPacoteTransporteRepositorio : RepositorioAbstrato<FormatoPacoteTransporte>, IRepositorioFormatoPacoteTransporte
    {
        protected override string HqlLoadById()
        {
            return "from FormatoPacoteTransporte n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FormatoPacoteTransporte n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
