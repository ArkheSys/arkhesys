using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OrdemProducaoRepositorio : RepositorioAbstrato<OrdemProducao>, IRepositorioOrdemProducao
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public cwkGestao.Modelo.OrdemProducao GetByCodigo(int codigo)
        {

            using (var session = GetSession())
            {
                string hql = "from OrdemProducao where Codigo = :codigo";

                return session.CreateQuery(hql).SetInt32("codigo", codigo).UniqueResult<OrdemProducao>();
            }

        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
