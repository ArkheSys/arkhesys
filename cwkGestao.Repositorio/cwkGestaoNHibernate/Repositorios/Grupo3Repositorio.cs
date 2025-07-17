using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Grupo3Repositorio : RepositorioAbstrato<Grupo3>, IRepositorioGrupo3
    {
        protected override string HqlLoadById()
        {
            return "from Grupo3 g"                  
                    + " where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Grupo3 g";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                return session.CreateQuery("select max(g.Codigo) from Grupo3 g").UniqueResult<int>() + 1;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
