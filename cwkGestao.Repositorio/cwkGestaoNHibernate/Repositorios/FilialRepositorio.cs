using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System.Reflection;
using NHibernate.Transform;
#pragma warning disable CS0105 // A diretiva using para "cwkGestao.Modelo.Proxy" apareceu anteriormente neste namespace
using cwkGestao.Modelo.Proxy;
#pragma warning restore CS0105 // A diretiva using para "cwkGestao.Modelo.Proxy" apareceu anteriormente neste namespace

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FilialRepositorio : RepositorioAbstrato<Filial>, IRepositorioFilial
    {
        protected override string HqlLoadById()
        {
            return "from Filial f" +
                  " left join fetch f.Cidade " +
                  " left join fetch f.Empresa " +
                  " left join fetch f.TabelaFaixaFatSimples " +
                  " where f.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Filial f" +
                  " left join fetch f.Cidade " +
                  " left join fetch f.Empresa ";
        }

        public Modelo.Filial GetFilialPrincipal()
        {
            using (var session = GetSession())
            {
                string hql = "from Filial f order by f.Codigo";
                var resultado = session.CreateQuery(hql).SetMaxResults(1).List<Modelo.Filial>();
                if (resultado.Count > 0)
                    return resultado[0];
                return null;
            }
        }

        public Modelo.Filial GetPorTipo(string tipo)
        {
            using (var session = GetSession())
            {
                string hql = "from Filial f where f.Tipo = :tipo";
                var resultado = session.CreateQuery(hql).SetString("tipo",tipo).SetMaxResults(1).List<Modelo.Filial>();
                 if (resultado.Count > 0)
                    return resultado[0];
                return null;
            }
        }

        public Modelo.Filial GetFilialbyId(int IdFilial)
        {
            using (var session = GetSession())
            {
                string hql = "from Filial f where f.id = :Id";
               
                var resultado = session.CreateQuery(hql).SetInt32("Id", IdFilial).SetMaxResults(1).List<Modelo.Filial>();
               
                if (resultado.Count > 0)
                    return resultado[0];
               
                return null;
            }
        }

        public IList<cwkGestao.Modelo.Filial> GetEmpresasDoGrupo(int IDGrupo)
        {
            using (var session = GetSession())
            {
                string hql = "from Filial f where f.Empresa.ID = :IDGrupo order by f.Codigo";
                var resultado = session.CreateQuery(hql).SetInt32("IDGrupo", IDGrupo).List<cwkGestao.Modelo.Filial>();
                if (resultado.Count > 0)
                    return resultado;
                return null;
            }
        }

        public IList<pxyListaEstoque> GetListaEstoque(int idFilial)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT	Pro.Codigo AS CodigoProduto
		                                , Pro.Nome AS NomeProduto
		                                , U.Sigla AS Unidade
		                                , (SELECT dbo.f_SaldoEstoqueFilial(:idFilial, Pro.ID)) AS EstoqueFisico
		                                , (SELECT dbo.f_PedSaldoEstoqueFilial(:idFilial, Pro.ID)) AS QtdPedido
		                                , Est.Qtd_Estoque_Min AS EstMinimo
		                                , Pro.Barra AS CodBarra
		                                , Pro.BarraFornecedor AS CodBarraFornecedor
		                                , Gr.Nome AS GrupoEstoque
		                                , Pro.NCM AS NCMProduto
		                                , Gr1.Nome AS Grupo1
		                                , Gr2.Nome AS Grupo2
		                                , Gr3.Nome AS Grupo3
                                FROM Produto Pro
                                INNER JOIN Unidade U ON U.ID = Pro.IDUnidade
                                LEFT JOIN Estoque Est ON Est.IDProduto = Pro.ID AND Est.IDFilial = :idFilial
                                LEFT JOIN GrupoEstoque Gr ON Gr.ID = Pro.IDGrupoEstoque
                                LEFT JOIN Grupo1 Gr1 ON Gr1.Id = Pro.IDGrupo1
                                LEFT JOIN Grupo2 Gr2 ON Gr2.Id = Pro.IDGrupo2
                                LEFT JOIN Grupo3 Gr3 ON Gr3.Id = Pro.IDGrupo3";

                IQuery query = session.CreateSQLQuery(hql).SetInt32("idFilial", idFilial);
                ConstructorInfo construtor = typeof(pxyListaEstoque).GetConstructor(new Type[] {
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string)});

                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer).List<pxyListaEstoque>();
            }
        }

        public IList<pxyEstoqueInventario> GetEstoquesInventario(Filial filial, DateTime data, TipoValorProduto tipo)
        {
            using (var session = GetSession())
            {
                String hql = @"select Produto.ID,";
                if (tipo == TipoValorProduto.UltimoCusto)
                    hql += " Produto.UltimoCusto as valor,";
                else if (tipo == TipoValorProduto.CustoMedio)
                    hql += " dbo.f_BuscaCustoMedio (:idFilial, Produto.ID, :data, 0) as valor,";
                hql += @" dbo.f_SaldoEstoqueFilialData (:idFilial, Produto.ID, :data, 0) as quantidade
                  from Produto";
                IQuery query = session.CreateSQLQuery(hql)                                      
                                      .SetInt32("idFilial", filial.ID)
                                      .SetDateTime("data", data);
                ConstructorInfo construtor = typeof(pxyEstoqueInventario).GetConstructor(new Type[] {
                typeof(int),
                typeof(decimal),
                typeof(decimal),
                });
                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer)
                            .List<pxyEstoqueInventario>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
