using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaPerfuracoesRepositorio : RepositorioAbstrato<Ofd_PropostaPerfuracoes>, IRepositorioOfd_PropostaPerfuracoes
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_PropostaPerfuracoes n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_PropostaPerfuracoes n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
