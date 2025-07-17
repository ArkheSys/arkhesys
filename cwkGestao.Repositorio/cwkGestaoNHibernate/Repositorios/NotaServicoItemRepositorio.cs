using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NotaServicoItemRepositorio : RepositorioAbstrato<NotaServicoItem>, IRepositorioNotaServicoItem
    {
        protected override string HqlLoadById()
        {
            return
                   " from NotaServicoItem n " +
                   " left join fetch n.Servico " +                   
                   " left join fetch n.NotaServico" +
                   " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return
                   " from NotaServicoItem n " +
                   " left join fetch n.Servico " +
                   " left join fetch n.NotaServico";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
