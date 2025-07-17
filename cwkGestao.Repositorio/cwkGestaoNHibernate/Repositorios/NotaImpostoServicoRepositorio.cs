using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class NotaImpostoServicoRepositorio : RepositorioAbstrato<NotaImpostoServico>, IRepositorioNotaImpostoServico
    {
        protected override string HqlLoadById()
        {
            return " from NotaImpostoServico n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return " from NotaImpostoServico n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
