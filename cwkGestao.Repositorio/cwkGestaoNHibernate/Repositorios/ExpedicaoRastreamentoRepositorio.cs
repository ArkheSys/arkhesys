using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ExpedicaoRastreamentoRepositorio : RepositorioAbstrato<ExpedicaoRastreamento>, IRepositorioExpedicaoRastreamento
    {
        protected override string HqlLoadById()
        {
            return "from ExpedicaoRastreamento n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ExpedicaoRastreamento n";
        }

        public virtual IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoPorPedido(int id)
        {
            using (var session = GetSession())
            {
                string hql = @"from ExpedicaoRastreamento e where e.Pedido.ID = :idPedido";
                return session.CreateQuery(hql).SetParameter("idPedido", id).List<ExpedicaoRastreamento>();
            }
        }

        public virtual IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoNaoCompletas(int idPedido)
        {
            using (var session = GetSession())
            {
                string hql = @"from ExpedicaoRastreamento e
                             INNER JOIN FETCH e.IDRastreamento r 
                             where e.IDPedido = :idPedido and r.RastreamentoCompleto = 0";
                return session.CreateQuery(hql).SetInt32("idPedido", idPedido).List<ExpedicaoRastreamento>();
            }
        }

        public virtual ExpedicaoRastreamento GetExpedicaoRastreamentoByRastreamento(int idRastramento)
        {
            using (var session = GetSession())
            {
                string hql = "from ExpedicaoRastreamento e where e.IDRastreamento = :idRastramento";
                return session.CreateQuery(hql).SetParameter("idRastramento", idRastramento).List<ExpedicaoRastreamento>().FirstOrDefault();
            }
        }

        public virtual IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoPorPLP(PLP plp)
        {
            try
            {
                string idPedido = "(";
                foreach (var item in plp.PLPDetalhes)
                {
                    idPedido += item.Pedido.ID + ",";
                }

                using (var session = GetSession())
                { 
                    PLPDetalhe x = new PLPDetalhe();
                    string hql = @"SELECT expR
                                   FROM ExpedicaoRastreamento expR 
                                   INNER JOIN FETCH expR.IDPedido p 
                                   WHERE p.ID IN " + idPedido.Substring(0, idPedido.Length -1) + ")";
                    IList<ExpedicaoRastreamento> temp = session.CreateQuery(hql).List<ExpedicaoRastreamento>();
                    return temp;
                }
            }
            catch (Exception s)
            {
                
                throw s;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
