using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_ClienteRepositorio : RepositorioAbstrato<Tel_Cliente>, IRepositorioTel_Cliente
    {
        protected override string HqlLoadById()
        {
            return @"from Tel_Cliente c 
                     join fetch c.GrupoCliente gc 
                     join fetch c.Pessoa pe
                     left join fetch pe.PessoaEnderecos pen
                     left join fetch pen.Cidade ci
                     left join fetch c.Acrescimo ac
                     where c.ID = :id";
        }

        public override Tel_Cliente LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadById();

                Tel_Cliente retorno = session.CreateQuery(sql).SetParameter("id", id).List<Tel_Cliente>().FirstOrDefault();
                if (retorno != null)
                {
                    NHibernateUtil.Initialize(retorno.Precos);
                    NHibernateUtil.Initialize(retorno.Telefones);
                    NHibernateUtil.Initialize(retorno.Servicos);
                    NHibernateUtil.Initialize(retorno.Pessoa.PessoaTelefones);
                }
                return retorno;
            }
        }

        protected override string hqlGetAll()
        {
            return @"select new Tel_Cliente(c.ID, c.Pessoa.Nome, c.Pessoa.CNPJ_CPF, c.GrupoCliente.Nome, c.Pessoa.Fantasia, c.Pessoa.Codigo, c.NumeroPiloto) from Tel_Cliente c ";
            //return @" from Tel_Cliente c ";
        }

        public IList<cwkGestao.Modelo.Tel_Cliente> GetByGrupo(cwkGestao.Modelo.Tel_GrupoCliente grupoCliente)
        {
            using (var session = GetSession())
            {
                string hql = @"select distinct c from Tel_Cliente c 
                               join fetch c.GrupoCliente g 
                               left join fetch c.Precos 
                               where g.ID = :idGrupo and c.Pessoa.BAtivo = true";
                return session.CreateQuery(hql).SetInt32("idGrupo", grupoCliente.ID).List<cwkGestao.Modelo.Tel_Cliente>();
            }
        }

        public IList<Tel_Cliente> GetAllByGrupo(Tel_GrupoCliente grupoCliente)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + "join c.GrupoCliente g where g.ID = :idGrupo";
                return session.CreateQuery(hql).SetInt32("idGrupo", grupoCliente.ID).List<cwkGestao.Modelo.Tel_Cliente>();
            }
        }

        public Tel_Cliente GetPessoaID(int ID)
        {
            using (var session = GetSession())
            {
                string hql = @"from Tel_Cliente c
                              where c.Pessoa.ID = :ID";

                return session.CreateQuery(hql).SetInt32("ID", ID).UniqueResult() as Tel_Cliente;
            }
        }

        public Tel_Cliente GetByTelefone(string telefone)
        {
            using (var session = GetSession())
            {
                var hql = @"select c
                            from Tel_ClienteTelefone ct
                            join ct.Cliente c
                            join ct.GrupoClienteTelefone gct
                            where gct.Telefone = :telefone AND
                            ct.DataDesligamento is null";
                var result = session.CreateQuery(hql).SetString("telefone", telefone).List<Tel_Cliente>();
                return result.FirstOrDefault();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
