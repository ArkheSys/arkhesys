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
    partial class LocalEstoqueRepositorio : RepositorioAbstrato<LocalEstoque>, IRepositorioLocalEstoque
    {
        protected override string HqlLoadById()
        {
            return "from LocalEstoque n join fetch n.Filial f where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from LocalEstoque n join fetch n.Filial f";
        }

        public IList<pxyListaEstoque> GetListaEstoque(int idLocalEstoque)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT	Pro.Codigo AS CodigoProduto
		                                , Pro.Nome AS NomeProduto
		                                , U.Sigla AS Unidade
		                                , (SELECT dbo.f_SaldoEstoqueLocalEstoque(:idLocalEstoque, Pro.ID)) AS EstoqueFisico
		                                , (SELECT dbo.f_PedSaldoEstoqueLocalEstoque(:idLocalEstoque, Pro.ID)) AS QtdPedido
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
                                LEFT JOIN Estoque Est ON Est.IDProduto = Pro.ID AND Est.IDFilial = 1
                                LEFT JOIN GrupoEstoque Gr ON Gr.ID = Pro.IDGrupoEstoque
                                LEFT JOIN Grupo1 Gr1 ON Gr1.Id = Pro.IDGrupo1
                                LEFT JOIN Grupo2 Gr2 ON Gr2.Id = Pro.IDGrupo2
                                LEFT JOIN Grupo3 Gr3 ON Gr3.Id = Pro.IDGrupo3";

                IQuery query = session.CreateSQLQuery(hql).SetInt32("idLocalEstoque", idLocalEstoque);
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

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
