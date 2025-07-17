using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ServicoEnvioCorreioRepositorio : RepositorioAbstrato<ServicoEnvioCorreio>, IRepositorioServicoEnvioCorreio
    {
        protected override string HqlLoadById()
        {
            return "from ServicoEnvioCorreio n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ServicoEnvioCorreio n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
