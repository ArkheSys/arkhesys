using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_ClienteTelefoneRepositorio : RepositorioAbstrato<Tel_ClienteTelefone>, IRepositorioTel_ClienteTelefone
    {
        protected override string HqlLoadById()
        {
            return "from Tel_ClienteTelefone g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_ClienteTelefone g";
        }

        public IList<Tel_ClienteTelefone> GetAtivosByCliente(Tel_Cliente pessoa)
        {
            using (var session = GetSession())
            {
                var hql = "from Tel_ClienteTelefone ct join fetch ct.Cliente c where c.ID = :id and ct.DataDesligamento is null";
                return session.CreateQuery(hql).SetInt32("id", pessoa.ID).List<Tel_ClienteTelefone>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
