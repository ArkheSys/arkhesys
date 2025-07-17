using System;
using System.Collections.Generic;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class LacreRodoviarioMdfeRepositorio : RepositorioAbstrato<LacreRodoviarioMdfe>, IRepositorioLacreRodoviarioMdfe
    {
        protected override string HqlLoadById()
        {
            return "from LacreRodoviarioMdfe where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from LacreRodoviarioMdfe";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end) => throw new NotImplementedException();

        public IList<LacreRodoviarioMdfe> GetLacresPorMDFe(int IDMDFe)
        {
            using (var session = GetSession())
            {
                string hql = @"from LacreRodoviarioMdfe l where l.Mdfe.ID = :idMdfe";
                return session.CreateQuery(hql).SetInt32("idMdfe", IDMDFe).List<LacreRodoviarioMdfe>();
            }
        }
    }
}
