using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using NHibernate;
using System.Reflection;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ChequeRepositorio : RepositorioAbstrato<Cheque>, IRepositorioCheque
    {
        protected override string HqlLoadById()
        {
            return "from Cheque c where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Cheque c";
        }

        public IList<ChequeHistorico> GetHistorico(Cheque cheque)
        {
            using (var session = GetSession())
            {
                string hql = @"select ch from ChequeHistorico ch
                               left join ch.Cheque c
                               left join ch.Documento doc
                               left join ch.MovCaixa m
                               left join ch.Historico h 
                               where c.ID = :idcheque";
                return session.CreateQuery(hql).SetInt32("idcheque", cheque.ID).List<ChequeHistorico>();
            }
        }

        public IList<Cheque> GetChequesNaoUtilizados()
        {
            IList<Cheque> result = new List<Cheque>();
            using (var session = GetSession())
            {
                try
                {
                    string hql = @"from Cheque c where  c.Status = 0";
                    result = session.CreateQuery(hql).List<Cheque>();
                }
                catch (Exception e)
                {
                    
                    throw e;
                }
            }
            return result;
        }

        public IList<pxyChequeSaldoRestante> GetChequesComSaldoRestante(int IDMovCaixaExcluido, int EntSai)
        {
            IList<Cheque> resCheque = new List<Cheque>();

            IList<pxyChequeSaldoRestante> result = new List<pxyChequeSaldoRestante>();
            using (var session = GetSession())
            {
                try
                { 
                    IQuery q = null;
                    string hql = @"select distinct c from Cheque c 
                                   left join fetch c.ChequeHistorico ch
                                   left join fetch c.MovCaixas mc
                                   
                                   ";
                    q = session.CreateQuery(hql);
                    if (IDMovCaixaExcluido > 0)
                    {
                        hql += @" and mc.ID != :idMovCaixa";
                        q = q.SetInt32("idMovCaixa", IDMovCaixaExcluido);
                    }
                    resCheque = q.List<Cheque>();
                    if (resCheque != null)
                    {
                        foreach (var item in resCheque)
                        {
                            if (item.MovCaixas != null)
                            {
                                result.Add(new pxyChequeSaldoRestante(
                                    item,
                                    item.MovCaixas.Where(w => w.Ent_Sai == Ent_SaiMovCaixa.Entrada).Sum(s => s.Valor),
                                    //item.ChequeHistorico.Where(w => w.MovCaixa.Ent_Sai == Ent_SaiMovCaixa.Entrada).Sum(s => s.MovCaixa.Valor),
                                    item.Valor - item.MovCaixas.Where(w => w.Ent_Sai == Ent_SaiMovCaixa.Entrada).Sum(s => s.Valor),
                                    //item.Valor - item.ChequeHistorico.Where(w => w.MovCaixa.Ent_Sai == Ent_SaiMovCaixa.Entrada).Sum(s => s.MovCaixa.Valor),
                                    item.MovCaixas.Where(w => w.Ent_Sai == Ent_SaiMovCaixa.Saída).Sum(s => s.Valor),
                                    //item.ChequeHistorico.Where(w => w.MovCaixa.Ent_Sai == Ent_SaiMovCaixa.Saída).Sum(s => s.MovCaixa.Valor),
                                    item.Valor - item.MovCaixas.Where(w => w.Ent_Sai == Ent_SaiMovCaixa.Saída).Sum(s => s.Valor)
                                    //item.Valor - item.ChequeHistorico.Where(w => w.MovCaixa.Ent_Sai == Ent_SaiMovCaixa.Saída).Sum(s => s.MovCaixa.Valor)
                                    )
                                );
                            }
                            else
                            {
                                result.Add(new pxyChequeSaldoRestante(item, item.Valor, Decimal.Zero, Decimal.Zero, item.Valor));
                            }                          
                        }
                    }
                    if (EntSai == 1)
                    {
                        result = result.Where(w => w.SaldoRestanteSaida > 0).ToList();
                    }
                    else
                    {
                        result = result.Where(w => w.SaldoRestanteEntrada > 0).ToList();
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return result;
        }

        public IList<pxyChequeSaldoRestante> GetChequesComSaldoRestante()
        {
            IList<Cheque> resCheque = new List<Cheque>();
            IList<pxyChequeSaldoRestante> result = new List<pxyChequeSaldoRestante>();
            using (var session = GetSession())
            {
                try
                {
                    string sql = SQLGetChequesComSaldoRestante();

                    IQuery query = session.CreateSQLQuery(sql);

                    ConstructorInfo construtor = GetConstructorInfoChequeComSaldo();
                    result = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyChequeSaldoRestante>();

                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return result;
        }

        
        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            string hql = @"from Cheque c where c.IncData between :dataInicio and :dataFim ";
            return hql;
        }

       

        private string SQLGetChequesComSaldoRestante()
        {
            string sql = @"
                            select t.*, t.valor - t.utilizado as saldo
	                        from (
		                        select c.ID
			                          ,c.Banco
			                          ,c.Agencia
			                          ,c.ContaCorrente
			                          ,c.Numero
			                          ,c.Emitente
			                          ,c.CNPJCPF_Emitente
			                          ,c.Valor
			                          ,c.ChTerceiro
			                          ,c.Vencimento
			                        isnull((select sum(mc.valor) 
						                        from MovCaixa mc 
						                        join ChequeHistorico ch on mc.ID = ch.IDMovCaixa 
						                        where ch.IDCheque = c.ID and mc.ID != 0), 0) as utilizado 
		                        from cheque c
                        ) t
                         ";
            return sql;
        }

        public ConstructorInfo GetConstructorInfoChequeComSaldo()
        {
            return typeof(pxyChequeSaldoRestante).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(decimal),
                                                    typeof(bool),
                                                    typeof(DateTime),
                                                    typeof(decimal),
                                                    typeof(decimal),
                                                    typeof(decimal),
                                                    typeof(decimal)
                                                    });
        }

        #region IRepositorioCheque Members




        #endregion
    }
}
