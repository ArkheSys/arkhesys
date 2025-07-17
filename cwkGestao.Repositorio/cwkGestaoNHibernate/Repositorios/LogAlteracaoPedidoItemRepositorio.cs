using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class LogAlteracaoPedidoItemRepositorio : RepositorioAbstrato<LogAlteracaoPedidoItem>, IRepositorioLogAlteracaoPedidoItem
    {
        protected override string HqlLoadById()
        {
            return "from LogAlteracaoPedidoItem n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from LogAlteracaoPedidoItem n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<LogAlteracaoPedidoItem> LoadObjectByIdItem(int ID, string Campo)
        {
            using (var session = GetSession())
            {
                string hql = " from LogAlteracaoPedidoItem log";
                if (Campo == "Produto")
                    hql += " where log.Produto.ID = " + ID;
                else
	                hql += " where log.Servico.ID = " + ID;
                            
                var query = session.CreateQuery(hql).List<LogAlteracaoPedidoItem>();

                return query;
            }
        }

        public IList<LogAlteracaoPedidoItem> LoadObjectByData(DateTime dataInicio, DateTime dataFim)
        {
            using (var session = GetSession())
            {
                string hql = " from LogAlteracaoPedidoItem log";

                hql += " where log.DataInicialDesc >= :dataInicio "+
                       " and log.DataFinalDesc < :dataFim";
               

                var query = session.CreateQuery(hql).SetDateTime("dataInicio", dataInicio).SetDateTime("dataFim", dataFim).List<LogAlteracaoPedidoItem>();

                return query;
            }
        }

        public void DeleteObjectByIdItem(int ID, string Campo)
        {
            using (var session = GetSession())
            {
                string hql = "Delete from LogAlteracaoPedidoItem";
                if (Campo == "Produto")
                    hql += " where IDProduto = " + ID;
                else
                    hql += " where IDServico = " + ID;
                using (ITransaction trans = session.BeginTransaction())
                {
                    var query = session.CreateSQLQuery(hql).ExecuteUpdate();
                    trans.Commit();
                }
            }
        }
    }
}
