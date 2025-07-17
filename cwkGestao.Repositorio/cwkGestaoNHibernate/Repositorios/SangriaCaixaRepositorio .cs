using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class SangriaCaixaRepositorio : RepositorioAbstrato<SangriaCaixa>, IRepositorioSangriaCaixa
    {
        protected override string hqlGetAll()
        {
            return "from SangriaCaixa c";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from SangriaCaixa c where c.ID = :id";
        }

        public decimal GetSangriaVendasComSangriaESuprimentos(int IDUsuario, int IDFluxoCaixa)
        {
            string hql = @"SELECT (COALESCE(VENDAS.TOTALVENDAS, 0) + 
                                   COALESCE(FLUXOCAIXA.VALORCAIXA, 0) + 
                                   COALESCE(SUPRIMENTOS.TOTALSUPRIMENTO, 0)) - 
                                   COALESCE(SANGRIAS.TOTALSANGRIA, 0) AS TOTAL
                              FROM FLUXOCAIXA
	                            LEFT JOIN (

			                            SELECT IDFLUXOCAIXA,
                                               sum(CASE WHEN FormaPagamento.Nome = 'DINHEIRO' THEN PedidoParcela.Valor - (CASE WHEN FrenteCaixaNota.Troco < 0 THEN 0 ELSE FrenteCaixaNota.Troco END) ELSE PedidoParcela.Valor END) as TOTALVENDAS
				                               -- SUM(PEDIDOPARCELA.VALOR) AS TOTALVENDAS
			                              FROM FRENTECAIXANOTA
				                            INNER JOIN FLUXOCAIXA ON (FRENTECAIXANOTA.IDFLUXOCAIXA = FLUXOCAIXA.ID)
				                            INNER JOIN PEDIDO ON (FRENTECAIXANOTA.IDPEDIDO = PEDIDO.ID AND PEDIDO.DTCANCELAMENTO IS NULL)
                              				INNER JOIN PEDIDOPARCELA ON (PEDIDO.ID = PEDIDOPARCELA.IDPEDIDO)
				                            INNER JOIN TIPODOCUMENTO ON (PEDIDOPARCELA.IDTIPODOCUMENTO = TIPODOCUMENTO.ID)
				                            INNER JOIN FORMAPAGAMENTO ON (TIPODOCUMENTO.IDFORMAPAGAMENTO = FORMAPAGAMENTO.ID AND FORMAPAGAMENTO.NOME = 'DINHEIRO')
			                            GROUP BY IDFLUXOCAIXA

	                            ) VENDAS ON (FLUXOCAIXA.ID = VENDAS.IDFLUXOCAIXA)

	                            LEFT JOIN (

			                            SELECT IDFLUXOCAIXA,
				                               SUM(VALOR) AS TOTALSANGRIA
			                              FROM SANGRIACAIXA
				                            INNER JOIN FLUXOCAIXA ON (SANGRIACAIXA.IDFLUXOCAIXA = FLUXOCAIXA.ID)
			                            GROUP BY IDFLUXOCAIXA

	                            ) SANGRIAS ON (FLUXOCAIXA.ID = SANGRIAS.IDFLUXOCAIXA)

	                            LEFT JOIN (

		                            SELECT IDFLUXOCAIXA,
			                               SUM(VALOR) AS TOTALSUPRIMENTO
		                              FROM SUPRIMENTOCAIXA
			                            INNER JOIN FLUXOCAIXA ON (SUPRIMENTOCAIXA.IDFLUXOCAIXA = FLUXOCAIXA.ID)
		                            GROUP BY IDFLUXOCAIXA

	                            ) SUPRIMENTOS ON (FLUXOCAIXA.ID = SUPRIMENTOS.IDFLUXOCAIXA)
                            WHERE FLUXOCAIXA.ID = :IDFluxoCaixa
                              AND FLUXOCAIXA.IDUSUARIO = :IDUsuario";

            using (var session = GetSession())
            {
                return session.CreateSQLQuery(hql).SetInt32("IDUsuario", IDUsuario).SetInt32("IDFluxoCaixa", IDFluxoCaixa).UniqueResult<decimal>();
            }
        }

        public List<SangriaCaixa> GetSangriasPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(@"from SangriaCaixa c where c.IDFluxoCaixa = :IDFluxoCaixa").SetInt32("IDFluxoCaixa", IDFluxoCaixa).List<SangriaCaixa>().ToList();
            }
        }

        public SangriaCaixa GetSangria(int IDSangriaCaixa)
        {
            using (var session = GetSession())
            {
                return session.CreateQuery(@"from SangriaCaixa c where c.ID = :ID").SetInt32("ID", IDSangriaCaixa).List<SangriaCaixa>().ToList().FirstOrDefault();
            }
        }

        public IList<pxFrenteCaixaNota> GetRateiosPorFormaDePagamento(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                IQuery query = session.CreateSQLQuery(@"select FormaPagamento.ID as IDFormaDePagamento,
                                                               FormaPagamento.Nome as FormaDePagamento,
                                                               sum(CASE WHEN FormaPagamento.Nome = 'DINHEIRO' THEN PedidoParcela.Valor - (CASE WHEN FrenteCaixaNota.Troco < 0 THEN 0 ELSE FrenteCaixaNota.Troco END) ELSE PedidoParcela.Valor END) as ValorPago
	                                                        from PedidoParcela 
	                                                          inner join Pedido on (PedidoParcela.IDPedido = Pedido.ID)
	                                                          inner join FrenteCaixaNota on (Pedido.ID = FrenteCaixaNota.IDPedido)
	                                                          inner join TipoDocumento on (PedidoParcela.IDTipoDocumento = TipoDocumento.ID)
	                                                          inner join FormaPagamento on (TipoDocumento.IDFormaPagamento = FormaPagamento.ID)
                                                        where FrenteCaixaNota.IDFluxoCaixa = :IDFluxoCaixa
                                                          and Pedido.DtCancelamento IS NULL
                                                        group by FormaPagamento.ID, FormaPagamento.Nome").SetInt32("IDFluxoCaixa", IDFluxoCaixa);

                ConstructorInfo construtor = typeof(pxFrenteCaixaNota).GetConstructor(new Type[] { typeof(int), typeof(string), typeof(decimal) });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxFrenteCaixaNota>();
            }
        }

        public pxyDetalhesFechamento GetDetalhesFechamento(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                IQuery query = session.CreateSQLQuery(@"select 
                                (Select Coalesce(SUM(IIF(TT.Ent_Sai=2,1,0)),0) from FrenteCaixaNota FF Join TipoPedido TT on TT.ID = FF.IdTipoPedido AND FF.IDFluxoCaixa = :IDFluxoCaixa) TotalVendas,
                                (Select Coalesce(SUM(IIF(TT.Ent_Sai=1,1,0)),0) from FrenteCaixaNota FF Join TipoPedido TT on TT.ID = FF.IdTipoPedido AND FF.IDFluxoCaixa = :IDFluxoCaixa) TotalDevolucoes,
                                  (Select Coalesce(SUM(IIF(TT.Ent_Sai=1,FF.TotalGeral,0)),0) from FrenteCaixaNota FF Join TipoPedido TT on TT.ID = FF.IdTipoPedido AND FF.IDFluxoCaixa = :IDFluxoCaixa)CreditoGerado,
                                Coalesce(Count(IIF(T.Ent_Sai=1,FI.ID,null)),0) ItensDevolvidos
                                from FrenteCaixaNota F
                                Join FrenteCaixaNotaItem FI on FI.IDFrenteCaixaNota = F.ID
                                Join TipoPedido T on T.ID = F.IdTipoPedido
                                Where F.IDFluxoCaixa = :IDFluxoCaixa
                                And F.Cancelado =0;").SetInt32("IDFluxoCaixa", IDFluxoCaixa);

                ConstructorInfo construtor = typeof(pxyDetalhesFechamento).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(decimal), typeof(int) });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyDetalhesFechamento>().FirstOrDefault();
            }
        }

        public List<FrenteCaixaNota> GetNotasPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                /*return session.CreateQuery(@"SELECT new FrenteCaixaNota(DescFormaPagamento, SUM(TotalPago))
                                               FROM  FrenteCaixaNota
                                             WHERE IDFluxoCaixa = :IDFluxoCaixa
                                               GROUP BY DescFormaPagamento").SetInt32("IDFluxoCaixa", IDFluxoCaixa).List<FrenteCaixaNota>().ToList();*/

                return session.CreateQuery(@"SELECT new FrenteCaixaNota(FormasPagamento.Nome, SUM(Parcelas.Valor))
                                               FROM NotaParcela Parcelas
                                                 left join Parcelas.Nota Notas
                                                 left join Notas.FrenteCaixaNotas FrenteDeCaixaNotas
                                                 left join Parcelas.TipoDocumento Documentos
                                                 left join Documentos.FormaPagamento FormasPagamento
                                               WHERE FrenteDeCaixaNotas.IDFluxoCaixa = :IDFluxoCaixa
                                                   and FrenteDeCaixaNotas.IDPedido not in (select id from Pedido where DtCancelamento IS NOT NULL)
                                             group by FormasPagamento.Nome").SetInt32("IDFluxoCaixa", IDFluxoCaixa).List<FrenteCaixaNota>().ToList();
            }
        }

        public IList<pxSangriaCaixa> GetSangrias(DateTime dtInicio, DateTime dtFinal)
        {
            using (var session = GetSession())
            {

                // " JOIN NotaItemServico nis on nis.IDNota = n.ID " +
                IQuery query = session.CreateSQLQuery(@"select DISTINCT sc.ID as IDSangriaCaixa,
                                                            cw.Nome as Usuario,
                                                            fc.DataAbertura,
                                                            fc.DataFechamento,
                                                            sc.DataSangria,
                                                            sc.Valor as ValorSangria,
                                                            sc.Observacao,
                                                            CASE WHEN sc.Retirada = 1 then 'DESPESA' ELSE 'SANGRIA' END AS Tipo
                                                       from SangriaCaixa sc
                                                          inner join FluxoCaixa fc on sc.IDFluxoCaixa = fc.ID
                                                     	  inner join cw_usuario cw on fc.IDUsuario = cw.id

                                                        order by sc.DataSangria desc");
                //.SetDateTime("dtInicio", dtInicio)
                //.SetDateTime("dtFinal", dtFinal);

                ConstructorInfo construtor = typeof(pxSangriaCaixa).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(string),
                                                    typeof(DateTime?),
                                                    typeof(DateTime?),
                                                    typeof(DateTime?),
                                                    typeof(decimal),
                                                    typeof(string),
                                                    typeof(string)
                                                    });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxSangriaCaixa>();
            }
        }
    }
}
