using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FabricanteRepositorio : RepositorioAbstrato<Fabricante>, IRepositorioFabricante
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from Fabricante p where p.ID = :id";
        }

        public override Fabricante LoadObjectById(int id)
        {
            return GetSession().Get<Fabricante>(id);
        }

        protected override string hqlGetAll()
        {
            return "select distinct p from Fabricante p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
