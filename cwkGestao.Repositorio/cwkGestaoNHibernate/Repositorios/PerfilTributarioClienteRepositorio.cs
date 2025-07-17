using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PerfilTributarioClienteRepositorio : RepositorioAbstrato<PerfilTributarioCliente>, IRepositorioPerfilTributarioCliente
    {
        protected override string hqlGetAll()
        {
            return "from PerfilTributarioCliente";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from PerfilTributarioCliente where ID = :id";
        }
    }
}
