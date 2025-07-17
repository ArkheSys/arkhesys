using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ClienteRevendaRepositorio : RepositorioAbstrato<ClienteRevenda>, IRepositorioClienteRevenda
    {
        protected override string HqlLoadById()
        {
            return "from ClienteRevenda where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ClienteRevenda";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<ClienteRevenda>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        public IList<cwkGestao.Modelo.ClienteRevenda> GetAllByPessoa(int pIdPessoa)
        {
            using (var session = GetSession())
            {
                string hql = "select new ClienteRevenda( cr.ID, cr.Codigo, cr.Nome, cr.Cnpj, cr.Telefone, cr.Email) from ClienteRevenda cr " +
                "join cr.Pessoa p where p.ID = " + pIdPessoa;
                HashSet<ClienteRevenda> set = new HashSet<ClienteRevenda>(session.CreateQuery(hql).List<ClienteRevenda>());
                return set.ToList<ClienteRevenda>();
            }
        }

        public IList<ClienteRevenda> GetAllByMatriz(int pIdMatriz)
        {
            using (var session = GetSession())
            {
                string hql = "from ClienteRevenda cr where cr.Matriz.ID = " + pIdMatriz;
                return session.CreateQuery(hql).List<ClienteRevenda>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
