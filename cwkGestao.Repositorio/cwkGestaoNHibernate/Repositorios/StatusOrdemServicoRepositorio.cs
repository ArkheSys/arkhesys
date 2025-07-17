using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class StatusOrdemServicoRepositorio : RepositorioAbstrato<StatusOrdemServico>, IRepositorioStatusOrdemServico
    {
        protected override string HqlLoadById()
        {
            return "from StatusOrdemServico where ID = :id";

        }

        protected override string hqlGetAll()
        {
            return "from StatusOrdemServico";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<StatusOrdemServico>(session) select t.Codigo).Max() + 1;
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
