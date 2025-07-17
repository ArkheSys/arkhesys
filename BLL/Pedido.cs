using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Xml;
using cwkGestao.Repositorio.cwkGestaoFileSystem;
using Modelo;
using Modelo.ModeloProxy;

namespace BLL
{
    public class Pedido : IDisposable
    {
        private Modelo.DataClassesDataContext db;

        //public Pedido()
        //{
        //    db = new Modelo.DataClassesDataContext();
        //    //db.ObjectTrackingEnabled = false;
        //    //db.DeferredLoadingEnabled = false;
        //}

        public Pedido(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public List<string> IncluirProduto(Modelo.DataClassesDataContext pdb, List<int> pLista, Modelo.Pedido objPedido, List<Modelo.ItensOrcamento> pListaOrcamentoItem, bool pUtilizaFichaTecnica)
        {

            List<string> naoIncluidos = new List<string>();
            Modelo.Produto objProduto = new Modelo.Produto();
            Modelo.Unidade objUnidade = new Modelo.Unidade();

            BLL.Produto bllProduto = new BLL.Produto(db);
            BLL.Unidade bllUnidade = new BLL.Unidade(db);

            for (int y = 0; y < pLista.Count; y++)
            {
                //Modelo.PedidoItem objPedidoItem = new Modelo.PedidoItem();
                Modelo.ItensOrcamento pedidoitem = new Modelo.ItensOrcamento();
                objProduto = (Modelo.Produto)bllProduto.getObjeto(pLista[y]);
                try
                {
                    if (pListaOrcamentoItem.Count == 0)
                        pedidoitem.Sequencia = 1;
                    else
                        pedidoitem.Sequencia = pListaOrcamentoItem.Max(m => m.Sequencia) + 1;

                    objUnidade = (Modelo.Unidade)bllUnidade.getObjeto((int)objProduto.IDUnidade);

                    pedidoitem.Produto = objProduto.Codigo;
                    pedidoitem.Descricao = objProduto.Nome;
                    pedidoitem.Quantidade = 1;
                    if (objPedido.TabelaPreco != null)
                    {
                        pedidoitem.ValorMin = bllProduto.getPreco(objProduto, objPedido.TabelaPreco.Codigo, Modelo.TipoPrecoType.Mínimo);
                        pedidoitem.Valor = bllProduto.getPreco(objProduto, objPedido.TabelaPreco.Codigo, Modelo.TipoPrecoType.Normal);
                    }
                    else
                    {
                        if (objPedido.TipoPedido != null && objPedido.TipoPedido.Tipo == TipoPedidoType.Requisição)
                        {
                            pedidoitem.ValorMin = GetPrecoRequisicao(pdb, objPedido.TipoPedido, objProduto, bllProduto);
                            pedidoitem.Valor = pedidoitem.ValorMin;
                        }
                        else
                        {
                            pedidoitem.ValorMin = objProduto.PrecoBase.HasValue ? objProduto.PrecoBase.Value : 0;
                            pedidoitem.Valor = pedidoitem.ValorMin;
                        }
                    }
                    pedidoitem.ValorSemDesc = pedidoitem.Valor;
                    pedidoitem.ValorCalc = pedidoitem.Valor;
                    pedidoitem.Desc = 0;
                    pedidoitem.Total = pedidoitem.Quantidade * pedidoitem.Valor;
                    pedidoitem.Unidade = bllUnidade.getSigla((int)objProduto.IDUnidade);
                    pedidoitem.qtdFracionada = objUnidade.bQtdFracionada == Modelo.TituloType.Sim ? true : false;
                    pedidoitem.Acao = 1;
                    pedidoitem.ID = 0;
                    pedidoitem.ValorRetidoICMS = 0;
                    if (pUtilizaFichaTecnica)
                    {
                        IncluirSubItem(objPedido, objProduto, bllProduto, bllUnidade, pedidoitem);
                    }

                    pListaOrcamentoItem.Add(pedidoitem);
                }
                catch (Exception)
                {
                    naoIncluidos.Add("Produto: " + objProduto.Codigo.ToString() + " - " + objProduto.Nome);
                }
            }

            bllProduto.Dispose();
            bllUnidade.Dispose();

            return naoIncluidos;
        }

        public decimal GetPrecoRequisicao(Modelo.DataClassesDataContext pdb, Modelo.TipoPedido objTipoPedido, Modelo.Produto objProd, BLL.Produto bllProduto)
        {
            switch (objTipoPedido.TipoPreco)
            {
                case 0: //Venda
                    var tabPreco = db.TabelaPrecos.OrderBy(t => t.ID);
                    if (tabPreco.Count() > 0)
                        return bllProduto.getPreco(objProd, tabPreco.First().ID, Modelo.TipoPrecoType.Normal);
                    break;
                case 1: //Último Custo
                    return objProd.UltimoCusto.HasValue ? objProd.UltimoCusto.Value : 0.0m;
                case 2: //Custo Médio
                    var ultimaNotaItem = db.NotaItems.Where(n => n.CalcCustoMedio == true && objProd.ID == n.IDProduto)
                                                     .OrderByDescending(a => a.ID).Take(1);
                    if (ultimaNotaItem.Count() == 1)
                        return ultimaNotaItem.Single().ValorCustoMedio;
                    break;
            }
            return 0.0m;
        }

        public void IncluirSubItem(Modelo.Pedido objPedido, Modelo.Produto objProduto, BLL.Produto bllProduto, BLL.Unidade bllUnidade, Modelo.ItensOrcamento pedidoitem)
        {
            if (objProduto.FichaTecnicas.Count > 0)
            {
                Modelo.FichaTecnica objFicha = objProduto.FichaTecnicas.OrderByDescending(f => f.Versao).First();
                foreach (Modelo.ItemFichaTecnica itemFicha in objFicha.ItemFichaTecnicas)
                {
                    Modelo.ItensOrcamento subItem = new Modelo.ItensOrcamento();
                    objProduto = (Modelo.Produto)bllProduto.getObjeto(itemFicha.IDProduto);
                    if (pedidoitem.ItensProduto.Count == 0)
                        subItem.Sequencia = 1;
                    else
                        subItem.Sequencia = pedidoitem.ItensProduto.Max(m => m.Sequencia) + 1;

                    Modelo.Unidade objUnidade = (Modelo.Unidade)bllUnidade.getObjeto((int)objProduto.IDUnidade);

                    subItem.Produto = objProduto.Codigo;
                    subItem.Descricao = objProduto.Nome;
                    subItem.Quantidade = itemFicha.Quantidade;
                    if (objPedido.TabelaPreco != null)
                    {
                        subItem.ValorMin = bllProduto.getPreco(objProduto, objPedido.TabelaPreco.Codigo, Modelo.TipoPrecoType.Mínimo);
                        subItem.Valor = bllProduto.getPreco(objProduto, objPedido.TabelaPreco.Codigo, Modelo.TipoPrecoType.Normal);
                    }
                    else
                    {
                        subItem.ValorMin = objProduto.PrecoBase.HasValue ? objProduto.PrecoBase.Value : 0;
                        subItem.Valor = subItem.ValorMin;
                    }
                    subItem.ValorSemDesc = subItem.Valor;
                    subItem.ValorCalc = subItem.Valor;
                    subItem.Desc = 0;
                    subItem.Total = subItem.Quantidade * subItem.Valor;
                    subItem.Unidade = bllUnidade.getSigla((int)objProduto.IDUnidade);
                    subItem.qtdFracionada = objUnidade.bQtdFracionada == Modelo.TituloType.Sim ? true : false;
                    subItem.Acao = 1;
                    subItem.ID = 0;
                    subItem.ValorRetidoICMS = 0;
                    pedidoitem.ItensProduto.Add(subItem);
                }
            }
        }

        public IList getAll(int pCliente, int pFilial, int pTipo, DateTime pDataI, DateTime pDataF)
        {
            return (from p in db.Pedidos
                    where p.IDPessoa == pCliente
                    && p.IDFilial == pFilial
                    && p.Dt >= pDataI
                    && p.Dt <= pDataF
                    && p.Status == Modelo.StatusOrcamento.Aberto
                    && p.IDTipoPedido == pTipo
                    select new
                    {
                        Codigo = p.Codigo,
                        Data = p.Dt,
                        Digitacao = p.DtDigitacao,
                        QtdItens = p.PedidoItems.Count,
                        QtdTotProdutos = String.Format("{0:C2}", p.TotalProduto),
                        Desconto = String.Format("{0:P2}", (p.PercDesconto / 100)),
                        Acrescimo = String.Format("{0:C2}", p.ValorAcrescimo),
                        TotalPedido = p.TotalPedido,
                        SubstTrib = String.Format("{0:C2}", p.PedidoItems.Sum(pi => pi.ValorRetidoICMS != null ? pi.ValorRetidoICMS : 0)),
                        TotalComImposto = p.TotalPedido + p.PedidoItems.Sum(pi => pi.ValorRetidoICMS != null ? pi.ValorRetidoICMS : 0),
                        ID = p.ID
                    }).ToList();
        }

        public IList getAll(int pIdPedido)
        {
            return (from p in db.Pedidos
                    where p.ID == pIdPedido
                    select new
                    {
                        Codigo = p.Codigo,
                        Data = p.Dt,
                        Digitacao = p.DtDigitacao,
                        QtdItens = p.PedidoItems.Count,
                        QtdTotProdutos = String.Format("{0:C2}", p.TotalProduto),
                        Desconto = String.Format("{0:P2}", (p.PercDesconto / 100)),
                        Acrescimo = String.Format("{0:C2}", p.ValorAcrescimo),
                        TotalPedido = p.TotalPedido,
                        SubstTrib = String.Format("{0:C2}", p.PedidoItems.Sum(pi => pi.ValorRetidoICMS != null ? pi.ValorRetidoICMS : 0)),
                        TotalComImposto = p.TotalPedido + p.PedidoItems.Sum(pi => pi.ValorRetidoICMS != null ? pi.ValorRetidoICMS : 0),
                        ID = p.ID
                    }).ToList();
        }

        public IQueryable getPedidoFaturar(int pIDPedido)
        {
            IQueryable lista;
            lista = from p in db.Pedidos
                    where p.ID == pIDPedido
                    select new
                    {
                        Codigo = p.Codigo,
                        Data = p.Dt,
                        Digitacao = p.DtDigitacao,
                        QtdItens = p.PedidoItems.Count,
                        QtdTotProdutos = String.Format("{0:C2}", p.TotalProduto),
                        Desconto = String.Format("{0:P2}", (p.PercDesconto / 100)),
                        Acrescimo = String.Format("{0:C2}", p.ValorAcrescimo),
                        TotalPedido = p.TotalPedido,
                        SubstTrib = String.Format("{0:C2}", p.PedidoItems.Sum(pi => pi.ValorRetidoICMS != null ? pi.ValorRetidoICMS : 0)),
                        TotalComImposto = p.TotalPedido + p.PedidoItems.Sum(pi => pi.ValorRetidoICMS != null ? pi.ValorRetidoICMS : 0),
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar a maior sequencia de uma determinada lista
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXSequencia(Modelo.Pedido pPedido)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (int)(from c in pPedido.PedidoItems
                                  where c.Sequencia == pPedido.PedidoItems.Max(b => b.Sequencia)
                                  select c).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// Método responsável em excluir os pedidos selecionados
        /// </summary>
        /// <param name="pListaIdPedido">Lista dos ID dos Pedidos</param>
        public void excluirListaPedido(List<int> pListaIdPedido)
        {
            int cp = 0;
            Modelo.Pedido objPedido = new Modelo.Pedido();

            //Foreach em todos os pedidos a ser faturados
            for (int i = 0; i < pListaIdPedido.Count; i++)
            {
                cp = pListaIdPedido[i];

                //Carrega o Pedido
                Modelo.Pedido p;
                p = (from p1 in db.Pedidos
                     where p1.ID == cp
                     select p1).Single();

                db.Pedidos.DeleteOnSubmit(p);
                db.SubmitChanges();
            }
        }

        public List<pxyPedido> getListaTipo(InOutType tipo, int? IDPessoa)
        {
            int periodo = 0;
            CarregaPeriodo(ref periodo);

            List<pxyPedido> lista;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  Pedido.Codigo as Codigo");
            sql.AppendLine(", Pedido.Dt as Data");
            sql.AppendLine(", Pedido.DtDigitacao as Digitacao");
            sql.AppendLine(", Pedido.Status as Status");
            sql.AppendLine(", Pedido.PessoaNome as Nome");
            sql.AppendLine(", Pedido.TotalPedido as Total");
            sql.AppendLine(", Pessoa.Fantasia as Fantasia");
            sql.AppendLine(", Vendedor.Nome as Vendedor");
            sql.AppendLine(", TipoPedido.Nome as Tipo");
            sql.AppendLine(", Pedido.ID as ID");
            sql.AppendLine(", Pedido.Numero");
            sql.AppendLine(" FROM Pedido Pedido");
            sql.AppendLine(" INNER JOIN Pessoa Pessoa ON Pessoa.ID = Pedido.IDPessoa");
            sql.AppendLine(" LEFT JOIN PessoaVendedor PessoaVendedor ON PessoaVendedor.ID = Pedido.IDVendedor");
            sql.AppendLine(" LEFT JOIN Pessoa Vendedor ON Vendedor.ID = PessoaVendedor.IDPessoa");
            sql.AppendLine(" INNER JOIN TipoPedido TipoPedido ON TipoPedido.ID = Pedido.IDTipoPedido");
            sql.AppendLine(" WHERE Pedido.Ent_Sai = " + ((int)tipo).ToString() + " AND (TipoPedido.Tipo = 1 OR TipoPedido.Tipo = 2) ");
            if (IDPessoa.HasValue)
                sql.AppendLine(" AND Pedido.IDPessoa = " + IDPessoa.Value.ToString());

            if (periodo != 999)
                sql.AppendLine(" AND Pedido.Dt >= CONVERT(DATETIME,SUBSTRING(CONVERT(VARCHAR(MAX),DATEADD(DAY, -" + periodo + " , GETDATE()),101),1,10),101)");

            object[] param = new object[0];
            lista = db.ExecuteQuery<pxyPedido>(sql.ToString(), param).ToList();
            return lista;
        }

        public IList<object> getListaOrcamentos(List<int> Empresas, string pStatus, List<int> Vendedores, List<int> TipoOrcamentos, DateTime DataInicial, DateTime DataFinal)
        {
            IList<Modelo.Objeto.pxyOrcamento> lista;

            lista = (from p in db.Pedidos
                     join t in db.TipoPedidos on p.IDTipoPedido equals t.ID
                     join pes in db.Pessoas on p.IDPessoa equals pes.ID
                     join f in db.Filials on p.IDFilial equals f.ID
                     join pv in db.Vendedors on p.IDVendedor equals pv.ID
                     where t.Tipo == Modelo.TipoPedidoType.Orçamento
                     && p.Status == Modelo.StatusOrcamento.Aberto
                     && Vendedores.Contains(pv.ID)
                     && TipoOrcamentos.Contains(t.ID)
                     && Empresas.Contains(f.ID)
                     && p.Dt >= DataInicial && p.Dt <= DataFinal
                     orderby pv.Pessoa.Nome + " " + pv.Pessoa.Codigo.ToString(), t.Nome, p.Dt
                     select new Modelo.Objeto.pxyOrcamento
                     {
                         Dt = (DateTime)p.Dt,
                         Codigo = p.Codigo,
                         Cliente = pes.Nome + " " + pes.Codigo.ToString(),
                         Desconto = (Decimal)(p.TotalProduto >= 0 ? (100 - ((p.TotalPedido / p.PedidoItems.Sum(s => (s.ValorCalculado * s.Quantidade))) * 100)) : p.TotalProduto),
                         TotalPedido = (Decimal)p.TotalPedido,
                         TipoPedido = t.Nome,
                         Vendedor = pv.Pessoa.Nome + " " + pv.Pessoa.Codigo.ToString(),
                         Status = p.Status,
                         NotaFiscal = "0"
                     }).ToList<Modelo.Objeto.pxyOrcamento>();


            IList<object> retorno = new List<object>();
            foreach (var item in lista)
            {
                retorno.Add(item);
            }

            //return lista.ToList<object>();
            //StringBuilder sql = new StringBuilder();
            //sql.AppendLine("SELECT	Pedido.Dt");
            //sql.AppendLine(", Pedido.Codigo");
            //sql.AppendLine(", Filial.Codigo AS Filial");
            //sql.AppendLine(", (Pessoa.Nome + '(' + CAST(Pessoa.Codigo AS VARCHAR) + ')') AS Cliente");
            //sql.AppendLine(", CASE WHEN Pedido.TotalProduto > 0 THEN (100 - (((Pedido.TotalPedido / Pedido.TotalProduto)) * 100))");
            //sql.AppendLine("  ELSE Pedido.TotalProduto END AS Desconto");
            //sql.AppendLine(",  Pedido.ValorDesconto");
            //sql.AppendLine(", Pedido.TotalPedido");
            //sql.AppendLine(", TipoPedido.Nome AS TipoPedido");
            //sql.AppendLine(", (Vendedor.Nome + '(' + CAST(Vendedor.Codigo AS VARCHAR) + ')') AS Vendedor");
            //sql.AppendLine(", CASE	WHEN Pedido.Status = 1 THEN 'Aberto'");
            //sql.AppendLine(" WHEN Pedido.Status = 2 THEN 'Faturado'");
            //sql.AppendLine(" WHEN Pedido.Status = 3 THEN 'Cancelado'");
            //sql.AppendLine(" WHEN Pedido.Status = 4 THEN 'Faturado Parcialmente'");
            //sql.AppendLine(" WHEN Pedido.Status = 5 THEN 'Fechado'");
            //sql.AppendLine(" END AS Status");
            //sql.AppendLine(" FROM	Pedido");
            //sql.AppendLine(" INNER JOIN TipoPedido on TipoPedido.ID = Pedido.IDTipoPedido");
            //sql.AppendLine(" INNER JOIN Pessoa ON Pedido.IDPessoa = Pessoa.ID ");
            //sql.AppendLine(" INNER JOIN Filial ON Pedido.IDFilial = Filial.ID ");
            //sql.AppendLine(" INNER JOIN PessoaVendedor ON Pedido.IDVendedor = PessoaVendedor.ID");
            //sql.AppendLine(" INNER JOIN Pessoa AS Vendedor ON Vendedor.ID = PessoaVendedor.IDPessoa");
            //sql.AppendLine(" WHERE TipoPedido.Tipo = 1");
            //sql.AppendLine(" AND Pedido.Status = " + pStatus + "AND Vendedor.ID in(" + Vendedores + ")");
            //sql.AppendLine(" AND TipoPedido.ID in (" + TipoOrcamentos + ")");
            //sql.AppendLine(" AND Pedido.Dt >= '" + DataInicial.ToShortDateString() + "' AND Pedido.Dt <= '" + DataFinal.ToShortDateString() + "'" );

            //object[] param = new object[0];
            //lista = db.ExecuteQuery <object>(sql.ToString(), param).ToList<object>();         

            return retorno;
        }

        public IList<object> getListaPedidos(List<int> Empresas, string pStatus, List<int> Vendedores, List<int> TipoOrcamentos, DateTime DataInicial, DateTime DataFinal)
        {
            IList<Modelo.Objeto.pxyOrcamento> lista;
            List<Modelo.StatusOrcamento> status = new List<Modelo.StatusOrcamento>();
            IList<object> retorno;
            switch (pStatus)
            {
                case "Aberto":
                    lista = (from p in db.Pedidos
                             join t in db.TipoPedidos on p.IDTipoPedido equals t.ID
                             join pes in db.Pessoas on p.IDPessoa equals pes.ID
                             join f in db.Filials on p.IDFilial equals f.ID
                             join pv in db.Vendedors on p.IDVendedor equals pv.ID
                             join pitem in db.PedidoItems on p.ID equals pitem.IDPedido
                             //join nitem in db.NotaItems on pitem.ID equals nitem.IDPedidoItem
                             //join nota in db.Notas on nitem.IDNota equals nota.ID
                             where t.Tipo == Modelo.TipoPedidoType.Pedido
                             && p.Status == Modelo.StatusOrcamento.Aberto
                             && Vendedores.Contains(pv.ID)
                             && TipoOrcamentos.Contains(t.ID)
                             && Empresas.Contains(f.ID)
                             && p.Dt >= DataInicial && p.Dt <= DataFinal
                             orderby pv.Pessoa.Nome + " " + pv.Pessoa.Codigo.ToString(), t.Nome, p.Dt
                             select new Modelo.Objeto.pxyOrcamento
                             {
                                 Dt = (DateTime)p.Dt,
                                 Codigo = p.Codigo,
                                 Cliente = pes.Nome + " " + pes.Codigo.ToString(),
                                 Desconto = (Decimal)(p.TotalProduto >= 0 ? (100 - ((p.TotalPedido / p.PedidoItems.Sum(s => (s.ValorCalculado * s.Quantidade))) * 100)) : p.TotalProduto),
                                 TotalPedido = (Decimal)p.TotalPedido,
                                 TipoPedido = t.Nome,
                                 Vendedor = pv.Pessoa.Nome,
                                 Status = p.Status,
                                 NotaFiscal = ""
                             }).Distinct().ToList<Modelo.Objeto.pxyOrcamento>();
                    retorno = new List<object>();
                    foreach (var item in lista)
                    {
                        retorno.Add(item);
                    }
                    break;
                case "Faturado":
                    lista = (from p in db.Pedidos
                             join t in db.TipoPedidos on p.IDTipoPedido equals t.ID
                             join pes in db.Pessoas on p.IDPessoa equals pes.ID
                             join f in db.Filials on p.IDFilial equals f.ID
                             join pv in db.Vendedors on p.IDVendedor equals pv.ID
                             join pitem in db.PedidoItems on p.ID equals pitem.IDPedido
                             join nitem in db.NotaItems on pitem.ID equals nitem.IDPedidoItem
                             join nota in db.Notas on nitem.IDNota equals nota.ID
                             where t.Tipo == Modelo.TipoPedidoType.Pedido
                             && p.Status == Modelo.StatusOrcamento.Faturado
                             && Vendedores.Contains(pv.ID)
                             && TipoOrcamentos.Contains(t.ID)
                             && Empresas.Contains(f.ID)
                             && p.Dt >= DataInicial && p.Dt <= DataFinal
                             orderby pv.Pessoa.Nome + " " + pv.Pessoa.Codigo.ToString(), t.Nome, p.Dt
                             select new Modelo.Objeto.pxyOrcamento
                             {
                                 Dt = (DateTime)p.Dt,
                                 Codigo = p.Codigo,
                                 Cliente = pes.Nome + " " + pes.Codigo.ToString(),
                                 Desconto = (Decimal)(p.TotalProduto >= 0 ? (100 - ((p.TotalPedido / p.PedidoItems.Sum(s => (s.ValorCalculado * s.Quantidade))) * 100)) : p.TotalProduto),
                                 TotalPedido = (Decimal)p.TotalPedido,
                                 TipoPedido = t.Nome,
                                 Vendedor = pv.Pessoa.Nome,
                                 Status = p.Status,
                                 NotaFiscal = nota.Numero.ToString()
                             }).Distinct().ToList<Modelo.Objeto.pxyOrcamento>();
                    retorno = new List<object>();
                    foreach (var item in lista)
                    {
                        retorno.Add(item);
                    }

                    break;
                default:
                    lista = (from p in db.Pedidos
                             join t in db.TipoPedidos on p.IDTipoPedido equals t.ID
                             join pes in db.Pessoas on p.IDPessoa equals pes.ID
                             join f in db.Filials on p.IDFilial equals f.ID
                             join pv in db.Vendedors on p.IDVendedor equals pv.ID
                             join pitem in db.PedidoItems on p.ID equals pitem.IDPedido
                             //join nitem in db.NotaItems on pitem.ID equals nitem.IDPedidoItem
                             //join nota in db.Notas on nitem.IDNota equals nota.ID
                             where t.Tipo == Modelo.TipoPedidoType.Pedido
                             && p.Status == Modelo.StatusOrcamento.Aberto
                             && Vendedores.Contains(pv.ID)
                             && TipoOrcamentos.Contains(t.ID)
                             && Empresas.Contains(f.ID)
                             && p.Dt >= DataInicial && p.Dt <= DataFinal
                             orderby pv.Pessoa.Nome + " " + pv.Pessoa.Codigo.ToString(), t.Nome, p.Dt
                             select new Modelo.Objeto.pxyOrcamento
                             {
                                 Dt = (DateTime)p.Dt,
                                 Codigo = p.Codigo,
                                 Cliente = pes.Nome + " " + pes.Codigo.ToString(),
                                 Desconto = (Decimal)(p.TotalProduto >= 0 ? (100 - ((p.TotalPedido / p.PedidoItems.Sum(s => (s.ValorCalculado * s.Quantidade))) * 100)) : p.TotalProduto),
                                 TotalPedido = (Decimal)p.TotalPedido,
                                 TipoPedido = t.Nome,
                                 Vendedor = pv.Pessoa.Nome,
                                 Status = p.Status,
                                 NotaFiscal = ""
                             }).Distinct().ToList<Modelo.Objeto.pxyOrcamento>();
                    retorno = new List<object>();
                    foreach (var item in lista)
                    {
                        retorno.Add(item);
                    }

                    lista = (from p in db.Pedidos
                             join t in db.TipoPedidos on p.IDTipoPedido equals t.ID
                             join pes in db.Pessoas on p.IDPessoa equals pes.ID
                             join f in db.Filials on p.IDFilial equals f.ID
                             join pv in db.Vendedors on p.IDVendedor equals pv.ID
                             join pitem in db.PedidoItems on p.ID equals pitem.IDPedido
                             join nitem in db.NotaItems on pitem.ID equals nitem.IDPedidoItem
                             join nota in db.Notas on nitem.IDNota equals nota.ID
                             where t.Tipo == Modelo.TipoPedidoType.Pedido
                             && p.Status == Modelo.StatusOrcamento.Faturado
                             && Vendedores.Contains(pv.ID)
                             && TipoOrcamentos.Contains(t.ID)
                             && Empresas.Contains(f.ID)
                             && p.Dt >= DataInicial && p.Dt <= DataFinal
                             orderby pv.Pessoa.Nome + " " + pv.Pessoa.Codigo.ToString(), t.Nome, p.Dt
                             select new Modelo.Objeto.pxyOrcamento
                             {
                                 Dt = (DateTime)p.Dt,
                                 Codigo = p.Codigo,
                                 Cliente = pes.Nome + " " + pes.Codigo.ToString(),
                                 Desconto = (Decimal)(p.TotalProduto >= 0 ? (100 - ((p.TotalPedido / p.PedidoItems.Sum(s => (s.ValorCalculado * s.Quantidade))) * 100)) : p.TotalProduto),
                                 TotalPedido = (Decimal)p.TotalPedido,
                                 TipoPedido = t.Nome,
                                 Vendedor = pv.Pessoa.Nome,
                                 Status = p.Status,
                                 NotaFiscal = nota.Numero.ToString()
                             }).Distinct().ToList<Modelo.Objeto.pxyOrcamento>();

                    foreach (var item in lista)
                    {
                        retorno.Add(item);
                    }
                    break;
            }
            //return lista.ToList<object>();
            //StringBuilder sql = new StringBuilder();
            //sql.AppendLine("SELECT	Pedido.Dt");
            //sql.AppendLine(", Pedido.Codigo");
            //sql.AppendLine(", Filial.Codigo AS Filial");
            //sql.AppendLine(", (Pessoa.Nome + '(' + CAST(Pessoa.Codigo AS VARCHAR) + ')') AS Cliente");
            //sql.AppendLine(", CASE WHEN Pedido.TotalProduto > 0 THEN (100 - (((Pedido.TotalPedido / Pedido.TotalProduto)) * 100))");
            //sql.AppendLine("  ELSE Pedido.TotalProduto END AS Desconto");
            //sql.AppendLine(",  Pedido.ValorDesconto");
            //sql.AppendLine(", Pedido.TotalPedido");
            //sql.AppendLine(", TipoPedido.Nome AS TipoPedido");
            //sql.AppendLine(", (Vendedor.Nome + '(' + CAST(Vendedor.Codigo AS VARCHAR) + ')') AS Vendedor");
            //sql.AppendLine(", CASE	WHEN Pedido.Status = 1 THEN 'Aberto'");
            //sql.AppendLine(" WHEN Pedido.Status = 2 THEN 'Faturado'");
            //sql.AppendLine(" WHEN Pedido.Status = 3 THEN 'Cancelado'");
            //sql.AppendLine(" WHEN Pedido.Status = 4 THEN 'Faturado Parcialmente'");
            //sql.AppendLine(" WHEN Pedido.Status = 5 THEN 'Fechado'");
            //sql.AppendLine(" END AS Status");
            //sql.AppendLine(" FROM	Pedido");
            //sql.AppendLine(" INNER JOIN TipoPedido on TipoPedido.ID = Pedido.IDTipoPedido");
            //sql.AppendLine(" INNER JOIN Pessoa ON Pedido.IDPessoa = Pessoa.ID ");
            //sql.AppendLine(" INNER JOIN Filial ON Pedido.IDFilial = Filial.ID ");
            //sql.AppendLine(" INNER JOIN PessoaVendedor ON Pedido.IDVendedor = PessoaVendedor.ID");
            //sql.AppendLine(" INNER JOIN Pessoa AS Vendedor ON Vendedor.ID = PessoaVendedor.IDPessoa");
            //sql.AppendLine(" WHERE TipoPedido.Tipo = 1");
            //sql.AppendLine(" AND Pedido.Status = " + pStatus + "AND Vendedor.ID in(" + Vendedores + ")");
            //sql.AppendLine(" AND TipoPedido.ID in (" + TipoOrcamentos + ")");
            //sql.AppendLine(" AND Pedido.Dt >= '" + DataInicial.ToShortDateString() + "' AND Pedido.Dt <= '" + DataFinal.ToShortDateString() + "'" );

            //object[] param = new object[0];
            //lista = db.ExecuteQuery <object>(sql.ToString(), param).ToList<object>();         

            return retorno;
        }

        public List<Modelo.Objeto.pxyRequisicao> GetRequisicao(string pFilial)
        {
            List<Modelo.Objeto.pxyRequisicao> lista = new List<Modelo.Objeto.pxyRequisicao>();
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select Pedido.Codigo as CodigoPedido, Pedido.Dt as Data");
            sql.AppendLine(", Pedido.PessoaNome as Cliente");
            sql.AppendLine(", case when Pedido.Status = 1 then 'Aberto' else 'Fechado' end as Estado, ");
            sql.AppendLine("case when ISNULL(Projeto.Nome, '') = '' then 'Sem Projeto' else Projeto.Nome end as Projeto");
            sql.AppendLine(", TipoPedido.Nome as TipoPedido");
            sql.AppendLine("from Pedido");
            sql.AppendLine("inner join TipoPedido on TipoPedido.ID = Pedido.IDTipoPedido");
            sql.AppendLine("left join Projeto on Projeto.ID = Pedido.IDProjeto");
            sql.AppendLine("where Pedido.IDFilial IN(" + pFilial + ") and TipoPedido.Tipo = 3");

            object[] param = new object[0];
            lista = db.ExecuteQuery<Modelo.Objeto.pxyRequisicao>(sql.ToString(), param).ToList();

            return lista;
        }

        public List<Modelo.Objeto.pxyRequisicao> GetPedido(string pFilial)
        {
            List<Modelo.Objeto.pxyRequisicao> lista = new List<Modelo.Objeto.pxyRequisicao>();
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select Pedido.Codigo as CodigoPedido, Pedido.Dt as Data");
            sql.AppendLine(", Pedido.TotalPedido as ValorTotal, Pedido.PessoaNome as Cliente");
            sql.AppendLine(", case when Pedido.Status = 1 then 'Aberto' else 'Fechado' end as Estado, ");
            sql.AppendLine("case when ISNULL(Projeto.Nome, '') = '' then 'Sem Projeto' else Projeto.Nome end as Projeto");
            sql.AppendLine("from Pedido");
            sql.AppendLine("inner join TipoPedido on TipoPedido.ID = Pedido.IDTipoPedido");
            sql.AppendLine("left join Projeto on Projeto.ID = Pedido.IDProjeto");
            sql.AppendLine("where Pedido.IDFilial IN(" + pFilial + ") and TipoPedido.Tipo <> 3");

            object[] param = new object[0];
            lista = db.ExecuteQuery<Modelo.Objeto.pxyRequisicao>(sql.ToString(), param).ToList();

            return lista;
        }

        public List<Modelo.Objeto.pxyRequisicaoProdutoStatus> GetRequisicaoProdutoStatus(int pIdPedido)
        {
            List<Modelo.Objeto.pxyRequisicaoProdutoStatus> lista = new List<Modelo.Objeto.pxyRequisicaoProdutoStatus>();
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("select Pedido.Codigo as CodigoPedido, Pedido.Dt as Data, Produto.Nome as Produto");
            sql.AppendLine("	, PedidoItem.Quantidade as Quantidade, PedidoItem.Unidade as Unidade");
            sql.AppendLine("	, case when (PedidoItem.Quantidade - (PedidoItem.QtdRetirada + PedidoItem.QtdCancelada)) > 0 then 'Pendentes' else 'Retirados' end as Status");
            sql.AppendLine("	, Produto.Codigo as CodigoProduto, PedidoItem.QtdRetirada as QuantidadeEntregue");
            sql.AppendLine("	, (PedidoItem.Quantidade - (PedidoItem.QtdRetirada + PedidoItem.QtdCancelada)) as QuantidadeNaoEntregue");
            sql.AppendLine("    , Pedido.PessoaNome as Cliente");
            sql.AppendLine("    , case when Pedido.Status = 1 then 'Aberto' else 'Fechado' end as Estado, Projeto.Nome as Projeto");
            sql.AppendLine("    , Unidade.bQtdFracionada as Fracao");
            sql.AppendLine("    from Pedido");
            sql.AppendLine("    inner join TipoPedido on TipoPedido.ID = Pedido.IDTipoPedido");
            sql.AppendLine("    left join Projeto on Projeto.ID = Pedido.IDProjeto");
            sql.AppendLine("    inner join PedidoItem on PedidoItem.IDPedido = Pedido.ID");
            sql.AppendLine("    inner join Produto on Produto.ID = PedidoItem.IDProduto");
            sql.AppendLine("    inner join Unidade on Unidade.ID = Produto.IDUnidade");
            sql.AppendLine("where TipoPedido.Tipo = 3 and Pedido.ID = " + pIdPedido);

            object[] param = new object[0];
            lista = db.ExecuteQuery<Modelo.Objeto.pxyRequisicaoProdutoStatus>(sql.ToString(), param).ToList();

            return lista;
        }

        public List<pxyPedido> getListaRequisicoes(InOutType tipo, int? IDPessoa)
        {
            int periodo = 0;
            CarregaPeriodo(ref periodo);

            List<pxyPedido> lista;

            StringBuilder sql = new StringBuilder();
            sql.AppendLine("SELECT  Pedido.Codigo as Codigo");
            sql.AppendLine(", Pedido.Dt as Data");
            sql.AppendLine(", Pedido.DtDigitacao as Digitacao");
            sql.AppendLine(", Pedido.Status as Status");
            sql.AppendLine(", Pedido.PessoaNome as Nome");
            sql.AppendLine(", Pedido.TotalPedido as Total");
            sql.AppendLine(", Pessoa.Fantasia as Fantasia");
            sql.AppendLine(", '' as Vendedor");
            sql.AppendLine(", TipoPedido.Nome as Tipo");
            sql.AppendLine(", Pedido.ID as ID");
            sql.AppendLine(" FROM Pedido Pedido");
            sql.AppendLine(" INNER JOIN Pessoa Pessoa ON Pessoa.ID = Pedido.IDPessoa");
            sql.AppendLine(" INNER JOIN TipoPedido TipoPedido ON TipoPedido.ID = Pedido.IDTipoPedido");
            sql.AppendLine(" WHERE Pedido.Ent_Sai = " + ((int)tipo).ToString());
            sql.AppendLine(" AND TipoPedido.tipo = 3");
            if (IDPessoa.HasValue)
                sql.AppendLine(" AND Pedido.IDPessoa = " + IDPessoa.Value.ToString());

            if (periodo != 999)
                sql.AppendLine(" AND Pedido.Dt >= CONVERT(DATETIME,SUBSTRING(CONVERT(VARCHAR(MAX),DATEADD(DAY, -" + periodo + " , GETDATE()),101),1,10),101)");

            object[] param = new object[0];
            lista = db.ExecuteQuery<pxyPedido>(sql.ToString(), param).ToList();
            return lista;
        }

        private static void CarregaPeriodo(ref int periodo)
        {
            XmlDocument documentoXml = ConfiguracaoLocalRepositorio.GetArquivoConfiguracao();
            int opcaofiltro = ConfiguracaoLocalRepositorio.GetPeriodoPedido(documentoXml);

            switch (opcaofiltro)
            {
                case 0:
                    periodo = 0;
                    break;
                case 1:
                    periodo = 1;
                    break;
                case 2:
                    periodo = 2;
                    break;
                case 3:
                    periodo = 3;
                    break;
                case 4:
                    periodo = 4;
                    break;
                case 5:
                    periodo = 9;
                    break;
                case 6:
                    periodo = 14;
                    break;
                case 7:
                    periodo = 29;
                    break;
                default:
                    periodo = 999;
                    break;
            }
        }

        public struct AuxOrcamentoPedido
        {
            public int IDOrcamentoItem { get; set; }
            public Modelo.PedidoItem objPedidoItem { get; set; }
        }

        public struct AuxCotacaoProdutoPedidoItem
        {
            public int IDOrcamentoItem { get; set; }
            public Modelo.CotacaoProdutoPedidoItem objCotacaoProdutoPedidoItem { get; set; }
        }

        public bool GravarPedido(Modelo.DataClassesDataContext _db, int pAcao, Modelo.Pedido pedido, List<Modelo.ItensOrcamento> listaItensOrcamento, List<Modelo.PedidoHistorico> listaModificacoes)
        {
            bool ret = true;
            List<AuxOrcamentoPedido> orcamentoPedidosItem = new List<AuxOrcamentoPedido>();
            List<AuxCotacaoProdutoPedidoItem> cotacaoProdutoPedidoItens = new List<AuxCotacaoProdutoPedidoItem>();
            BLL.Unidade bllUnidade_ = new BLL.Unidade(_db);
            BLL.Produto bllProduto_ = new BLL.Produto(_db);
            Modelo.Pedido objPedido = null;
            if (_db.Connection.State != ConnectionState.Open)
                _db.Connection.Open();
            _db.Transaction = _db.Connection.BeginTransaction();
            try
            {
                if (pAcao != 1)
                {
                    objPedido = (from p in _db.Pedidos
                                 where p.ID == pedido.ID
                                 select p).Single();

                    objPedido.AltData = DateTime.Now;
                    objPedido.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                    objPedido.itensExcluir.AddRange(objPedido.PedidoItems.Where(i => !objPedido.PedidoItems.Contains(i.PedidoItem1)).ToList());
                    foreach (Modelo.PedidoItem item in objPedido.PedidoItems)
                    {
                        foreach (Modelo.PedidoItem filho in item.PedidoItems)
                        {
                            //Significa que o item é de um outro pedido
                            if (filho.IDPedido != objPedido.ID)
                            {
                                filho.IDOrcamentoItem = null;
                                AuxOrcamentoPedido aux = new AuxOrcamentoPedido();
                                aux.IDOrcamentoItem = item.ID;
                                aux.objPedidoItem = filho;
                                orcamentoPedidosItem.Add(aux);
                            }
                        }

                        foreach (Modelo.CotacaoProdutoPedidoItem cotP in item.CotacaoProdutoPedidoItems)
                        {
                            cotP.IDPedidoItem = null;
                            AuxCotacaoProdutoPedidoItem aux = new AuxCotacaoProdutoPedidoItem();
                            aux.IDOrcamentoItem = item.ID;
                            aux.objCotacaoProdutoPedidoItem = cotP;
                            cotacaoProdutoPedidoItens.Add(aux);
                        }
                    }
                    objPedido.PedidoItems.Clear();
                    //objPedido.PedidoParcelas.Clear();

                    objPedido.Codigo = pedido.Codigo;

                    objPedido.Filial = pedido.Filial;

                    objPedido.TipoPedido = pedido.TipoPedido;

                    if (pedido.TabelaPreco != null)
                        objPedido.TabelaPreco = pedido.TabelaPreco;
                    else
                        objPedido.TabelaPreco = null;

                    objPedido.Dt = pedido.Dt;
                    objPedido.DtDigitacao = pedido.DtDigitacao;

                    if (pedido.Condicao != null)
                        objPedido.Condicao = pedido.Condicao;
                    else
                        objPedido.Condicao = null;

                    //objPedido.IDPessoa = pedido.Pessoa.ID;
                    objPedido.Pessoa = pedido.Pessoa;

                    if (pedido.Vendedor != null)
                    {
                        objPedido.Vendedor = pedido.Vendedor;
                    }

                    objPedido.PessoaNome = pedido.PessoaNome;
                    objPedido.PessoaEndereco = pedido.PessoaEndereco;
                    objPedido.PessoaBairro = pedido.PessoaBairro;
                    objPedido.PessoaCidade = pedido.PessoaCidade;
                    objPedido.PessoaUF = pedido.PessoaUF;
                    objPedido.PessoaCNPJCPF = pedido.PessoaCNPJCPF;
                    objPedido.PessoaInscRG = pedido.PessoaInscRG;
                    objPedido.PessoaTelefone = pedido.PessoaTelefone;
                    objPedido.PessoaCEP = pedido.PessoaCEP;
                    objPedido.Ent_Sai = pedido.Ent_Sai;
                    objPedido.TotalProduto = pedido.TotalProduto;
                    objPedido.PercDesconto = pedido.PercDesconto;
                    objPedido.ValorDesconto = pedido.ValorDesconto;
                    objPedido.ValorAcrescimo = pedido.ValorAcrescimo;
                    objPedido.TotalPedido = pedido.TotalPedido;
                    objPedido.Observacao1 = pedido.Observacao1;
                    objPedido.Observacao2 = pedido.Observacao2;
                    objPedido.Observacao3 = pedido.Observacao3;
                    objPedido.Numero = pedido.Numero;
                    objPedido.ComplementoNumero = pedido.ComplementoNumero;
                    objPedido.IDProjeto = pedido.IDProjeto;

                    #region Parcelas
                    //if (pedido.PedidoParcelas.HasLoadedOrAssignedValues)
                    //{
                    //    Modelo.PedidoParcela objParc;
                    //    foreach (Modelo.PedidoParcela parc in pedido.PedidoParcelas)
                    //    {
                    //        objParc = new Modelo.PedidoParcela();
                    //        objParc.bAlterado = parc.bAlterado;
                    //        objParc.bEntrada = parc.bEntrada;
                    //        objParc.Emitente = parc.Emitente;
                    //        objParc.IDBanco = parc.Banco == null ? parc.IDBanco : parc.Banco.ID;
                    //        objParc.IDPortador = parc.Portador == null ? parc.IDPortador : parc.Portador.ID;
                    //        objParc.IDTipoDocumento = parc.TipoDocumento == null ? parc.IDTipoDocumento : parc.TipoDocumento.ID;
                    //        objParc.NumAgencia = parc.NumAgencia;
                    //        objParc.NumBanco = parc.NumBanco;
                    //        objParc.NumCheque = parc.NumCheque;
                    //        objParc.NumContaCorrente = parc.NumContaCorrente;
                    //        objParc.Parcela = parc.Parcela;
                    //        objParc.Valor = parc.Valor;
                    //        objParc.Vencimento = parc.Vencimento;

                    //        objPedido.PedidoParcelas.Add(objParc);
                    //    }
                    //}
                    #endregion
                }
                else
                {
                    objPedido = pedido;
                    objPedido.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                    objPedido.IncData = DateTime.Now;
                    objPedido.AltData = DateTime.Now;
                    objPedido.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                    int cod = objPedido.EncontraCodigoDisponivel(_db);
                    if (objPedido.Codigo != cod)
                        objPedido.Codigo = cod;

                    _db.Pedidos.InsertOnSubmit(objPedido);

                    objPedido.Status = Modelo.StatusOrcamento.Aberto;
                }

                #region Pedido Itens
                Modelo.Produto objProduto = new Modelo.Produto();
                Modelo.PedidoItem objPedidoItem;
                List<Modelo.PedidoItem> itensPedido = new List<PedidoItem>();
                int seq = 1;
                foreach (Modelo.ItensOrcamento itemorcamento in listaItensOrcamento)
                {
                    if (itemorcamento.Acao == 3)
                        continue;
                    if (itemorcamento.Produto == "0" || (itemorcamento.Total == 0 && objPedido.TipoPedido.Tipo != TipoPedidoType.Requisição))
                        continue;

                    objPedidoItem = new Modelo.PedidoItem();
                    objProduto = (Modelo.Produto)bllProduto_.getObjeto(bllProduto_.getID(itemorcamento.Produto.ToString()));
                    objPedidoItem.ID = itemorcamento.ID;
                    objPedidoItem.Sequencia = seq;// itemorcamento.Sequencia;
                    objPedidoItem.IDProduto = objProduto.ID;
                    objPedidoItem.ProdutoNome = itemorcamento.Descricao;
                    objPedidoItem.ProdutoDescReduzida = objProduto.DescReduzida;
                    objPedidoItem.Quantidade = itemorcamento.Quantidade;
                    objPedidoItem.Valor = itemorcamento.Valor; //itemorcamento.ValorSemDesc;
                    objPedidoItem.ValorCalculado = itemorcamento.ValorCalc;
                    objPedidoItem.ValorDesconto = 0;
                    objPedidoItem.PercDesconto = itemorcamento.Desc;
                    objPedidoItem.SubTotal = itemorcamento.Total;
                    objPedidoItem.Total = itemorcamento.Total;
                    objPedidoItem.Unidade = itemorcamento.Unidade;//bllUnidade.getSigla((int)objProduto.IDUnidade);
                    objPedidoItem.Ent_Sai = objPedido.Ent_Sai;//objPedido.TipoPedido.Ent_Sai;
                    objPedidoItem.Dt = objPedido.Dt;
                    objPedidoItem.bReservaEstoque = objPedido.TipoPedido.bReservaEstoque;
                    objPedidoItem.PesoBruto = objProduto.PesoBruto;
                    objPedidoItem.PesoLiquido = objProduto.PesoLiquido;
                    objPedidoItem.InformacoesAdicionais = itemorcamento.InformacoesAdicionais;
                    if (itemorcamento.IDOrcamentoItem > 0)
                        objPedidoItem.IDOrcamentoItem = itemorcamento.IDOrcamentoItem;

                    if (orcamentoPedidosItem.Where(o => o.IDOrcamentoItem == itemorcamento.ID).Count() > 0)
                    {
                        objPedidoItem.PedidoItems.AddRange((from p in orcamentoPedidosItem
                                                            where p.IDOrcamentoItem == itemorcamento.ID
                                                            select p.objPedidoItem));
                    }

                    if (cotacaoProdutoPedidoItens.Where(o => o.IDOrcamentoItem == itemorcamento.ID).Count() > 0)
                    {
                        objPedidoItem.CotacaoProdutoPedidoItems.AddRange((from p in cotacaoProdutoPedidoItens
                                                                          where p.IDOrcamentoItem == itemorcamento.ID
                                                                          select p.objCotacaoProdutoPedidoItem));
                    }

                    if (itemorcamento.ItensProduto.Count > 0)
                    {
                        foreach (Modelo.ItensOrcamento subItem in itemorcamento.ItensProduto)
                        {
                            if (subItem.Acao == 3)
                                continue;
                            if (subItem.Produto == "0")
                                continue;

                            Modelo.PedidoItem objSubItem = new Modelo.PedidoItem();
                            objProduto = (Modelo.Produto)bllProduto_.getObjeto(bllProduto_.getID(subItem.Produto.ToString()));
                            objSubItem.ID = subItem.ID;
                            objSubItem.Sequencia = seq;// subItem.Sequencia;
                            objSubItem.IDProduto = objProduto.ID;
                            objSubItem.ProdutoNome = subItem.Descricao;
                            objSubItem.ProdutoDescReduzida = objProduto.DescReduzida;
                            objSubItem.Quantidade = subItem.Quantidade;
                            objSubItem.Valor = subItem.ValorSemDesc;
                            objSubItem.ValorCalculado = subItem.ValorCalc;
                            objSubItem.ValorDesconto = 0;
                            objSubItem.PercDesconto = subItem.Desc;
                            objSubItem.SubTotal = subItem.Total;
                            objSubItem.Total = subItem.Total;
                            objSubItem.Unidade = subItem.Unidade;//bllUnidade.getSigla((int)objProduto.IDUnidade);
                            objSubItem.Ent_Sai = objPedido.Ent_Sai;//objPedido.TipoPedido.Ent_Sai;
                            objSubItem.Dt = objPedido.Dt;
                            objSubItem.bReservaEstoque = objPedido.TipoPedido.bReservaEstoque;
                            objSubItem.PesoBruto = objProduto.PesoBruto;
                            objSubItem.PesoLiquido = objProduto.PesoLiquido;
                            objSubItem.InformacoesAdicionais = String.Empty;
                            objSubItem.Pedido = objPedido;
                            objSubItem.PedidoItem1 = objPedidoItem;
                        }
                    }

                    objPedido.PedidoItems.Add(objPedidoItem);

                    seq++;
                }
                #endregion

                BLL.Estoque bllEstoque = new Estoque();
                bllEstoque.AtualizaEstoquePedido(objPedido, pAcao == 3, _db);
                objPedido.itensExcluir.Clear();
                if (listaModificacoes != null)
                    _db.PedidoHistoricos.InsertAllOnSubmit(listaModificacoes);
                _db.SubmitChanges();
                pedido.ID = objPedido.ID;
                _db.Transaction.Commit();
            }
            catch (ChangeConflictException cce)
            {
                DesfazerAlteracoes(_db, pedido);
                _db.Transaction.Rollback();
                ret = false;
                throw new Exception(cce.Message);
            }
            catch (SqlException ex)
            {
                DesfazerAlteracoes(_db, pedido);
                _db.Transaction.Rollback();
                string mensagem = ex.Message;
                if (ex.Number == 1205)
                    mensagem = "Não foi possivel faturar o pedido.\nTente novamente.";

                ret = false;
                throw new Exception(mensagem);
            }
            catch (Exception ex)
            {
                DesfazerAlteracoes(_db, pedido);
                _db.Transaction.Rollback();
                ret = false;
                throw new Exception(ex.Message);
            }
            finally
            {
                if (_db.Transaction != null)
                    _db.Transaction.Dispose();
                _db.Transaction = null;
            }

            return ret;
        }

        private void DesfazerAlteracoes(Modelo.DataClassesDataContext pDb, Modelo.Pedido objPedido)
        {
            Modelo.MetodosEstaticos.DiscardInsertsAndDeletes(pDb);
            if (objPedido.ID > 0)
                pDb.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, objPedido);
        }

        public int PedidosAbertosCliente(int idPessoa, ref DateTime datai, ref DateTime dataf)
        {
            var aux = from p in db.Pedidos
                      where p.IDPessoa == idPessoa
                      && p.Status == StatusOrcamento.Aberto
                      select new
                      {
                          p.Dt
                      };
            if (aux.Count() > 0)
            {
                datai = aux.Min(a => a.Dt).Value;
                dataf = aux.Max(a => a.Dt).Value;
            }
            return aux.Count();
        }

        public Modelo.Pedido GetPed(int pID)
        {
            Modelo.Pedido a;

            a = (from c in db.Pedidos
                 where c.ID == pID
                 select c).First();

            return a;
        }        
    }
}
