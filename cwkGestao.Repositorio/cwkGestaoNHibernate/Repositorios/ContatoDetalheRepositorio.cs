using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ContatoDetalheRepositorio : RepositorioAbstrato<ContatoDetalhe>, IRepositorioContatoDetalhe
    {
        protected override string HqlLoadById()
        {
            return "from ContatoDetalhe n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ContatoDetalhe n";
        }
		
		protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return "from ContatoDetalhe x where x.Data between :dataInicio and :dataFim order by x.Data desc";
        }

        public IList<ContatoDetalhe> GetPorIDContato(int IDContato)
        {
            using (var session = GetSession())
            {
                string hql = " from ContatoDetalhe cd " +
                             " left join fetch cd.Contato c " +
                             " left join fetch cd.StatusContato st" +
                             " where c.ID = :IDContato ";

                IList<ContatoDetalhe> detalhes = session.CreateQuery(hql).SetInt32("IDContato", IDContato).List<ContatoDetalhe>();
                return detalhes;
            }
        }
    }
}
