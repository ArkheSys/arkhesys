using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

using Modelo.ModeloProxy;

using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MovimentacaoCreditoPedidoRepositorio : RepositorioAbstrato<MovimentacaoCreditoPedido>, IRepositorioMovimentacaoCreditoPedido
    {
        protected override string HqlLoadById()
        {
            return "select t from MovimentacaoCreditoPedido t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from MovimentacaoCreditoPedido t";
        }

        public IList<MovimentacaoCreditoPedido> GetTodasMovimentacoes()
        {
            using (var session = GetSession())
            {
                string hql = "from MovimentacaoCreditoPedido tn";
                IQuery query = session.CreateQuery(hql);
                return query.List<MovimentacaoCreditoPedido>();
            }
        }

        private ConstructorInfo GetContructorInfoPxyClientesComCredito()
        {
            return typeof(pxyClientesComCredito).GetConstructor(new Type[] {
                                                    typeof(Int32),  //Codigo
                                                    typeof(String),  //Nome
                                                    typeof(String), // CNPJ_CPF
                                                      typeof(Decimal)});//Saldo
        }
        public IList<pxyClientesComCredito> GetAllClientesComCredito()
        {
            //using (var session = GetSession())
            //    try
            //    {
            //        string hql = "";
            //        try
            //        {
            //            hql = "select p.Codigo, p.Nome, p.cnpj_cpf, mp.Saldo from MovimentacaoCreditoPedido as mp inner join mp.pessoa as p on p.ID = mp.IDCliente where mp.saldo > 0";
            //        }
            //        catch (Exception)
            //        {
            //            hql = "from " + typeof(pxyClientesComCredito).Name + " x ";
            //        }

            //        Query query = session.createQuery(sqlQuery);

            //        // codigo original
            //        HashSet<pxyClientesComCredito> set = new HashSet<pxyClientesComCredito>(session.CreateQuery(hql).List<pxyClientesComCredito>());
            //        ///HashSet<T> set = new HashSet<T>(session.CreateQuery(hql).SetFirstResult(0).SetMaxResults(10).List<T>());
            //        return set.ToList();
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception(ex.Message);
            //    }
            //    finally
            //    {
            //        session.Close();
            //    }
            try
            {
                using (var session = GetSession())
                {
                    //string Sql = @"select p.Codigo, p.Nome, p.cnpj_cpf, mp.Saldo from MovimentacaoCreditoPedido as mp inner join pessoa as p on p.ID = mp.IDCliente where mp.saldo > 0";
                    string Sql = @"Select p.codigo,
                                          p.Nome,
                                    	  p.CNPJ_CPF,
                                    	  mc.Saldo
                                     from MovimentacaoCreditoPedido mc
                                       inner join (select max(id) id,
                                                          max(data) as data,
                                    	                   idcliente
                                                     from MovimentacaoCreditoPedido
                                                    --where tipo = 1
                                                   group by idcliente) mp on (mc.id = mp.id)
                                    	inner join pessoa p on p.ID = mc.IDCliente
                                    where mc.Saldo > 0";

                    ConstructorInfo construtor = GetContructorInfoPxyClientesComCredito();

                    IQuery query = session.CreateSQLQuery(Sql.ToString());
                    return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyClientesComCredito>();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}