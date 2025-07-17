using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PLPRepositorio : RepositorioAbstrato<PLP>, IRepositorioPLP
    {
        protected override string HqlLoadById()
        {
            return "from PLP n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PLP n";
        }

        public IList<pxyPLPDetalhe> GetListaPedidosParaPLP()
        {
            using (var session = GetSession())
            {
                string sql = @"select p.ID IdPedido,
                               p.Codigo,
                               fi.Nome nomeFilial, 
                               p.PessoaNome, 
                               rc.Letra + rc.Numero + 'BR' CodRastreamento, 
                               pe.Nome, 
                               co.Nome nomeCondicao,
                               p.TotalProduto, 
                               p.PercDesconto, 
                               p.PercComissao, 
                               p.ValorComissao,
                               p.ValorDesconto, 
                               p.ValorAcrescimo, 
                               p.TotalPedido,
                               0 
                         from RastreamentoCorreio rc
                         join CorreiosPrecoPrazo cpp on rc.ID = cpp.IDRastreamento
                         join Pedido p on p.ID = cpp.IDPedido
                         join Filial fi on fi.ID = rc.IDFilial
                         join Condicao co on co.ID = p.IDCondicao
                         join Pessoa pe on pe.ID = p.IDVendedor2
                         where p.Status = 2 and rc.SolicitadoWScorreios = 1
                         and p.ID not in (select pd.IDPedido from PLPDetalhe pd)";

                IQuery query = session.CreateSQLQuery(sql);

                ConstructorInfo construtor = typeof(pxyPLPDetalhe).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal), 
                typeof(decimal),
                typeof(decimal),
                typeof(int)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyPLPDetalhe>();
            }
        }

         public IList<pxyPLPDetalhe> GetListaPedidosParaPLPComIDPLP(int idPLP)
        {
            using (var session = GetSession())
            {
                string sql = @"select p.ID IdPedido,
                               det.IDPLP IDPLP,
                               det.ID IDPLPDet,
                               p.Codigo,
                               fi.Nome nomeFilial, 
                               p.PessoaNome, 
                               rc.Letra + rc.Numero + 'BR' CodRastreamento, 
                               pe.Nome, 
                               co.Nome nomeCondicao,
                               p.TotalProduto, 
                               p.PercDesconto, 
                               p.PercComissao, 
                               p.ValorComissao,
                               p.ValorDesconto, 
                               p.ValorAcrescimo, 
                               p.TotalPedido,
                               0 
                         from RastreamentoCorreio rc
                         join CorreiosPrecoPrazo cpp on rc.ID = cpp.IDRastreamento
                         join Pedido p on p.ID = cpp.IDPedido
                         join Filial fi on fi.ID = rc.IDFilial
                         join Condicao co on co.ID = p.IDCondicao
                         join Pessoa pe on pe.ID = p.IDVendedor2
                         join PLPDetalhe det on det.IDPedido = p.ID
                         where p.Status = 2 and rc.SolicitadoWScorreios = 1
                         and det.IDPLP = :idPLP";

                IQuery query = session.CreateSQLQuery(sql).SetInt32("idPLP",idPLP);

                ConstructorInfo construtor = typeof(pxyPLPDetalhe).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal), 
                typeof(decimal),
                typeof(decimal),
                typeof(int)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyPLPDetalhe>();
            }
        }

         protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
         {
             throw new NotImplementedException();
         }
    }
}
