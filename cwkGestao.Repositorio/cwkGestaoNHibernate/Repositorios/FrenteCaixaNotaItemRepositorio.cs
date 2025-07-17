using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FrenteCaixaNotaItemRepositorio : RepositorioAbstrato<FrenteCaixaNotaItem>, IRepositorioFrenteCaixaNotaItem
    {
        protected override string HqlLoadById()
        {
            return " from FrenteCaixaNotaItem n"
                   + " left join fetch n.FrenteCaixaNota"
                   + " left join fetch n.Produto"
                   + " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public override FrenteCaixaNotaItem LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string hql = " from FrenteCaixaNotaItem ni"
                       + " left join fetch ni.FrenteCaixaNota n"
                       + " left join fetch ni.Produto"
                       + " where ni.ID = :id";

                var frentecaixanotaitem = session.CreateQuery(hql).SetInt32("id", id).UniqueResult<FrenteCaixaNotaItem>();
                return frentecaixanotaitem;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
    
}