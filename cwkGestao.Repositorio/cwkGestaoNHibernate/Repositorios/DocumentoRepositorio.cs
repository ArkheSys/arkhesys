using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate.Criterion;
using NHibernate.Transform;
using NHibernate.Loader.Criteria;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using MoreLinq;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class DocumentoRepositorio : RepositorioAbstrato<Documento>, IRepositorioDocumento
    {
        protected override string HqlLoadById()
        {
            return "from Documento d"
                 + " left join fetch d.Movimentos m"
                 + " left join fetch m.MovCaixa mc"
                 + " left join fetch mc.Banco bccc"
                 + " left join fetch m.FormaPagamentoMovimentos fpm"
                 + " left join fetch fpm.FormaPagamento fpmfp"
                 + " left join fetch d.Acrescimo a"
                 + " left join fetch d.Condicao con"
                 + " left join fetch m.PlanoConta pcdob"
                 + " left join fetch d.TipoDocumento tdd"
                 + " left join fetch d.Banco bcc"
                 + " left join fetch d.Filial f"
                 + " left join fetch d.Portador fdp"
                 + " left join fetch f.Cidade c "
                 + " left join fetch c.UF "
                 + " where d.ID = :id";
        }


        private string SelectNewDoc()
        {
            return @"select new Documento(doc.Codigo, doc.DtVencimento, doc.DtPrevisao,doc.NumDocumento, doc.NossoNumero, pes.Nome,
                            doc.Valor,doc.Saldo,doc.Situacao, doc.DtUltimaBaixa, doc.NumRequisicao, fil.Nome, pes.Fantasia, 
                            td.Nome, por.Nome, doc.Parcela, doc.QtParcela, doc.Dt,doc.ID,doc.Tipo,
                            doc.IncData, doc.IncUsuario, doc.AltData, doc.AltUsuario, doc.DataDesconto, doc.NumeroPedido, doc.DtTransferenciaOuBaixaRet, hist.Nome, doc.ComplementoHist,
                            (select sum(m.Valor) from Movimento m where m.Tipo = 3 and doc.ID = m.Documento.ID) as Juros)
                            from Documento doc
                            join doc.Portador por 
                            join doc.Pessoa pes 
                            join doc.TipoDocumento td 
                            join doc.Historico hist
                            left join doc.Condicao cond
                            join doc.Filial fil ";
        }

        private string SelectNewDocTel()
        {
            return @"select new Documento( doc.Codigo, doc.DtVencimento, doc.DtPrevisao,doc.NumDocumento, doc.NossoNumero, pes.Nome,
                            doc.Valor,doc.Saldo,doc.Situacao, doc.DtUltimaBaixa, doc.NumRequisicao, fil.Nome, pes.Fantasia, 
                            td.Nome, por.Nome, doc.Parcela, doc.QtParcela, doc.Dt,doc.ID,doc.Tipo,
                            doc.IncData, doc.IncUsuario, doc.AltData, doc.AltUsuario, doc.DataDesconto, grpCli.Nome, doc.NumeroPedido, doc.DtTransferenciaOuBaixaRet, hist.Nome, doc.ComplementoHist,
                            (select sum(m.Valor) from Movimento m where m.Tipo = 3 and doc.ID = m.Documento.ID) as Juros) 
                            from Documento doc
                            join doc.Portador por 
                            join doc.Pessoa pes 
                            join doc.TipoDocumento td
                            join doc.Historico hist
                            join doc.Filial fil 
                            left join pes.ClientesTelefonia cliTel 
                            left join cliTel.GrupoCliente grpCli ";
        }

        public override Documento LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadById();

                Documento retorno = session.CreateQuery(sql).SetParameter("id", id)
                    .List<Documento>().FirstOrDefault();

                if (retorno.Nota != null)
                {
                    NHibernateUtil.Initialize(retorno.Nota);
                    NHibernateUtil.Initialize(retorno.Nota.NotaImpostoServicos);
                    NHibernateUtil.Initialize(retorno.Nota.NotaItemsServicos);
                    NHibernateUtil.Initialize(retorno.Nota.TipoNota);
                    if (retorno.Nota.TipoNota != null)
                        NHibernateUtil.Initialize(retorno.Nota.TipoNota.PlanoContaServico);
                }

                return retorno;
            }
        }

        protected override string hqlGetAll()
        {
            return " from Documento ";
        }

        public IList<object> GetContaPorPortador(DateTime dataInicial, DateTime dataFinal, int tipoDoc, string idsEmpresas, string idsportadores, string situacao)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select	d.Codigo");
                hql.AppendLine(", f.Codigo as Filial");
                hql.AppendLine(", d.NumDocumento as Doc");
                hql.AppendLine(", (cast(coalesce(d.Parcela, 0) as String) + '/' + cast(coalesce(d.QtParcela, 0) as String)) as ParcQt	");
                hql.AppendLine(", b.Codigo AS Banco");
                hql.AppendLine(", (pe.Nome + (cast(pe.Codigo AS String))) AS Cliente");
                hql.AppendLine(", d.Valor ");
                hql.AppendLine(", d.DtVencimento as Vencimento");
                hql.AppendLine(", d.DtPrevisao as Previsao");
                hql.AppendLine(", d.Dt ");
                hql.AppendLine(", d.Situacao ");
                hql.AppendLine(", d.Saldo ");
                hql.AppendLine(", p.Nome AS Portador");
                hql.AppendLine("from Documento d");
                hql.AppendLine("inner join d.Filial f");
                hql.AppendLine("left join d.Portador p");
                hql.AppendLine("left join d.Banco b");
                hql.AppendLine("inner join d.Pessoa pe");
                hql.AppendLine("where (d.DtVencimento >= :dataInicial) ");
                hql.AppendLine("and (d.DtVencimento <= :dataFinal)");
                hql.AppendLine("and (d.Tipo = :tipo)");
                if (idsEmpresas != String.Empty)
                {
                    hql.AppendLine("and  f.ID in ");
                    hql.AppendLine(idsEmpresas);
                }
                hql.AppendLine("and  p.ID in ");
                hql.AppendLine(idsportadores);

                if (situacao == "Aberto")
                    hql.AppendLine("and d.Situacao in('Norm', 'BxP')");
                else
                    hql.AppendLine("and (d.Situacao = :situacao)");

                hql.AppendLine("order by d.Dt");

                Type tipoContasPorPortador = typeof(pxyContasPorPortador);
                ConstructorInfo construtor = tipoContasPorPortador.GetConstructor(new Type[] { typeof(int),
                                                                                                    typeof(int),
                                                                                                    typeof(string),
                                                                                                    typeof(string),
                                                                                                    typeof(int),
                                                                                                    typeof(string),
                                                                                                    typeof(decimal),
                                                                                                    typeof(DateTime?),
                                                                                                    typeof(DateTime?),
                                                                                                    typeof(DateTime?),
                                                                                                    typeof(string),
                                                                                                    typeof(decimal),
                                                                                                    typeof(string)});
                IQuery query = session.CreateQuery(hql.ToString()).SetInt32("tipo", tipoDoc);
                query = query.SetDateTime("dataInicial", dataInicial).SetDateTime("dataFinal", dataFinal);
                if (situacao != "Aberto")
                    query = query.SetString("situacao", situacao);
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public cwkGestao.Modelo.Documento GetDocumentoPorNumero(Int32 pNumDoc)
        {
            using (var session = GetSession())
            {
                string hql = "from Documento d where d.NumDocumento LIKE :NumeroDocumento";
                IList<cwkGestao.Modelo.Documento> lista = session.CreateQuery(hql).SetInt32("NumeroDocumento", pNumDoc).List<cwkGestao.Modelo.Documento>();
                if (lista.Count >= 1)
                {
                    return lista.First();
                }
                return null;
            }
        }

        public IList<Modelo.Documento> GetDocumentosPorNota(int idNota)
        {
            using (var session = GetSession())
            {
                string hql = "select d from Documento d " +
                             "left join fetch d.Nota n " +
                             "left join fetch d.Historico h " +
                             "left join fetch d.Movimentos mov " +
                             "left join fetch d.Acrescimo a " +
                             "left join fetch d.Portador p " +
                             " where n.ID = :idNota";
                return session.CreateQuery(hql).SetInt32("idNota", idNota).List<Modelo.Documento>();
            }
        }

        public IList<Modelo.Documento> GetDocumentosPorNumeroPedido(int NumeroPedido)
        {
            using (var session = GetSession())
            {
                string hql = "select d from Documento d " +
                             "left join fetch d.Nota n " +
                             "left join fetch d.Historico h " +
                             "left join fetch d.Movimentos mov " +
                             "left join fetch d.Acrescimo a " +
                             " where d.NumeroPedido = :numero";
                return session.CreateQuery(hql).SetInt32("numero", NumeroPedido).List<Modelo.Documento>();
            }
        }

        public IList<pxyDocumentoArquivoRemessa> GetDocumentosParaArquivoRemessa(Filial Filial, DateTime DataInicial, DateTime DataFinal, bool porVencimento, TipoDocumentoType tipo, int? idArquivoRemessa, int? IdTipoDocumento, int? IdPortador)
        {
            using (var session = GetSession())
            {
                string hql = @"select d.ID
                        , d.Codigo
                        , f.Nome as Filial
                        , p.Nome as Cliente
                        , d.Situacao
                        , p.Fantasia as NomeFantasia
                        , d.Parcela
                        , d.DtVencimento as DataVencimento
                        , d.NumDocumento as NumeroDocumento
                        , d.Valor
                        , d.Saldo
                        , d.QtParcela as QtdParcela
                        , a.ID as IDRemessa
                        ,t.Nome
                        ,o.Nome 
                    from Documento d
                    join d.Filial f
                    join d.Pessoa p
                    join d.TipoDocumento t
                    join d.Portador o
                    left join d.ArquivoRemessa a
                    where d.Situacao <> 'BxT' and d.Situacao <> 'BxR' and d.Situacao <> 'Canc'
                    and f.ID = :idfilial
                    and d.Tipo = :tipo";

                if (idArquivoRemessa == null)
                {
                    hql += " and a.ID = NULL";
                }
                else
                {
                    hql += @" and (a.ID = NULL
                              or a.ID = :idArquivoRemessa)";
                }

                if (IdTipoDocumento != null)
                {
                    hql += @" and (t.ID = NULL
                              or t.ID = :IdTipoDocumento)";
                }

                if (IdPortador != null)
                {
                    hql += @" and (o.ID = NULL
                              or o.ID = :IdPortador)";
                }

                if (porVencimento)
                    hql += " and d.DtVencimento >= :datainicial and d.DtVencimento <= :datafinal";
                else
                    hql += " and d.Dt >= :datainicial and d.Dt <= :datafinal";

                pxyDocumentoArquivoRemessa proxyRemessa = new pxyDocumentoArquivoRemessa();

                ConstructorInfo construtor = GetConstructorInfopxyDocumentoArquivoRemessa();


                IQuery query = session.CreateQuery(hql).SetInt32("idfilial", Filial.ID)
                                                       .SetInt32("tipo", (int)tipo)
                                                       .SetDateTime("datainicial", DataInicial)
                                                       .SetDateTime("datafinal", DataFinal);
                if (IdTipoDocumento != null)
                {
                    query.SetInt32("IdTipoDocumento", (int)IdTipoDocumento);
                }
                if (IdPortador != null)
                {
                    query.SetInt32("IdPortador", (int)IdPortador);
                }

                if (idArquivoRemessa != null)
                {
                    query.SetInt32("idArquivoRemessa", (int)idArquivoRemessa);
                }

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyDocumentoArquivoRemessa>();
            }
        }

        public IList<pxyRemessa> GetDocumentosPorArquivoRemessa(int idArquivoRemessa)
        {
            using (var session = GetSession())
            {
                string hql = @"select d.ID
                        , d.Codigo
                        , d.DtVencimento as DataVencimento
                        , p.Nome as Cliente
                        , d.NumDocumento as NumeroDocumento
                        , d.Valor
                        , d.Saldo
                        , d.Situacao
                        , f.Nome as Filial
                        , p.Fantasia as NomeFantasia
                        , d.Tipo as TipoDocumento
                        , d.Parcela
                        , d.QtParcela as QtdParcela
                        , d.Dt as DataEmissao
                        , t.Nome as tipoDocumentoCob
                        , t.Nome as tipoCobranca
                    from Documento d
                    join d.TipoDocumento t
                    join d.Filial f
                    join d.Pessoa p
                    join d.ArquivoRemessa arq
                    where d.Situacao <> 'BxT' and d.Situacao <> 'Canc'
                        and arq.ID = :idArquivoRemessa";
                ConstructorInfo construtor = GetContructorInfoPxyRemessa();

                IQuery query = session.CreateQuery(hql).SetInt32("idArquivoRemessa", idArquivoRemessa);
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyRemessa>();
            }
        }

        public IList<pxyDocumentoArquivoRemessa> GetPxyDocumentosArqRemessaPorArquivoRemessa(int idArquivoRemessa)
        {
            using (var session = GetSession())
            {
                /*
                string hql = @"select d.ID
                        , d.Codigo
                        , f.Nome
                        , p.Nome
                        , d.Situacao
                        , p.Fantasia
                        , d.Parcela
                        , d.DtVencimento
                        , d.NumDocumento
                        , d.Valor
                        , d.Saldo
                        , d.QtParcela
                        , arq.ID 
                    from Documento d
                    join d.Filial f
                    join d.Pessoa p
                    join d.ArquivoRemessa arq
                    where d.Situacao <> 'BxT' and d.Situacao <> 'Canc'
                    and arq.ID = :idArquivoRemessa";
                */
                string hql = HqlGetRemessa() + " and d.IDArquivoRemessa = :idArquivoRemessa";

                ConstructorInfo construtor = GetConstructorInfopxyDocumentoArquivoRemessa();

                IQuery query = session.CreateQuery(hql).SetInt32("idArquivoRemessa", idArquivoRemessa);
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyDocumentoArquivoRemessa>();
            }
        }

        public IList<Object> GetDocumentoPorFormaPgto(DateTime dataInicial, DateTime dataFinal, String IDsEmpresas, String IDsFormasPagamento, TipoDocumentoType Tipo, Boolean SemFormaPagto)
        {
            using (var session = GetSession())
            {
                StringBuilder Sql = new StringBuilder();
                /*                Sql.Append(@"SELECT D.Codigo AS DocCod, 
                                           F.Nome AS NomeFilial, 
                                           D.NumDocumento AS NumeroDoc, 
                                           (CONVERT(VARCHAR, D.Parcela) + '/' + CONVERT(VARCHAR, D.QtParcela)) AS Parcelas,
                                           B.Codigo AS CodBanco,
                                           TD.Nome AS TipoDocumento,
                                           P.Nome AS Cliente,
                                           D.DtVencimento AS Vencimento,
                                           M.Valor AS ValorMovimento,
                                           FM.Valor AS FormaMovPgto,
                                           ISNULL(FP.Nome, '*Sem Forma de Pagamento') AS FormaPagamento,
                                           M.Sequencia AS SequenciaMovimento,
                                           M.ID AS IDMovimento,
                                           ISNULL((SELECT SUM(FP1.Valor) 
                                            FROM FormaPgtoMovimento FP1
                                            WHERE FP1.IDMovimento = M.ID), 0) AS ValorFormaPgtoMov
                                           FROM Documento AS D
                                           JOIN Filial F ON F.ID = D.IDFilial
                                           JOIN Banco B ON B.ID = D.IDBanco
                                           JOIN TipoDocumento TD ON TD.ID = D.IDTipoDocumento
                                           JOIN Pessoa P ON P.ID = D.IDPessoa
                                           LEFT JOIN Movimento M ON M.IDDocumento = D.ID
                                           LEFT JOIN FormaPgtoMovimento FM ON FM.IDMovimento = M.ID
                                           LEFT JOIN FormaPagamento FP ON FP.ID = FM.IDFormaPagamento
                                           WHERE (D.IDFilial IN " + IDsEmpresas + " OR D.IDFilial IS Null) AND (FM.IDFormaPagamento IN "
                                                                  + IDsFormasPagamento + (SemFormaPagto == true ?
                                                                  " OR FM.IDFormaPagamento IS Null " : String.Empty) +
                                                                  @") AND M.Dt >= :DataInicial AND M.Dt <= :DataFinal 
                                                                  AND (M.Operacao = 2 OR M.Operacao = 3) AND D.Tipo = "
                                                                  + (Tipo == TipoDocumentoType.Pagar ? "2" : "1"));
                */
                Sql.Append(@"
               SELECT D.Codigo AS DocCod, 
                                          F.Nome AS NomeFilial, 
                                          D.NumDocumento AS NumeroDoc, 
                                          (CONVERT(VARCHAR, D.Parcela) + '/' + CONVERT(VARCHAR, D.QtParcela)) AS Parcelas,
                                          B.Codigo AS CodBanco,
                                          TD.Nome AS TipoDocumento,
                                          P.Nome AS Cliente,
                                          M.dt AS Vencimento,
                                          M.Valor AS ValorMovimento,
                                          np.Valor AS FormaMovPgto,
                                          ISNULL(FP.Nome, '*Sem Forma de Pagamento') AS FormaPagamento,
                                          M.Sequencia AS SequenciaMovimento,
                                          M.ID AS IDMovimento,
                                          -- ISNULL((SELECT SUM(np1.Valor) 
                                          -- FROM notaparcela np1
                                          --WHERE np1.idnota = d.IDnota), 0)
										  np.Valor AS ValorFormaPgtoMov
                                          FROM Documento AS D
                                          JOIN Filial F ON F.ID = D.IDFilial
                                          JOIN Banco B ON B.ID = D.IDBanco
                                          JOIN TipoDocumento TD ON TD.ID = D.IDTipoDocumento
                                          JOIN Pessoa P ON P.ID = D.IDPessoa
                                          LEFT JOIN Movimento M ON M.IDDocumento = D.ID
                                          --LEFT JOIN FormaPgtoMovimento FM ON FM.IDMovimento = M.ID
                                          LEFT jOIN notaparcela NP on np.idnota = d.IDNota
                                          LEFT JOIN FormaPagamento FP ON FP.ID = np.TipoFormaPagamento
                                           WHERE
                                            (D.IDFilial IN " + IDsEmpresas + " OR D.IDFilial IS Null)AND " +
                                              " (NP.TipoFormaPagamento IN "
                                    + IDsFormasPagamento + (SemFormaPagto == true ?
                                    " OR NP.TipoFormaPagamento IS Null " : String.Empty) +
                                                   @")AND M.Dt >= :DataInicial AND M.Dt <= :DataFinal 
                                                                 AND (M.Operacao in(1,2,3)) AND D.Tipo = "
                                                   + (Tipo == TipoDocumentoType.Pagar ? "2" : "1"));

                Type FormaPgtoDocumento = typeof(pxyFormaPgtoDocumento);
                ConstructorInfo construtor = FormaPgtoDocumento.GetConstructor(new Type[] {typeof(Int32),
                                                                                            typeof(String),
                                                                                            typeof(String),
                                                                                            typeof(String),
                                                                                            typeof(Int32),
                                                                                            typeof(String),
                                                                                            typeof(String),
                                                                                            typeof(DateTime?),
                                                                                            typeof(Decimal),
                                                                                            typeof(Decimal?),
                                                                                            typeof(String),
                                                                                            typeof(Int32),
                                                                                            typeof(Int32),
                                                                                            typeof(Decimal)});

                IQuery query = session.CreateSQLQuery(Sql.ToString()).SetDateTime("DataInicial", dataInicial).SetDateTime("DataFinal", dataFinal);

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<Object>();
            }
        }

        private string HqlGetAllPossuiSaldoPorPeriodo()
        {
            return @"select new Documento(doc.Codigo, doc.DtVencimento, doc.DtPrevisao,doc.NumDocumento, doc.NossoNumero, pes.Nome,
                            doc.Valor,doc.Saldo,doc.Situacao, doc.DtUltimaBaixa, doc.NumRequisicao, fil.Nome, pes.Fantasia, 
                            td.Nome, por.Nome, doc.Parcela, doc.QtParcela, doc.Dt,doc.ID,doc.Tipo,
                            doc.IncData, doc.IncUsuario, doc.AltData, doc.AltUsuario, doc.DataDesconto, doc.NumeroPedido, doc.DtTransferenciaOuBaixaRet, hist.Nome, doc.ComplementoHist,
                            (select sum(m.Valor) from Movimento m where m.Tipo = 3 and doc.ID = m.Documento.ID) as Juros) 
                            from Documento doc
                            join doc.Portador por 
                            join doc.Pessoa pes 
                            join doc.TipoDocumento td 
                            join doc.Filial fil
                            join doc.Historico hist
                            where doc.DtVencimento >= :datainicial 
                            and doc.Saldo > 0                            
                            and doc.DtVencimento <= :datafinal ";
        }

        public IList<Documento> GetAllPossuiSaldoPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            using (var sessao = GetSession())
            {
                IQuery query = sessao.CreateQuery(HqlGetAllPossuiSaldoPorPeriodo())
                                     .SetDateTime("datainicial", dataInicial)
                                     .SetDateTime("datafinal", dataFinal);
                return query.List<Modelo.Documento>();
            }
        }

        public void AtualizaDocumento(string idsdocto, string idremessa)
        {
            try
            {
                String hql = " update Documento d set IDArquivoRemessa = " + idremessa + " where d.id in (" + idsdocto + ")";
                using (ISession sessao = GetSession())
                {
                    using (ITransaction trans = sessao.BeginTransaction())
                    {
                        sessao.CreateQuery(hql).ExecuteUpdate();
                        trans.Commit();
                    }
                }
            }
            catch (Exception e)
            {
                var a = e;
                throw;
            }

        }

        public IList<Documento> GetDocumentoPorCodigo(int codigo)
        {
            using (var sessao = GetSession())
            {
                string hql = "select d from Documento d " +
                            "left join fetch d.Nota n " +
                            "left join fetch d.Historico h " +
                            "left join fetch d.Movimentos mov" +
                            " where d.Codigo = :codigo";
                return sessao.CreateQuery(hql).SetInt32("codigo", codigo).List<Modelo.Documento>();
            }
        }

        public IList<Documento> GetDocumentoPorCodigo2(int codigo)
        {
            using (var sessao = GetSession())
            {
                string hql = "select d from Documento d " +
                            " where d.Codigo = :codigo";
                return sessao.CreateQuery(hql).SetInt32("codigo", codigo).List<Modelo.Documento>();
            }
        }


        public IList<Documento> GetAllPossuiSaldoPorPeriodoETipoDocumento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? receber, bool FlagTelefonia)
        {
            using (var sessao = GetSession())
            {
                string hql = "";

                if (!FlagTelefonia)
                {
                    hql = SelectNewDoc();
                }
                else
                {
                    hql = SelectNewDocTel();
                }

                hql += @" where doc.DtVencimento >= :datainicial 
                            and doc.Saldo > 0                            
                            and doc.DtVencimento < :datafinal ";

                if (receber != null)
                {
                    hql += " and doc.Tipo = :rec";
                }

                IQuery query = sessao.CreateQuery(hql)
                                     .SetDateTime("datainicial", dataInicial.Date)
                                     .SetDateTime("datafinal", dataFinal.Date.AddDays(1));
                if (receber != null)
                {
                    query = query.SetEnum("rec", receber);
                }
                IList<Documento> result = new List<Documento>();
                result = query.List<Modelo.Documento>();
                return result;
            }
        }

        public IList<Documento> GetAllDocumentoPorClienteEPeriodo(Pessoa cliente, DateTime dataInicial, DateTime dataFinal)
        {
            using (var sessao = GetSession())
            {
                string hql = "select new Documento(doc.ID, doc.NumDocumento, doc.Parcela, doc.QtParcela, doc.Dt, doc.DtVencimento, doc.Valor, doc.Saldo, doc.Tipo) from Documento doc where doc.DtVencimento >= :datainicial and doc.DtVencimento < :datafinal and doc.Pessoa.ID = :idPes and doc.Saldo > 0";
                IQuery query = sessao.CreateQuery(hql)
                                     .SetDateTime("datainicial", dataInicial.Date)
                                     .SetDateTime("datafinal", dataFinal.AddDays(1)).SetInt32("idPes", cliente.ID);
                return query.List<Modelo.Documento>().Distinct().ToList();
            }
        }

        public IList<Documento> GetAllBaixadoPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? receber)
        {
            using (var sessao = GetSession())
            {
                string hql = @"select new Documento(doc.Codigo, doc.DtVencimento, doc.DtPrevisao,doc.NumDocumento, doc.NossoNumero, pes.Nome,
                            doc.Valor,doc.Saldo,doc.Situacao, doc.DtUltimaBaixa, doc.NumRequisicao, fil.Nome, pes.Fantasia, 
                            td.Nome, por.Nome, doc.Parcela, doc.QtParcela, doc.Dt,doc.ID,doc.Tipo,
                            doc.IncData, doc.IncUsuario, doc.AltData, doc.AltUsuario, doc.DataDesconto, doc.NumeroPedido, doc.DtTransferenciaOuBaixaRet, hist.Nome doc.ComplementoHist,
                            (select sum(m.Valor) from Movimento m where m.Tipo = 3 and doc.ID = m.Documento.ID) as Juros) 
                            from Documento doc
                            join doc.Historico hist
                            join doc.Portador por join doc.Pessoa pes join doc.TipoDocumento td join doc.Filial fil
                            where doc.DtVencimento >= :datainicial and doc.DtVencimento < :datafinal and doc.Situacao = 'BxT'";
                if (receber != null)
                {
                    hql += "and doc.Tipo = :rec";
                }
                IQuery query = sessao.CreateQuery(hql)
                                     .SetDateTime("datainicial", dataInicial.Date)
                                     .SetDateTime("datafinal", dataFinal.AddDays(1));

                if (receber != null)
                {
                    query = query.SetEnum("rec", receber);
                }
                return query.List<Modelo.Documento>();
            }
        }

        public IList<Documento> GetAllAbertosPorCliente(Pessoa cliente)
        {
            using (var sessao = GetSession())
            {
                string hql = @"select new Documento(doc.Codigo, doc.DtVencimento, doc.DtPrevisao, doc.NumDocumento, doc.NossoNumero, pes.Nome,
                            doc.Valor,doc.Saldo,doc.Situacao, doc.DtUltimaBaixa, doc.NumRequisicao, fil.Nome, pes.Fantasia, 
                            td.Nome, por.Nome, doc.Parcela, doc.QtParcela, doc.Dt,doc.ID,doc.Tipo,
                            doc.IncData, doc.IncUsuario, doc.AltData, doc.AltUsuario, doc.DataDesconto, doc.NumeroPedido, doc.DtTransferenciaOuBaixaRet,hist.Nome, doc.ComplementoHist,
                            (select sum(m.Valor) from Movimento m where m.Tipo = 3 and doc.ID = m.Documento.ID) as Juros)  
                            from Documento doc
                            join doc.Historico hist
                            join doc.Portador por join doc.Pessoa pes join doc.TipoDocumento td join doc.Filial fil
                            where doc.Situacao <> 'Canc' and doc.Situacao <> 'BxT' and doc.Pessoa.ID = :idPessoa";

                return sessao.CreateQuery(hql).SetInt32("idPessoa", cliente.ID).List<Modelo.Documento>();
            }
        }

        public Decimal GetValorSaldoAllAbertosPorCliente(Pessoa cliente)
        {
            using (var sessao = GetSession())
            {
                string hql = @"select coalesce(sum(doc.Saldo),0)
                            from Documento doc
                            join doc.Historico hist
                            join doc.Portador por 
                            join doc.Pessoa pes 
                            join doc.TipoDocumento td 
                            join doc.Filial fil
                            where doc.Situacao <> 'Canc' and doc.Situacao <> 'BxT' and doc.Pessoa.ID = :idPessoa";

                return sessao.CreateQuery(hql).SetInt32("idPessoa", cliente.ID).List<Decimal>().FirstOrDefault();
            }
        }

        public IList<Documento> GetAllPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? receber, bool FlagTelefonia)
        {
            using (var sessao = GetSession())
            {
                string hql = "";

                if (!FlagTelefonia)
                {
                    hql = SelectNewDoc();
                }
                else
                {
                    hql = SelectNewDocTel();
                }

                hql += " where doc.DtVencimento >= :datainicial and doc.DtVencimento < :datafinal";
                if (receber != null)
                {
                    hql += " and doc.Tipo = :rec";
                }
                IQuery query = sessao.CreateQuery(hql)
                                     .SetDateTime("datainicial", dataInicial.Date)
                                     .SetDateTime("datafinal", dataFinal.AddHours(23).AddMinutes(59).AddSeconds(59));
                if (receber != null)
                {
                    query = query.SetEnum("rec", receber);
                }
                IList<Documento> result = new List<Documento>();
                result = query.List<Modelo.Documento>();
                return result;
            }
        }

        public IList<Documento> GetAllSaldoZeroPorVencimento(DateTime dataInicial, DateTime dataFinal, TipoDocumentoType? receber, bool FlagTelefonia)
        {
            using (var sessao = GetSession())
            {
                string hql = "";

                if (!FlagTelefonia)
                {
                    hql = SelectNewDoc();
                }
                else
                {
                    hql = SelectNewDocTel();
                }
                hql += " where doc.DtVencimento >= :datainicial and doc.DtVencimento < :datafinal and doc.Saldo = 0";
                if (receber != null)
                {
                    hql += " and doc.Tipo = :rec";
                }
                IQuery query = sessao.CreateQuery(hql)
                                     .SetDateTime("datainicial", dataInicial.Date)
                                     .SetDateTime("datafinal", dataFinal.AddHours(23).AddMinutes(59).AddSeconds(59));
                if (receber != null)
                {
                    query = query.SetEnum("rec", receber);
                }
                IList<Documento> result = new List<Documento>();
                result = query.List<Modelo.Documento>();
                return result;
            }
        }

        public long GetNumeroDocumentoValido()
        {
            using (var sessao = GetSession())
            {
                string hql = "select max(NumDocumento) from Documento";
                long num = 0;
                try
                {
                    num = Convert.ToInt64(sessao.CreateQuery(hql).UniqueResult()) + 1;
                }
                catch
                {
                }
                return num;
            }
        }

        public IList<Documento> GetPorProjeto(Projeto projeto)
        {
            using (var session = GetSession())
            {
                string hql = "select distinct d from Documento d " +
                             " join d.Movimentos mov" +
                             " join mov.Rateio_Movs rateio" +
                             " where rateio.Projeto.ID = :idProjeto";
                return session.CreateQuery(hql).SetInt32("idProjeto", projeto.ID).List<Documento>();
            }
        }

        private string HqlGetRemessa()
        {
            return @"select d.ID
                        , d.Codigo
                        , d.DtVencimento
                        , p.Nome
                        , d.NumDocumento
                        , d.Valor
                        , d.Saldo
                        , d.Situacao
                        , f.Nome
                        , p.Fantasia
                        , d.Tipo
                        , d.Parcela
                        , d.QtParcela
                        , d.Dt
                        ,t.Nome
                        , por.Nome
                    from Documento d
                    join d.Filial f
                    join d.Pessoa p
                    join d.TipoDocumento t
                    join d.Portador por
                    where d.Situacao <> 'BxT' and d.Situacao <> 'Canc'";
        }

        private ConstructorInfo GetContructorInfoPxyRemessa()
        {
            return typeof(pxyRemessa).GetConstructor(new Type[] {
                                                    typeof(Int32),
                                                    typeof(Int32),
                                                    typeof(DateTime?),
                                                    typeof(String),
                                                    typeof(String),
                                                    typeof(Decimal?),
                                                    typeof(Decimal?),
                                                    typeof(String),
                                                    typeof(String),
                                                    typeof(String),
                                                    typeof(Int32),
                                                    typeof(Int32?),
                                                    typeof(Int32?),
                                                    typeof(DateTime?),
                                                    typeof(String),
                                                    typeof(String)});
        }

        private ConstructorInfo GetConstructorInfopxyDocumentoArquivoRemessa()
        {
            return typeof(pxyDocumentoArquivoRemessa).GetConstructor(new Type[]{
                                                                      typeof(Int32),
                                                                      typeof(Int32),
                                                                      typeof(String),
                                                                      typeof(String),
                                                                      typeof(String),
                                                                      typeof(String),
                                                                      typeof(Int32),
                                                                      typeof(DateTime?),
                                                                      typeof(String),
                                                                      typeof(Decimal?),
                                                                      typeof(Decimal?),
                                                                      typeof(Int32?),
                                                                      typeof(Int32?),
                                                                      typeof(String),
                                                                      typeof(String)
                                                                      });
        }

        public IList<pxyRemessa> GetRemessa(Filial Filial, DateTime DataInicial, DateTime DataFinal, bool porVencimento, TipoDocumentoType tipo, bool impresso, bool geradoRemessa, bool geradoPDF, bool enviadoEmail, bool TodosDocumentos)
        {
            using (var session = GetSession())
            {
                if (TodosDocumentos)
                {
                    string hql = HqlGetRemessa() +
                                @"and f.ID = :idfilial
                                and d.Tipo = :tipo and d.Situacao <> 'BxR'";


                    if (porVencimento)
                        hql += " and d.DtVencimento >= :datainicial and d.DtVencimento <= :datafinal";
                    else
                        hql += " and d.Dt >= :datainicial and d.Dt <= :datafinal";
                    ConstructorInfo construtor = GetContructorInfoPxyRemessa();
                    IQuery query = session.CreateQuery(hql).SetInt32("idfilial", Filial.ID)
                                                           .SetInt32("tipo", (int)tipo)
                                                           .SetDateTime("datainicial", DataInicial)
                                                           .SetDateTime("datafinal", DataFinal);

                    return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyRemessa>();
                }
                else
                {
                    string hql = HqlGetRemessa() +
                           @"and f.ID = :idfilial
                            and d.Tipo = :tipo
                            and d.Situacao <> 'BxR'
                            and ((:impresso = true and d.BImpresso = true) or (:impresso = false and isNull(d.BImpresso,false) = false))
                            and ((:emailenviado = true and d.BEnviadoEmail = true) or (:emailenviado = false and isNull(d.BEnviadoEmail, false) = false))
                            and ((:pdfgerado = true and d.BGeradoPDF = true) or (:pdfgerado = false and isNull(d.BGeradoPDF, false) = false))
                            and ((:remessaenviada = true and d.BGeradoRemessa = true) or (:remessaenviada = false and isNull(d.BGeradoRemessa, false) = false))";

                    if (porVencimento)
                        hql += " and d.DtVencimento >= :datainicial and d.DtVencimento <= :datafinal";
                    else
                        hql += " and d.Dt >= :datainicial and d.Dt <= :datafinal";
                    ConstructorInfo construtor = GetContructorInfoPxyRemessa();

                    IQuery query = session.CreateQuery(hql).SetInt32("idfilial", Filial.ID)
                                                           .SetInt32("tipo", (int)tipo)
                                                           .SetBoolean("impresso", impresso)
                                                           .SetBoolean("emailenviado", enviadoEmail)
                                                           .SetBoolean("remessaenviada", geradoRemessa)
                                                           .SetBoolean("pdfgerado", geradoPDF)
                                                           .SetDateTime("datainicial", DataInicial)
                                                           .SetDateTime("datafinal", DataFinal);
                    return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyRemessa>();
                }
            }
        }

        public IList<cwkGestao.Modelo.Proxy.pxyRemessa> GetRemessaPorNota(Nota nota)
        {
            using (var session = GetSession())
            {
                string hql = HqlGetRemessa() +
                            @"and d.Nota.ID = :idnota";

                ConstructorInfo construtor = GetContructorInfoPxyRemessa();

                IQuery query = session.CreateQuery(hql).SetInt32("idnota", nota.ID);
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyRemessa>();
            }
        }

        public IList<Documento> GetGerarBoletoRemessa(List<cwkGestao.Modelo.Proxy.pxyRemessa> list)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select d from Documento d ");
                hql.AppendLine("left join fetch d.Acrescimo a ");
                hql.AppendLine("left join fetch d.Filial f ");
                hql.AppendLine("left join fetch d.Pessoa p ");
                hql.AppendLine("left join fetch p.PessoaEnderecos pe ");
                hql.AppendLine("left join fetch pe.Cidade cid ");
                hql.AppendLine("left join fetch cid.UF uf ");
                hql.AppendLine("left join fetch p.PessoaTelefones pt ");
                hql.AppendLine("left join fetch d.Nota nta ");
                hql.AppendLine(" where d.ID in (");
                if (list.Count == 0)
                    throw new Exception("Nenhuma Remessa encontrada.");

                for (int i = 0; i < list.Count; i++)
                {
                    if (i > 0)
                        hql.Append(", ");
                    hql.Append(list[i].ID);
                }
                hql.Append(")");

                //aqui

                return session.CreateQuery(hql.ToString()).List<Modelo.Documento>();
            }
        }

        protected string HqlLoadByCodigo()
        {
            return "from Documento d"
                 + " left join fetch d.Movimentos m"
                 + " left join fetch m.MovCaixa mc"
                 + " left join fetch mc.Banco bccc"
                 + " left join fetch m.FormaPagamentoMovimentos fpm"
                 + " left join fetch fpm.FormaPagamento fpmfp"
                 + " left join fetch d.Acrescimo a"
                 + " left join fetch d.Condicao con"
                 + " left join fetch m.PlanoConta pcdob"
                 + " left join fetch d.TipoDocumento tdd"
                 + " left join fetch d.Banco bcc"
                 + " left join fetch d.Filial f"
                 + " left join fetch d.Portador fdp"
                 + " left join fetch f.Cidade c "
                 + " left join fetch c.UF "
                 + " where d.Codigo = :codigo";
        }

        public Documento LoadObjectByCodigo(int codigo)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadByCodigo();

                Documento retorno = session.CreateQuery(sql).SetParameter("codigo", codigo)
                    .List<Documento>().FirstOrDefault();

                if (retorno != null && retorno.Nota != null)
                {
                    NHibernateUtil.Initialize(retorno.Nota);
                    NHibernateUtil.Initialize(retorno.Nota.NotaImpostoServicos);
                    NHibernateUtil.Initialize(retorno.Nota.NotaItemsServicos);
                    NHibernateUtil.Initialize(retorno.Nota.TipoNota);
                    if (retorno.Nota.TipoNota != null)
                        NHibernateUtil.Initialize(retorno.Nota.TipoNota.PlanoContaServico);
                }

                return retorno;
            }
        }

        public IList<Documento> GetAllByID(IList<int> ids)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from Documento d");
                hql.AppendLine(" left join fetch d.Movimentos m");
                hql.AppendLine(" left join fetch m.MovCaixa mc");
                hql.AppendLine(" where d.ID in (");
                for (int i = 0; i < ids.Count(); i++)
                {
                    if (i > 0)
                        hql.Append(", ");
                    hql.Append(ids[i]);
                }
                hql.Append(")");
                HashSet<Documento> set = new HashSet<Documento>(session.CreateQuery(hql.ToString()).List<Documento>());
                return set.ToList<Documento>();
            }
        }

        public IList<Documento> LoadAllByID(List<int> idsDocumentos)
        {
            using (var session = GetSession())
            {
                String hql = "from Documento d"
                    + " left join fetch d.Movimentos m"
                    + " left join fetch m.MovCaixa mc"
                    + " left join fetch mc.Banco bccc"
                    + " left join fetch m.FormaPagamentoMovimentos fpm"
                    + " left join fetch fpm.FormaPagamento fpmfp"
                    + " left join fetch d.Acrescimo a"
                    + " left join fetch d.Condicao con"
                    + " left join fetch m.PlanoConta pcdob"
                    + " left join fetch d.TipoDocumento tdd"
                    + " left join fetch d.Banco bcc"
                    + " left join fetch d.Filial f"
                    + " left join fetch d.Portador fdp"
                    + " left join fetch f.Cidade c "
                    + " left join fetch c.UF "
                    + "where d.ID in (:idsDocumentos)";

                IList<Modelo.Documento> documentosPorID = session.CreateQuery(hql).SetParameterList("idsDocumentos", idsDocumentos).List<Modelo.Documento>();
                documentosPorID = documentosPorID.DistinctBy(s => s.ID).ToList();

                return documentosPorID;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            string hql = SelectNewDoc() + " where doc.DtVencimento between :dataInicio and :dataFim";
            return hql;
        }

        public override IList<Documento> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros)
        {
            TipoDocumentoType? tipoDoc = null;
            bool somenteBaixados = false;
            bool usaTelefonia = false;
            bool confSomenteBaixados = false;

            using (var session = GetSession())
            {
                Configuracao conf = new Configuracao();
                ConfiguracaoRepositorio repConf = new ConfiguracaoRepositorio();
                conf = repConf.GetAll().FirstOrDefault();

                if (conf != null)
                {
                    usaTelefonia = Convert.ToBoolean(conf.UtilizaTelefonia);
                    confSomenteBaixados = conf.BListarBaixados;
                }
            }

            if (parametros == null)
            {
                return base.GetAllByPeriod(start, end, parametros);
            }

            if (parametros.ContainsKey("tipoDocumento"))
            {
                tipoDoc = (TipoDocumentoType)parametros["tipoDocumento"];
            }
            if (parametros.ContainsKey("somenteBaixados"))
            {
                somenteBaixados = Convert.ToBoolean(parametros["somenteBaixados"]);
            }
            if (somenteBaixados)
            {
                return GetAllSaldoZeroPorVencimento(start, end, tipoDoc, usaTelefonia);
            }
            if (confSomenteBaixados)
            {
                return GetAllPorVencimento(start, end, tipoDoc, usaTelefonia);
            }
            return GetAllPossuiSaldoPorPeriodoETipoDocumento(start, end, tipoDoc, usaTelefonia);
        }

        public int GetNovoCodigoDocumento()
        {
            using (var session = GetSession()) return session.CreateSQLQuery("SELECT NEXT VALUE FOR [SEQ_CODIGO_DOCUMENTO]").UniqueResult<int>();
        }

        public IList<Documento> GetAllRegistro0400(DateTime dataInicial, DateTime dataFinal)
        {
            using (var sessao = GetSession())
            {
                string hql = SelectNewDoc();
                
                hql += @" where doc.Dt >= :datainicial                            
                            and doc.Dt < :datafinal ";

                IQuery query = sessao.CreateQuery(hql)
                    .SetDateTime("datainicial", dataInicial.Date)
                    .SetDateTime("datafinal", dataFinal.Date.AddDays(1));

                IList<Documento> result = new List<Documento>();
                result = query.List<Modelo.Documento>();
                return result;
            }
        }
    }
}
