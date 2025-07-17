using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SincronismoPendenteMercadoLivreRepositorio : RepositorioAbstrato<SincronismoPendenteMercadoLivre>, IRepositorioSincronismoPendenteMercadoLivre
    {
        protected override string HqlLoadById()
        {
            return "from SincronismoPendenteMercadoLivre n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from SincronismoPendenteMercadoLivre n";
        }

        public SincronismoPendenteMercadoLivre hqlLoadByIdMercadoLivre(int id)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = @"FROM SincronismoPendenteMercadoLivre s WHERE s.PedidoMercadoLivre = " + id;
                    return session.CreateQuery(hql).List<SincronismoPendenteMercadoLivre>().ToList().FirstOrDefault();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
