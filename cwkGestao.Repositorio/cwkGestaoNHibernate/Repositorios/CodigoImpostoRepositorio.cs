using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CodigoImpostoRepositorio : RepositorioAbstrato<CodigoImposto>, IRepositorioCodigoImposto
    {
        protected override string HqlLoadById()
        {
            return "from CodigoImposto n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CodigoImposto n";
        }
		
		protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<CodigoImposto> GetAllPorTipoImposto(string tributacao)
        {
            IList<CodigoImposto> result = new List<CodigoImposto>();
            using (var session = GetSession())
            {
                string hql = "from CodigoImposto c where c.Imposto.Nome = :trib";
                result = session.CreateQuery(hql).SetString("trib", tributacao).List<CodigoImposto>();
            }
            return result;
        }
    }
}
