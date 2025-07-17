using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ReparcelamentoDocRepositorio : RepositorioAbstrato<ReparcelamentoDoc>, IRepositorioReparcelamentoDoc
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

        

        public IList<ReparcelamentoDoc> LoadByIdDocumento(int idDocumento)
        {
            using (var session = GetSession())
            {
                string hql = @"from ReparcelamentoDoc rd
                               left join fetch rd.Documento d
                               left join fetch rd.Reparcelamento r
                              where rd.Documento.ID = :idDocumento";
                return session.CreateQuery(hql).SetInt32("idDocumento", idDocumento).List<Modelo.ReparcelamentoDoc>();
            }
        }

        public IList<ReparcelamentoDoc> LoadListByIdDocumento(List<int> idsDocumento)
        {
            using (var session = GetSession())
            {
                string hql = @"from ReparcelamentoDoc rd
                               left join fetch rd.Documento d
                               left join fetch rd.Reparcelamento r
                              where rd.Documento.ID in (:idsDocumento)";
                return session.CreateQuery(hql).SetParameterList("idsDocumento", idsDocumento).List<Modelo.ReparcelamentoDoc>();
            }
        }
    }
}
