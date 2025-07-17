using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class EstoqueRepositorio : RepositorioAbstrato<Estoque>, IRepositorioEstoque
    {
        protected override string HqlLoadById()
        {
            return "from Estoque e where e.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Estoque e";
        }

        public Estoque GetEstoque(Filial filial, Produto produto)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch le.Filial f"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " where 1 = 1";
            if (produto != null)
                hql += " and p.ID = " + produto.ID;
            if (filial != null)
                hql += " and f.ID = " + filial.ID;
            using (ISession sessao = GetSession())
            {
                var ret = sessao.CreateQuery(hql).List<Estoque>().ToList();
                if (ret.Count > 0)
                    return ret[0];
            }
            return null;
        }

        public IList<Estoque> GetEstoque(Filial filial)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch le.Filial f"
                        + " join fetch e.Produto p"
                        + " join fetch p.TabelaPrecoProdutos tp"
                        + " where f.ID = :idFilial";
            using (ISession sessao = GetSession())
            {
                return sessao.CreateQuery(hql).SetInt32("idFilial", filial.ID).List<Estoque>();
            }
        }

        public List<Estoque> GetEstoque(String filiais, Produto produto)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch le.Filial f"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " where 1 = 1";
            if (produto != null)
                hql += " and p.ID = " + produto.ID;
            if (!String.IsNullOrEmpty(filiais))
                hql += " and f.ID in " + filiais;
            List<Estoque> retorno = null;
            using (ISession sessao = GetSession())
            {
                retorno = sessao.CreateQuery(hql).List<Estoque>().ToList();
            }
            return retorno;
        }

        public List<Estoque> GetEstoque(String filiais, GrupoEstoque grupoEstoque)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch le.Filial f"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " where ge.Classificacao like '" + grupoEstoque.Classificacao + "%'"
                        + " and f.ID in " + filiais;
            List<Estoque> retorno = null;
            using (ISession sessao = GetSession())
            {
                retorno = sessao.CreateQuery(hql).List<Estoque>().ToList();
            }
            return retorno;
        }

        public List<Estoque> GetEstoque(String filiais, String produtoNome)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch le.Filial f"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " where p.Nome Like '" + produtoNome + "%'"
                        + " and f.ID in " + filiais;
            List<Estoque> retorno = null;
            using (ISession sessao = GetSession())
            {
                retorno = sessao.CreateQuery(hql).List<Estoque>().ToList();
            }
            return retorno;
        }

        public IList<object> GetRelMovimentacaoPorProduto(String filiais, String produtos, String classificacao, String tiponota, DateTime datainicial, DateTime datafinal)
        {
            using (ISession sessao = GetSession())
            {
                String hql = " select gp.BTitulo as Titulo, p.Nome as produto, gp.Nome as grupo, gp.Classificacao, ni.Dt as data" +
                ", n.Numero as nf, ni.Ent_Sai as tipomvt, n.Codigo as codigonf," +
                " coalesce((select ni2.Quantidade from NotaItem ni2 where ni2.Produto = ni.Produto" +
                " and ni2.Ent_Sai = 1 and ni2.ID = ni.ID AND ni2.Nota.ID = n.ID and ni2.Dt >= ni.Dt and ni2.Dt <= ni.Dt), 0) as entradas," +
                "coalesce((select ni2.Quantidade from NotaItem ni2 where ni2.Produto = ni.Produto" +
                " and ni2.Ent_Sai = 2 and ni2.ID = ni.ID and ni2.Nota.ID = n.ID and ni2.Dt >= ni.Dt and ni2.Dt <= ni.Dt),0) as saidas," +
                " (coalesce((select SUM(ni2.Quantidade) from NotaItem ni2 where ni2.Produto = ni.Produto" +
                " and ni2.Ent_Sai = 1 and ni2.Dt < :datainicial), 0) - coalesce((select SUM(ni2.Quantidade) from NotaItem ni2" +
                " where ni2.Produto = ni.Produto and ni2.Ent_Sai = 2 and ni2.Dt < :datainicial),0)) as SaldoAnt, un.Nome as unidade" +
                " from NotaItem ni" +
                " join ni.Nota n" +
                " join n.Filial f" +
                " join n.TipoNota tn" +
                " join ni.Produto p" +
                " join p.Unidade un" +
                " right join p.GrupoEstoque gp" +
                " where (p.Nome is not null or gp.BTitulo = 1) and (tn.ID in " + tiponota + " OR tn.ID is null) AND" +
                " (f.ID in " + filiais + " OR f.ID is null) AND" +
                " ((ni.Dt >= :datainicial AND ni.Dt <= :datafinal) OR ni.Dt is null)";
                if (classificacao != null)
                    hql += " AND gp.Classificacao like '" + classificacao + "%'";
                if (produtos != null)
                    hql += "  AND (p.ID in (" + produtos + ") OR p.ID is null)";
                hql += " order by gp.Classificacao, p.Nome, ni.Dt asc";
#pragma warning disable CS0219 // A variável "retorno" é atribuída, mas seu valor nunca é usado
                List<object> retorno = null;
#pragma warning restore CS0219 // A variável "retorno" é atribuída, mas seu valor nunca é usado
                
                IQuery query = sessao.CreateQuery(hql).SetDateTime("datainicial", datainicial).SetDateTime("datafinal", datafinal);

                ConstructorInfo construtor = typeof(pxyRelMovimentoPorProduto).GetConstructor(new Type[] {
                typeof(bool),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(DateTime),
                typeof(int?),
                typeof(int),
                typeof(int),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal), 
                typeof(string)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public IList<object> GetRelTempoEstoque(String filiais, String CodProduto, String classificacao)
        {
            using (ISession sessao = GetSession())
            {
                #region hql
                string hql = @" select p.codigo as CodProduto, gp.Nome as grupo,"+
                " gp.classificacao as classificacao," +
                " ni.Dt as DataEntrada, i.Ano as Ano, i.Sequencia as Sequencia" +
                " ni.Quantidade as Quantidade," +
                " datediff(day,ni.Dt,getdate()) as TempoEstoque "+
                " from NotaItem ni" +
                " join fetch ni.Produto p" +
                " join fetch ni.Nota n" +
                " right join fetch p.GrupoEstoque gp" +
                " join n.Identificador i" +
                " where (p.codigo is not null and TempoEstoque > 0 or gp.BTitulo = 1)";
                if (classificacao != null)
                    hql += @" AND gp.Classificacao like '" + classificacao + "%'";
                if (CodProduto != null && (CodProduto != ""))
                    hql += @" AND (p.codigo like '%" + CodProduto + "%'";
                hql += @" order by TempoEstoque desc";
                #endregion


                IList<object> x = null;

                ConstructorInfo construtor = typeof(pxyRelTempoEstoque).GetConstructor(new Type[] {
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(DateTime),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int)
                });

                IQuery query = sessao.CreateQuery(hql);
                x = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();

                return x;
            }
        }
        public IList<EstoqueProdutoView> GetEstoquePorFilial(Filial filial)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch le.Filial f"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " left join fetch p.Grupo1 g1"
                        + " left join fetch p.Grupo2 g2"
                        + " left join fetch p.Grupo3 g3"
                        + " where f.ID = :idfilial";
            using (ISession sessao = GetSession())
            {
                return (from e in sessao.CreateQuery(hql).SetInt32("idfilial", filial.ID).List<Estoque>()
                        select new EstoqueProdutoView(e)).OrderBy(prod => prod.NomeOrdenado).ToList();
            }
        }

        public IList<Estoque> GetEstoquePorProduto(Produto produto)
        {
            String hql = " from Estoque e"
                        + " join fetch e.Produto p"
                        + " join fetch e.LocalEstoque le"
                        + " where p.ID = " + produto.ID;

            using (ISession sessao = GetSession())
            {
                return sessao.CreateQuery(hql).List<Estoque>();
            }
        }

        public IList<EstoqueProdutoView> GetEstoquePorLocalEstoque(LocalEstoque localEstoque)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque le"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " left join fetch p.Grupo1 g1"
                        + " left join fetch p.Grupo2 g2"
                        + " left join fetch p.Grupo3 g3"
                        + " where le.ID = :idLocalEstoque";
            using (ISession sessao = GetSession())
            {
                return (from e in sessao.CreateQuery(hql).SetInt32("idLocalEstoque", localEstoque.ID).List<Estoque>()
                        select new EstoqueProdutoView(e)).OrderBy(prod => prod.NomeOrdenado).ToList();
            }
        }

        public Estoque GetEstoque(LocalEstoque localEstoque, Produto produto)
        {
            String hql = " from Estoque e"
                        + " join fetch e.LocalEstoque l"
                        + " join fetch e.Produto p"
                        + " join fetch p.GrupoEstoque ge"
                        + " join fetch p.Unidade u"
                        + " where 1 = 1";
            if (produto != null)
                hql += " and p.ID = " + produto.ID;
            if (localEstoque != null)
                hql += " and l.ID = " + localEstoque.ID;
            using (ISession sessao = GetSession())
            {
                var ret = sessao.CreateQuery(hql).List<Estoque>().ToList();
                if (ret.Count > 0)
                    return ret[0];
            }
            return null;
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
