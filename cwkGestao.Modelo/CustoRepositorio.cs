using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CustoRepositorio : RepositorioAbstrato<Custo>, IRepositorioCusto
    {
        protected override string HqlLoadById()
        {
            return "from Custo c where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Custo c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Custo> GetAllByIDNota(int idNota)
        {
            using (var session = GetSession())
            {
                string hql = @"from Custo c 
                               left join fetch c.Nota n
                               where n.ID = :idNota";
                IList<Custo> custos = session.CreateQuery(hql).SetInt32("idNota", idNota).List<Custo>();

                return custos;
            }
        }

    }
}
