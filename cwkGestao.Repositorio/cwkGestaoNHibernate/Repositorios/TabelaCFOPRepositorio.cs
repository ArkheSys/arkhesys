using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TabelaCFOPRepositorio : RepositorioAbstrato<TabelaCFOP>, IRepositorioTabelaCFOP
    {
        protected override string HqlLoadById()
        {
            return "from TabelaCFOP t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from TabelaCFOP";
        }

        public TabelaCFOP GetCFOPPorNumero(string codCFOP)
        {
            if (string.IsNullOrEmpty(codCFOP))
                return null;

            using (var session = GetSession())
            {
                string hql = "from TabelaCFOP t where t.CFOP = " + codCFOP;

                IQuery query = session.CreateQuery(hql);
                return query.List<TabelaCFOP>().FirstOrDefault();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
