using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ConversaoUnidadeRepositorio : RepositorioAbstrato<ConversaoUnidade>, IRepositorioConversaoUnidade
    {
        protected override string HqlLoadById()
        {
            return "from ConversaoUnidade n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ConversaoUnidade n";
        } 

        private ConstructorInfo GetContructorInfoPxyConversaoUnidade()
        {
            return typeof(pxyConversaoUnidade).GetConstructor(new Type[] {        
                                                    typeof(Int32),  //idConversaoUnidade
                                                    typeof(String),  //CodigoConversaoUnidade
                                                    typeof(String), // Descricao
                                                    typeof(Int32),  // idUnEntrada
                                                    typeof(String), // SiglaUnEntrada
                                                    typeof(Int32),  // idUnEstoque
                                                    typeof(String), //SiglaUnEstoque
                                                    typeof(Decimal)});//FatorConversao
        }

        public IList<pxyConversaoUnidade> GetConversaoUnidadeProduto(int idProduto)
        {
            using (var session = GetSession())
            {
                string Sql = @"select d.id, d.codigo, d.Descricao, d.IdUnEntrada, uEnt.Sigla SlUnEntrada, d.IdUnEstoque, uEst.Sigla SlUnEstoque, d.FatorConversao
                                  from (
											/*Unidades de conversão ligadas no produto*/
			                                Select cu.ID, cu.Codigo, p.id idproduto, cu.Descricao Descricao, cu.IDUnidadeEnt IdUnEntrada, cu.IDUnidadeSai IdUnEstoque, cu.FatorConversao
			                                  from Produto as p
			                                 inner join ProdutoConversao as pc on pc.IDProduto = p.ID
			                                 inner join ConversaoUnidade as cu on pc.IDConversaoUnidade = cu.ID and cu.Padrao = 0
			                                 where p.id = :idProduto
											 union
											/*Unidades de conversões padrões que podem ser utilizadas para o produto*/
										    Select cu.ID, cu.Codigo, p.id idproduto, cu.Descricao Descricao, cu.IDUnidadeEnt IdUnEntrada, cu.IDUnidadeSai IdUnEstoque, cu.FatorConversao
			                                  from Produto as p
			                                 inner join ConversaoUnidade as cu on cu.IDUnidadeSai = p.IDUnidade and cu.Padrao = 1
			                                 where p.id = :idProduto
		                                     union
										    /*Cria conversao padrao da unidade de estoque para unidade de estoque para quando não utilizar Conversao de unidade*/
		                                    select null id,null Codigo,p.id idproduto, 'Conversão padrão de mesma unidade' Descricao, p.IDUnidade IdUnEntrada, p.IDUnidade IDUnEstoque, 1 FatorConversao
			                                  from Produto as p
			                                 where p.id = :idProduto
			                                   and not exists (Select 1
																 from ProdutoConversao as pc
															    inner join ConversaoUnidade as cu on cu.IDUnidadeSai = p.IDUnidade AND CU.IDUnidadeEnt = P.IDUnidadeEntrada
															    where pc.IDProduto = p.ID
																union
															   Select 1
															     from ConversaoUnidade as cu
																where cu.IDUnidadeSai = p.IDUnidade and cu.IDUnidadeEnt = p.IDUnidadeEntrada and cu.Padrao = 1)
	                                   ) d
                                   inner join Unidade as uEnt on d.IdUnEntrada = uEnt.ID
                                   inner join Unidade as uEst on d.IDUnEstoque = uest.ID";

                ConstructorInfo construtor = GetContructorInfoPxyConversaoUnidade();

                IQuery query = session.CreateSQLQuery(Sql.ToString()).SetInt32("idProduto", idProduto);
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyConversaoUnidade>();
            }
        }

        public bool ValidaExistenciaConversao(Unidade unidadeEntrada, Unidade unidadeSaida)
        {
            using (var session = GetSession())
            {
                string hql = @"select conversao
                               from ConversaoUnidade conversao
                               where conversao.UnidadeSai = :unidadeSaida
                               and conversao.UnidadeEnt = :unidadeEntrada
                               and conversao.UnidadeSai <> conversao.UnidadeEnt
                               and conversao.Padrao = 1";

                var resultado = session.CreateQuery(hql).SetParameter("unidadeEntrada",unidadeEntrada).SetParameter("unidadeSaida",unidadeSaida).List<ConversaoUnidade>();

                if (resultado.Count >= 1 || unidadeEntrada.ID == unidadeSaida.ID)
                    return true;
                else
                    return false;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
