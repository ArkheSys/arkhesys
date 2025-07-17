using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate;
using NHibernate.Transform;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PedidoParcelaRepositorio : RepositorioAbstrato<PedidoParcela>, IRepositorioPedidoParcela
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            return "from PedidoParcela p";
        }

        public IList<PedidoParcela> GetParcelasPedido(Pedido p)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = @"FROM PedidoParcela p left join fetch p.Pedido ped WHERE ped.ID = :id";
                    return session.CreateQuery(hql).SetInt32("id", p.ID).List<PedidoParcela>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public IList<pxyRelFluxoCaixa> GetItemsFluxoDeCaixa(DateTime dataInicial, DateTime dataFinal, ConsiderarPedidos filtroPedidos)
        {
            using (var sessao = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select pp.Vencimento as Data");
                hql.AppendLine(" , case when p.Ent_Sai = :inoutEntrada then pp.Valor else 0 end as Pagar");
                hql.AppendLine(" , case when p.Ent_Sai = :inoutSaida then pp.Valor else 0 end as Receber");
                hql.AppendLine(" , p.Numero");
                hql.AppendLine(" , p.PessoaNome as Pessoa");
                hql.AppendLine(" , pp.Parcela");
                hql.AppendLine(" , c.QtParcela as QtdParcelas");
                hql.AppendLine(" from PedidoParcela pp ");
                hql.AppendLine(" inner join pp.Pedido p ");
                hql.AppendLine(" inner join p.Condicao c ");
                hql.AppendLine(" where pp.Vencimento >= :datainicial ");
                hql.AppendLine(" and pp.Vencimento < :datafinal");
                hql.AppendLine(" and p.Status = :statusAberto");

                if (filtroPedidos == ConsiderarPedidos.Compra)
                    hql.AppendLine(" and p.Ent_Sai = " + ((Int32)InOutType.Entrada).ToString());
                if (filtroPedidos == ConsiderarPedidos.Venda)
                    hql.AppendLine(" and p.Ent_Sai = " + ((Int32)InOutType.Saída).ToString());

                Type tipoContasPorPortador = typeof(pxyRelFluxoCaixa);
                ConstructorInfo construtor = tipoContasPorPortador.GetConstructor(new Type[] { typeof(DateTime),                                                                                                    
                                                                                                    typeof(decimal),
                                                                                                    typeof(decimal), 
                                                                                                    typeof(int),
                                                                                                    typeof(string),
                                                                                                    typeof(int),
                                                                                                    typeof(int)});

                IQuery query = sessao.CreateQuery(hql.ToString());

                query.SetDateTime("datainicial", dataInicial.Date);
                query.SetDateTime("datafinal", dataFinal.AddDays(1));
                query.SetInt32("statusAberto", (Int32)StatusOrcamento.Aberto);
                query.SetInt32("inoutEntrada", (Int32)InOutType.Entrada);
                query.SetInt32("inoutSaida", (Int32)InOutType.Saída);

                IQuery a = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor));

                return a.List<pxyRelFluxoCaixa>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
