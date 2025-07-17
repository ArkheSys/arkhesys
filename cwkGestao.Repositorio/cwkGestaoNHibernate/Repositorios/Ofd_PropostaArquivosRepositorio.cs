using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaArquivosRepositorio : RepositorioAbstrato<Ofd_PropostaArquivos>, IRepositorioOfd_PropostaArquivos
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_PropostaArquivos n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_PropostaArquivos n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
