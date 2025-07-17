using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_TipoLigacaoRepositorio : RepositorioAbstrato<Tel_TipoLigacao>, IRepositorioTel_TipoLigacao
    {
        
        protected override string HqlLoadById()
        {
            return "from Tel_TipoLigacao l where l.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_TipoLigacao l";
        }

        public HashSet<int> GetHashCodigos()
        {
            using (var session = GetSession())
            {
                var hql = "select t.Codigo from Tel_TipoLigacao t";
                var result = session.CreateQuery(hql).List<int>();
                return new HashSet<int>(result);
            }
        }

        public IList<Tel_TipoLigacao> GetIncompletos()
        {
            using (var session = GetSession())
            {
                var hql = @"from Tel_TipoLigacao t 
                            where (t.ResumoFatura is null or t.ResumoFatura = '')
                            or t.ID not in (select tp.TipoLigacao.ID from Tel_TipoLigacaoPreco tp)";
                return session.CreateQuery(hql).List<Tel_TipoLigacao>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
