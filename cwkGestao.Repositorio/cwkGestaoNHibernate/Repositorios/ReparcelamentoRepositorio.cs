using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ReparcelamentoRepositorio : RepositorioAbstrato<Reparcelamento>, IRepositorioReparcelamento
    {
        protected override string HqlLoadById()
        {
            return @"from Reparcelamento r
                    left join fetch r.ReparcelamentoDocs rd
                    left join fetch rd.Documento d
                    where r.id = :id ";
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Reparcelamento> LoadListByIdReparcelamento(List<int> idsReparcelamentos)
        {
            using (var session = GetSession())
            {
                string hql = @"from Reparcelamento r
                                left join fetch r.ReparcelamentoDocs rd
                                left join fetch rd.Documento d
                              where r.ID in (:idsReparcelamentos)";
                return session.CreateQuery(hql).SetParameterList("idsReparcelamentos", idsReparcelamentos).List<Modelo.Reparcelamento>();
            }
        }

        public IList<Reparcelamento> LoadListByIdReparcelamentoDoc(List<int> idsReparcelamentoDocs)
        {
            using (var session = GetSession())
            {
                string hql = @"from Reparcelamento r
                                left join fetch r.ReparcelamentoDocs rd
                                left join fetch rd.Documento d
                              where rd.ID in (:idsReparcelamentoDocs)";
                return session.CreateQuery(hql).SetParameterList("idsReparcelamentoDocs", idsReparcelamentoDocs).List<Modelo.Reparcelamento>();
            }
        }

        public IList<Reparcelamento> LoadByIdReparcelamentoDoc(int idReparcelamentoDocs)
        {
            using (var session = GetSession())
            {
                string hql = @"from Reparcelamento r
                                left join fetch r.ReparcelamentoDocs rd
                                left join fetch rd.Documento d
                              where rd.ID = :idReparcelamentoDocs";
                return session.CreateQuery(hql).SetInt32("idDocumento", idReparcelamentoDocs).List<Modelo.Reparcelamento>();
            }
        }
    }
}
