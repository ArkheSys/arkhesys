using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TipoPedidoRepositorio : RepositorioAbstrato<TipoPedido>, IRepositorioTipoPedido
    {
        protected override string HqlLoadById()
        {
            return "from TipoPedido t left join fetch t.TipoNota left join fetch t.TipoNota.Operacao left join fetch t.Impressora where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from TipoPedido t";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                return session.CreateQuery("select max(p.Codigo) from TipoPedido p").UniqueResult<int>() + 1;
            }
        }

        public IList<TipoPedido> GetAll(InOutType entSai, TipoPedidoType? tipoPedido)
        {
            using (var session = GetSession())
            {
                string hql = "from TipoPedido t where t.Ent_Sai = " + (int)entSai;
                if (tipoPedido.HasValue)
                {
                    hql += " and t.Tipo = " + (int)tipoPedido.Value;
                }
                IQuery query = session.CreateQuery(hql);
                HashSet<TipoPedido> set = new HashSet<TipoPedido>(query.List<TipoPedido>());
                return set.ToList<TipoPedido>();
            }
        }

        /// <summary>
        /// Pega todos os tipos pedidos de entrada e Tipo 1 - Orçamento 2 - Pedido
        /// </summary>
        /// <returns>Lista de TipoPedido</returns>
        public IList<TipoPedido> GetByTipoAndEnt_Sai(InOutType EntSai)
        {
            int varInOut = (int)EntSai;
            using (var session = GetSession())
            {
                string hql = "from TipoPedido t where t.Ent_Sai = :varInOut and t.Tipo in (1,2)";

                return session.CreateQuery(hql).SetInt32("varInOut", varInOut).List<TipoPedido>();            
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
