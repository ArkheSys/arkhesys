using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PLPDetalheRepositorio : RepositorioAbstrato<PLPDetalhe>, IRepositorioPLPDetalhe
    {
        protected override string HqlLoadById()
        {
            return "from PLPDetalhe n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PLPDetalhe n";
        }

        public IList<PLPDetalhe> GetPLPDetalhePorIDPLP(int IDPLP)
        {
            using (var session = GetSession())
            {
                string hql = " from PLPDetalhe det " +
                             " left join fetch det.PLP plp" +
                             " where plp.ID = :IDPLP ";

                IList<PLPDetalhe> lista = session.CreateQuery(hql).SetInt32("IDPLP", IDPLP).List<PLPDetalhe>();
                return lista;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
