using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaFuncionarioRepositorio : RepositorioAbstrato<PessoaFuncionario>, IRepositorioPessoaFuncionario
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public PessoaFuncionario GetVendedorAssociadoComUsuarioLogado(string login)
        {
            using (var session = GetSession())
            {
                string hql = @"select pf from PessoaFuncionario pf 
                               left join fetch pf.Pessoa p 
                               left join fetch pf.CwUsuario cw 
                               where cw.Login = :login ";
                return session.CreateQuery(hql).SetString("login", login).List<PessoaFuncionario>().FirstOrDefault();
            }

        }

    }
}
