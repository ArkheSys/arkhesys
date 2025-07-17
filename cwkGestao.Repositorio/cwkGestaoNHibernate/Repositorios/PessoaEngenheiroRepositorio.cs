using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaEngenheiroRepositorio : RepositorioAbstrato<PessoaEngenheiro>, IRepositorioPessoaEngenheiro
    {
        protected override string HqlLoadById()
        {
            return "from PessoaEngenheiro p where p.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PessoaEngenheiro p ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
