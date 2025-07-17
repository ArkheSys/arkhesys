using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NotaItemServicoRepositorio : RepositorioAbstrato<NotaItemServico>, IRepositorioNotaItemServico
    {
        protected override string HqlLoadById()
        {
            return " from NotaItemServico n"
                   + " left join fetch n.Nota"
                   + " left join fetch n.Servico"
                   + " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return " from NotaItemServico n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
