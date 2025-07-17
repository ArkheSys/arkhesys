using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ExpedicaoHistoricoRepositorio : RepositorioAbstrato<ExpedicaoHistorico>, IRepositorioExpedicaoHistorico
    {

#pragma warning disable CS0414 // O campo "ExpedicaoHistoricoRepositorio.ExpedicaoHistorico" é atribuído, mas seu valor nunca é usado
        string ExpedicaoHistorico = @"from ExpedicaoHistorico e where e.DataEvento <= :dtInicio AND e.DataEvento >= :dtFinal";
#pragma warning restore CS0414 // O campo "ExpedicaoHistoricoRepositorio.ExpedicaoHistorico" é atribuído, mas seu valor nunca é usado

        protected override string HqlLoadById()
        {
            return "from ExpedicaoHistorico n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ExpedicaoHistorico n";
        }

        public string HqlExpedicaoHistorico()
        {
            return "from ExpedicaoHistorico e where e.DataEvento >= :dtInicio AND e.DataEvento <= :dtFinal";
        }

        public IList<ExpedicaoHistorico> GetHistoricosPorCliente(DateTime dtInicio, DateTime dtFinal, int idCliente)
        {
            using (var session = GetSession())
            {
                string sql = HqlExpedicaoHistorico() + @" AND e.ExpedicaoRastreamento.Pedido.Pessoa.ID = :idCliente";

                return session.CreateQuery(sql)
                    .SetDateTime("dtInicio",dtInicio)
                    .SetDateTime("dtFinal",dtFinal)
                    .SetInt32("idCliente",idCliente)
                    .List<ExpedicaoHistorico>();
            }
        }

        public IList<ExpedicaoHistorico> GetHistoricos(DateTime dtInicio, DateTime dtFinal, int idEmpresa)
        {
            using (var session = GetSession())
            {
                string sql = HqlExpedicaoHistorico() + @" AND e.ExpedicaoRastreamento.Pedido.Filial.ID = :idEmpresa";

                return session.CreateQuery(sql)
                    .SetDateTime("dtInicio", dtInicio)
                    .SetDateTime("dtFinal", dtFinal)
                    .SetInt32("idEmpresa", idEmpresa)
                    .List<ExpedicaoHistorico>();
            }
        }

        public IList<ExpedicaoHistorico> GetHistoricosPorExpedicaoRastreamento(int id)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + " where ExpedicaoRastreamento.ID = :id";

                return session.CreateQuery(hql).SetInt32("id", id).List<ExpedicaoHistorico>();
            }
        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
