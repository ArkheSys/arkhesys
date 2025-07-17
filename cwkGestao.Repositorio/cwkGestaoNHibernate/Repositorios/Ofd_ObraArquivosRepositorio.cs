using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    class Ofd_ObraArquivosRepositorio : RepositorioAbstrato<Ofd_ObraArquivos>, IRepositorioOfd_ObraArquivos
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_ObraArquivos n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_ObraArquivos n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
