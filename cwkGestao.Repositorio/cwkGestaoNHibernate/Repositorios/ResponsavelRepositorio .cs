using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ResponsavelRepositorio : RepositorioAbstrato<Responsavel>, IRepositorioResponsavel
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from Responsavel p where p.ID = :id";
        }

        public override Responsavel LoadObjectById(int id)
        {
            return GetSession().Get<Responsavel>(id);
        }

        protected override string hqlGetAll()
        {
            return "from Responsavel p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
