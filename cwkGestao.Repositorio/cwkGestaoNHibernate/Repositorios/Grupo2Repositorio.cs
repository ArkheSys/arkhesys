using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Grupo2Repositorio : RepositorioAbstrato<Grupo2>, IRepositorioGrupo2
    {
        protected override string HqlLoadById()
        {
            return "from Grupo2 g"                  
                    + " where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Grupo2 g";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                return session.CreateQuery("select max(g.Codigo) from Grupo2 g").UniqueResult<int>() + 1;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
