using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class VersaoRepositorio : RepositorioAbstrato<Versao>, IRepositorioVersao
    {

        protected override string HqlLoadById()
        {
            return "from Versao v left join fetch v.Sistema where v.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Versao v left join fetch v.Sistema";
        }

        public IList<cwkGestao.Modelo.Versao> GetAllBySistema(int pIdSistema)
        {
            using (var session = GetSession())
            {
                string hql = "from Versao v where v.Sistema.ID = :idSistema";

                return session.CreateQuery(hql).SetInt32("idSistema", pIdSistema).List<Versao>();
            }
        }

        public bool IsReferenced(Versao versao)
        {
            using (var session = GetSession())
            {
                String hql = @"select SUM(regs.l1) from (
                                select COUNT(*) as l1 from CwkPedido where IDVersao = :idVersao
                                union 
                                select COUNT(*) as l2 from OrdemServico where IDVersao = :idVersao) as regs";
                Object a = session.CreateSQLQuery(hql).SetInt32("idVersao", versao.ID).UniqueResult();
                return Convert.ToInt64(a) > 0;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
