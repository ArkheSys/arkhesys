using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaClienteRepositorio : RepositorioAbstrato<PessoaCliente>, IRepositorioPessoaCliente
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            return "from Pessoa Where bCliente = true";
        }

        public IList<Modelo.Pessoa> GetRevendas()
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where (select count(*) from ClienteRevenda c where c.Pessoa = p and p.BAtivo = true) > 0";
                return session.CreateQuery(hql).List<Modelo.Pessoa>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
