using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SincronismoPendenteRepositorio : RepositorioAbstrato<SincronismoPendente>, IRepositorioSincronismoPendente
    {
        protected override string HqlLoadById()
        {
            return "from SincronismoPendente n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from SincronismoPendente n";
        }

        public IList<SincronismoPendente> _GetPedidosPendentes()
        {
            using (var session = GetSession())
            {
                string hql = "from SincronismoPendente p where IDIntegracao <> null";
                return session.CreateQuery(hql).List<Modelo.SincronismoPendente>();
            }
        }

        public SincronismoPendente _GetPedidoPendente(int IDIntegracao)
        {
            using (var session = GetSession())
            {
                string hql = "from SincronismoPendente p where IDIntegracao = " + IDIntegracao;
                return session.CreateQuery(hql).List<Modelo.SincronismoPendente>().FirstOrDefault();

                
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
