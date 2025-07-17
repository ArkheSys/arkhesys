using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MaquinaRepositorio : RepositorioAbstrato<Maquina>, IRepositorioMaquina
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from Maquina p where p.ID = :id";
        }

        public override Maquina LoadObjectById(int id)
        {
            return GetSession().Get<Maquina>(id);
        }

        protected override string hqlGetAll()
        {
            return "select distinct p from Maquina p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
