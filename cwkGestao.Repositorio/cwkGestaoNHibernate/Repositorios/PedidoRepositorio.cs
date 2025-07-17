using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    internal class PedidoRepositorio : RepositorioAbstrato<Pedido>, IRepositorioPedido
    {
        #region primeiros metodos

        protected override string HqlLoadById()
        {
            return "select distinct p from Pedido p " +
                   "left join fetch p.Filial " +
                   "left join fetch p.TipoPedido tp " +
                   "left join fetch p.Pessoa pess" +
                   "left join fetch p.Vendedor " +
                   "left join fetch p.TabelaPreco " +
                   "left join fetch p.Condicao " +
                   "left join fetch p.Items pi " +
                   "left join fetch pi.Produto prod " +
                   "left join fetch prod.Unidade " +
                   "left join fetch prod.TabelaPrecoProdutos " +
                   "left join fetch p.ItemsServicos pis " +
                   "left join fetch pi.Servico serv " +
                   "left join fetch p.Parcelas " +
                   "left join fetch p.PessoaTransportadora " +
                   "left join fetch p.Projeto " +
                   "left join fetch p.CorreiosPrecoPrazo " +
                   "where p.ID = :id";
        }

        public override Pedido LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(Pedido), "p")
                    .SetFetchMode("Filial", FetchMode.Eager)
                    .SetFetchMode("TipoPedido", FetchMode.Eager)
                    .SetFetchMode("Pessoa", FetchMode.Eager)
                    .SetFetchMode("Vendedor", FetchMode.Eager)
                    .SetFetchMode("TabelaPreco", FetchMode.Eager)
                    .SetFetchMode("Condicao", FetchMode.Eager)
                    .SetFetchMode("PessoaTransportadora", FetchMode.Eager)
                    .SetFetchMode("Projeto", FetchMode.Eager)
                    .SetFetchMode("LocalEstoquePedido", FetchMode.Eager)
                    .Add(Restrictions.Eq("ID", id));
                Pedido pedido = criteria.UniqueResult<Pedido>();

                NHibernateUtil.Initialize(pedido.Items);
                NHibernateUtil.Initialize(pedido.ItemsServicos);
                NHibernateUtil.Initialize(pedido.ItemsExpedicao);

                if (pedido.Vendedor != null) NHibernateUtil.Initialize(pedido.Vendedor.PessoaVendedores);
                NHibernateUtil.Initialize(pedido.Parcelas);
                NHibernateUtil.Initialize(pedido.TipoPedido.Impressora);
                NHibernateUtil.Initialize(pedido.Filial.Cidade);
                NHibernateUtil.Initialize(pedido.Filial.Cidade.UF);
                
                foreach(var item in pedido.Items)
                    NHibernateUtil.Initialize(item.Produto.Estoques);

                return pedido;
            }
        }

        public IList<Pedido> LoadAllByID(List<int> ids)
        {
            IList<Pedido> lstPedidos = new List<Pedido>();
            using (var session = GetSession())
            {
                string hql = @" from Pedido p
                                left join fetch p.Filial fil
                                left join fetch p.TipoPedido tp 
                                left join fetch p.Pessoa pess
                                left join fetch p.Vendedor vend
                                left join fetch p.TabelaPreco tab
                                left join fetch p.Condicao cond
                                left join fetch p.PessoaTransportadora transp
                                left join fetch p.Projeto proj
                                left join fetch p.LocalEstoquePedido loc
                                where p.id in (:ids)";

                lstPedidos = session.CreateQuery(hql).SetParameterList("ids", ids).List<Pedido>();

                return lstPedidos;
            }
        }

        public Pedido LoadObjectByIdMercadoLivre(int id)
        {
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " where p.IDPedidoMercadoLivre = " + id;

                var query = session.CreateQuery(hql).UniqueResult<Pedido>();

                return query;
            }
        }

        protected override string hqlGetAll()
        {
            return @"select new Pedido(pedido.ID, pedido.Codigo, pedido.DtDigitacao, pedido.Status, pedido.PessoaNome, pessoa.Fantasia, pessoa.Apelido, filial.Nome, 
                                        pessoa.TipoPessoa, pedido.TotalPedido, vendedor.Nome, tipo.Nome, pedido.Numero, pedido.Dt, pedido.IncData, pedido.IncUsuario, " +
                   "pedido.AltData, pedido.AltUsuario, pedido.PercComissao, pedido.ValorComissao, pedido.UsuarioRevogacao, pedido.UsuarioAprovacao, " +
                   "pedido.dtAprovacao, pedido.dtRevogacao, pedido.primeiraParcela, pedido.UsuarioCancelamento, pedido.DtCancelamento, " +
                   "pedido.MotivoCancelamento, pedido.IDPedidoMercadoLivre, pedido.NumeroPos, pedido.NomeMaquina, pedido.SerialImpressora)" +
                   "from Pedido pedido " +
                   "left join pedido.Vendedor vendedor " +
                   "left join pedido.Pessoa pessoa " +
                   "join pedido.TipoPedido tipo " +
                   "join pedido.Filial filial ";
        }

        public string hqlGetAllPedidos()
        {
            return "select new Pedido(pedido.ID, pedido.Codigo, pedido.DtDigitacao, pedido.Status, pedido.PessoaNome, pessoa.Fantasia, pessoa.Apelido, filial.Nome, pessoa.TipoPessoa, " +
                   "pedido.TotalPedido, vendedor.Nome, tipo.Nome, pedido.Numero, pedido.Dt, pedido.IncData, pedido.IncUsuario, " +
                   "pedido.AltData, pedido.AltUsuario, pedido.PercComissao, pedido.ValorComissao, " +
                   "pedido.UsuarioRevogacao, pedido.UsuarioAprovacao, pedido.dtAprovacao, pedido.dtRevogacao, pedido.primeiraParcela, pedido.UsuarioCancelamento, pedido.DtCancelamento, pedido.MotivoCancelamento, pedido.IDPedidoMercadoLivre, pedido.NumeroPos, pedido.NomeMaquina, pedido.SerialImpressora) " +
                   "from Pedido pedido " +
                   "left join pedido.Vendedor vendedor " +
                   "left join pedido.Pessoa pessoa " +
                   "join pedido.TipoPedido tipo " +
                   "join pedido.Filial filial " +
                   "where tipo.Ent_Sai = :inout " +
                   "and tipo.Tipo in (:tipos)";
        }

        public IList<PedidoItem> GetProdutoVendaPorClienteNoPeriodo(Pessoa pessoa, Produto produto, DateTime dateTime, DateTime dateTime_3, InOutType inOut)
        {
            using (var session = GetSession())
            {
                string hql = " from PedidoItem ni" +
                             " left join fetch ni.Pedido n " +
                             " left join fetch n.TipoPedido t " +
                             " where n.Dt between :dInicio and :dFim " +
                             " and ni.Pedido.Ent_Sai = " + (int)inOut +
                             " and ni.Pedido.TipoPedido.Tipo <> :tipoPedido ";

                if (pessoa != null)
                    hql += " and n.Pessoa.ID = " + pessoa.ID;
                if (produto != null)
                    hql += " and ni.Produto.ID = " + produto.ID;

                var query = session.CreateQuery(hql).SetDateTime("dInicio", dateTime).SetDateTime("dFim", dateTime_3).SetEnum("tipoPedido", TipoPedidoType.Requisição);

                var notaItens = query.List<PedidoItem>();

                return notaItens;
            }
        }

        public IList<int> GetBytipo(TipoPedido tipopedido)
        {
            using (var session = GetSession())
            {
                string sql = " select p.numero from" +
                             " pedido as p" +
                             " inner join tipopedido as t on t.id = p.idtipopedido where p.idtipopedido =" + tipopedido.ID;

                var query = session.CreateSQLQuery(sql);
                var tipopedidos = query.List<int>();
                return tipopedidos;
            }
        }

        public IList<Pedido> GetByCliente(Pessoa cliente, int maxResults)
        {
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " left join fetch p.Items pi " +
                             " left join fetch p.ItemsServicos pis " +
                             " left join fetch p.Vendedor v " +
                             " where p.Pessoa.ID = " + cliente.ID +
                             " order by p.Dt desc";

                var query = session.CreateQuery(hql).SetMaxResults(maxResults);

                var notaItens = query.List<Pedido>();

                return notaItens;
            }
        }

        public IList<Pedido> GetByClienteAberto(Pessoa cliente, int maxResults)
        {
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " left join fetch p.Items pi " +
                             " left join fetch p.ItemsServicos pis " +
                             " left join fetch p.Vendedor v " +
                             " where p.Pessoa.ID = " + cliente.ID +
                             " and p.Status =  " + (int)StatusOrcamento.Aberto + " or p.Status = " + (int)StatusOrcamento.FaturadoParcial +
                             " order by p.Dt desc";

                var query = session.CreateQuery(hql).SetMaxResults(maxResults);

                var notaItens = query.List<Pedido>();

                return notaItens;
            }
        }

        public int CodigoDisponivelPedido()
        {
            return MaxCodigo();
        }

        public int MaxCodigoIntegracao()
        {
            using (var session = GetSession())
                try
                {
                    string hql = "select p from Pedido p where p.ID = (select max(ID) from Pedido)";
                    return Convert.ToInt32(session.CreateQuery(hql).UniqueResult<Pedido>().ID) + 1;
                }
                catch
                {
                    return 1;
                }
        }

        public IList<Pedido> GetByCliente(Pessoa cliente, DateTime inicio, DateTime fim)
        {
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " left join fetch p.Items pi " +
                             " left join fetch p.ItemsServicos pis " +
                             " left join fetch p.Vendedor v " +
                             " where p.Dt between :inicio and :fim " +
                             (cliente == null ? "" : " p.Pessoa.ID = " + cliente.ID) +
                             " order by p.Dt ";

                var query = session.CreateQuery(hql).SetDateTime("inicio", inicio).SetDateTime("fim", fim);

                var pedidoItens = query.List<Pedido>();

                return pedidoItens;
            }
        }

        public IList<Pedido> GetByPCP_Lote(int IDPCP_lote)
        {
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " left join fetch p.Items pi " +
                             " left join fetch p.ItemsServicos pis " +
                             " left join fetch p.Vendedor v " +
                             " where p.PCP_Lote.ID = " + IDPCP_lote;

                var query = session.CreateQuery(hql);
                var pedidoItens = query.List<Pedido>();

                return pedidoItens;
            }
        }

        public IList<Pedido> GetByTipoPedido(Pessoa cliente, Filial filial, TipoPedido tipopedido)
        {
            using (var session = GetSession())
            {
                string hql = " from pedido p" +
                             " left join fetch p.Items pi " +
                             " left join fetch p.ItemsServicos pis " +
                             " where p.Cliente.ID = :idPessoa" +
                             " order by p.Dt";

                var query = session.CreateQuery(hql);
                var pedidos = query.List<Pedido>();

                return pedidos;
            }
        }

        public Pedido GetByCodigo(int p)
        {
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " left join fetch p.Items pi " +
                             " left join fetch p.ItemsServicos pis " +
                             " left join fetch p.Vendedor v " +
                             " where p.Codigo = :codigo ";

                var query = session.CreateQuery(hql).SetInt32("codigo", p);

                var pedido = query.UniqueResult<Pedido>();

                return pedido;
            }
        }

        public Pedido LoadPedidoImpressaoById(int id)
        {
            using (var session = GetSession())
            {
                string query = "select p from Pedido p " +
                               "left join fetch p.Filial f " +
                               "left join fetch f.Cidade " +
                               "left join fetch p.TipoPedido tp " +
                               "left join fetch tp.Impressora " +
                               "left join fetch p.Pessoa pes " +
                               "left join fetch pes.PessoaEmails " +
                               "left join fetch p.Vendedor " +
                               "left join fetch p.TabelaPreco " +
                               "left join fetch p.Condicao " +
                               "left join fetch p.Items pi " +
                               "left join fetch pi.Produto prod " +
                               "left join fetch prod.Unidade " +
                               "left join fetch prod.TabelaPrecoProdutos " +
                               "left join fetch p.ItemsServicos pis " +
                               "left join fetch p.Parcelas parc " +
                               "left join fetch parc.TipoDocumento " +
                               "left join fetch p.PessoaTransportadora " +
                               "left join fetch p.Projeto " +
                               "where p.ID = :id";
                var objeto = session.CreateQuery(query).SetParameter("id", id).List<Pedido>()[0];
                ;
                return objeto;
            }
        }

        public int MaxNumero(Filial filial, TipoPedido tipoPedido)
        {
            using (var session = GetSession())
                try
                {
                    NHibernateUtil.Initialize(tipoPedido);
                    string query = "select max(Numero) from Pedido p " +
                                   "where p.TipoPedido.Tipo = :tipo " +
                                   "and p.Filial.ID = :idFilial ";

                    int maxNumero = session.CreateQuery(query).SetInt32("tipo", (int)tipoPedido.Tipo).SetInt32("idFilial", filial.ID).UniqueResult<int>();

                    return maxNumero + 1;
                }
                catch (Exception a)
                {
                    throw a;
                }
        }

        public int NumeroDisponivelPorComplemento(Filial filial, TipoPedido tipoPedido, string complementoNumero)
        {
            using (var session = GetSession())
            {
                string query = "select max(Numero) from Pedido p " +
                               "where p.TipoPedido.Tipo = :tipo " +
                               "and p.Filial.ID = :idFilial and ";
                if (string.IsNullOrEmpty(complementoNumero))
                    query += " (p.ComplementoNumero = :complemento or p.ComplementoNumero is null) ";
                else
                    query += " (p.ComplementoNumero = :complemento) ";

                return session.CreateQuery(query)
                    .SetInt32("tipo", (int)tipoPedido.Tipo)
                    .SetInt32("idFilial", filial.ID)
                    .SetString("complemento", complementoNumero)
                    .UniqueResult<int>() + 1;
            }
        }

        public bool NumeroComplementoExiste(Pedido objeto)
        {
            using (var session = GetSession())
            {
                string query = "select count(p.ID) from Pedido p " +
                               "where p.Numero = :numero and ";
                if (string.IsNullOrEmpty(objeto.ComplementoNumero))
                    query += " (p.ComplementoNumero = :complemento or p.ComplementoNumero is null) ";
                else
                    query += " (p.ComplementoNumero = :complemento) ";

                long qtd = session.CreateQuery(query)
                    .SetInt32("numero", objeto.Numero)
                    .SetString("complemento", objeto.ComplementoNumero)
                    .UniqueResult<long>();
                return qtd > 0;
            }
        }

        public IList<Pedido> GetAllPedidos(InOutType inOutType, DateTime? Inicio, DateTime? Fim, TipoPedidoType tipoPedido)
        {
            return GetAll(inOutType, Inicio, Fim, new[] { tipoPedido });
        }

        #endregion

        public IList<Pedido> GetAll(InOutType inOutType, DateTime? Inicio, DateTime? Fim, TipoPedidoType[] tipoPedido)
        {
            XElement TagXmlShl = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("UtilizaSHL");
            IList<Pedido> listaPedidos = new List<Pedido>();

            using (var session = GetSession())
            {
                string hql = hqlGetAllPedidos();
                if (Inicio.HasValue && Fim.HasValue)
                {
                    hql += " and pedido.Dt between :inicio and :fim";
                    listaPedidos = session.CreateQuery(hql)
                        .SetEnum("inout", inOutType)
                        .SetParameterList("tipos", tipoPedido)
                        .SetDateTime("inicio", Inicio.Value)
                        .SetDateTime("fim", Fim.Value)
                        .List<Pedido>();
                }
                else
                {
                    listaPedidos = session.CreateQuery(hql).SetEnum("inout", inOutType).SetParameterList("tipos", tipoPedido).List<Pedido>();
                }

                if (Convert.ToInt32(TagXmlShl.Value) == 1)
                    try
                    {
                        foreach (Pedido p in listaPedidos)
                        {
                            string hqlCodsRastreamento = "select rc.Letra + rc.Numero from CorreiosPrecoPrazo cpp " +
                                                         "join cpp.IDRastreamento rc " +
                                                         "join cpp.IDPedido p " +
                                                         "where p.ID = :IdPedido";
                            IList<string> Cods = new List<string>();
                            Cods = session.CreateQuery(hqlCodsRastreamento).SetInt32("IdPedido", p.ID).List<string>();
                            if (Cods.Count > 0)
                            {
                                string CodsRastreamento = "";
                                foreach (string item in Cods) CodsRastreamento = CodsRastreamento + item + "BR, ";
                                if (CodsRastreamento.Length > 0) CodsRastreamento = CodsRastreamento.Substring(0, CodsRastreamento.Length - 2);
                                p.CodigosRastreamento = CodsRastreamento;
                            }
                        }
                    }
                    catch (Exception i)
                    {
                        throw i;
                    }

                return listaPedidos;
            }
        }

        #region Outros métodos

        public bool GetPercFatProdNota(int codigoPedido)
        {
            using (var session = GetSession())
            {
                string sql = @"select PercFatProdNota from Pedido p where p.ID = :id";
                var valor = session.CreateSQLQuery(sql).SetInt32("id", codigoPedido).UniqueResult<decimal>();

                return valor > 0;
            }
        }

        public decimal[] getTotalPorID(int id)
        {
            using (var session = GetSession())
            {
                string sql = @"select p.TotalPedido, p.TotalProduto from Pedido p where p.ID = :id";
                var valores = session.CreateSQLQuery(sql).SetInt32("id", id).List<object[]>();

                string totalPedidos = valores[0][0].ToString().Replace(".", ",");
                string totalProdutos = valores[0][1].ToString().Replace(".", ",");

                decimal[] totais = new decimal[2];

                totais[0] = Convert.ToDecimal(totalPedidos);
                totais[1] = Convert.ToDecimal(totalProdutos);

                return totais;
            }
        }

        public IList<Pedido> GetPedidosAbertos(Pessoa pessoa)
        {
            using (var session = GetSession())
            {
                string hql = "select pedido " +
                             "from Pedido pedido " +
                             "left join fetch pedido.Items " +
                             "left join fetch p.ItemsServicos pis " +
                             "where (pedido.Status = :status or pedido.Status = :status2 or pedido.Status = :status3)" +
                             "and pedido.Pessoa.ID = :idPessoa order by pedido.Dt";

                return session.CreateQuery(hql).SetEnum("status", StatusOrcamento.Aberto)
                    .SetEnum("status2", StatusOrcamento.FaturadoParcial)
                    .SetEnum("status3", StatusOrcamento.AguardandoFaturamento)
                    .SetInt32("idPessoa", pessoa.ID).List<Pedido>();
            }
        }

        public IList<Pedido> GetPedidoPorNumero(int Numero)
        {
            using (var session = GetSession())
            {
                string hql = "select pedido " +
                             "from Pedido pedido " +
                             "left join pedido.Items " +
                             "where pedido.Numero = :numero";
                return session.CreateQuery(hql).SetInt32("numero", Numero).List<Pedido>();
            }
        }

        public IList<Pedido> GetPedidosAbertosLimiteCredito(Pessoa pessoa)
        {
            using (var session = GetSession())
            {
                string hql = "select pedido " +
                             "from Pedido pedido " +
                             "where (pedido.Status = :status or pedido.Status = :status2 or pedido.Status = :status3) " +
                             "and pedido.Pessoa.ID = :idPessoa order by pedido.Dt";

                return session.CreateQuery(hql).SetEnum("status", StatusOrcamento.Aberto)
                    .SetEnum("status2", StatusOrcamento.FaturadoParcial)
                    .SetEnum("status3", StatusOrcamento.AguardandoFaturamento)
                    .SetInt32("idPessoa", pessoa.ID).List<Pedido>();
            }
        }

        public decimal GetValorTotalPedidosAbertosLimiteCredito(Pessoa pessoa)
        {
            using (var session = GetSession())
            {
                string hql = "select coalesce(sum(pedido.TotalPedido),0) " +
                             "from Pedido pedido " +
                             "where (pedido.Status = :status or pedido.Status = :status2 or pedido.Status = :status3) " +
                             "and pedido.Pessoa.ID = :idPessoa";

                decimal retorno = session.CreateQuery(hql).SetEnum("status", StatusOrcamento.Aberto)
                    .SetEnum("status2", StatusOrcamento.FaturadoParcial)
                    .SetEnum("status3", StatusOrcamento.AguardandoFaturamento)
                    .SetInt32("idPessoa", pessoa.ID).List<decimal>().FirstOrDefault();

                return retorno;
            }
        }

        public IList<Pedido> GetPedidosAbertos(Pessoa pessoa, Filial filial, TipoPedido tipoPedido, DateTime inicio, DateTime fim)
        {
            using (var session = GetSession())
            {
                inicio = inicio.Date;
                fim = fim.Date.AddHours(23).AddMinutes(59);
                string hql =
                    "select new Pedido(pedido.ID, pedido.Codigo, pedido.Numero, pedido.DtDigitacao," +
                    " pedido.TotalProduto, (select count(it) from Pedido ped left join ped.Items it where ped.ID = pedido.ID), " +
                    " pedido.TotalServico, (select count(it) from Pedido ped left join ped.ItemsServicos it where ped.ID = pedido.ID), " +
                    " pedido.RespRetencao, pedido.ValorFrete, pedido.ValorAcrescimo, pedido.TotalPedido, pedido.Dt,  pedido.TotalST, pedido.TotalIPI, pedido.PercComissao, " +
                    " pedido.ValorComissao) " +
                    "from Pedido pedido " +
                    "left join pedido.Pessoa pessoa " +
                    "join pedido.TipoPedido tipo " +
                    "join pedido.Filial filial " +
                    "where (pedido.Status = :status or pedido.Status = :status2 or pedido.Status = :status3) " +
                    "and pedido.Pessoa.ID = :idPessoa " +
                    "and pedido.Filial.ID = :idFilial " +
                    "and pedido.TipoPedido.ID = :idTipoPedido " +
                    "and pedido.Dt between :inicio and :fim order by pedido.Dt ";

                try
                {
                    return session.CreateQuery(hql).SetEnum("status", StatusOrcamento.Aberto)
                        .SetEnum("status2", StatusOrcamento.FaturadoParcial)
                        .SetEnum("status3", StatusOrcamento.AguardandoFaturamento)
                        .SetInt32("idPessoa", pessoa.ID)
                        .SetInt32("idFilial", filial.ID)
                        .SetInt32("idTipoPedido", tipoPedido.ID)
                        .SetDateTime("inicio", inicio)
                        .SetDateTime("fim", fim)
                        .List<Pedido>();
                }
                catch (Exception ex)
                {
                    string msg = "Erro ao buscar pedidos" + ex;

                    throw;
                }

                /*     try
                     {
                         return session.CreateQuery(hql).SetEnum("status", StatusOrcamento.Aberto)
                             .SetInt32("idPessoa", pessoa.ID)
                             .SetInt32("idFilial", filial.ID)
                             .SetInt32("idTipoPedido", tipoPedido.ID)
                             .SetDateTime("inicio", inicio)
                             .SetDateTime("fim", fim)
                             .List<Pedido>();
                     }
                     catch
                     {
                         throw new Exception();
                         return new List<Pedido>();
                     }*/
            }
        }

        public HashSet<int> GetHashIdsIntegracao()
        {
            using (var session = GetSession())
            {
                var result = session.CreateQuery("select p.IDIntegracao from Pedido p where p.IDIntegracao is not null and p.IDIntegracao > 0").List<int>();
                return new HashSet<int>(result);
            }
        }

        public HashSet<int> GetHashIdsIntegracaoPendente()
        {
            using (var session = GetSession())
            {
                var result = session.CreateQuery("select p.IDIntegracao from SincronismoPendente p where p.IDIntegracao is not null and p.IDIntegracao > 0").List<int>();
                return new HashSet<int>(result);
            }
        }

        public IList<object> getListaPedidos(List<int> Empresas, string pStatus, List<int> Vendedores, List<int> TipoOrcamentos, DateTime DataInicial, DateTime DataFinal)
        {
            using (var session = GetSession())
            {
                IList<object> retorno = new List<object>();
                string listaEmpresa = "(";
                foreach (var emp in Empresas) listaEmpresa = listaEmpresa.TrimEnd() + emp + ",";
                listaEmpresa = listaEmpresa.TrimEnd() + "0)";

                string listaVendedores = "(";
                foreach (var vend in Vendedores) listaVendedores = listaVendedores.TrimEnd() + vend + ",";
                listaVendedores = listaVendedores.TrimEnd() + "0)";

                string listaTipoOrcamento = "(";
                foreach (var tipo in TipoOrcamentos) listaTipoOrcamento = listaTipoOrcamento.TrimEnd() + tipo + ",";
                listaTipoOrcamento = listaTipoOrcamento.TrimEnd() + "0)";

                string hql = @"SELECT	 Ped.Dt
                                , Ped.Codigo
                                , Ped.Numero                                
                                , Ped.PessoaNome+' '+CAST(Pes.Codigo AS VARCHAR) as Cliente

								,CASE WHEN (SELECT (SUM(Pedi.ValorCalculado * Pedi.Quantidade))
												FROM PedidoItem Pedi WHERE Pedi.IDPedido = Ped.ID) > 0 then
										(SELECT (100 - ((Ped.TotalPedido / SUM(Pedi.ValorCalculado * Pedi.Quantidade)) * 100))
												FROM PedidoItem Pedi WHERE Pedi.IDPedido = Ped.ID)
									  else 0 end    		
												 AS Desconto
                                , Ped.TotalPedido
                                , TPed.Nome as TipoPedido
                                , Ven.Nome + ' - ' + CAST(Ven.ID AS VARCHAR) as Vendedor
                                , Ped.Status,
           
                                              
                               CAST((select max(n.numero) from Nota n 
                                INNER JOIN PedidoItem Pedi ON Pedi.IDPedido = Ped.ID
                                LEFT JOIN NotaItemPedidoItem NIPI ON NIPI.IDPedidoItem = Pedi.ID 
                                LEFT JOIN NotaItem NI ON NI.ID= NIPI.IDNotaItem
                                where n.ID = ni.IDNota )AS VARCHAR) as NotaFiscal
                                                            
                                
                                FROM Pedido Ped
                                INNER JOIN TipoPedido TPed ON TPed.ID = Ped.IDTipoPedido
                                INNER JOIN Pessoa Pes ON Pes.ID = Ped.IDPessoa
                                INNER JOIN Filial Fil ON Fil.ID = Ped.IDFilial
                                INNER JOIN Pessoa Ven ON Ven.ID = Ped.IDVendedor2
                                
                                WHERE TPed.Tipo = 2
                                AND Ped.IDFilial IN" + listaEmpresa;

                hql = hql + " AND Ped.IDVendedor IN " + listaVendedores;
                hql = hql + " AND Ped.IDTipoPedido IN " + listaTipoOrcamento;
                switch (pStatus)
                {
                    case "Aberto":
                        hql = hql.TrimEnd() + " AND (Ped.Status = 1)";
                        break;
                    case "Faturado":
                        hql = hql.TrimEnd() + " AND (Ped.Status = 2)";
                        break;
                    default:
                        hql = hql.TrimEnd() + " AND (Ped.Status = 1 OR Ped.Status = 2)";
                        break;
                }

                hql = hql.TrimEnd() + " AND CAST(Ped.Dt AS DATE) >= :DataInicial";
                hql = hql.TrimEnd() + " AND CAST(Ped.Dt AS DATE) <= :DataFinal";
                //hql = hql.TrimEnd() + " ORDER BY Ped.PessoaNome, Pes.Codigo, TPed.Nome, Ped.Dt";

                IQuery query = session.CreateSQLQuery(hql).SetDateTime("DataInicial", DataInicial)
                    .SetDateTime("DataFinal", DataFinal);

                ConstructorInfo construtor = typeof(pxyOrcamento).GetConstructor(new[]
                {
                    typeof(DateTime),
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(string),
                    typeof(string),
                    typeof(int),
                    typeof(string)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        private string GetSqlListaCodRastreamento(int pedido)
        {
            string hql = @"SELECT	DISTINCT Ped.ID
		                            , CAST(Ped.Codigo AS INT) AS CodigoPedido
		                            , Ped.Dt AS DataPedido
		                            , R.Letra + CAST(R.Numero AS varchar) + 'BR' AS CodigoRastreamento
		                            , CASE WHEN Ent.CEP IS NULL THEN CAST(Ped.PessoaCEP AS varchar) ELSE CAST(Ent.CEP AS varchar) END AS Cep
		                            , CASE WHEN N.ID IS NULL THEN 'NF: 0' ELSE 'NF: ' + CAST(N.Numero AS varchar) END AS NotaFiscal
		                            , '' AS Volume
		                            , Ped.PessoaNome AS Destinatario
		                            , CASE WHEN Ent.Endereco IS NULL THEN Ped.PessoaEndereco ELSE Ent.Endereco + ', ' + CAST(ENT.Numero AS VARCHAR) END AS Endereco
		                            , CASE WHEN Ent.Bairro IS NULL THEN RTRIM(ISNULL(Ped.Complemento,'')) + ' ' + RTRIM(ISNULL(Ped.PessoaBairro,'')) ELSE RTRIM(ISNULL(Ent.Complemento,'')) + ' ' + RTRIM(ISNULL(Ent.Bairro,'')) END AS ComplementoBairro
		                            , CASE WHEN EntCid.Nome IS NULL THEN RTRIM(ISNULL(Ped.PessoaCidade,'')) + ' - ' + RTRIM(ISNULL(Ped.PessoaUF,'')) ELSE RTRIM(ISNULL(EntCid.Nome,'')) + ' - ' + RTRIM(ISNULL(EntUF.Sigla,'')) END AS CidadeEstado
		                            , 'Peso(g): '+ CAST(E.PesoLiquido AS VARCHAR) AS Peso
		                            , Fil.Nome as Remetente
		                            , RTRIM(ISNULL(Fil.Endereco,'')) + ', ' + CAST(Fil.Numero AS VARCHAR) AS EndRemetente
		                            , ISNULL(Fil.Bairro,'') AS CompelementoBairroRemetente
		                            , CAST(Fil.CEP AS VARCHAR) AS CepRemetente
		                            , RTRIM(ISNULL(CidFil.Nome,'')) + ' - ' + RTRIM(ISNULL(UfFil.Sigla,'')) AS CidadeEstadoRemetente
		                            , Ped.Observacao1 AS Observacao
                                    , tips.CaminhoImagem AS CaminhoImagem
                            FROM ExpedicaoRastreamento E
                            INNER JOIN Pedido Ped ON Ped.ID = E.IDPedido
                            INNER JOIN RastreamentoCorreio R ON R.ID = E.IDRastreamento
                            INNER JOIN Pessoa Pes ON Pes.ID = Ped.IDPessoa
                            INNER JOIN Filial Fil ON Fil.ID = Ped.IDFilial
                            INNER JOIN Cidade CidFil ON CidFil.ID = Fil.IDCidade
                            INNER JOIN UF UfFil ON UfFil.ID = CidFil.IDUF
                            INNER JOIN TipoServicoTransporte tips on tips.ID = R.IDTipoServico
                            LEFT JOIN PessoaEndereco Ent ON Ent.ID = Ped.IDEnderecoEntrega
                            LEFT JOIN PedidoItem PedI ON PedI.IDPedido = Ped.ID
                            LEFT JOIN NotaItemPedidoItem NIPI ON NIPI.IDPedidoItem = PedI.ID
                            LEFT JOIN NotaItem NI ON NI.ID = NIPI.IDNotaItem
                            LEFT JOIN Cidade EntCid ON EntCid.ID = Ent.IDCidade
                            LEFT JOIN UF EntUF ON EntUF.ID = EntCid.IDUF
                            LEFT JOIN Nota N ON N.ID = NI.IDNota";

            if (pedido > 0)
                hql = hql.TrimEnd() + " where Ped.ID = " + pedido;

            return hql;
        }

        public IList<pxyImpressaoEtiquetaExpedicao> GetListaCodRastreamento()
        {
            return GetListaCodRastreamento(0);
        }

        public IList<pxyImpressaoEtiquetaExpedicao> GetListaCodRastreamento(int pedido)
        {
            string hql = GetSqlListaCodRastreamento(pedido);

            using (ISession sessao = GetSession())
            {
                IQuery query = sessao.CreateSQLQuery(hql);

                ConstructorInfo construtor = typeof(pxyImpressaoEtiquetaExpedicao).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(int),
                    typeof(DateTime),
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
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyImpressaoEtiquetaExpedicao>();
            }
        }

        public IList<Pedido> GetPedidosFaturados()
        {
            using (var session = GetSession())
            {
                string hql = "from Pedido p where p.Status in (2,4)";

                return session.CreateQuery(hql).List<Pedido>();
            }
        }

        public IList<Pedido> GetByAllParameters(Pessoa cliente, int maxResults, DateTime inicio, DateTime fim, TipoPedido tipoPedido, Filial filial)
        {
            using (var session = GetSession())
            {
                string hql = " FROM Pedido p" +
                             " LEFT JOIN FETCH p.Pessoa pessoa " +
                             " LEFT JOIN FETCH p.Filial filial " +
                             " LEFT JOIN FETCH p.TipoPedido pedido " +
                             " WHERE p.Dt between :inicio and :fim " +
                             " AND filial.ID = :idFilial ";

                if (cliente != null) hql += " AND pessoa.ID = :idPessoa ";
                if (tipoPedido != null) hql += " AND pedido.ID = :idPedido ";

                var query = session.CreateQuery(hql).SetMaxResults(maxResults);
                query.SetDateTime("inicio", inicio);
                query.SetDateTime("fim", fim);
                query.SetInt32("idFilial", filial.ID);
                if (cliente != null) query.SetInt32("idPessoa", cliente.ID);

                if (tipoPedido != null) query.SetInt32("idPedido", tipoPedido.ID);
                var notaItens = query.List<Pedido>();
                return notaItens;
            }
        }

        public IList<Pedido> GetPedidosVendedor(int idVendedor)
        {
            using (var session = GetSession())
            {
                string hql = "from Pedido p where IDVendedor = :idVendedor";
                var query = session.CreateQuery(hql).SetInt32("idVendedor", idVendedor).List<Pedido>();

                return query;
            }
        }

        public IList<Pedido> GetPedidosParaAprovar()
        {
            IList<Pedido> pedidos = new List<Pedido>();
            Pedido pedido = new Pedido();
            using (var session = GetSession())
            {
                string hql = " from Pedido p" +
                             " left join fetch p.Vendedor " +
                             " left join fetch p.Pessoa " +
                             " join fetch p.TipoPedido tipo " +
                             " join fetch p.Filial " +
                             " where p.Status = 10 " +
                             " and tipo.Ent_Sai = 2 " +
                             " or (p.dtAprovacao != null " +
                             " and tipo.StatusPosAprovacao = p.Status)";
                var listaPedidos = session.CreateQuery(hql).List<Pedido>();

                return listaPedidos;
            }
        }

        public IList<PedidoItem> GetItensDoPedido(int IDPedido)
        {
            using (var session = GetSession())
            {
                string hql = "from PedidoItem pi where IDPedido = :IDPedido";
                var query = session.CreateQuery(hql).SetInt32("IDPedido", IDPedido).List<PedidoItem>();

                return query;
            }
        }

        public IList<Pedido> GetAllPorIdSISeCommerce(int IdSis)
        {
            IList<Pedido> result = new List<Pedido>();
            using (var session = GetSession())
            {
                string hql = "from Pedido p where p.IDPedidoSISeCommerce = :IdSis";
                try
                {
                    result = session.CreateQuery(hql).SetInt32("IdSis", IdSis).List<Pedido>();
                }
                catch (Exception z)
                {
                    throw z;
                }
            }

            return result;
        }

        public IList<Pedido> GetAllEnviadosSHL()
        {
            IList<Pedido> result = new List<Pedido>();
            using (var session = GetSession())
            {
                string hql = "from Pedido p where p.CodShl > 0";
                try
                {
                    result = session.CreateQuery(hql).List<Pedido>();
                }
                catch (Exception z)
                {
                    throw z;
                }
            }

            return result;
        }

        public NotaItemPedidoItem LoadNotaItemPedidoItemByIdNota(int id)
        {
            using (var session = GetSession())
            {
                string hql = " from NotaItemPedidoItem p" +
                             " where p.NotaItem.Nota.ID = " + id;

                var query = session.CreateQuery(hql).SetMaxResults(1).UniqueResult<NotaItemPedidoItem>();

                return query;
            }
        }

        public IList<NotaItemPedidoItem> getListaNotaItemPedidoItem(int idnota)
        {
            using (var session = GetSession())
            {
                string hql = " from NotaItemPedidoItem p" +
                             " where p.NotaItem.Nota.ID = " + idnota;

                var query = session.CreateQuery(hql).List<NotaItemPedidoItem>();

                return query;
            }
        }

        public IList<Pedido> GetAllEnviadosSHLNaoFinalizados()
        {
            IList<Pedido> result = new List<Pedido>();
            using (var session = GetSession())
            {
                string hql = "from Pedido p where p.CodShl > 0 and p.StatusSHL not in(6,7,9,12,19,20)";
                try
                {
                    result = session.CreateQuery(hql).List<Pedido>();
                }
                catch (Exception z)
                {
                    throw z;
                }
            }

            return result;
        }

        public IList<pxyEntregaPedido> GetAllParaEntregas(int idpessoa, int Entrega, int StatusEntrega, DateTime DataInicial, DateTime DataFinal)
        {
            //IList<Pedido> result = new List<Pedido>();
            using (var session = GetSession())
            {
                string sql = @"

                    select d.* from(

                        select t.ID, t.Codigo, t.Numero, t.PessoaNome, t.Status, t.StatusEntrega, t.TotalPedido, 
	                        sum(t.QtdItens) qtditens, isnull(sum(t.Saldo),sum(t.QtdItens)) saldo from(

                            select p.ID,
	                            p.Codigo,
	                            p.Numero,
	                            p.PessoaNome,
	                            p.Status,
	                            p.StatusEntrega,
	                            p.TotalPedido,
	                            sum(pedi.quantidade) as QtdItens,
	                            sum(pedi.quantidade) - (select sum(ei.QuantidadeEntrega) from EntregaItem ei where ei.IDPedidoItem = pedi.ID) as Saldo
	                            from Pedido p
	                            join PedidoItem pedi on p.id = pedi.IDPedido

	                            where p.Dt between :DataInicial and :DataFinal
                                and p.idpessoa = :idpessoa
	                            group by
	                            p.ID,
	                            p.Codigo,
	                            p.Numero,
	                            p.PessoaNome,
	                            p.Status,
	                            p.StatusEntrega,
	                            p.TotalPedido,
	                            pedi.ID 
	                        ) t
	                        group by t.ID, t.Codigo, t.Numero, t.PessoaNome, t.Status, t.StatusEntrega, t.TotalPedido) 
                    d
                    where ((d.StatusEntrega = :StatusEntrega or :StatusEntrega = 3)) and ((d.qtditens - d.saldo = 0 and :ent = 1) or (d.qtditens - d.saldo > 0 and :ent = 0) or (:ent = 2))

                             ";

                var query = session.CreateSQLQuery(sql).SetDateTime("DataInicial", DataInicial).SetDateTime("DataFinal", DataFinal).SetInt32("StatusEntrega", StatusEntrega).SetInt32("ent", Entrega).SetInt32("idpessoa", idpessoa);

                ConstructorInfo construtor = typeof(pxyEntregaPedido).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(StatusOrcamento),
                    typeof(StatusEntregaType),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal?)
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyEntregaPedido>();

                return retorno;
            }
        }

        #endregion

        public override IList<Pedido> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros)
        {
            InOutType tipo = InOutType.Saída;
            List<TipoPedidoType> tiposPedidos = new List<TipoPedidoType>();
            if (parametros.ContainsKey("inOutType")) tipo = (InOutType)parametros["inOutType"];
            if (parametros.ContainsKey("tipoPedido"))
            {
                var tipos = (TipoPedidoType[])parametros["tipoPedido"];
                foreach (var item in tipos) tiposPedidos.Add(item);
            }

            return GetAll(tipo, start, end, tiposPedidos.ToArray());
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Pedido> GetPedidosDelivery()
        {
            IList<Pedido> result = new List<Pedido>();
            using (var session = GetSession())
            {
                string hql = "from Pedido p where p.BDelivery = 1";
                try
                {
                    result = session.CreateQuery(hql).List<Pedido>();
                }
                catch (Exception z)
                {
                    throw z;
                }
            }

            return result;
        }

        public FrenteCaixaNota GetFrenteCaixaNotaPorPedido(int IDPedido)
        {
            using (var session = GetSession())
            {
                string hql = "from FrenteCaixaNota p where p.IDPedido = :idPedido";
                try { return session.CreateQuery(hql).SetInt32("idPedido", IDPedido).List<FrenteCaixaNota>().FirstOrDefault(); }
                catch (Exception z) { throw z; }
            }
        }

        public IList<pxPedidosDashBoard> GetPedidosParaDashBoard()
        {
            using (var sessao = GetSession())
            {
                IQuery query = sessao.CreateSQLQuery(@"SELECT PEDIDO.ID,
                                                              PEDIDO.STATUS,
                                                              TIPOPEDIDO.TIPO
                                                       FROM PEDIDO
                                                         LEFT JOIN TIPOPEDIDO ON (PEDIDO.IDTIPOPEDIDO = TIPOPEDIDO.ID)");
                var construtor = typeof(pxPedidosDashBoard).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(int),
                    typeof(int),
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxPedidosDashBoard>();
            }
        }

        public IList<pxyPedidoFrenteCaixaCarregar> GetListaPedidosFrenteCaixaParaCarregar(bool Reservas = false, int? IDFluxoCaixa = null)
        {
            using (var sessao = GetSession())
            {
                IQuery query = sessao.CreateSQLQuery(IDFluxoCaixa.HasValue ? $@"SELECT DISTINCT PEDIDO.ID,
                                                                                       FRENTECAIXANOTA.ID AS IDFRENTECAIXANOTA,
                                                                                       PEDIDO.CODIGO,
                                                                                	   PEDIDO.DT,
                                                                                	   PEDIDO.PESSOANOME,
                                                                                       PEDIDO.STATUS,
                                                                                       CASE 
                                                                                         WHEN NOTA.STATUS = '2' THEN 'Autorizada'
                                                                                         WHEN COALESCE(NOTA.STATUS, '') = '' THEN ''
                                                                                       END AS STATUSNFCE,
                                                                                       PEDIDO.TOTALPEDIDO,
                                                                                       TIPOPEDIDO.NOME AS TIPOPEDIDO                                                                                       
                                                                                  FROM PEDIDO
                                                                                    LEFT JOIN FRENTECAIXANOTA ON PEDIDO.ID = FRENTECAIXANOTA.IDPEDIDO
                                                                                    LEFT JOIN TIPOPEDIDO ON FRENTECAIXANOTA.IDTIPOPEDIDO = TIPOPEDIDO.ID
                                                                                    LEFT JOIN NOTA ON PEDIDO.CODIGO = NOTA.CODIGOPEDIDO
                                                                                                  AND NOTA.STATUS = '2'
                                                                                WHERE FRENTECAIXANOTA.IDFLUXOCAIXA = {IDFluxoCaixa}
                                                                                    {(Reservas ? "AND PEDIDO.BRESERVA = 1" : "")}
                                                                                  ORDER BY PEDIDO.DT DESC, PEDIDO.PESSOANOME ASC" :
                                                                              $@"SELECT DISTINCT PEDIDO.ID,
                                                                                       FRENTECAIXANOTA.ID AS IDFRENTECAIXANOTA,
                                                                                	   PEDIDO.CODIGO,
                                                                                	   PEDIDO.DT,
                                                                                	   PEDIDO.PESSOANOME,
                                                                                       PEDIDO.STATUS,
                                                                                       CASE 
                                                                                          WHEN NOTA.STATUS = '2' THEN 'Autorizada'
                                                                                          WHEN COALESCE(NOTA.STATUS, '') = '' THEN ''
                                                                                        END AS STATUSNFCE,                                                                                       
                                                                                	   PEDIDO.TOTALPEDIDO,
                                                                                       TIPOPEDIDO.NOME AS TIPOPEDIDO
                                                                                  FROM PEDIDO
                                                                                    LEFT JOIN FRENTECAIXANOTA ON PEDIDO.ID = FRENTECAIXANOTA.IDPEDIDO
                                                                                    LEFT JOIN TIPOPEDIDO ON FRENTECAIXANOTA.IDTIPOPEDIDO = TIPOPEDIDO.ID
                                                                                    LEFT JOIN NOTA ON PEDIDO.CODIGO = NOTA.CODIGOPEDIDO
                                                                                                  AND NOTA.STATUS = '2'
                                                                                WHERE PEDIDO.STATUS in (1, 2)
                                                                                    {(Reservas ? "AND PEDIDO.BRESERVA = 1" : "")}
                                                                                  ORDER BY PEDIDO.DT DESC, PEDIDO.PESSOANOME ASC");

                var construtor = typeof(pxyPedidoFrenteCaixaCarregar).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(int?),
                    typeof(int),
                    typeof(DateTime),
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(decimal),
                    typeof(string)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyPedidoFrenteCaixaCarregar>();
            }
        }
    }
}