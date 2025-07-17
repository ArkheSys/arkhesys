using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;

using NHibernate;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    internal class NotaRepositorio : RepositorioAbstrato<Nota>, IRepositorioNota
    {
        protected override string HqlLoadById()
        {
            return " from Nota n" +
                   " left join fetch n.Filial " +
                   " left join fetch n.NotaICMSs " +
                   " left join fetch n.NotaItems " +
                   " left join fetch n.NotaItemsServicos " +
                   " left join fetch n.NotaImpostoServicos " +
                   " left join fetch n.NotaParcelas " +
                   " left join fetch n.Vendedor " +
                   " left join fetch n.TabelaPreco " +
                   " left join fetch n.PessoaTransportadora " +
                   " left join fetch n.TipoNota " +
                   " left join fetch n.Condicao " +
                   " left join fetch n.EnderecoEntrega " +
                   " left join fetch n.Cartas " +
                   " left join fetch n.NotaReferenciada " +
                   " where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return
                " select distinct new Nota(n.ID, n.Dt, n.Codigo, n.ModeloDocto, n.Serie, n.Numero, n.Pessoa.Nome, n.TotalNota, n.Filial.ID, n.Filial.Nome, n.TipoNota.Nome, n.Status, n.IncData, n.IncUsuario, n.AltData, n.AltUsuario, n.NumeroNFSe, n.CodigoPedido, n.DataEnvioNfe, n.NumeroEmpenho, n.StatusLocal, '') from Nota n ";
        }

        public override Nota LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string hql = " from Nota n" +
                             " left join fetch n.Filial " +
                             " left join fetch n.Vendedor " +
                             " left join fetch n.TabelaPreco " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.PessoaTransportadora " +
                             " left join fetch n.EnderecoEntrega " +
                             " left join fetch n.NotaComplementada " +
                             " left join fetch n.Pessoa " +
                             " left join fetch n.NotaItems ni " +
                             " left join fetch n.NotaReferenciada " +
                             " where n.ID = :id";

                Nota nota = session.CreateQuery(hql).SetInt32("id", id).UniqueResult<Nota>();
                NHibernateUtil.Initialize(nota.NotaICMSs);
                NHibernateUtil.Initialize(nota.NotaParcelas);
                NHibernateUtil.Initialize(nota.NotaImpostoServicos);
                NHibernateUtil.Initialize(nota.NotaItemsServicos);
                NHibernateUtil.Initialize(nota.Cartas);
                NHibernateUtil.Initialize(nota.Documentos);
                NHibernateUtil.Initialize(nota.NotaItems);
                NHibernateUtil.Initialize(nota.NotaReferencias);

                foreach (var notaItem in nota.NotaItems)
                {
                    notaItem.ProdutoNome = notaItem.Produto.Nome;
                    NHibernateUtil.Initialize(notaItem.Produto);
                    NHibernateUtil.Initialize(notaItem.ICMS);
                    NHibernateUtil.Initialize(notaItem.Produto.ProdutoCodBarra);
                    NHibernateUtil.Initialize(notaItem.Produto.GrupoEstoque);
                    if (notaItem.PedidoItens != null)
                        foreach (var item in notaItem.PedidoItens)
                            if (item.PedidoItem != null)
                            {
                                NHibernateUtil.Initialize(item.PedidoItem);
                                NHibernateUtil.Initialize(item.PedidoItem.Pedido);
                                NHibernateUtil.Initialize(item.PedidoItem.Pedido.Items);
                            }
                }

                if (nota.NotaComplementada != null) NHibernateUtil.Initialize(nota.NotaComplementada.NotaItems);

                return nota;
            }
        }

        public IList<NotaItem> GetProdutoVendaPorClienteNoPeriodo(Pessoa pessoa, Produto produto, DateTime dateTime, DateTime dateTime_3, InOutType inOut)
        {
            using (var session = GetSession())
            {
                string hql = " from NotaItem ni" +
                             " left join fetch ni.Nota n " +
                             " left join fetch n.TipoNota t " +
                             " where n.Dt between :dInicio and :dFim " +
                             " and ni.Nota.Ent_Sai = " + (int)inOut;
                if (pessoa != null)
                    hql += " and n.Pessoa.ID = " + pessoa.ID;
                if (produto != null)
                    hql += " and ni.Produto.ID = " + produto.ID;

                var query = session.CreateQuery(hql).SetDateTime("dInicio", dateTime).SetDateTime("dFim", dateTime_3);

                var notaItens = query.List<NotaItem>();

                return notaItens;
            }
        }

        public Nota GetByCodigo(int codigo)
        {
            using (var session = GetSession())
            {
                string hql = " from Nota n" +
                             " left join fetch n.Filial " +
                             " left join fetch n.Vendedor " +
                             " left join fetch n.TabelaPreco " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.PessoaTransportadora " +
                             " left join fetch n.EnderecoEntrega " +
                             " left join fetch n.NotaReferenciada " +
                             " where n.Codigo = :id";

                Nota nota = session.CreateQuery(hql).SetInt32("id", codigo).UniqueResult<Nota>();
                NHibernateUtil.Initialize(nota.NotaICMSs);
                NHibernateUtil.Initialize(nota.NotaParcelas);
                NHibernateUtil.Initialize(nota.NotaImpostoServicos);
                NHibernateUtil.Initialize(nota.NotaItemsServicos);
                NHibernateUtil.Initialize(nota.NotaItems);
                NHibernateUtil.Initialize(nota.NotaReferencias);

                return nota;
            }
        }

        public Nota GetBySerieENumero(string serie, int numero)
        {
            using (var session = GetSession())
            {
                string hql = " from Nota n" +
                             " left join fetch n.Filial " +
                             " left join fetch n.Vendedor " +
                             " left join fetch n.TabelaPreco " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.PessoaTransportadora " +
                             " left join fetch n.EnderecoEntrega " +
                             " left join fetch n.NotaReferenciada " +
                             " where n.Serie = :serie AND n.Numero = :numero";

                return session.CreateQuery(hql)
                    .SetString("serie", serie)
                    .SetInt32("numero", numero)
                    .List<Nota>().FirstOrDefault();
            }
        }

        public int GetNovoNumeroSerie(Nota nota, int? Serie)
        {
            using (var session = GetSession())
            {
                string hql = "select max(n.Numero) from Nota n" +
                             " left join n.TipoNota tn" +
                             " where n.Filial.ID = " + nota.Filial.ID +
                             " and n.Serie like '" + (Serie.HasValue ? Serie.Value.ToString() : nota.TipoNota.Serie) + "'" +
                             " and (n.ModeloDocto != 2 or n.ModeloDocto != 3)" +
                             " and (n.ModeloDocto = " + nota.ModeloDocto + ")" +
                             " and (tn.Categoria = " + (int)TipoNota.CategoriaMovimentacao.Produtos +
                             "      or tn.Categoria = " + (int)TipoNota.CategoriaMovimentacao.ProdutosEServicos + ")" +
                             " and n.TipoNota.NFOrigem = 1";
                return Convert.ToInt32(session.CreateQuery(hql).List()[0]);
            }
        }

        public int GetNovoNumeroNFSe(Nota nota)
        {
            using (var session = GetSession())
            {
                string hql = "select max(n.Numero) from Nota n" +
                             " left join n.TipoNota tn" +
                             " where n.Filial.ID = " + nota.Filial.ID +
                             " and n.Serie like '" + nota.TipoNota.Serie + "'" +
                             " and tn.Categoria = " + (int)TipoNota.CategoriaMovimentacao.Servicos +
                             " and (n.ModeloDocto = 2 or n.ModeloDocto = 3)";

                return Convert.ToInt32(session.CreateQuery(hql).List()[0]);
            }
        }

        public int GetNovoCodigoNota()
        {
            using (var session = GetSession()) return session.CreateSQLQuery("SELECT NEXT VALUE FOR[SEQ_CODIGO_NOTA]").UniqueResult<int>();
        }

        public int GetNovoCodigoFrenteCaixaNota()
        {
            using (var session = GetSession()) return session.CreateSQLQuery("SELECT NEXT VALUE FOR [SEQ_CODIGO_FRENTECAIXANOTA]").UniqueResult<int>();
        }

        public int GetNovoNumeroLoteNFSe()
        {
            using (var session = GetSession())
            {
                string hql = "select max(n.LoteNFSe) from Nota n";
                return Convert.ToInt32(session.CreateQuery(hql).List()[0]);
            }
        }

        public IList<Nota> GetNotasPorClienteNoPeriodo(Pessoa cliente, DateTime inicio, DateTime fim)
        {
            using (var session = GetSession())
            {
                string hql = @"from Nota n 
                               join fetch n.NotaImpostoServicos nis 
                               left join fetch n.NotaReferenciada
                               where n.Pessoa.ID = :idCliente and n.Dt between :inicio and :fim";
                IQuery query =
                    session.CreateQuery(hql).SetDateTime("inicio", inicio).SetDateTime("fim", fim);
                query.SetInt32("idCliente", cliente.ID);
                return query.List<Nota>();
            }
        }

        public IList<Nota> GetAll(InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas)
        {
            using (var session = GetSession())
            {
                var hql =
                    " select distinct new Nota(n.ID, n.Dt, n.Codigo, n.ModeloDocto, n.Serie, n.Numero, n.Pessoa.Nome, n.TotalNota, n.Filial.ID, n.Filial.Nome, n.TipoNota.Nome, n.Status, n.IncData, n.IncUsuario, n.AltData, n.AltUsuario, n.NumeroNFSe, n.CodigoPedido, n.DataEnvioNfe, n.NumeroEmpenho, n.StatusLocal, pv.Nome) from Nota n ";

                switch (produtoServico)
                {
                    case ProdutoServicoType.Produtos:
                        hql += " join n.NotaItems";
                        break;
                    case ProdutoServicoType.Servicos:
                        hql += " join n.NotaItemsServicos";
                        break;
                }

                hql += " left join n.Vendedor v";
                hql += " left join v.Pessoa pv";

                hql += " WHERE n.Ent_Sai = :ent_sai"
                       + " AND n.ModeloDocto > 0"
                       + " AND n.NotaComplementada is null";

                if (notasCanceladas)
                    hql += " AND n.CancDt is not null";
                else
                    hql += " AND n.CancDt is null";

                var query = session.CreateQuery(hql).SetEnum("ent_sai", entSai);
                return query.List<Nota>();
            }
        }

        public IList<Nota> GetAllPeriodo(DateTime dataInicial, DateTime dataFinal, InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas)
        {
            using (var session = GetSession())
            {
                var hql =
                    " select distinct new Nota(n.ID, n.Dt, n.Codigo, n.ModeloDocto, n.Serie, n.Numero, n.Pessoa.Nome, n.TotalNota, n.Filial.ID, n.Filial.Nome, n.TipoNota.Nome, n.Status, n.IncData, n.IncUsuario, n.AltData, n.AltUsuario, n.NumeroNFSe, n.CodigoPedido, n.DataEnvioNfe, n.NumeroEmpenho, n.StatusLocal, pv.Nome) from Nota n ";

                switch (produtoServico)
                {
                    case ProdutoServicoType.Produtos:
                        hql += " join n.NotaItems";
                        break;
                    case ProdutoServicoType.Servicos:
                        hql += " join n.NotaItemsServicos";
                        break;
                }

                hql += " left join n.Vendedor v";
                hql += " left join v.Pessoa pv";

                hql += " WHERE n.Dt between :dataInicial and :dataFinal"
                       + " AND n.Ent_Sai = :ent_sai"
                       + " AND n.ModeloDocto > 0"
                       + " AND n.NotaComplementada is null";

                if (notasCanceladas)
                    hql += " AND n.CancDt is not null";
                else
                    hql += " AND n.CancDt is null";

                var query = session.CreateQuery(hql)
                    .SetDateTime("dataInicial", dataInicial)
                    .SetDateTime("dataFinal", dataFinal)
                    .SetEnum("ent_sai", entSai);

                return query.List<Nota>();
            }
        }

        public IList<Nota> LoadAllByIds(List<int> idsNotas)
        {
            using (var session = GetSession())
            {
                var hql = " from Nota n" +
                          " left join fetch n.Filial " +
                          " left join fetch n.Vendedor " +
                          " left join fetch n.TabelaPreco " +
                          " left join fetch n.Condicao " +
                          " left join fetch n.TipoNota " +
                          " left join fetch n.PessoaTransportadora " +
                          " left join fetch n.EnderecoEntrega " +
                          " left join fetch n.NotaComplementada " +
                          " left join fetch n.Pessoa " +
                          " left join fetch n.NotaItems " +
                          " left join fetch n.NotaReferenciada " +
                          " where n.ID in (:ids)";

                var query = session.CreateQuery(hql).SetParameterList("ids", idsNotas);

                return query.List<Nota>();
            }
        }

        public IList<Nota> GetAutorizadasPorCliente(InOutType entSai, ProdutoServicoType produtoServico, bool notasCanceladas, int idPessoa, string statusNota)
        {
            using (var session = GetSession())
            {
                var hql = hqlGetAll();

                switch (produtoServico)
                {
                    case ProdutoServicoType.Produtos:
                        hql += " join n.NotaItems";
                        break;
                    case ProdutoServicoType.Servicos:
                        hql += " join n.NotaItemsServicos";
                        break;
                }

                hql += " WHERE n.Ent_Sai = :ent_sai"
                       + " AND n.ModeloDocto > 0"
                       + " AND n.NotaComplementada is null";

                if (notasCanceladas)
                    hql += " AND n.CancDt is not null";
                else
                    hql += " AND n.CancDt is null";

                IQuery query;
                if (entSai == InOutType.Saída)
                    hql += " AND n.Status = :statusNota";

                if (idPessoa != 0)
                {
                    hql += " AND n.Pessoa.ID = :idPessoa";
                    if (entSai == InOutType.Saída)
                        query = session.CreateQuery(hql).SetEnum("ent_sai", entSai).SetInt32("idPessoa", idPessoa).SetString("statusNota", statusNota);
                    else
                        query = session.CreateQuery(hql).SetEnum("ent_sai", entSai).SetInt32("idPessoa", idPessoa);
                }
                else
                {
                    if (entSai == InOutType.Saída)
                        query = session.CreateQuery(hql).SetEnum("ent_sai", entSai).SetString("statusNota", statusNota);
                    else
                        query = session.CreateQuery(hql).SetEnum("ent_sai", entSai);
                }

                var retorno = query.List<Nota>();

                return retorno;
            }
        }

        public IList<Nota> GetNotaAutorizadaPorPedido(int CodigoPedido, bool Autorizada = true)
        {
            using (var session = GetSession())
                return session.CreateQuery($@"from Nota n 
                                               where n.CodigoPedido = :CodigoPed
                                                 {(Autorizada ? "and n.Status = '2'" : "")}
                                                 and n. CancDt is null").SetInt32("CodigoPed", CodigoPedido).List<Nota>();
        }

        public IList<Nota> GetAllComplementares()
        {
            using (var session = GetSession())
            {
                var hql = hqlGetAll()
                          + " join n.NotaComplementada "
                          + " join n.NotaItems "
                          + " WHERE n.ModeloDocto = 55 AND n.CancDt is null ";

                var query = session.CreateQuery(hql);
                return query.List<Nota>();
            }
        }

        public Nota GetNotaComplementar(Nota nota)
        {
            using (var session = GetSession())
            {
                string hql = " from Nota n" +
                             " join fetch n.NotaComplementada " +
                             " left join fetch n.Filial " +
                             " left join fetch n.Vendedor " +
                             " left join fetch n.TabelaPreco " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.PessoaTransportadora " +
                             " left join fetch n.EnderecoEntrega " +
                             " left join fetch n.NotaReferenciada " +
                             " where n.NotaComplementada.ID = :idNota";

                var result = session.CreateQuery(hql).SetInt32("idNota", nota.ID).List<Nota>();

                if (result.Count > 0)
                {
                    var notaComplementar = result[0];
                    NHibernateUtil.Initialize(notaComplementar.NotaICMSs);
                    NHibernateUtil.Initialize(notaComplementar.NotaParcelas);
                    NHibernateUtil.Initialize(notaComplementar.NotaImpostoServicos);
                    NHibernateUtil.Initialize(notaComplementar.NotaItemsServicos);
                    NHibernateUtil.Initialize(notaComplementar.NotaItems);
                    NHibernateUtil.Initialize(notaComplementar.NotaReferencias);
                    return notaComplementar;
                }

                return new Nota();
            }
        }

        public Nota GetUltimoCupomEmitido()
        {
            using (var session = GetSession())
            {
                string hql = "select n from Nota n" +
                             " where n.TipoNota.ModeloDocto = 33" +
                             " order by n.Dt desc, n.Numero desc";
                var query = session.CreateQuery(hql);
                query.SetMaxResults(1);
                return query.List<Nota>().FirstOrDefault();
            }
        }

        public IList<Nota> GetAllComplementares(InOutType inOut)
        {
            using (var session = GetSession())
            {
                var hql = hqlGetAll();
                hql += " JOIN n.NotaComplementada nc "
                       + " WHERE n.Ent_Sai = :ent_sai"
                       + " AND n.ModeloDocto > 0"
                       + " AND n.CancDt is null";

                var query = session.CreateQuery(hql).SetEnum("ent_sai", inOut);
                return query.List<Nota>();
            }
        }

        public IList<Nota> GetAllRequisicoesBaixadas(InOutType inOut)
        {
            using (var session = GetSession())
            {
                var hql = hqlGetAll();
                hql += " WHERE n.Ent_Sai = :ent_sai"
                       + " AND n.ModeloDocto = 0";

                var query = session.CreateQuery(hql).SetEnum("ent_sai", inOut);
                return query.List<Nota>();
            }
        }

        public IList<Nota> GetNotasPorFilialNoPeriodo(Filial filial, DateTime inicio, DateTime fim)
        {
            using (var session = GetSession())
            {
                string hql = " from Nota n" +
                             " left join fetch n.Filial f " +
                             " left join fetch n.Vendedor " +
                             " left join fetch n.TabelaPreco " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.PessoaTransportadora " +
                             " left join fetch n.EnderecoEntrega " +
                             " left join fetch n.NotaComplementada " +
                             " left join fetch n.NotaItems ni " +
                             " left join fetch n.NotaReferenciada " +
                             " left join fetch ni.PedidoItem pi " +
                             " left join fetch pi.Pedido p " +
                             " where f.ID = :idfilial and n.Dt between :inicio and :fim";
                IQuery query = session.CreateQuery(hql)
                    .SetDateTime("inicio", inicio)
                    .SetDateTime("fim", fim)
                    .SetInt32("idfilial", filial.ID);

                return query.List<Nota>();
            }
        }

        public IList<Nota> GetNotasValidaParana(Filial filial, DateTime inicio, DateTime fim, InOutType? pEntSai, bool Cupom)
        {
            inicio = inicio.Date;
            fim = fim.Date;

            using (var session = GetSession())
            {
                string hql = "from Nota n" +
                             " left join fetch n.Filial f " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " where f.ID = :idfilial and n.Dt between :inicio and :fim ";

                if (pEntSai != null && Convert.ToInt32(pEntSai) != 3)
                    hql = hql + " and n.Ent_Sai = :pEntSai";
                else
                    hql = hql + " and n.Ent_Sai <> :pEntSai";

                if (Cupom)
                    hql = hql + " and n.ModeloDocto = n.ModeloDocto";
                else
                    hql = hql + " and n.ModeloDocto != 33";

                hql = hql + " order by n.Dt, n.Numero";
                IQuery query = session.CreateQuery(hql)
                    .SetDateTime("inicio", inicio)
                    .SetDateTime("fim", fim)
                    .SetInt32("idfilial", filial.ID)
                    .SetInt32("pEntSai", pEntSai != null ? Convert.ToInt32(pEntSai) : 0);

                IList<Nota> temp = query.List<Nota>();
                foreach (Nota nota in temp)
                {
                    if (nota.NotaItems != null)
                        foreach (var item in nota.NotaItems)
                        {
                            NHibernateUtil.Initialize(item);
                            NHibernateUtil.Initialize(item.Produto);
                        }

                    if (nota.NotaICMSs != null)
                        foreach (var item in nota.NotaICMSs)
                            NHibernateUtil.Initialize(item);
                }

                return temp;
            }
        }

        public IList<Nota> GetNotasItensValidaParana(Filial filial, DateTime inicio, DateTime fim, InOutType? pEntSai, bool Cupom)
        {
            inicio = inicio.Date;
            fim = fim.Date;

            using (var session = GetSession())
            {
                string hql = "from Nota n" +
                             " left join fetch n.Filial f " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.NotaItems ni " +
                             " left join fetch ni.Produto p " +
                             " left join fetch p.TabelaNCM " +
                             " left join fetch n.NotaReferenciada " +
                             " where f.ID = :idfilial and n.Dt between :inicio and :fim";

                if (pEntSai != null && Convert.ToInt32(pEntSai) != 3)
                    hql = hql + " and n.Ent_Sai = :pEntSai";
                else
                    hql = hql + " and n.Ent_Sai <> :pEntSai";

                if (Cupom)
                    hql = hql + " and n.ModeloDocto = n.ModeloDocto";
                else
                    hql = hql + " and n.ModeloDocto != 33";

                hql = hql + " order by n.Dt, n.Numero";
                IQuery query = session.CreateQuery(hql)
                    .SetDateTime("inicio", inicio)
                    .SetDateTime("fim", fim)
                    .SetInt32("idfilial", filial.ID)
                    .SetInt32("pEntSai", pEntSai != null ? Convert.ToInt32(pEntSai) : 0);

                IList<Nota> temp = query.List<Nota>();
                foreach (Nota nota in temp)
                {
                    if (nota.NotaItems != null)
                        foreach (var item in nota.NotaItems)
                        {
                            NHibernateUtil.Initialize(item);
                            NHibernateUtil.Initialize(item.Produto);
                        }

                    if (nota.NotaICMSs != null)
                        foreach (var item in nota.NotaICMSs)
                            NHibernateUtil.Initialize(item);
                }

                return temp;
            }
        }

        public IList<Nota> GetNotasPorImpostoENota(ServicoImposto servicoImposto, Nota nota)
        {
            using (var session = GetSession())
            {
                string hql = " from Nota n" +
                             " left join fetch n.Filial f " +
                             " left join fetch n.Vendedor " +
                             " left join fetch n.TabelaPreco " +
                             " left join fetch n.Condicao " +
                             " left join fetch n.TipoNota " +
                             " left join fetch n.PessoaTransportadora " +
                             " left join fetch n.EnderecoEntrega " +
                             " left join fetch n.NotaItemsServicos ns " +
                             " left join fetch n.NotaImpostoServicos nis " +
                             " left join fetch n.NotaReferenciada " +
                             " where n.ID <> :idNota and n.Status != 3 and nis.Imposto.ID = :idServicoImposto and n.Pessoa.ID = :idPessoa and n.Dt between :inicio and :fim";
                IQuery query = session.CreateQuery(hql)
                    .SetInt32("idNota", nota.ID)
                    .SetInt32("idPessoa", nota.Pessoa.ID)
                    .SetInt32("idServicoImposto", servicoImposto.ID)
                    .SetDateTime("inicio", new DateTime(nota.Dt.Year, nota.Dt.Month, 1, 0, 0, 0))
                    .SetDateTime("fim", nota.Dt);
                return query.List<Nota>();
            }
        }

        public ConstructorInfo GetContructorInfopxyCreditoDebito()
        {
            return typeof(pxyCreditoDebito).GetConstructor(new[]
            {
                typeof(DateTime),
                typeof(InOutType),
                typeof(bool),
                typeof(bool),
                typeof(bool),
                typeof(bool),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal)
            });
        }

        public IList<pxRelatorioProdutosMaisvendidos> GetProdutosMaisVendidos(int IDEmpresa,
            int? IDPessoa, DateTime DataInicio, DateTime DataFinal,
            int Status = 2, string Ativos = "", string modeloDocto = "")
        {
            using (var session = GetSession())
            {
                List<string> sqlUnion = new List<string>
                {
                    $@"select Produto.id as idproduto,
		                               Produto.barra,
		                               Produto.codigoReferencia,
		                               Produto.codigo,
		                               Produto.nome,
		                               Nota.IDFilial,
		                               sum(COALESCE(Notaitem.quantidade, 0)) as quantidade,
		                               sum(COALESCE(Notaitem.valor, 0)) as valor,
		                               sum(COALESCE(Notaitem.total, 0)) as total
	                            from nota
		                            inner join NotaItem on (nota.id = NotaItem.IDNota)
		                            inner join Produto on (notaitem.idproduto = produto.id)
		                            where nota.Status = {Status}
                                      and nota.IDFilial = :idFilial
                                      and cast(nota.Dt as date) between :dataInicio and :dataFim
                                      {Ativos}
                                      {modeloDocto}
                                      {(IDPessoa.HasValue ? "and Produto.IDFornecedor = " + IDPessoa : string.Empty)}
	                            group by Produto.id,
		                               Produto.barra,
		                               Produto.codigoReferencia,
		                               Produto.codigo,
		                               Produto.nome,
		                               Nota.IDFilial"
                };

                if (modeloDocto == null) // é somente Pedidos
                    sqlUnion.Clear();

                if (string.IsNullOrEmpty(modeloDocto))
                {
                    sqlUnion.Add($@"select Produto.id as idproduto,
		                                   Produto.barra,
		                                   Produto.codigoReferencia,
		                                   Produto.codigo,
		                                   Produto.nome,
		                                   Pedido.IDFilial,
		                                   sum(COALESCE(PedidoItem.quantidade, 0)) as quantidade,
		                                   sum(COALESCE(PedidoItem.valor, 0)) as valor,
		                                   sum(COALESCE(PedidoItem.total, 0)) as total
	                                from Pedido
		                                inner join PedidoItem on (Pedido.id = PedidoItem.IDPedido)
		                                inner join Produto on (PedidoItem.idproduto = produto.id)
		                                where Pedido.Status = 2
                                          and Pedido.Codigo not in (select n.codigopedido from Nota n where n.status is not null)
                                          and Pedido.IDFilial = :idFilial
                                          and cast(Pedido.Dt as date) between :dataInicio and :dataFim
                                          {Ativos}
                                          {(IDPessoa.HasValue ? "and Produto.IDFornecedor = " + IDPessoa : string.Empty)}
                                    group by Produto.id,
		                                   Produto.barra,
		                                   Produto.codigoReferencia,
		                                   Produto.codigo,
		                                   Produto.nome,
		                                   Pedido.IDFilial");
                }

                var sqlExecutar = $@"select idproduto,
                                            barra,
		                                    codigoreferencia,
		                                    codigo,
		                                    nome,
                                            dbo.f_SaldoEstoqueFilial(IDFilial, IDProduto) as quantidadeEstoque,
		                                    sum(quantidade) as quantidade,
		                                    sum(valor) as valor,
		                                    sum(total) as total
                                     from ({string.Join(" UNION ALL ", sqlUnion)}) as x 
                                    group by idproduto, barra, codigoreferencia, codigo, nome, dbo.f_SaldoEstoqueFilial(IDFilial, IDProduto)
                                    order by nome";

                IQuery query = session.CreateSQLQuery(sqlExecutar)
                    .SetDateTime("dataInicio", DataInicio)
                    .SetDateTime("dataFim", DataFinal)
                    .SetInt32("idFilial", IDEmpresa);

                ConstructorInfo construtor = typeof(pxRelatorioProdutosMaisvendidos).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(decimal?),
                    typeof(decimal?),
                    typeof(decimal?),
                    typeof(decimal?)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxRelatorioProdutosMaisvendidos>();
            }
        }

        public IList<pxRelatorioItensCanceladosPDV> GetItensCanceladosPDV(int IDEmpresa, DateTime DataInicial, DateTime DataFinal, int? codigoInicial, int? codigoFinal)
        {
            using (var session = GetSession())
            {
                string SQL = $@"select cw_usuario.login as usuario,
                                       Produto.id as idproduto,
                                       Produto.codigo,
                                       Produto.nome,
									   FrenteCaixaNotaItem.MotivoCancelamento,
                               	       COALESCE(sum(FrenteCaixaNotaItem.quantidade), 0) as quantidade,
                               	       COALESCE(sum(FrenteCaixaNotaItem.valor), 0) as valor,
                                       COALESCE(sum(FrenteCaixaNotaItem.total), 0) as total
                                from FrenteCaixaNota
								    inner join Nota on (FrenteCaixaNota.IDNota = Nota.ID)
                                    inner join FrenteCaixaNotaItem on (FrenteCaixaNota.ID = FrenteCaixaNotaItem.IdFrenteCaixaNota)
                               	    inner join Produto on (FrenteCaixaNotaItem.IdProduto = produto.id)
									 left join cw_usuario on (FrenteCaixaNota.IncUsuario = cw_usuario.login)
							    where FrenteCaixaNotaItem.Cancelado = 1
                                  and nota.idfilial = :idFilial
                                  {(codigoInicial.HasValue && codigoFinal.HasValue ? $"and (cw_usuario.codigo >= {codigoInicial} and cw_usuario.codigo <= {codigoFinal})" : "")}
								  and cast(nota.Dt as date) between :dataInicio and :dataFim
                                group by cw_usuario.login, Produto.id, produto.nome, Produto.Codigo, FrenteCaixaNotaItem.MotivoCancelamento
                                order by Produto.nome";

                IQuery query = session.CreateSQLQuery(SQL)
                    .SetDateTime("dataInicio", DataInicial)
                    .SetDateTime("dataFim", DataFinal)
                    .SetInt32("idFilial", IDEmpresa);

                ConstructorInfo construtor = typeof(pxRelatorioItensCanceladosPDV).GetConstructor(new[]
                {
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(decimal?),
                    typeof(decimal?),
                    typeof(decimal?)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxRelatorioItensCanceladosPDV>();
            }
        }

        ////relatorio de tabela de preço 
        //public IList<pxRelatorioTabelaPreco> GetItensCanceladosPDV(int IDEmpresa, DateTime DataInicial, DateTime DataFinal, int? codigoInicial, int? codigoFinal)
        //{
        //    using (var session = GetSession())
        //    {
        //        string SQL = $@"select cw_usuario.login as usuario,         

        //        ConstructorInfo construtor = typeof(pxRelatorioTabelaPreco).GetConstructor(new[]
        //        {
        //            typeof(string),

        //        });

        //        return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxRelatorioItensCanceladosPDV>();
        //    }
        //}

        ////relatorio de tabela de preço  por itens de venda
        //public IList<pxRelatorioItensCanceladosPDV> GetItensCanceladosPDV(int IDEmpresa, DateTime DataInicial, DateTime DataFinal, int? codigoInicial, int? codigoFinal)
        //{
        //    using (var session = GetSession())
        //    {
        //        string SQL = $@"select cw_usuario.login as usuario,


        //        ConstructorInfo construtor = typeof(pxRelatorioItensCanceladosPDV).GetConstructor(new[]
        //        {
        //            typeof(string),

        //        });

        //        return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxRelatorioItensCanceladosPDV>();
        //    }
        //}
        public IList<pxyCreditoDebito> GetNotasCreDeb(DateTime dtInicio, DateTime dtFinal, int idEmpresa)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT nota.Dt,
                                notaItem.Ent_Sai, 
                                notaItem.PIS_CREDEB,
                                notaItem.COFINS_CREDEB,
                                notaItem.IPI_CREDEB,
                                notaItem.ICMS_CREDEB,
                                notaItem.VPIS_Q09,
                                notaItem.VIPI_O14,
                                notaItem.VCOFINS_S11, 
                                notaItem.ValorICMS
                                from Nota nota
                                left join nota.NotaItems notaItem                              
                                where nota.Dt between :dtInicio and :dtFinal and nota.Filial.ID = :idEmpresa
                                and notaItem != null and notaItem.Ent_Sai != 0 order by nota.Dt asc";

                IQuery query = session.CreateQuery(hql)
                    .SetDateTime("dtInicio", dtInicio)
                    .SetDateTime("dtFinal", dtFinal)
                    .SetInt32("idEmpresa", idEmpresa);

                ConstructorInfo construtor = GetContructorInfopxyCreditoDebito();
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyCreditoDebito>();
            }
        }

        public IList<Nota> VerificaSeExisteNFSeComNumeroNoBD(int numRPS)
        {
            using (var session = GetSession())
            {
                string hql = "from Nota n where n.Numero = :numRPS and (ModeloDocto = 2 or ModeloDocto = 3)";

                IQuery query = session.CreateQuery(hql).SetInt32("numRPS", numRPS);

                return query.List<Nota>();
            }
        }

        public Nota GetByPedido(Pedido pedido)
        {
            using (var session = GetSession())
            {
                string sql = "select top 1 ID from Nota n where n.CodigoPedido = :cod order by n.ID desc;";

                int idNota=  session.CreateSQLQuery(sql).SetInt32("cod", pedido.Codigo).UniqueResult<int>();

                string hql = @"from Nota n
                                where n.ID = :ID ";

                return session.CreateQuery(hql).SetInt32("ID", idNota).UniqueResult<Nota>();
            }
        }

        #region Membros de IRepositorioNota

        public Nota GetByNumeroETipoNota(int numero, int tipoNota)
        {
            using (var session = GetSession())
            {
                string hql = @"from Nota n
                               left join fetch n.TipoNota tn
                               where n.Numero = :num AND tn.ModeloDocto = :tipoNota";

                return session.CreateQuery(hql).SetInt32("num", numero).SetInt32("tipoNota", tipoNota).List<Nota>().FirstOrDefault();
            }
        }

        #endregion

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            string hql = hqlGetAll();
            hql += " where n.Dt between :dataInicio and :dataFim ";
            return hql;
        }

        public override IList<Nota> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros)
        {
            try
            {
                IList<Nota> result;
                if (parametros == null)
                {
                    using (var session = GetSession())
                    {
                        string hql = hqlGetAllByPeriod(start, end);
                        result = session.CreateQuery(hql).SetDateTime("dataInicio", start).SetDateTime("dataFim", end).List<Nota>();
                    }
                }
                else
                {
                    var inOut = parametros.ContainsKey("inOut") ? (InOutType)parametros["inOut"] : InOutType.Saída;
                    var produtoServico = parametros.ContainsKey("produtoServico") ? (ProdutoServicoType)parametros["produtoServico"] : ProdutoServicoType.Produtos;
                    var notasCanceladas = parametros.ContainsKey("notasCanceladas") && (bool)parametros["notasCanceladas"];

                    result = GetAllPeriodo(start, end, inOut, produtoServico, notasCanceladas);
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GetByIdOSOrdemServico(int idNota)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT os.ID OSOrdemServico " +
                             " from Nota n " +
                             " JOIN NotaItemServico nis on nis.IDNota = n.ID " +
                             " JOIN OSServicoItem si on si.ID = nis.IDOSServicoItem " +
                             " JOIN OSOrdemServico os on os.ID = si.IDOSOrdemServico " +
                             " WHERE n.ID = :IDNota" +
                             " GROUP BY os.ID ";

                return session.CreateSQLQuery(hql).SetInt32("IDNota", idNota).UniqueResult<int>();
            }
        }

        public int GetByIdOSOrdemServicoProduto(int idNota)
        {
            using (var session = GetSession())
            {
                string hql = "select os.ID OSOrdemServico " +
                             " from Nota n " +
                             " JOIN NotaItem ni on ni.IDNota = n.ID " +
                             " JOIN OSProdutoItem opi on opi.ID = ni.IDOSProdutoItem " +
                             " JOIN OSOrdemServico os on os.ID = opi.IDOSOrdemServico " +
                             " WHERE n.ID = :IDNota" +
                             " GROUP BY os.ID ";

                return session.CreateSQLQuery(hql).SetInt32("IDNota", idNota).UniqueResult<int>();
            }
        }

        public int GetByIdNotaPorIdOSServico(int idOSOrdemServico)
        {
            using (var session = GetSession())
            {
                string hql = "select n.ID " +
                             " FROM Nota n " +
                             " JOIN NotaItemServico nis on nis.IDNota = n.ID " +
                             " JOIN OSServicoItem si on si.ID = nis.IDOSServicoItem " +
                             " JOIN OSOrdemServico os on os.id = si.IDOSOrdemServico " +
                             " WHERE os.ID = :idOSOrdemServico" +
                             " GROUP BY n.ID ";

                return session.CreateSQLQuery(hql).SetInt32("idOSOrdemServico", idOSOrdemServico).UniqueResult<int>();
            }
        }

        public int GetByIdNotaPorIdOSProduto(int idOSOrdemServico)
        {
            using (var session = GetSession())
            {
                string hql = "select n.ID " +
                             " FROM Nota n " +
                             " JOIN NotaItem ni on ni.IDNota = n.ID " +
                             " JOIN OSProdutoItem opi on opi.ID = ni.IDOSProdutoItem " +
                             " JOIN OSOrdemServico os on os.id = opi.IDOSOrdemServico " +
                             " WHERE os.ID = :idOSOrdemServico" +
                             " GROUP BY n.ID ";

                return session.CreateSQLQuery(hql).SetInt32("idOSOrdemServico", idOSOrdemServico).UniqueResult<int>();
            }
        }

        public int GetNumeroPedidoPorCodigoPedido(int codigoPedido)
        {
            using (var session = GetSession())
            {
                string hql = " select p.Numero " +
                             " FROM Pedido p " +
                             " WHERE Codigo = :CodigoPedido ";

                return session.CreateSQLQuery(hql).SetInt32("CodigoPedido", codigoPedido).UniqueResult<int>();
            }
        }

        public int GetNotaPelaChave(string ChaveNota)
        {
            using (var session = GetSession())
            {
                string hql = " SELECT COUNT(*) " +
                             " FROM Nota " +
                             " WHERE ChaveNota = :Chave AND ModeloDocto = 55 AND Ent_Sai = 1";

                return session.CreateSQLQuery(hql).SetString("Chave", ChaveNota).UniqueResult<int>();
            }
        }

        public IList<pxMargemLucroProduto> GetMargemdeLucroProdutos(DateTime dataInicial, DateTime dataFinal, int IDEmpresa, string ativos, string modeloDocumento,
            string idProduto)
        {
            using (var session = GetSession())
            {
                string SQL = $@"select Produto.ID as IDProduto,
                                       Produto.Codigo,
                                	   Produto.Nome,
                                	   Unidade.Sigla as Unidade,
                                
                                	   Round(iif(NotaItem.Total=0,1,NotaItem.Total) / iif(NotaItem.Quantidade=0,1,NotaItem.Quantidade),4) as PrecoVenda,
                                	   sum(Produto.PrecoBase * NotaItem.Quantidade) as UltimoCusto,
                                	   sum(NotaItem.Quantidade) as Quantidade,	   
                                	   sum((Round((iif(NotaItem.Total=0,1,NotaItem.Total) / iif(NotaItem.Quantidade=0,1,NotaItem.Quantidade)),4) - Produto.PrecoBase) * NotaItem.Quantidade) as ValorDeLucro,
                                	   sum(Round((iif(NotaItem.Total=0,1,NotaItem.Total) / iif(NotaItem.Quantidade=0,1,NotaItem.Quantidade)),4) * NotaItem.Quantidade) as Total
                                  from NotaItem
                                    inner join Nota on (NotaItem.IDNota = Nota.ID)
                                    inner join Produto on (NotaItem.IDProduto = Produto.ID)
                                	inner join Unidade on (Produto.IDUnidade = Unidade.ID)
                                where CAST(Nota.Dt AS DATE) between :dataInicio and :dataFim
                                  and Nota.IDFilial = :idFilial
                                  and Nota.Status <> 3
                                  and Nota.Ent_Sai = 2
                                  {ativos}
                                  {modeloDocumento}
                                  {idProduto}
                                  group by Produto.ID, Produto.Codigo, Produto.Nome, Unidade.Sigla, NotaItem.Total, NotaItem.Quantidade";

                if (string.IsNullOrEmpty(modeloDocumento))
                {
                    SQL += $@" UNION
                                select Produto.ID as IDProduto,
                                       Produto.Codigo,
	                                   Produto.Nome,
	                                   Unidade.Sigla as Unidade,

	                                   Round((iif(PedidoItem.Total=0,1,PedidoItem.Total) / iif(PedidoItem.Quantidade=0,1,PedidoItem.Quantidade)),4) as PrecoVenda,
	                                   sum(Produto.PrecoBase * PedidoItem.Quantidade) as UltimoCusto,
	                                   sum(PedidoItem.Quantidade) as Quantidade,	   
	                                   sum((Round((iif(PedidoItem.Total=0,1,PedidoItem.Total) / iif(PedidoItem.Quantidade=0,1,PedidoItem.Quantidade)),4) - Produto.PrecoBase) * PedidoItem.Quantidade) as ValorDeLucro,
	                                   sum(Round((iif(PedidoItem.Total=0,1,PedidoItem.Total) / iif(PedidoItem.Quantidade=0,1,PedidoItem.Quantidade)),4) * PedidoItem.Quantidade) as Total
                                  from PedidoItem
                                    inner join Pedido on (PedidoItem.IDPedido = Pedido.ID)
                                    inner join Produto on (PedidoItem.IDProduto = Produto.ID)
	                                inner join Unidade on (Produto.IDUnidade = Unidade.ID)
                                where CAST(Pedido.Dt AS DATE) between :dataInicio and :dataFim
                                  and Pedido.IDFilial = {IDEmpresa}
                                  and Pedido.Status <> 3
                                  and pedido.Ent_Sai = 2
                                  and PEDIDO.Codigo NOT IN (SELECT NOTA.CodigoPedido FROM NOTA where status = 2)
                                    and PEDIDO.Status =2
                                  {ativos}
                                  {idProduto}
                                  group by Produto.ID, Produto.Codigo, Produto.Nome, Unidade.Sigla, PedidoItem.Total, PedidoItem.Quantidade";
                }

                var sqlExecutar = $@"SELECT IDPRODUTO,
                                            CODIGO,
                                            NOME,
                                            UNIDADE,

                                            AVG(PRECOVENDA) AS PRECOVENDA,
                                            SUM(ULTIMOCUSTO) AS ULTIMOCUSTO,
                                            SUM(QUANTIDADE) AS QUANTIDADE,
                                            SUM(VALORDELUCRO) AS VALORDELUCRO,
                                            SUM(TOTAL) AS TOTAL,
											ROUND(
                                                (( IIF( ((SUM(TOTAL) - SUM(ULTIMOCUSTO))*100) =0, 1, ((SUM(TOTAL) - SUM(ULTIMOCUSTO))*100) )
    
                                                / IIF(SUM(ULTIMOCUSTO)=0,1,SUM(ULTIMOCUSTO))
                                                )),2) as MARGEMLUCRO
                                     FROM ({SQL}) A
                                    GROUP BY IDPRODUTO, CODIGO, NOME, UNIDADE 
                                    ORDER BY NOME";

                IQuery query = session.CreateSQLQuery(sqlExecutar)
                .SetDateTime("dataInicio", dataInicial)
                .SetDateTime("dataFim", dataFinal)
                .SetInt32("idFilial", IDEmpresa);

                ConstructorInfo construtor = typeof(pxMargemLucroProduto).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxMargemLucroProduto>();
            }
        }
    }
}