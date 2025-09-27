using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CESTRepositorio : RepositorioAbstrato<CEST>, IRepositorioCEST
    {
        protected override string HqlLoadById()
        {
            return "select n from CEST n "
                   + " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CEST n";
        }

        public CEST GetByCEST(string Codigo)
        {
            using (var session = GetSession())
            
                return session.QueryOver<CEST>()
                              .Where(c => c.Codigo == Codigo) // Usando a propriedade correta "Cest"
                              .SingleOrDefault();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
