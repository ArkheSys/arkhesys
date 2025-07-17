using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NCMRepositorio : RepositorioAbstrato<NCM>, IRepositorioNCM
    {
        protected override string HqlLoadById()
        {
            return   "select n from NCM n "
                   + " left join fetch n.NCMItens ni "
                   + " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from NCM n";
        }

        public NCM GetByNcm(string ncm)
        {
            using (var session = GetSession())
            {
                string hql = "select n from NCM n "
                   + " left join fetch n.NCMItens ni "
                   + " where n.Ncm = :ncm";

                var result = session.CreateQuery(hql).SetString("ncm", ncm).List<NCM>();

                if (result.Count > 0)
                    return result[0];
            }
            return null;
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
