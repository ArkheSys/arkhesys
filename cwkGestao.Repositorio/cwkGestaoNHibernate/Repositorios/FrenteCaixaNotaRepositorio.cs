using System;
using System.Collections.Generic;
using System.Reflection;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;

using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FrenteCaixaNotaRepositorio : RepositorioAbstrato<FrenteCaixaNota>, IRepositorioFrenteCaixaNota
    {
        protected override string HqlLoadById()
        {
            return @"from FrenteCaixaNota f 
                     left join fetch f.Vendedor
                     left join fetch f.Pessoa
                     left join fetch f.Condicao
                     left join fetch f.ListaFrenteCaixaNotaItem
                     where f.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FrenteCaixaNota g";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                return session.CreateQuery("select max(f.Codigo) from FrenteCaixaNota f").UniqueResult<int>() + 1;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<pxPedidoParcelaFrenteCaixa> GetPagamentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                string hql = @"select FormaPagamento.Nome as FormaDePagamento,
                                      sum(CASE WHEN FormaPagamento.Nome = 'DINHEIRO' THEN PedidoParcela.Valor - (CASE WHEN FrenteCaixaNota.Troco < 0 THEN 0 ELSE FrenteCaixaNota.Troco END) ELSE PedidoParcela.Valor END) as Valor
	                               from PedidoParcela 
	                                 inner join Pedido on (PedidoParcela.IDPedido = Pedido.ID)
	                                 inner join FrenteCaixaNota on (Pedido.ID = FrenteCaixaNota.IDPedido)
	                                 inner join TipoDocumento on (PedidoParcela.IDTipoDocumento = TipoDocumento.ID)
	                                 inner join FormaPagamento on (TipoDocumento.IDFormaPagamento = FormaPagamento.ID)
                               where FrenteCaixaNota.IDFluxoCaixa = :IDFluxoCaixa
                                 and Pedido.DtCancelamento IS NULL
                               group by FormaPagamento.Nome";

                IQuery query = session.CreateSQLQuery(hql).SetInt32("IDFluxoCaixa", IDFluxoCaixa);

                ConstructorInfo construtor = typeof(pxPedidoParcelaFrenteCaixa).GetConstructor(new[]
                {
                    typeof(string),
                    typeof(decimal),
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxPedidoParcelaFrenteCaixa>();
            }
        }

        public IList<pxProdutosPedidoFrenteCaixa> GetProdutosPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT PESSOA.ID AS IDPESSOA,
                                      PESSOA.NOME AS PESSOA,
	                                  PRODUTO.ID AS IDPRODUTO,
	                                  PRODUTO.NOME AS PRODUTO,
	                                  PEDIDOITEM.UNIDADE,
	                                  SUM(PEDIDOITEM.QUANTIDADE) AS QUANTIDADE,
	                                  SUM(PEDIDOITEM.VALOR) AS VALOR,
	                                  SUM(PEDIDOITEM.TOTAL) AS TOTAL

	                               FROM PEDIDOITEM 
	                                 INNER JOIN PEDIDO ON (PEDIDOITEM.IDPEDIDO = PEDIDO.ID)
	                                 INNER JOIN PESSOA ON (PEDIDO.IDPESSOA = PESSOA.ID)
	                                 INNER JOIN PRODUTO ON (PEDIDOITEM.IDPRODUTO = PRODUTO.ID)
	                                 INNER JOIN FRENTECAIXANOTA ON (PEDIDO.ID = FRENTECAIXANOTA.IDPEDIDO)
	                               
                               WHERE FRENTECAIXANOTA.IDFLUXOCAIXA = :IDFluxoCaixa
                                 AND PEDIDO.DTCANCELAMENTO IS NULL
                               GROUP BY PESSOA.ID, PESSOA.NOME, PRODUTO.ID, PRODUTO.NOME, PEDIDOITEM.UNIDADE";

                IQuery query = session.CreateSQLQuery(hql).SetInt32("IDFluxoCaixa", IDFluxoCaixa);

                ConstructorInfo construtor = typeof(pxProdutosPedidoFrenteCaixa).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxProdutosPedidoFrenteCaixa>();
            }
        }

        public IList<pxClientesEPagamentosPedidoFrenteCaixa> GetClientesEPagamentosPorFluxoCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT FORMAPAGAMENTO.ID AS IDFORMAPAGAMENTO,
                                      FORMAPAGAMENTO.NOME AS FORMAPAGAMENTO,
                               
	                                  CONCAT(PEDIDO.NUMERO, '/', PESSOA.ID) AS IDPESSOA,
                                      CONCAT(PEDIDO.NUMERO, '/', PESSOA.NOME) AS PESSOA,
                               
                                      -- SUM(PEDIDOPARCELA.VALOR) AS VALOR
                                      -- SUM(FRENTECAIXANOTA.TOTALPAGO - (CASE WHEN FRENTECAIXANOTA.TROCO < 0 THEN 0 ELSE FRENTECAIXANOTA.TROCO END)) AS VALOR
                                      sum(CASE WHEN FormaPagamento.Nome = 'DINHEIRO' THEN PedidoParcela.Valor - (CASE WHEN FrenteCaixaNota.Troco < 0 THEN 0 ELSE FrenteCaixaNota.Troco END) ELSE PedidoParcela.Valor END) AS VALOR
                                FROM PEDIDO
                                   INNER JOIN PEDIDOPARCELA ON (PEDIDO.ID = PEDIDOPARCELA.IDPEDIDO)
                                   INNER JOIN TIPODOCUMENTO ON (PEDIDOPARCELA.IDTIPODOCUMENTO = TIPODOCUMENTO.ID)
                                   INNER JOIN FORMAPAGAMENTO ON (TIPODOCUMENTO.IDFORMAPAGAMENTO = FORMAPAGAMENTO.ID)
                                   INNER JOIN PESSOA ON (PEDIDO.IDPESSOA = PESSOA.ID)
                                   INNER JOIN FRENTECAIXANOTA ON (PEDIDO.ID = FRENTECAIXANOTA.IDPEDIDO)
                                WHERE FRENTECAIXANOTA.IDFLUXOCAIXA = :IDFluxoCaixa
                                   AND PEDIDO.DTCANCELAMENTO IS NULL
                               GROUP BY PESSOA.ID, PESSOA.NOME, FORMAPAGAMENTO.ID, FORMAPAGAMENTO.NOME, PEDIDO.NUMERO";
                IQuery query = session.CreateSQLQuery(hql).SetInt32("IDFluxoCaixa", IDFluxoCaixa);
                ConstructorInfo construtor = typeof(pxClientesEPagamentosPedidoFrenteCaixa).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(decimal)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxClientesEPagamentosPedidoFrenteCaixa>();
            }
        }

        public IList<FrenteCaixaNotaItem> GetItensPorFrenteCaixaNotaItem(int IDFrenteCaixaNotaItem)
        {
            using (var session = GetSession())
            {
                string hql = " from FrenteCaixaNotaItem ni"
                             + " left join fetch ni.FrenteCaixaNota n"
                             + " left join fetch ni.Produto"
                             + " where n.ID = :id";

                return session.CreateQuery(hql).SetInt32("id", IDFrenteCaixaNotaItem).List<FrenteCaixaNotaItem>();
            }
        }

        public IList<FrenteCaixaNotaItem> GetItensCancelados(int IDFrenteCaixaNota)
        {
            using (var session = GetSession())
            {
                string hql = " from FrenteCaixaNotaItem ni"
                             + " left join fetch ni.FrenteCaixaNota n"
                             + " left join fetch ni.Produto"
                             + " where n.ID = :id AND ni.Cancelado = 1";

                return session.CreateQuery(hql).SetInt32("id", IDFrenteCaixaNota).List<FrenteCaixaNotaItem>();
            }
        }

        public bool SeItensCancelados(int idFrenteCaixaNota)
        {
            using (var session = GetSession())
            {
                string sql = "SELECT COUNT(*) as Retorno FROM FrenteCaixaNotaItem ni WHERE ni.IdFrenteCaixaNota = :idFrenteCaixaNota AND ni.Cancelado = 1";
                var valor = session.CreateSQLQuery(sql).SetInt32("idFrenteCaixaNota", idFrenteCaixaNota).UniqueResult<int>();

                return valor > 0;
            }
        }

        public IList<pxyItemFrenteCaixaPorFilial> GetItensPorFilial(int? IDTabelaPreco, DateTime? DataInicio, DateTime? DataFim, string CodigoInicial, string CodigoFinal, string OperadorInicial, string OperadorFinal)
        {
            using (var session = GetSession())
            {
                List<string> filtros = new List<string>();
                if (DataInicio.HasValue && DataFim.HasValue)
                    filtros.Add("cast(FrenteCaixaNota.Data as date) between :dtInicio and :dtFim");

                if (IDTabelaPreco.HasValue)
                    filtros.Add("TabelaPreco.ID = " + IDTabelaPreco);

                if (!string.IsNullOrEmpty(CodigoInicial) && !string.IsNullOrEmpty(CodigoFinal))
                    filtros.Add($"Produto.Codigo >= '{CodigoInicial}' and Produto.Codigo <= '{CodigoFinal}'");

                if (!string.IsNullOrEmpty(OperadorInicial) && !string.IsNullOrEmpty(OperadorFinal))
                    filtros.Add($"Vendedor.Codigo >= '{OperadorInicial}' and Vendedor.Codigo <= '{OperadorFinal}'");

                string hql = @"select distinct
                                      Filial.ID as IDFilial,
	                                  Filial.Nome as Filial,
	                                  Vendedor.ID as IDOperador,
	                                  Vendedor.Nome as Operador,
	                                  FrenteCaixaNota.Data as DataCadastro,
	                                  Produto.ID as IDProduto,
	                                  Produto.Codigo,
	                                  Produto.Nome,
	                                  TabelaPreco.ID as IDTabelaPreco,
	                                  TabelaPreco.Nome as TabelaPreco,
	                                  FrenteCaixaNotaItem.Quantidade,
	                                  FrenteCaixaNotaItem.Total
                                 from FrenteCaixaNotaItem
                                   inner join FrenteCaixaNota on (FrenteCaixaNotaItem.IdFrenteCaixaNota = FrenteCaixaNota.ID)
	                               inner join Produto on (FrenteCaixaNotaItem.IdProduto = Produto.ID)

	                               inner join Pessoa Vendedor on (FrenteCaixaNota.IDVendedor = Vendedor.ID)
	                               inner join Nota on (FrenteCaixaNota.IDNota = Nota.ID)
	                               inner join Filial on (Nota.IDFilial = Filial.ID)
	                               inner join TabelaPreco on (Nota.IDTabelaPreco = TabelaPreco.ID)
                               where 1=1 " + (filtros.Count > 0 ? " and " + string.Join(" and ", filtros) : "");

                IQuery query = session.CreateSQLQuery(hql);
                if (DataInicio.HasValue && DataFim.HasValue)
                {
                    query.SetDateTime("dtInicio", DataInicio.Value.Date);
                    query.SetDateTime("dtFim", DataFim.Value.Date);
                }

                ConstructorInfo construtor = typeof(pxyItemFrenteCaixaPorFilial).GetConstructor(new[]
                {
                     typeof(int),
                     typeof(string),
                     typeof(int),
                     typeof(string),
                     typeof(DateTime),
                     typeof(int),
                     typeof(string),
                     typeof(string),
                     typeof(int),
                     typeof(string),
                     typeof(decimal),
                     typeof(decimal)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyItemFrenteCaixaPorFilial>();
            }
        }

        public IList<pxyFrenteCaixaNotaItemCancelado> GetVendasCanceladasPDV(int? IDFilial, DateTime? DataInicio, DateTime? DataFim)
        {
            using (var session = GetSession())
            {
                List<string> filtros = new List<string>();
                if (DataInicio.HasValue && DataFim.HasValue)
                    filtros.Add("cast(FrenteCaixaNota.Data as date) between :dtInicio and :dtFim");

                if (IDFilial.HasValue)
                    filtros.Add("FrenteCaixaNota.IDFilial = " + IDFilial.Value);

                string hql = @"select distinct FrenteCaixaNota.Codigo,
                                               FrenteCaixaNota.IncUsuario as operador,
                                               FrenteCaixaNota.GerenteDesconto as Gerente,
                                               FrenteCaixaNota.DescricaoPDV as NumeroPDV,
                                               FrenteCaixaNotaItem.MotivoCancelamento,
                                               FrenteCaixaNota.TotalGeral
                                 from FrenteCaixaNota
                                   inner join FrenteCaixaNotaItem on (FrenteCAixaNota.Id = FrenteCaixaNotaItem.IdFrenteCaixaNota)
                               where FrenteCaixaNota.Cancelado = 1 " + (filtros.Count > 0 ? " and " + string.Join(" and ", filtros) : "");

                IQuery query = session.CreateSQLQuery(hql);
                if (DataInicio.HasValue && DataFim.HasValue)
                {
                    query.SetDateTime("dtInicio", DataInicio.Value.Date);
                    query.SetDateTime("dtFim", DataFim.Value.Date);
                }

                ConstructorInfo construtor = typeof(pxyFrenteCaixaNotaItemCancelado).GetConstructor(new[]
                {
                     typeof(int),
                     typeof(string),
                     typeof(string),
                     typeof(string),
                     typeof(string),
                     typeof(decimal)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyFrenteCaixaNotaItemCancelado>();
            }
        }
    }
}
