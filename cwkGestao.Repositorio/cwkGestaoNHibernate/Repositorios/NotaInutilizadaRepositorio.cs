using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NotaInutilizadaRepositorio : RepositorioAbstrato<NotaInutilizada>, IRepositorioNotaInutilizada
    {
        protected override string HqlLoadById()
        {
            return "from NotaInutilizada n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from NotaInutilizada n";
        }

        public IList<NotaInutilizada> GetAllByFilial(int idFilial)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + " where n.Filial = :idFilial";

                return session.CreateQuery(hql).SetInt32("idFilial", idFilial).List<NotaInutilizada>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<NotaInutilizada> GetRelNotaPeriodo(String idsEmpresas, DateTime dataInicial, DateTime dataFinal)
        {
            using (var session = GetSession())
            {
                String hql = "from NotaInutilizada n " +
                             " join fetch n.Filial f  " +
                             " where n.ModeloNota = 55 and " +
                             " n.Data between :dataInicial and :dataFinal and " +
                             " n.Filial.ID in (" + idsEmpresas + ")";

                   return session.CreateQuery(hql).SetDateTime("dataInicial", dataInicial).SetDateTime("dataFinal", dataFinal).List<NotaInutilizada>();
               

            }
        }
    }
}