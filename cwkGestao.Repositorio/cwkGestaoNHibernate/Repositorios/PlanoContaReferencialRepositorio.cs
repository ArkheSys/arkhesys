using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PlanoContaReferencialRepositorio : RepositorioAbstrato<PlanoContaReferencial>, IRepositorioPlanoContaReferencial
    {

        protected override string HqlLoadById()
        {
            return "from PlanoContaReferencial p where p.ID = :id";
        }

        public PlanoContaReferencial HqlLoadByCode(int p)
        {
            using (var session = GetSession())
            {
                PlanoContaReferencial plano = new PlanoContaReferencial();
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from PlanoContaReferencial p");
                hql.AppendLine("where Codigo = :p");
                plano = session.CreateQuery(hql.ToString()).SetInt32("p", p).List<PlanoContaReferencial>().FirstOrDefault();
                return plano;
            }
        }

        protected override string hqlGetAll()
        {
            return "from PlanoContaReferencial p";
        }

        public IList<PlanoContaReferencial> RetornarTodos()
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("FROM PlanoContaReferencial p");
                IList<PlanoContaReferencial> lista = session.CreateQuery(hql.ToString()).List<PlanoContaReferencial>();
                return lista;
            }
            
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
