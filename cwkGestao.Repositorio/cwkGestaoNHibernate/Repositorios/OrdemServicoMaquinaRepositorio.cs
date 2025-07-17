using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OrdemServicoMaquinaRepositorio : RepositorioAbstrato<OrdemServicoMaquina>, IRepositorioOrdemServicoMaquina
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from OrdemServicoMaquina p where p.ID = :id";
        }

        public override OrdemServicoMaquina LoadObjectById(int id)
        {
            return GetSession().Get<OrdemServicoMaquina>(id);
        }

        protected override string hqlGetAll()
        {
            return "select distinct p from OrdemServicoMaquina p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
