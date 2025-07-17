using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_GrupoClientePrecoRepositorio : RepositorioAbstrato<Tel_GrupoClientePreco>, IRepositorioTel_GrupoClientePreco
    {
        protected override string HqlLoadById()
        {
            return "from Tel_GrupoClientePreco g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_GrupoClientePreco g";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
