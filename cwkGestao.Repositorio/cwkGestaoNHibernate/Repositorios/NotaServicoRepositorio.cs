using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NotaServicoRepositorio : RepositorioAbstrato<NotaServico>, IRepositorioNotaServico
    {
        protected override string HqlLoadById()
        {
            return
                   " from NotaServico n " +
                   " left join fetch n.Cliente " +                  
                   " left join fetch n.Condicao " +
                   " left join fetch n.Filial " + 
                   " left join fetch n.NotaServicoItems" +
                   " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return
                   " from NotaServico n " +
                   " left join fetch n.Cliente " +
                   " left join fetch n.Condicao " +
                   " left join fetch n.Filial ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
