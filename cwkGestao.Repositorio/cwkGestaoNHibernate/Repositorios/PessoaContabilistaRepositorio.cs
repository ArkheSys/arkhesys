using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaContabilistaRepositorio : RepositorioAbstrato<PessoaContabilista>, IRepositorioPessoaContabilista
    {
        protected override string HqlLoadById()
        {
            return "from PessoaContabilista n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PessoaContabilista n";
        }

        public PessoaContabilista GetPessoaContabilistaPorIDPessoa(int IDPessoa)
        {
            using (var session = GetSession())
            {
                string hql = " from PessoaContabilista pc" +
                             " left join fetch pc.Pessoa p " +
                             " where pc.Pessoa.ID = :IDPessoa ";

                var retorno =  session.CreateQuery(hql).SetInt32("IDPessoa", IDPessoa).List<PessoaContabilista>().FirstOrDefault();
                return retorno;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
