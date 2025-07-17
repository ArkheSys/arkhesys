using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_GrupoClienteRepositorio : RepositorioAbstrato<Tel_GrupoCliente>, IRepositorioTel_GrupoCliente
    {
        protected override string HqlLoadById()
        {
            return "from Tel_GrupoCliente g left join fetch g.Precos where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_GrupoCliente g";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
