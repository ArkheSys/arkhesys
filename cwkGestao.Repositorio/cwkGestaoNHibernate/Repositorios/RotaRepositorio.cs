using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class RotaRepositorio : RepositorioAbstrato<Rota>, IRepositorioRota
    {
        protected override string HqlLoadById()
        {
            return "from Rota n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Rota n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public int getMaxCodigo()
        {
            using (var session = GetSession())
            {
                string hql = @"select ISNUll(max(r.Codigo), 0) Codigo from Rota r";

                var retorno = session.CreateSQLQuery(hql).UniqueResult();

                return Convert.ToInt32(retorno);
            }
        }
    }
}
