using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaEmailRepositorio : RepositorioAbstrato<PessoaEmail>, IRepositorioPessoaEmail
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public IList<PessoaEmail> GetEmailFinanceiroPorIDPessoa(int idPessoa)
        {
            using (var session = GetSession())
            {
                string hql = "select pe from PessoaEmail pe " +
                             "left join fetch pe.Pessoa p " +
                             "where p.ID = :idPessoa " +
                             "and pe.BFinanceiro = 1";
                return session.CreateQuery(hql).SetInt32("idPessoa", idPessoa).List<PessoaEmail>();
            }
        }

        public IList<PessoaEmail> GetEmailNFSe(int idPessoa)
        {
            using (var session = GetSession())
            {
                string hql = "select pe from PessoaEmail pe " +
                             "left join fetch pe.Pessoa p " +
                             "where p.ID = :idPessoa " +
                             "and pe.BNFSe = 1";
                return session.CreateQuery(hql).SetInt32("idPessoa", idPessoa).List<PessoaEmail>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
