using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate.Exceptions;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Rateio_MovRepositorio : RepositorioAbstrato<Rateio_Mov>, IRepositorioRateio_Mov
    {
        protected override string HqlLoadById()
        {
            return "from Rateio_Mov f left join fetch f.Movimento m left join fetch f.Projeto p where f.id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Rateio_Mov f left join fetch f.Movimento m left join fetch f.Projeto p";
        }

        public IList<Rateio_Mov> GetRateiosPorProjeto(int p)
        {
            using (var session = GetSession())
            {
                string hql = "from Rateio_Mov f left join fetch f.Movimento m left join fetch m.Documento where f.Projeto.ID = " + p;
                var resultado = session.CreateQuery(hql).List<Rateio_Mov>();

                return resultado;
            }
        }

        public IList<Rateio_Mov> GetRateiosPorMovimentoID(int id)
        {
            using (var session = GetSession())
            {
                string hql = "from Rateio_Mov f left join fetch f.Movimento m left join fetch m.Documento where f.Movimento.ID = " + id;
                var resultado = session.CreateQuery(hql).List<Rateio_Mov>();

                return resultado;
            }
        }

        public int MaxCodigo(Movimento m)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "select max(r.Sequencia) from Rateio_Mov r left join r.Movimento m where m.ID = :id";
                    int result = session.CreateQuery(hql).SetInt32("id",m.ID).UniqueResult<int>() + 1;
                    return result;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
