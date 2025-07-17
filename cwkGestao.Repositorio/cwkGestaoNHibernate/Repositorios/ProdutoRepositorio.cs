using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoRepositorio : RepositorioAbstrato<Produto>, IRepositorioProduto
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from Produto p"
                  + " left join fetch p.GrupoEstoque"
                  + " left join fetch p.Estoque"
                  + " left join fetch p.Unidade"
                  + " left join fetch p.CFOP"
                  + " left join fetch p.Fornecedor"
                  + " left join fetch p.PlanoContaEstoque"
                  + " left join fetch p.TabelaPrecoProdutos"
                  + " left join fetch p.Produto_Icmss"
                  + " where p.ID = :id"
                  ;
        }

        public override Produto LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                Produto produto = session.Get<Produto>(id);
                if (produto == null)
                    return null;

                NHibernateUtil.Initialize(produto.GrupoEstoque);
                NHibernateUtil.Initialize(produto.Unidade);
                NHibernateUtil.Initialize(produto.CFOP);
                NHibernateUtil.Initialize(produto.Fornecedor);
                NHibernateUtil.Initialize(produto.TabelaPrecoProdutos);
                NHibernateUtil.Initialize(produto.PlanoContaEstoque);
                NHibernateUtil.Initialize(produto.Produto_Icmss);
                NHibernateUtil.Initialize(produto.Estoques);
                NHibernateUtil.Initialize(produto.ProdutoCodBarra);
                NHibernateUtil.Initialize(produto.ProdutoSKU);
                NHibernateUtil.Initialize(produto.Imeis);

                return produto;
            }
        }

        protected override string hqlGetAll()
        {
            return @"   select new Produto(produto.ID, produto.Codigo, produto.Nome, unidade
                        , produto.Barra, produto.BarraFornecedor, grupo.Nome, produto.DescReduzida
                        , produto.CaminhoImagem, grupo.ClassificacaoProduto, produto.NCM, grupo1.Nome
                        , grupo2.Nome, grupo3.Nome, produto.IncData, produto.IncUsuario, produto.AltData, produto.AltUsuario
                        , produto.Inativo
                        , produto.Observacao
                        , produto.Localizacao
                        , produto.UtilizarIMEI
                        , produto.EscRelevante
                        , produto.RAZAO_Fab
                        , produto.CNPJ_Fab
                        , produto.CBenef
                        , produto.PrecoBase
                        , forn.Nome
                        , produto.DtCadastroProduto
                        , produto.CodigoReferencia
                        , produto.DataValidade
                        , produto.CodigoFabricante
                        , produto.CodigoOriginal
                        , produto.Aplicacao) 
                        from Produto produto 
                    join produto.Unidade unidade 
                    left join produto.GrupoEstoque grupo
                    left join produto.Grupo1 grupo1  
                    left join produto.Grupo2 grupo2
                    left join produto.Grupo3 grupo3
                    left join produto.Fornecedor forn";
        }

        public IList<Produto> GetAllAtivos()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + " where produto.Inativo = false OR produto.Inativo = null";

                return session.CreateQuery(hql).List<Produto>();
            }
        }

        public IList<Produto> GetAllInativos()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + " where produto.Inativo = true";

                return session.CreateQuery(hql).List<Produto>();
            }
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "select max(cast(Codigo as int)) from Produto;";
                    return Convert.ToInt32(session.CreateQuery(hql).UniqueResult<int>()) + 1;
                }
                catch
                {
                    return 1;
                }
            }
        }

        public string MaxCodigoIntegracao()
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "select p from Produto p where p.ID = (select max(ID) from Produto)";
                    return Convert.ToString(Convert.ToInt64(session.CreateQuery(hql).UniqueResult<Produto>().ID) + 1);
                }
                catch
                {
                    return "1";
                }
            }
        }

        public int AtualizaProdutos(GrupoEstoque grupo, String campo, String valor)
        {
            if (campo == "CST") campo = "Tributacao";
            String hql = " update Produto p set " + campo + " = " + valor + " where p.GrupoEstoque.ID = " + grupo.ID;
            using (ISession sessao = GetSession())
            {
                using (ITransaction trans = sessao.BeginTransaction())
                {
                    int regsModificados = sessao.CreateQuery(hql).ExecuteUpdate();
                    trans.Commit();
                    return regsModificados;
                }
            }
        }

        public List<TabelaPreco> GetTabelasPreco(Produto produto)
        {
            using (var session = GetSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(TabelaPrecoProduto));
                Example exemplo = Example.Create(new TabelaPrecoProduto() { Produto = produto });
                IList<TabelaPrecoProduto> tabelasPrecoProduto = criteria.Add(exemplo
                                        .ExcludeNone()
                                        .EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase()
                                        .ExcludeZeroes())
                                    .List<TabelaPrecoProduto>();
                HashSet<TabelaPreco> tabelasPreco = new HashSet<TabelaPreco>();
                foreach (TabelaPrecoProduto item in tabelasPrecoProduto)
                {
                    tabelasPreco.Add(item.TabelaPreco);
                }
                return tabelasPreco.ToList();
            }
        }

        public TabelaPrecoProduto GetTabelaPrecoProduto(TabelaPreco tabelaPreco, Produto produto)
        {
            using (var session = GetSession())
            {
                string hql = "from TabelaPrecoProduto tpp " +
                             " where tpp.Produto.ID = :idProduto " +
                             " and tpp.TabelaPreco.ID = :idTabelaPreco ";

                var retorno = session.CreateQuery(hql).SetInt32("idProduto", produto.ID).SetInt32("idTabelaPreco", tabelaPreco.ID).UniqueResult<TabelaPrecoProduto>();

                return retorno;

            }
        }

        public Produto GetProdutoByCodigo(String codigo)
        {
            using (var session = GetSession())
            {
                string hql = "from Produto p " +
                             " left join fetch p.Unidade " +
                             " where p.Codigo = :codigo ";

                var produto = session.CreateQuery(hql).SetString("codigo", codigo).UniqueResult<Produto>();
                if(produto != null)
                {
                    NHibernateUtil.Initialize(produto.Estoques);
                    NHibernateUtil.Initialize(produto.ProdutoCodBarra);
                }

                return produto;
            }
        }
        public Produto GetProdutoByDescricao(String descricao)
        {
            using (var session = GetSession())
            {
                string hql = "from Produto p " +
                             " left join fetch p.Unidade " +
                             " where p.Nome = :nome ";

                return session.CreateQuery(hql).SetString("nome", descricao).UniqueResult<Produto>();
            }
        }

        public IList<Produto> GetProdutoByCodigoString(string p)
        {
            using (var session = GetSession())
            {
                string hql = @" select new Produto(produto.ID, produto.Codigo, produto.Nome, unidade, unidadeEntrada , produto.Barra, produto.BarraFornecedor, grupo.Nome, produto.DescReduzida, produto.CaminhoImagem, produto.PrecoBase, produto.NCM, grupo1.Nome, grupo2.Nome, grupo3.Nome, produto.Inativo, produto.Observacao, produto.Localizacao, produto.UtilizarIMEI, produto.EscRelevante, produto.RAZAO_Fab, produto.CNPJ_Fab, produto.CBenef)
                               from Produto produto 
                               left join produto.Unidade unidade 
                               left join produto.UnidadeEntrada unidadeEntrada 
                               left join produto.GrupoEstoque grupo
                               left join produto.Grupo1 grupo1  
                               left join produto.Grupo2 grupo2
                               left join produto.Grupo3 grupo3
                               where produto.Codigo like :barrrr 
                               or produto.Nome like :barrrr";

                var produtos = session.CreateQuery(hql).SetString("barrrr", p + "%").List<Produto>();
                return produtos;
            }
        }

        public IList<object> GetRelatorio(Modelo.TipoRelatorioProduto tipo, string inicial, string final, bool codigoStr, int Status)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select p.ID");
                hql.AppendLine(", p.Codigo");
                hql.AppendLine(", p.Nome");
                hql.AppendLine(", p.NCM");
                hql.AppendLine(", u.Nome as Unidade");
                hql.AppendLine(", g.Nome as Grupo");
                hql.AppendLine(", p.Barra");
                hql.AppendLine("from Produto p join");
                hql.AppendLine("p.GrupoEstoque g");
                hql.AppendLine("join p.Unidade u");
                hql.AppendLine("where 1 = 1");
                switch (Status)
                {
                    // Ativo
                    case 0:
                        hql.AppendLine("and inativo = 0");
                        break;

                    // Inativo
                    case 1:
                        hql.AppendLine("and inativo = 1");
                        break;
                }
                switch (tipo)
                {
                    case TipoRelatorioProduto.Codigo:
                        if (codigoStr)
                        {
                            if (!String.IsNullOrEmpty(inicial))
                                hql.AppendLine("and p.Codigo >= '" + inicial + "'");
                            if (!String.IsNullOrEmpty(final))
                                hql.AppendLine("and p.Codigo <= '" + final + "'");
                            hql.AppendLine("order by p.Codigo");
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(inicial))
                                hql.AppendLine("and cast(p.Codigo as Int64) >= " + inicial);
                            if (!String.IsNullOrEmpty(final))
                                hql.AppendLine("and cast(p.Codigo as Int64) <= " + final);
                            hql.AppendLine("order by cast(p.Codigo as Int64)");
                        }

                        break;
                    case TipoRelatorioProduto.Nome:
                        if (!String.IsNullOrEmpty(inicial))
                            hql.AppendLine("and p.Nome >= '" + inicial + "'");
                        if (!String.IsNullOrEmpty(final))
                            hql.AppendLine("and p.Nome <= '" + final + "'");
                        hql.AppendLine("order by p.Nome");
                        break;
                }

                IQuery query = session.CreateQuery(hql.ToString());

                ConstructorInfo construtor = typeof(pxyRelProduto).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public IList<Produto> GetRelatorioPorGrupoEstoque(string classificacao)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from Produto p");
                hql.AppendLine("join fetch p.GrupoEstoque g");
                hql.AppendLine("join fetch p.Unidade u");
                hql.AppendLine("where g.Classificacao like :classificacao");
                IQuery query = session.CreateQuery(hql.ToString()).SetString("classificacao", classificacao + "%");
                return query.List<Produto>();
            }
        }

        public IList<Produto> FindByName(string p)
        {
            using (var session = GetSession())
            {
                string hql = @"   select new Produto(produto.ID, produto.Codigo, produto.Nome, unidade, unidadeEntrada, produto.Barra, produto.BarraFornecedor, grupo.Nome, produto.DescReduzida, produto.CaminhoImagem, produto.PrecoBase, produto.NCM, grupo1.Nome, grupo2.Nome, grupo3.Nome, produto.Inativo, produto.Observacao, produto.Localizacao, produto.UtilizarIMEI, produto.EscRelevante, produto.RAZAO_Fab, produto.CNPJ_Fab, produto.CBenef) 
                                from Produto produto 
                                join produto.Unidade unidade 
                                join produto.UnidadeEntrada unidadeEntrada
                                join produto.GrupoEstoque grupo 
                                left join produto.Grupo1 grupo1  
                                left join produto.Grupo2 grupo2
                                left join produto.Grupo3 grupo3
                                where produto.Nome like :nome or produto.Barra like :nome or produto.BarraFornecedor like :nome";

                return session.CreateQuery(hql).SetString("nome", p + "%").List<Produto>();
            }
        }

        public IList<Produto> FindByBarra(string p)
        {
            using (var session = GetSession())
            {
                string hql = @"   select new Produto(produto.ID, produto.Codigo, produto.Nome, unidade, unidadeEntrada, produto.Barra, produto.BarraFornecedor, grupo.Nome, produto.DescReduzida, produto.CaminhoImagem, produto.PrecoBase, produto.NCM, grupo1.Nome, grupo2.Nome, grupo3.Nome, produto.Inativo, produto.Observacao, produto.Localizacao, produto.UtilizarIMEI, produto.EscRelevante, produto.RAZAO_Fab, produto.CNPJ_Fab, produto.CBenef) 
                                from Produto produto 
                                join produto.Unidade unidade 
                                join produto.UnidadeEntrada unidadeEntrada
                                join produto.GrupoEstoque grupo 
                                left join produto.Grupo1 grupo1  
                                left join produto.Grupo2 grupo2
                                left join produto.Grupo3 grupo3
                                where produto.Barra like :barrrr ";

                return session.CreateQuery(hql).SetString("barrrr", p + "%").List<Produto>();
            }
        }

        public IList<Produto> CodigoDeBarrasExistente(string p, int IDProduto)
        {
            using (var session = GetSession())
            {
                string hql = @"   select new Produto(produto.ID, produto.Codigo, produto.Nome, unidade, unidadeEntrada, produto.Barra, produto.BarraFornecedor, grupo.Nome, produto.DescReduzida, produto.CaminhoImagem, produto.PrecoBase, produto.NCM, grupo1.Nome, grupo2.Nome, grupo3.Nome, produto.Inativo, produto.Observacao, produto.Localizacao, produto.UtilizarIMEI, produto.EscRelevante, produto.RAZAO_Fab, produto.CNPJ_Fab, produto.CBenef) 
                                from Produto produto 
                                join produto.Unidade unidade 
                                join produto.UnidadeEntrada unidadeEntrada
                                join produto.GrupoEstoque grupo 
                                left join produto.Grupo1 grupo1  
                                left join produto.Grupo2 grupo2
                                left join produto.Grupo3 grupo3
                                where produto.Barra like :barrrr 
                                  and produto.ID != :id";

                return session.CreateQuery(hql).SetString("barrrr", p + "%").SetInt32("id", IDProduto).List<Produto>();
            }
        }

        public Produto FindByCodigoBarra(string barra)
        {
            using (var session = GetSession())
            {
                string hql = @" select new Produto(produto.ID, produto.Codigo, produto.Nome, unidade, unidadeEntrada, produto.Barra, produto.BarraFornecedor, grupo.Nome, produto.DescReduzida, produto.CaminhoImagem, produto.PrecoBase, produto.NCM, grupo1.Nome, grupo2.Nome, grupo3.Nome, produto.Inativo, produto.Observacao, produto.Localizacao, produto.UtilizarIMEI, produto.EscRelevante, produto.RAZAO_Fab, produto.CNPJ_Fab, produto.CBenef) 
                                from Produto produto 
                                join produto.Unidade unidade
                                join produto.UnidadeEntrada unidadeEntrada
                                join produto.GrupoEstoque grupo 
                                left join produto.ProdutoCodBarra codBarra 
                                left join produto.Grupo1 grupo1  
                                left join produto.Grupo2 grupo2
                                left join produto.Grupo3 grupo3
                                where codBarra.CodigoBarra = :barrrr
                                Or produto.BarraFornecedor = :barrrr
                                Or produto.BarraFornecedor2 = :barrrr
                                Or produto.BarraFornecedor3 = :barrrr
                                Or produto.BarraFornecedor4 = :barrrr
                                Or produto.BarraFornecedor5 = :barrrr";

                return session.CreateQuery(hql).SetString("barrrr", barra).List<Produto>().FirstOrDefault();
            }
        }

        public Produto GetProdutoPorCodigoDeBarras(string Barra)
        {
            using (var session = GetSession())
            {
                string hql = @"from Produto p
                                 left join fetch p.Unidade
                               where p.Barra = :CodigoBarras
                                          or p.BarraFornecedor = :CodigoBarras
                                          or p.BarraFornecedor2 = :CodigoBarras
                                          or p.BarraFornecedor3 = :CodigoBarras
                                          or p.BarraFornecedor3 = :CodigoBarras
                                          or p.BarraFornecedor4 = :CodigoBarras
                                          or p.BarraFornecedor5 = :CodigoBarras";
                List<Produto> Produtos = session.CreateQuery(hql).SetString("CodigoBarras", Barra).List<Produto>().ToList();
                if (Produtos.Count > 1)
                    throw new ApplicationException("Mais de um produto com o código de barras informado");

                return Produtos.FirstOrDefault();
            }
        }

        public IList<Produto> FindByBarraObjCompleto(string p)
        {
            using (var session = GetSession())
            {
                string hql = @"select distinct p from Produto p
                                left join fetch p.GrupoEstoque
                                
                                left join fetch p.Unidade
                                left join fetch p.CFOP                  
                                left join fetch p.Fornecedor
                                left join fetch p.PlanoContaEstoque
                                left join fetch p.TabelaPrecoProdutos
                                left join fetch p.Produto_Icmss
                                where p.Barra like :barrrr";

                return session.CreateQuery(hql).SetString("barrrr", p + "%").List<Produto>();
            }
        }

        public IList<ImpressaoEtiquetaProdutoView> GetImpressaoEtiquetaProduto(int iDProduto)
        {
            String hql = @" select p.ID, e.Qtd_Estoque_Pedido, e.Qtd_Estoque_Fisico, 
                        e.Qtd_Estoque_Min, e.Qtd_Compra_Pedido, (e.Qtd_Estoque_Fisico - e.Qtd_Estoque_Pedido), p.Codigo, 
                        p.Barra, p.Nome, p.DescReduzida, u.Nome, ge.Nome, p.BarraFornecedor, p.NCM, g1.Nome, g2.Nome, 
                        g3.Nome, p.PrecoBase, 0
                        from Produto p 
                        join p.GrupoEstoque ge
                        join p.Unidade u
                        left join p.Grupo1 g1
                        left join p.Grupo2 g2
                        left join p.Grupo3 g3
                        left join p.Estoques e
                        where p.Barra is not empty";

            using (ISession sessao = GetSession())
            {
                IQuery query = sessao.CreateQuery(hql);

                ConstructorInfo construtor = typeof(ImpressaoEtiquetaProdutoView).GetConstructor(new Type[] {
                typeof(int),
                typeof(Decimal),
                typeof(Decimal),
                typeof(Decimal),
                typeof(Decimal),
                typeof(Decimal),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(Decimal),
                typeof(int)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<ImpressaoEtiquetaProdutoView>();
            }
        }

        public Hashtable GetProdutosImportados()
        {
            using (var session = GetSession())
            {
                string hql = @" select produto 
                                from Produto produto 
                                join produto.Unidade unidade 
                                join produto.GrupoEstoque grupo
                                left join produto.TabelaPrecoProdutos                                 
                                where produto.IDIntegracao > 0";

                var resultado = session.CreateQuery(hql).List<Produto>();
                Hashtable ret = new Hashtable();
                foreach (var item in resultado)
                {
                    ret.Add(item.ID, item);
                }
                return ret;
            }
        }

        public Hashtable GetProdutosImportadosPorCodigo()
        {
            using (var session = GetSession())
            {
                string hql = @" select produto
                                from Produto produto
                                join produto.Unidade unidade
                                join produto.GrupoEstoque grupo
                                left join produto.TabelaPrecoProdutos";

                var resultado = session.CreateQuery(hql).List<Produto>();
                Hashtable ret = new Hashtable();
                foreach (var item in resultado)
                {
                    if (!ret.ContainsKey(item.Codigo))
                        ret.Add(item.Codigo, item);
                }
                return ret;
            }
        }

        public Produto getProdutoByIDIntegracao(string id)
        {
            using (var session = GetSession())
            {
                string hql = @" select produto 
                                from Produto produto 
                                where produto.IDIntegracao = :id";

                return session.CreateQuery(hql).SetString("id", id).UniqueResult<Produto>();
            }
        }

        public string GetUltimoCodigoBarrasEAN13()
        {
            using (var session = GetSession())
            {
                string hql = "select p from Produto p where p.Barra like '00000%' and length(p.Barra) = 13 order by p.Barra desc";
                var result = session.CreateQuery(hql).SetMaxResults(1).List<Produto>();
                if (result.Count > 0)
                    return result[0].Barra;
                return String.Empty;
            }
        }

        public IList<pxyListagemPreco> GetListagemPorGrupoEstoqueETabelaPreco(string classificacao, TabelaPreco tabelaPreco)
        {
            using (var session = GetSession())
            {
                String hql = @" select p.ID as IDProduto, p.Codigo as codigoProduto
                                , p.Nome as nomeProduto
                                , u.Sigla as unidade
                                , p.PrecoBase as precoBase
                                , tpp.MargemLucro as margemLucro
                                , g1.Nome as nomeGrupo
                                , ge.Classificacao as classificacaoGrupoEstoque
                                , ge.ID as idGrupoEstoque
                                , ge.Nome as nomeGrupoEstoque
                                , p.Barra as cobBarra
                                , p.Observacao
                                , p.DescReduzida as reduzida
                        from Produto p 
                        join p.GrupoEstoque ge
                        join p.Unidade u
                        left join p.Grupo1 g1
                        left join p.TabelaPrecoProdutos tpp
                        where ge.Nome  like :classificacao and tpp.TabelaPreco.ID = :idTabelaPreco";
                IQuery query = session.CreateQuery(hql)
                                      .SetString("classificacao", classificacao + "%")
                                      .SetInt32("idTabelaPreco", tabelaPreco.ID);
                ConstructorInfo construtor = typeof(pxyListagemPreco).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string)
                });
                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer)
                            .List<pxyListagemPreco>();
            }
        }

        public IList<pxyListagemPreco> GetListagemPorGrupoEstoqueETabelaPrecoSemInativos(string classificacao, TabelaPreco tabelaPreco)
        {
            using (var session = GetSession())
            {
                String hql = @" select p.ID as IDProduto, p.Codigo as codigoProduto
                                , p.Nome as nomeProduto
                                , u.Sigla as unidade
                                , p.PrecoBase as precoBase
                                , tpp.MargemLucro as margemLucro
                                , g1.Nome as nomeGrupo
                                , ge.Classificacao as classificacaoGrupoEstoque
                                , ge.ID as idGrupoEstoque
                                , ge.Nome as nomeGrupoEstoque
                                , p.Barra as cobBarra
                                , p.Observacao
                                , p.DescReduzida as reduzida
                        from Produto p 
                        join p.GrupoEstoque ge
                        join p.Unidade u
                        left join p.Grupo1 g1
                        left join p.TabelaPrecoProdutos tpp
                        where ge.Nome  like :classificacao and tpp.TabelaPreco.ID = :idTabelaPreco and p.Inativo = 0";
                IQuery query = session.CreateQuery(hql)
                                      .SetString("classificacao", classificacao + "%")
                                      .SetInt32("idTabelaPreco", tabelaPreco.ID);
                ConstructorInfo construtor = typeof(pxyListagemPreco).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string)
                });
                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer)
                            .List<pxyListagemPreco>();
            }
        }

        public decimal BuscaCustoMedio(int idFilial, int idProduto, DateTime data)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_BuscaCustoMedio :idFilial,:idProduto,:data, 0
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto)
                                      .SetDateTime("data", data);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal CalculaCustoMedio(int idFilial, int idProduto, DateTime data, decimal quantidade, decimal custoUnitario, int idNota)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_CalculaCustoMedio :idFilial,:idProduto,:data,:quantidade,:custoUnitario,:idNota
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto)
                                      .SetDateTime("data", data)
                                      .SetDecimal("quantidade", quantidade)
                                      .SetDecimal("custoUnitario", custoUnitario)
                                      .SetInt32("idNota", idNota);

                return query.UniqueResult<decimal>();
            }
        }

        public IList<pxyEstoque> GetListaEstoque(int idProduto)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT   UPPER(LE.Nome) AS LocaEstoque
                                    , (SELECT dbo.f_SaldoEstoqueLocalEstoque(LE.ID, :idProduto)) AS EstoqueFisico
                                    , (SELECT dbo.f_PedSaldoEstoqueLocalEstoque(LE.ID, :idProduto)) AS QtdPedido
                                    , Est.Qtd_Estoque_Min AS EstoqueMinimo
                                    , Est.ID
                                    FROM Estoque Est
                                    JOIN LocalEstoque LE ON LE.ID = Est.IDLocalEstoque
                                    WHERE Est.IDProduto = :idProduto";


                IQuery query = session.CreateSQLQuery(hql).SetInt32("idProduto", idProduto);
                ConstructorInfo construtor = typeof(pxyEstoque).GetConstructor(new Type[] {
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(Int32)});

                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer).List<pxyEstoque>();
            }
        }

        public decimal GetQtdEstoqueFisico(int IdLocalEstoque, int idProduto)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueLocalEstoque :idLocalEstoque, :idProduto
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", IdLocalEstoque)
                                      .SetInt32("idProduto", idProduto);

                return query.UniqueResult<decimal>();
            }
        }

        public IList<pxyEstoque> GetListaEstoque(int idProduto, int idNota, int idPedido)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT TOP 1 UPPER(LE.Nome) AS LocaEstoque
                                    , (SELECT dbo.f_SaldoEstoqueLocalEstoqueData(LE.ID, :idProduto, GETDATE(), :idNota)) AS EstoqueFisico
                                    , (SELECT dbo.f_PedSaldoEstoqueLocalEstoqueData(LE.ID, :idProduto, GETDATE(), :idPedido)) AS QtdPedido
                                    , Est.Qtd_Estoque_Min AS EstoqueMinimo
                                    , Est.ID
                                    FROM Estoque Est
                                    JOIN LocalEstoque LE ON LE.ID = Est.IDLocalEstoque
                                    WHERE Est.IDProduto = :idProduto";

                IQuery query = session.CreateSQLQuery(hql).SetInt32("idProduto", idProduto)
                                                          .SetInt32("idPedido", idPedido)
                                                          .SetInt32("idNota", idNota);
                ConstructorInfo construtor = typeof(pxyEstoque).GetConstructor(new Type[] {
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(Int32)});

                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer).List<pxyEstoque>();
            }
        }

        public IList<pxyEstoqueProduto> GetListaEstoque(string filiais, GrupoEstoque grupoEstoque, string Situacao, int? IDLocalEstoque, int IdFornecedor)
        {
            using (var session = GetSession())
            {
                string hql = $@"SELECT   UPPER(LE.Nome) AS LocaEstoque
                            , (SELECT dbo.f_SaldoEstoqueLocalEstoque(LE.ID, Pro.ID)) AS EstoqueFisico
                            , (SELECT dbo.f_PedSaldoEstoqueLocalEstoque(LE.ID, Pro.ID)) AS QtdPedido
                            , Est.Qtd_Estoque_Min AS EstoqueMinimo
                            , Est.ID AS IDEstoque
                            , UPPER(Pro.Nome + ' - ' + Pro.Codigo) AS Produto
                            , UPPER(Uni.Sigla) AS Unidade
                            , UPPER(Fil.Nome) AS Filial
                            , GE.Classificacao AS Classificacao
                            , GE.Nome AS Grupoestoque
                            , GE.ID AS IDGrupoEstoque
                            , Pro.UltimoCusto Custo
                            , Pro.Codigo CodigoProduto	
                            , Pes.Nome
                            FROM Estoque Est
                            JOIN Produto Pro ON Pro.ID = Est.IDProduto
                            JOIN Unidade Uni ON Uni.ID = Pro.IDUnidade
                            JOIN GrupoEstoque GE ON GE.ID = Pro.IDGrupoEstoque
                            JOIN LocalEstoque LE ON LE.ID = Est.IDLocalEstoque
                            JOIN Filial Fil ON Fil.ID = LE.IDFilial
                            JOIN Pessoa Pes ON Pes.ID = Pro.IDFornecedor
                            WHERE GE.Classificacao like :classificacao
                            {(IDLocalEstoque.HasValue ? "AND Est.IDLocalEstoque = " + IDLocalEstoque.Value : string.Empty)}
                            AND Fil.ID in " + filiais;
                if (Situacao == "Ativos")
                {
                    hql += " AND Pro.Inativo = 0";
                }

                else if (Situacao == "Inativos")
                {
                    hql += " AND Pro.Inativo = 1";
                }

                if (IdFornecedor > 0)
                {
                    hql += $" AND (Pro.IDFornecedor = {IdFornecedor} OR Pro.IDFornecedor2 = {IdFornecedor} " +
                        $" OR Pro.IDFornecedor3 = {IdFornecedor} OR Pro.IDFornecedor4 = {IdFornecedor} " +
                        $" OR Pro.IDFornecedor5 = {IdFornecedor}) ";
                }

                IQuery query = session.CreateSQLQuery(hql).SetString("classificacao", grupoEstoque.Classificacao + "%");
                ConstructorInfo construtor = typeof(pxyEstoqueProduto).GetConstructor(new Type[] {
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(Int32),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(Int32),
                typeof(decimal),
                typeof(string),
                typeof(string)});

                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer).List<pxyEstoqueProduto>();
            }
        }

        public IList<pxyEstoqueProduto> GetListaEstoque(string filiais, Produto produto, int? IDLocalEstoque)
        {
            using (var session = GetSession())
            {
                string hql = $@"SELECT   UPPER(LE.Nome) AS LocaEstoque
                            , (SELECT dbo.f_SaldoEstoqueLocalEstoque(LE.ID, Pro.ID)) AS EstoqueFisico
                            , (SELECT dbo.f_PedSaldoEstoqueLocalEstoque(LE.ID, Pro.ID)) AS QtdPedido
                            , Est.Qtd_Estoque_Min AS EstoqueMinimo
                            , Est.ID AS IDEstoque
                            , UPPER(Pro.Nome + ' - ' + Pro.Codigo) AS Produto
                            , UPPER(Uni.Sigla) AS Unidade
                            , UPPER(Fil.Nome) AS Filial
                            , GE.Classificacao AS Classificacao
                            , GE.Nome AS Grupoestoque
                            , GE.ID AS IDGrupoEstoque
                            , Pro.UltimoCusto Custo
                            , Pro.Codigo CodigoProduto	
                            FROM Estoque Est
                            JOIN Produto Pro ON Pro.ID = Est.IDProduto
                            JOIN Unidade Uni ON Uni.ID = Pro.IDUnidade
                            JOIN GrupoEstoque GE ON GE.ID = Pro.IDGrupoEstoque
                            JOIN LocalEstoque LE ON LE.ID = Est.IDLocalEstoque
                            JOIN Filial Fil ON Fil.ID = LE.IDFilial
                            WHERE Pro.ID = :idProduto
                            {(IDLocalEstoque.HasValue ? "AND Est.IDLocalEstoque = " + IDLocalEstoque.Value : string.Empty)}
                            AND Fil.ID in " + filiais;

                IQuery query = session.CreateSQLQuery(hql).SetInt32("idProduto", produto.ID);
                ConstructorInfo construtor = typeof(pxyEstoqueProduto).GetConstructor(new Type[] {
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(Int32),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(Int32),
                typeof(decimal),
                typeof(string)});

                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer).List<pxyEstoqueProduto>();
            }
        }

        public string GetIDIntegracaoPorID(int id)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "select p from Produto p where p.ID = :id";
                    return session.CreateQuery(hql).SetInt32("id", id).UniqueResult<Produto>().IDIntegracao.ToString();
                }
                catch
                {
                    return "1";
                }
            }
        }

        public IList<ProdutoMercadoLivre> GetCodigosMercadoLivre(int idProduto)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT pml
                               FROM ProdutoMercadoLivre pml
                               JOIN FETCH pml.Produto p
                               WHERE p.ID = :id";
                return session.CreateQuery(hql).SetInt32("id", idProduto).List<ProdutoMercadoLivre>().OrderBy(x => x.ID).ToList();
            }
        }

        public IList<string> GetAllCodigoBarras()
        {
            using (var session = GetSession())
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("select p.Barra from Produto p where p.Barra is not null and p.Barra != ''");
                sql.AppendLine("union");
                sql.AppendLine("select f.Barra from PCP_FichaTecnica f where f.Barra is not null and f.Barra != ''");
                try
                {
                    IList<string> result = session.CreateSQLQuery(sql.ToString()).List<string>();
                    for (int i = 0; i < result.Count; i++)
                    {
                        result[i] = result[i].Remove(result[i].Length - 1);
                    }
                    return result;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public decimal GetMargemLucroTabelaPreco(TabelaPreco tabelapreco, Produto produto)
        {
            using (var session = GetSession())
            {
                string hql = "select tpp.MargemLucro from TabelaPrecoProduto tpp " +
                             " where tpp.Produto.ID = :produto " +
                             " and tpp.TabelaPreco.ID = :tabelapreco ";

                return session.CreateQuery(hql).SetInt32("produto", produto.ID).SetInt32("tabelapreco", tabelapreco.ID).UniqueResult<decimal>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Grupo1> GetGrupos1Produto()
        {
            using (var session = GetSession())
            {
                string hql = @"select gp1
                               from Produto produto
                                 join produto.Grupo1 gp1";
                return session.CreateQuery(hql).List<Grupo1>();
            }
        }

        public IList<Produto> GetProdutosPorGrupo1(int IDGrupo)
        {
            using (var session = GetSession())
            {
                string hql = $@"select produto
                                from Produto produto
                                  left join produto.Grupo1 gp1
                                where {(IDGrupo == -1 ? "produto.Grupo1 is null" : $"gp1.ID = {IDGrupo}")}";
                return session.CreateQuery(hql)./*SetInt32("IDGrupo", IDGrupo).*/List<Produto>();
            }
        }

        public IList<Produto> GetProdutosFiltroDelivery(string Descricao)
        {
            using (var session = GetSession())
            {
                string hql = $@"select produto
                                from Produto produto
                                  left join produto.Grupo1 gp1
                                where UPPER(produto.Codigo) like UPPER('%{Descricao}%')
                                   OR UPPER(produto.Barra) like UPPER('%{Descricao}%')
                                   OR UPPER(produto.Nome) like UPPER('%{Descricao}%')";

                return session.CreateQuery(hql).List<Produto>();
            }
        }

        public Produto GetProdutoByCodigo(int iDProduto)
        {
            return null;
        }

        public Produto GetProdutoByID(int ID)
        {
            using (var session = GetSession())
            {
                string hql = "from Produto p " +
                             " left join fetch p.Unidade " +
                             " where p.ID = :ID";

                return session.CreateQuery(hql).SetInt32("ID", ID).UniqueResult<Produto>();
            }
        }
        public void AtualizarEanProduto(int ID, string Barra)
        {
            string hql = $"update Produto p set Barra = '{Barra}' where p.ID = {ID}";
            using (ISession sessao = GetSession())
            {
                using (ITransaction trans = sessao.BeginTransaction())
                {
                    sessao.CreateQuery(hql).ExecuteUpdate();
                    trans.Commit();
                }
            }
        }
    }
}
