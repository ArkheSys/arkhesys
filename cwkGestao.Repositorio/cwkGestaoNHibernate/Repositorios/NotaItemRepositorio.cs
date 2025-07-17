using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class NotaItemRepositorio : RepositorioAbstrato<NotaItem>, IRepositorioNotaItem
    {
        protected override string HqlLoadById()
        {
            return " from NotaItem n"
                   + " left join fetch n.Nota"
                   + " left join fetch n.Produto"
                   + " left join fetch n.PedidoItem"
                   + " left join fetch n.CFOP"
                   + " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public override NotaItem LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string hql = " from NotaItem ni"
                       + " left join fetch ni.Nota n"
                       + " left join fetch ni.Produto"
                    //+ " left join fetch ni.PedidoItem"
                       + " left join fetch ni.CFOP"
                       + " left join fetch n.Filial"
                       + " where ni.ID = :id";

                var notaItem = session.CreateQuery(hql).SetInt32("id", id).UniqueResult<NotaItem>();
                NHibernateUtil.Initialize(notaItem.Rateio_NotaItems);

                if (notaItem.PedidoItens != null)
                {
                    foreach (var item in notaItem.PedidoItens)
                    {
                        NHibernateUtil.Initialize(item.PedidoItem);
                        NHibernateUtil.Initialize(item.PedidoItem.Pedido);
                        NHibernateUtil.Initialize(item.PedidoItem.Pedido.Items);
                    }
                }

                return notaItem;
            }
        }

        public IList<NotaItem> GetAllPorIDNota(int IDNota)
        {
            try
            {
                using (var session = GetSession())
                {
                    string hql = " from NotaItem ni " +
                                    " left join fetch ni.Nota n " +
                                    " left join fetch ni.Produto " +
                                    " left join fetch ni.CFOP " +
                                    " left join fetch  " +
                                    " where ni.Nota.ID = :IDNota";
                    IList<NotaItem> temp = session.CreateQuery(hql).SetInt32("IDNota", IDNota).List<NotaItem>();
                    return temp;
                }
            }
            catch (Exception z)
            {
                throw z;
            }

            
        }

        private string HqlRecuperaProdutoInterestadual()
        {
            return "select pi " +
                   "from Produto_Icms pi where " +
                   "pi.Produto.ID = :idProduto and " +
                   "pi.UFOrigem.Sigla = :siglaUfOrigem and " +
                   "pi.UFDestino.Sigla = :siglaUfDestino ";

        }

        private string HqlRecuperaIcmsInterestadual()
        {
            return "select pi " +
                   "from ICMS_Interestadual pi where " +
                   "pi.UFOrigem.Sigla = :siglaUfOrigem and " +
                   "pi.UFDestino.Sigla= :siglaUfDestino ";
        }

        private string HqlRecuperaNCMItem()
        {
            return "select ni " +
                   "from NCMItem ni " +
                   "join ni.NCM n " +
                   " where n.ID = :ID " +
                   "and ni.UFOrigem.Sigla = :siglaUfOrigem " +
                   "and ni.UFDestino.Sigla = :siglaUfDestino " +
                   "order by n.Ncm desc";
        }

        public IcmsBase RecuperaIcmsOriginal(string siglaUfOrigem, string siglaUfDestino, Produto produto, NotaItem _notaitem)
        {
            using (var session = GetSession())
            {
                var produtoIcms = session.CreateQuery(HqlRecuperaProdutoInterestadual())
                    .SetInt32("idProduto", produto.ID)
                    .SetString("siglaUfOrigem", siglaUfOrigem)
                    .SetString("siglaUfDestino", siglaUfDestino)
                    .List<Produto_Icms>();
                if (produtoIcms.Count > 0)
                    return produtoIcms.First();

                if (_notaitem.CodigoProduto != "")
                {
                    if (_notaitem.Nota.Filial.bSTSomenteRevenda)
                    {
                        if (_notaitem.Nota.Pessoa.bSTRevenda)
                        {
                            NCMItem _ncmItem = RecuperaNCM(siglaUfOrigem, siglaUfDestino, produto.ID_NCM, session);
                            if (_ncmItem != null)
                                return _ncmItem;
                        }
                        else
                        {
                            var _icmsInterestadual = session.CreateQuery(HqlRecuperaIcmsInterestadual())
                            .SetString("siglaUfOrigem", siglaUfOrigem)
                            .SetString("siglaUfDestino", siglaUfDestino)
                            .List<ICMS_Interestadual>();
                            if (_icmsInterestadual.Count > 0)
                                return _icmsInterestadual.First();
                        }
                    }
                }

                NCMItem ncmItem = RecuperaNCM(siglaUfOrigem, siglaUfDestino, produto.ID_NCM, session);
                if (ncmItem != null)
                    return ncmItem;
                                
                var icmsInterestadual = session.CreateQuery(HqlRecuperaIcmsInterestadual())
                            .SetString("siglaUfOrigem", siglaUfOrigem)
                            .SetString("siglaUfDestino", siglaUfDestino)
                            .List<ICMS_Interestadual>();
                if (icmsInterestadual.Count > 0)
                    return icmsInterestadual.First();
            }
            return null;

        }

        private NCMItem RecuperaNCM(string siglaUfOrigem, string siglaUfDestino, int id_ncm, NHibernate.ISession session)
        {
            var ncmItem = session.CreateQuery(HqlRecuperaNCMItem())
                    .SetInt32("ID", id_ncm)
                    .SetString("siglaUfOrigem", siglaUfOrigem)
                    .SetString("siglaUfDestino", siglaUfDestino)
                    .List<NCMItem>();
            if (ncmItem.Count > 0)
                return ncmItem.First();

            return null;
        }

        public decimal SaldoEstoque(string identificador, int id, int idProduto)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoque :identificador,:id,:idProduto
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetString("identificador", identificador)
                                      .SetInt32("id", id)
                                      .SetInt32("idProduto", idProduto);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueFilial(int idFilial, int idProduto)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueFilial :idFilial,:idProduto
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueFilialData(int idFilial, int idProduto, DateTime data, int idNota)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueFilialData :idFilial,:idProduto,:data,:idNota
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto)
                                      .SetDateTime("data", data)
                                      .SetInt32("idNota", idNota);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueFilialIdentificador(int idFilial, int idProduto, string idIdentificador)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueFilialIdentificador :idFilial,:idProduto,:idIdentificador
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto)
                                      .SetString("idIdentificador", idIdentificador);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueFilialIdentificadorData(int idFilial, int idProduto, string idIdentificador, DateTime data)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueFilialIdentificadorData :idFilial,:idProduto,:idIdentificador,:data
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto)
                                      .SetString("idIdentificador", idIdentificador)
                                      .SetDateTime("data", data);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueLocalEstoque(int idLocalEstoque, int idProduto)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueLocalEstoque :idLocalEstoque,:idProduto
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", idLocalEstoque)
                                      .SetInt32("idProduto", idProduto);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueLocalEstoqueData(int idLocalEstoque, int idProduto, DateTime data)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueLocalEstoqueData :idLocalEstoque,:idProduto,:data
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", idLocalEstoque)
                                      .SetInt32("idProduto", idProduto)
                                      .SetDateTime("data", data);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueLocalEstoqueIdentificador(int idLocalEstoque, int idProduto, int idIdentificador)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueLocalEstoqueIdentificador :idLocalEstoque,:idProduto,:idIdentificador
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", idLocalEstoque)
                                      .SetInt32("idProduto", idProduto)
                                      .SetInt32("idIdentificador", idIdentificador);

                return query.UniqueResult<decimal>();
            }
        }

        public decimal SaldoEstoqueLocalEstoqueIdentificadorData(int idLocalEstoque, int idProduto, int idIdentificador, DateTime data)
        {
            using (var session = GetSession())
            {
                string sql = @" declare @ret as numeric(18,4)
                                exec @ret = f_SaldoEstoqueLocalEstoqueIdentificadorData :idLocalEstoque,:idProduto,:idIdentificador,:data
                                select @ret";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", idLocalEstoque)
                                      .SetInt32("idProduto", idProduto)
                                      .SetInt32("idIdentificador", idIdentificador)
                                      .SetDateTime("data", data);

                return query.UniqueResult<decimal>();
            }
        }

        public IList<object> SaldoEstoqueFilialListaIdentificador(int idFilial, int idProduto)
        {
            using (var session = GetSession())
            {
                string sql = @"select * from f_SaldoEstoqueFilialListaIdentificador (:idFilial,:idProduto)";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto);

                return query.List<object>();
            }
        }

        public IList<object> SaldoEstoqueFilialListaIdentificadorData(int idFilial, int idProduto, DateTime data)
        {
            using (var session = GetSession())
            {
                string sql = @"select * from f_SaldoEstoqueFilialListaIdentificadorData (:idFilial,:idProduto,:data)";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idFilial", idFilial)
                                      .SetInt32("idProduto", idProduto)
                                      .SetDateTime("data", data);

                return query.List<object>();
            }
        }

        public IList<pxyEstoqueIdentificador> SaldoEstoqueLocalEstoqueListaIdentificador(int idLocalEstoque, int idProduto, int idNota)
        {
            using (var session = GetSession())
            {
                string sql = @"select CodigoIdentificador, Saldo, CustoUnitario, IdIdentificador from f_SaldoEstoqueLocalEstoqueListaIdentificador (:idLocalEstoque,:idProduto, :idNota)";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", idLocalEstoque)
                                      .SetInt32("idProduto", idProduto)
                                      .SetInt32("idNota", idNota);

                ConstructorInfo construtor = typeof(pxyEstoqueIdentificador).GetConstructor(new Type[] {
                typeof(string),
                typeof(decimal),
                typeof(decimal),
                typeof(int)
                });
                var resultTransformer = Transformers.AliasToBeanConstructor(construtor);
                return query.SetResultTransformer(resultTransformer)
                            .List<pxyEstoqueIdentificador>();
            }
        }

        public IList<object> SaldoEstoqueLocalEstoqueListaIdentificadorData(int idLocalEstoque, int idProduto, DateTime data)
        {
            using (var session = GetSession())
            {
                string sql = @"select * from f_SaldoEstoqueFilialListaIdentificadorData (:idLocalEstoque,:idProduto,:data)";

                IQuery query = session.CreateSQLQuery(sql)
                                      .SetInt32("idLocalEstoque", idLocalEstoque)
                                      .SetInt32("idProduto", idProduto)
                                      .SetDateTime("data", data);

                return query.List<object>();
            }
        }

        public IList<pxyMovimentoEstoqueProduto> GetMovimentosEstoque(int idProduto, int idLocalEstoque)
        {
            using (var session = GetSession())
            {
                if (idLocalEstoque == 0)
                {
                    var sql = @"SELECT	N.Codigo AS CodNota
	                    , F.Codigo AS CodFilial
	                    , N.Serie AS SerieNota
	                    , N.Numero AS NumeroNota
	                    , N.Dt AS Data
	                    , CASE WHEN N.Ent_Sai = 1 THEN 'Entrada' ELSE 'Saída' END AS Tipo
	                    , NI.Quantidade AS Quantidade
	                    , NI.Valor AS ValorUnit
	                    , NI.CustoUnit AS CustoUnit
	                    , NI.ValorCustoMedio AS CustoMedio
	                    , ISNULL(NI.CMVUnit, 0) AS CMVUnit
	                    , CAST(I.Ano AS VARCHAR) + REPLACE(STR(I.Sequencia,3,0),' ',0) AS Identificador
	                    , ISNULL(NIE.Quantidade, 0) AS QuantidadeIdent
	                    , ISNULL(NIE.CMVUnit, 0) AS CMVUnitIdent
                    FROM NotaItem NI
                    LEFT JOIN NotaItemEstoque NIE on nie.IDNotaItem = ni.ID
                    LEFT JOIN Identificador I ON I.ID = NIE.IDIdentificador
                    INNER JOIN Nota N ON N.ID = ni.IDNota
                    INNER JOIN Filial F ON F.ID = N.IDFilial
                    WHERE NI.IDProduto = :idProduto
                    AND NI.bAtualizaEstoque = 1
                    AND N.CancDt IS NULL
                    ORDER BY NI.ID DESC, NIE.ID ASC";

                    IQuery query = session.CreateSQLQuery(sql).SetInt32("idProduto", idProduto);

                    ConstructorInfo construtor = typeof(pxyMovimentoEstoqueProduto).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(int),
                    typeof(DateTime),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal), 
                    typeof(decimal),
                    typeof(string),
                    typeof(decimal), 
                    typeof(decimal)
                    });

                    var resultTransformer = Transformers.AliasToBeanConstructor(construtor);

                    return query.SetResultTransformer(resultTransformer).List<pxyMovimentoEstoqueProduto>();
                }
                else
                {
                    var sql = @"SELECT	N.Codigo AS CodNota
	                    , F.Codigo AS CodFilial
	                    , N.Serie AS SerieNota
	                    , N.Numero AS NumeroNota
	                    , N.Dt AS Data
	                    , CASE WHEN N.Ent_Sai = 1 THEN 'Entrada' ELSE 'Saída' END AS Tipo
	                    , NI.Quantidade AS Quantidade
	                    , NI.Valor AS ValorUnit
	                    , NI.CustoUnit AS CustoUnit
	                    , NI.ValorCustoMedio AS CustoMedio
	                    , ISNULL(NI.CMVUnit, 0) AS CMVUnit
	                    , CAST(I.Ano AS VARCHAR) + REPLACE(STR(I.Sequencia,3,0),' ',0) AS Identificador
	                    , ISNULL(NIE.Quantidade, 0) AS QuantidadeIdent
	                    , ISNULL(NIE.CMVUnit, 0) AS CMVUnitIdent
                    FROM NotaItem NI
                    LEFT JOIN NotaItemEstoque NIE on nie.IDNotaItem = ni.ID
                    LEFT JOIN Identificador I ON I.ID = NIE.IDIdentificador
                    INNER JOIN Nota N ON N.ID = ni.IDNota
                    INNER JOIN Filial F ON F.ID = N.IDFilial
                    WHERE NI.IDProduto = :idProduto
                    AND N.IDLocalEstoque = :idLocalEstoque
                    AND NI.bAtualizaEstoque = 1
                    AND N.CancDt IS NULL
                    ORDER BY NI.ID DESC, NIE.ID ASC";

                    IQuery query = session.CreateSQLQuery(sql).SetInt32("idProduto", idProduto).SetInt32("idLocalEstoque", idLocalEstoque);

                    ConstructorInfo construtor = typeof(pxyMovimentoEstoqueProduto).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(int),
                    typeof(DateTime),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal), 
                    typeof(decimal),
                    typeof(string),
                    typeof(decimal), 
                    typeof(decimal)
                    });

                    var resultTransformer = Transformers.AliasToBeanConstructor(construtor);

                    return query.SetResultTransformer(resultTransformer).List<pxyMovimentoEstoqueProduto>();
                }
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
