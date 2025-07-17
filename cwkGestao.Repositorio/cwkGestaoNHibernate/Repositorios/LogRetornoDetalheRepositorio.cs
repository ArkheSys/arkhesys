using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class LogRetornoDetalheRepositorio : RepositorioAbstrato<LogRetornoDetalhe>, IRepositorioLogRetornoDetalhe
    {
        protected override string HqlLoadById()
        {
            return "from LogRetornoDetalhe n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from LogRetornoDetalhe n";
        }

        public IList<LogRetornoDetalhe>  HqlLoadByIdLogRetorno(int idLogRet)
        {
            using (var session = GetSession())
            {
                string hql = "from LogRetornoDetalhe n where n.logRetorno = :idLogRetorno";
                return  session.CreateQuery(hql).SetInt32("idLogRetorno", idLogRet).List<Modelo.LogRetornoDetalhe>();
            }
        }

        protected override string hqlGetAllByPeriod(System.DateTime start, System.DateTime end)
        {
            throw new System.NotImplementedException();
        }
    }
}
