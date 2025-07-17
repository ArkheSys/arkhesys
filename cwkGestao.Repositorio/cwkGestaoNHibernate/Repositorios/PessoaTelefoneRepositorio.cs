using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaTelefoneRepositorio : RepositorioAbstrato<PessoaTelefone>, IRepositorioPessoaTelefone
    {
        protected override string HqlLoadById()
        {
            return " from PessoaTelefone pet where pet.ID = :id ";
                
        }

        protected override string hqlGetAll()
        {
            return " from PessoaTelefone ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
