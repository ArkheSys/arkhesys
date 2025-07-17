using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaEnderecoRepositorio : RepositorioAbstrato<PessoaEndereco>, IRepositorioPessoaEndereco
    {
        protected override string HqlLoadById()
        {
            return " from PessoaEndereco pend where pend.ID = :id ";
        }

        protected override string hqlGetAll()
        {
            return " from PessoaEndereco pend ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
