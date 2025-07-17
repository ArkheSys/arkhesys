using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Data;

namespace BLL
{
    public class Balanco
    {
        private Modelo.DataClassesDataContext db;

        public Balanco(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public IQueryable getLista()
        {
            IQueryable lista;

            lista = from p in db.Balancos
                    select new
                    {
                        Empresa = p.Filial.Nome,
                        Iniciado = p.IncData,
                        Fechamento = p.Data,
                        ID = p.ID
                    };
            return lista;
        }

        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Balanco objBalanco)
        {
            Dictionary<string, string> ret = ValidaObjeto(objBalanco);
            if (ret.Count == 0)
            {
                if (objBalanco.ID == 0)
                {
                    objBalanco.IncData = DateTime.Now.Date;
                    objBalanco.IncHora = DateTime.Now;
                    objBalanco.IncUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                }
                else
                {
                    objBalanco.AltData = DateTime.Now.Date;
                    objBalanco.AltHora = DateTime.Now;
                    objBalanco.AltUsuario = Modelo.cwkGlobal.objUsuarioLogado.Login;
                }
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.Balanco objBalanco)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objBalanco.IDFilial == 0)
                ret.Add("lkFilial", "Campo Obrigatório.");

            return ret;
        }

        public static DataTable Fechar(Modelo.Balanco objBalanco, Modelo.DataClassesDataContext db)
        {
            List<int> ids = (from b in objBalanco.BalancoItems select b.IDProduto).ToList();
            List<Modelo.Estoque> estoques = (from e in db.Estoques where e.IDFilial == objBalanco.IDFilial && ids.Contains(e.IDProduto) select e).ToList();
            List<LogBalanco> log = new List<LogBalanco>();
            List<Modelo.Objeto.NotaItem> notasItensCompra = new List<Modelo.Objeto.NotaItem>();
            List<Modelo.Objeto.NotaItem> notasItensVenda = new List<Modelo.Objeto.NotaItem>();
            Modelo.Estoque objEstoque;
            LogBalanco objLog;
            DataTable ret = new DataTable();
            using (TransactionScope trans = new TransactionScope())
            {
                foreach (Modelo.BalancoItem item in objBalanco.BalancoItems)
                {
                    var aux = estoques.Where(e => e.IDProduto == item.IDProduto);
                    if (aux.Count() > 0)
                    {
                        objEstoque = aux.First();
                    }
                    else
                    {
                        objEstoque = new Modelo.Estoque();
                        objEstoque.IDProduto = item.IDProduto;
                        objEstoque.IDFilial = objBalanco.IDFilial;
                        objEstoque.Localidade = String.Empty;
                        objEstoque.Qtd_Compra_Pedido = 0;
                        objEstoque.Qtd_Estoque_Min = 0;
                        objEstoque.Qtd_Estoque_Pedido = 0;
                        objEstoque.Qtd_Estoque_Fisico = 0;
                    }

                    if (item.Quantidade != objEstoque.Qtd_Estoque_Fisico)
                    {
                        objLog = new LogBalanco();
                        objLog.Produto = db.Produtos.Where(p => p.ID == objEstoque.IDProduto).First();
                        objLog.qtd_Ant = objEstoque.Qtd_Estoque_Fisico;
                        objLog.qtd_Atu = item.Quantidade;
                        objLog.diferenca = Math.Abs(objLog.qtd_Ant - objLog.qtd_Atu);
                        log.Add(objLog);
                        objEstoque.Qtd_Estoque_Fisico = item.Quantidade;
                        InsereNotaItem(ref objLog, item.Produto, notasItensCompra, notasItensVenda);
                    }
                }
                GravarNotas(objBalanco.Data.Value, objBalanco.IDFilial, notasItensCompra, notasItensVenda, db);

                ret = GeraRelatorioBalanco(log);
                trans.Complete();
            }
            return ret;
        }

        private static void InsereNotaItem(ref LogBalanco objLog, Modelo.Produto objProduto, List<Modelo.Objeto.NotaItem> compras, List<Modelo.Objeto.NotaItem> vendas)
        {
            Modelo.Objeto.NotaItem item = new Modelo.Objeto.NotaItem();
            item.IDProduto = objProduto.ID;

            item.ProdutoNome = objProduto.Nome;
            item.ProdutoDescReduzida = objProduto.DescReduzida;
            item.InfAdicionais = objProduto.InfAdicionais;
            item.Valor = objProduto.PrecoBase.Value;
            item.Quantidade = objLog.diferenca;
            item.ValorDesconto = 0;
            item.PercDesconto = 0;
            item.Unidade = objProduto.Unidade.Sigla;
            item.PesoBruto = objProduto.PesoBruto * item.Quantidade;
            item.PesoLiquido = objProduto.PesoLiquido * item.Quantidade;
            item.SubTotal = item.Quantidade * item.Valor;
            item.Total = item.SubTotal;

            if (objLog.qtd_Ant > objLog.qtd_Atu)
            {
                item.Ent_Sai = Modelo.InOutType.Saída;
                vendas.Add(item);
            }
            else
            {
                item.Ent_Sai = Modelo.InOutType.Entrada;
                compras.Add(item);
            }
            objLog.ent_sai = item.Ent_Sai;
        }

        private static void GravarNotas(DateTime dataBalanco, int idFilial, List<Modelo.Objeto.NotaItem> compras, List<Modelo.Objeto.NotaItem> vendas, Modelo.DataClassesDataContext db)
        {
            BLL.Estoque bllEstoque = new Estoque();
            BLL.NotaItem bllNotaItem = new NotaItem(db);
            BLL.Nota bllNota = new Nota(db);
            Modelo.Configuracao objConfiguracao = Modelo.Configuracao.GetConfiguracao(db);
            Modelo.Filial fil = (from f in db.Filials
                                 where f.ID == idFilial
                                 select f).Single();

            if (compras.Count > 0)
            {
                Modelo.Nota objNota = new Modelo.Nota();
                objNota.AltData = DateTime.Now;
                objNota.IncData = DateTime.Now;
                objNota.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                objNota.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                PreencheNota(dataBalanco, objConfiguracao.IDTipoNotaBalancoEnt.Value, fil, objNota, compras, db, bllNota, bllNotaItem);
                db.Notas.InsertOnSubmit(objNota);
                bllEstoque.AtualizaEstoqueNota(objNota, false, db);
                db.SubmitChanges();
            }

            if (vendas.Count > 0)
            {
                Modelo.Nota objNota = new Modelo.Nota();
                objNota.AltData = DateTime.Now;
                objNota.IncData = DateTime.Now;
                objNota.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                objNota.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
                PreencheNota(dataBalanco, objConfiguracao.IDTipoNotaBalancoSai.Value, fil, objNota, vendas, db, bllNota, bllNotaItem);
                db.Notas.InsertOnSubmit(objNota);
                bllEstoque.AtualizaEstoqueNota(objNota, false, db);
                db.SubmitChanges();
            }
        }

        private static void PreencheNota(DateTime dataBalanco, int idTipoMovto, Modelo.Filial fil, Modelo.Nota objNota, List<Modelo.Objeto.NotaItem> itens, Modelo.DataClassesDataContext db, BLL.Nota bllNota, BLL.NotaItem bllNotaItem)
        {
            objNota.ValorDesconto = 0;
            objNota.ValorAcrescimo = 0;
            objNota.Codigo = bllNota.RetornaMAXCodigo(db);
            objNota.Filial = fil;
            objNota.TipoNota = db.TipoNotas.Where(t => t.ID == idTipoMovto).First();
            objNota.Ent_Sai = objNota.TipoNota.Ent_Sai;
            objNota.Dt = dataBalanco;
            objNota.DtDigitacao = dataBalanco;
            objNota.PessoaNome = String.Empty;
            objNota.PessoaEndereco = String.Empty;
            objNota.PessoaBairro = String.Empty;
            objNota.PessoaCidade = String.Empty;
            objNota.PessoaUF = String.Empty;
            objNota.PessoaCNPJCPF = String.Empty;
            objNota.PessoaInscRG = String.Empty;
            objNota.PessoaTelefone = String.Empty;
            objNota.PessoaCEP = String.Empty;
            objNota.IDVendedor = null;
            objNota.IDTabelaPreco = null;
            objNota.IDCondicao = null;
            objNota.ModeloDocto = 0; 
            objNota.AltData = DateTime.Now;
            objNota.IncData = DateTime.Now;
            objNota.IncUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;
            objNota.AltUsuario = cwkControleUsuario.Facade.getUsuarioLogado.Login;

            objNota.Serie = objNota.TipoNota.Serie;
            objNota.Numero = bllNota.getRetornaUltimoNF(db, fil.ID, objNota.Serie);
            objNota.bGeraFinanceiro = (byte)objNota.TipoNota.bGeraFinanceiro;
            objNota.bPessoaContribuinte = 0;

            AuxGravarNotas(itens, bllNotaItem, objNota);
        }

        private static void AuxGravarNotas(List<Modelo.Objeto.NotaItem> itens, BLL.NotaItem bllNotaItem, Modelo.Nota objNota)
        {
            for (int i = 0; i < itens.Count; i++)
            {
                Modelo.NotaItem objNotaItem = new Modelo.NotaItem();
                objNotaItem.Sequencia = i + 1;
                objNotaItem.IDProduto = itens[i].IDProduto;
                objNotaItem.ProdutoNome = itens[i].ProdutoNome;
                objNotaItem.ProdutoDescReduzida = itens[i].ProdutoDescReduzida;
                objNotaItem.Quantidade = itens[i].Quantidade;
                objNotaItem.Valor = itens[i].Valor;
                objNotaItem.ValorDesconto = itens[i].ValorDesconto;
                objNotaItem.PercDesconto = itens[i].PercDesconto;
                objNotaItem.SubTotal = itens[i].SubTotal;
                objNotaItem.Total = itens[i].Total;
                objNotaItem.Unidade = itens[i].Unidade;
                objNotaItem.Ent_Sai = objNota.TipoNota.Ent_Sai;
                objNotaItem.Dt = objNota.Dt;
                objNotaItem.bAtualizaEstoque = objNota.TipoNota.bAtualizaEstoque;
                objNotaItem.PesoBruto = itens[i].PesoBruto;
                objNotaItem.PesoLiquido = itens[i].PesoLiquido;
                objNotaItem.InfAdicionais = itens[i].InfAdicionais;

                objNotaItem.BaseICMS = itens[i].BaseICMS;
                objNotaItem.ValorICMS = itens[i].ValorICMS;
                objNotaItem.ValorIsentoICMS = itens[i].ValorIsentoICMS;
                objNotaItem.ValorOutroICMS = itens[i].ValorOutroICMS;
                objNotaItem.ValorRetidoICMS = itens[i].ValorRetidoICMS;
                objNotaItem.BaseICMSSubst = itens[i].BaseICMSSubst;
                objNotaItem.AliqICMS = itens[i].AliqICMS;
                objNotaItem.AliqICMSNormal = itens[i].AliqICMSNormal;
                objNotaItem.IDICMS = itens[i].IDICMS;
                objNotaItem.SitTrib = itens[i].SitTrib;
                objNotaItem.ValorICMSReducao = itens[i].ValorICMSReducao;
                objNotaItem.ValorICMSReducaoDif = itens[i].ValorICMSReducaoDif;
                objNotaItem.BaseICMSSubst = itens[i].BaseICMSSubst;
                objNotaItem.ValorRetidoICMS = itens[i].ValorRetidoICMS;
                objNotaItem.TextoLei = itens[i].TextoLei;

                objNotaItem.TAG_CST = itens[i].TAG_CST;
                objNotaItem.modBC_N13 = itens[i].modBC_N13;
                objNotaItem.modBCST_N18 = itens[i].modBCST_N18;
                objNotaItem.CST_Pis = itens[i].CST_Pis;
                objNotaItem.vBC_Q07 = itens[i].vBC_Q07;
                objNotaItem.pPIS_Q08 = itens[i].pPIS_Q08;
                objNotaItem.vPIS_Q09 = itens[i].vPIS_Q09;
                objNotaItem.CST_Cofins = itens[i].CST_Cofins;
                objNotaItem.vBC_S07 = itens[i].vBC_S07;
                objNotaItem.pCOFINS_S08 = itens[i].pCOFINS_S08;
                objNotaItem.vCOFINS_S11 = itens[i].vCOFINS_S11;
                objNotaItem.orig_N11 = itens[i].orig_N11;
                objNotaItem.vBC_O10 = itens[i].vBC_O10;

                objNotaItem.vBC_O10 = itens[i].vBC_O10;
                objNotaItem.pIPI_O13 = itens[i].pIPI_O13;
                objNotaItem.vIPI_O14 = itens[i].vIPI_O14;
                objNotaItem.CST_Ipi = itens[i].CST_Ipi;
                objNotaItem.pRedBC_N14 = itens[i].pRedBC_N14;
                objNotaItem.pICMSST_N22 = itens[i].pICMSST_N22;
                objNotaItem.cEnq_O06 = itens[i].cEnq_O06;
                objNotaItem.AliqCupom = itens[i].AliqCupom;
                objNotaItem.bAtualizaEstoque = objNota.TipoNota.bAtualizaEstoque;

                objNota.NotaItems.Add(objNotaItem);
            }
        }

        private static DataTable GeraRelatorioBalanco(List<LogBalanco> log)
        {
            DataTable ret = new DataTable();
            ret.Columns.AddRange(
                new DataColumn[]
                {
                    new DataColumn("Codigo", typeof(String)),
                    new DataColumn("Produto", typeof(String)),
                    new DataColumn("QuantidadeAnt", typeof(Decimal)),
                    new DataColumn("QuantidadeAtu", typeof(Decimal)),
                    new DataColumn("Diferenca", typeof(Decimal)),
                    new DataColumn("Ent_Sai", typeof(String)),
                });

            DataRow row;
            foreach (LogBalanco item in log)
            {
                row = ret.NewRow();
                row["Codigo"] = item.Produto.Codigo;
                row["Produto"] = item.Produto.Nome;
                row["QuantidadeAnt"] = item.qtd_Ant;
                row["QuantidadeAtu"] = item.qtd_Atu;
                row["Diferenca"] = item.diferenca;
                row["Ent_Sai"] = item.ent_sai.ToString();
                ret.Rows.Add(row);
            }
            return ret;
        }

        public class LogBalanco
        {
            public Modelo.Produto Produto;
            public decimal qtd_Ant;
            public decimal qtd_Atu;
            public decimal diferenca;
            public Modelo.InOutType ent_sai;
        }
    }
}
