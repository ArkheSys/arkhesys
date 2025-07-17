using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;

using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TabelaPrecoRepositorio : RepositorioAbstrato<TabelaPreco>, IRepositorioTabelaPreco
    {
        protected override string HqlLoadById()
        {
            return "from TabelaPreco t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from TabelaPreco t";
        }

        public IList<TabelaPreco> hqlGetAllAtiva()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + "where bAtiva = 1";

                return session.CreateQuery(hql).List<TabelaPreco>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<pxyProdutoTabelaPreco> GetProdutosPorTabelaPreco(int? IDTabelaPreco, int? CodigoInicial, int? CodigoFinal, bool? ProdutosAtivos)
        {
            using (var session = GetSession())
            {
                var filtros = new List<string>();

                if (IDTabelaPreco.HasValue)
                    filtros.Add($"TabelaPreco.ID = {IDTabelaPreco.Value}");

                if (CodigoInicial.HasValue && CodigoFinal.HasValue)
                    filtros.Add($"(Produto.Codigo >= {CodigoInicial.Value} and Produto.Codigo <= {CodigoFinal.Value})");

                if (ProdutosAtivos.HasValue)
                    filtros.Add($"Produto.Inativo = {(ProdutosAtivos.Value ? "0" : "1")}");

                return session.CreateSQLQuery(
                             $@"select TabelaPreco.ID as IDTabelaPreco,
                                	   TabelaPreco.Nome as TabelaPreco,
                                	   Produto.ID as IDProduto,
                                       Produto.Codigo,
                                	   Produto.Nome as Produto,
                                	   Unidade.Sigla as Unidade,
                                	   Produto.PrecoBase
                                  from TabelaPreco
                                    inner join TabelaPrecoProduto on (TabelaPreco.ID = TabelaPrecoProduto.IDTabelaPreco)
                                	inner join Produto on (TabelaPrecoProduto.IDProduto = Produto.ID)
                                	 left join Unidade on (Produto.IDUnidade = Unidade.ID)
                                  WHERE 1=1 " + (filtros.Count > 0 ? " AND " + string.Join(" AND ", filtros) : "")
                    ).SetResultTransformer(Transformers.AliasToBeanConstructor(typeof(pxyProdutoTabelaPreco).GetConstructor(new[] {
                        typeof(int),
                        typeof(string),
                        typeof(int),
                        typeof(string),
                        typeof(string),
                        typeof(string),
                        typeof(decimal)
                }))).List<pxyProdutoTabelaPreco>();
            }
        }
    }
}
