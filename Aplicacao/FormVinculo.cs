using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using cwkGestao.Negocio;
using cwkGestao.Modelo;
using System.Linq;

namespace Aplicacao
{
    public partial class FormVinculo : IntermediariasTela.FormManutVinculoIntermediaria
    {
        private readonly Pessoa fornecedor;
        private readonly List<decimal> dadosVinculo = new List<decimal>();
        private readonly DemaisInforma��es outrasInfor = new DemaisInforma��es();
        private List<Vinculo> listaVinculo;
        private List<NotaItem> listaNotaItem = new List<NotaItem>();
        private readonly List<object> listaGeral = new List<object>();

        private readonly bool alteracaoVinculos = false;

        public FormVinculo(FormVinculoInicial formVinculo, Pessoa pessoa, List<NotaItem> NotaItem, List<Vinculo> Vinculo, List<decimal> dadosFornecedor, List<object> lista, DemaisInforma��es outras)
        {
            listaVinculo = Vinculo;
            listaNotaItem = NotaItem;
            listaGeral = lista;

            dadosVinculo = dadosFornecedor;
            fornecedor = pessoa;
            outrasInfor = outras;

            formVinculo.PBControle.Visible = true;
            formVinculo.PBControle.Minimum = 0;
            formVinculo.PBControle.Maximum = Vinculo.Count;

            PBControle.Value = 0;

            var objCfg = ConfiguracaoController.Instancia.GetConfiguracao();
            var UnidadesMedidas = UnidadeController.Instancia.GetAll();
            var estoques = LocalEstoqueController.Instancia.GetAll();

            foreach (var Item in listaVinculo)
            {
                try
                {
                    formVinculo.PBControle.Value++;
                }
                catch { }

                if (Item.ID != 0)
                    continue;

                if (Item.Produto == null)
                {
                    Produto ProdAux = null;
                    if ((bool)Item.CodEan?.ToUpper()?.Equals("SEM GTIN"))
                        Item.CodEan = string.Empty;

                    if(!string.IsNullOrEmpty(Item.CodEan))
                        ProdAux = ProdutoController.Instancia.FindByCodigoBarra(Item.CodEan);

                    if (ProdAux != null)
                    {
                        Item.Produto = ProdutoController.Instancia.LoadObjectById(ProdAux.ID);
                        if (Item.Produto?.Fornecedor?.ID == pessoa?.ID ||
                             Item.Produto?.Fornecedor2?.ID == pessoa?.ID ||
                             Item.Produto?.Fornecedor3?.ID == pessoa?.ID ||
                             Item.Produto?.Fornecedor4?.ID == pessoa?.ID ||
                             Item.Produto?.Fornecedor5?.ID == pessoa?.ID)
                        {
                            // Nada a se fazer, j� possui um fornecedor...
                        }
                        else
                        {
                            if (Item.Produto?.Fornecedor == null)
                            {
                                Item.Produto.Fornecedor = fornecedor;
                                Item.Produto.BarraFornecedor = Item.CodEan;
                                ProdutoController.Instancia.Salvar(Item.Produto, Acao.Alterar);
                            }
                            else if (Item.Produto?.Fornecedor2 == null)
                            {
                                Item.Produto.Fornecedor2 = fornecedor;
                                Item.Produto.BarraFornecedor2 = Item.CodEan;
                                ProdutoController.Instancia.Salvar(Item.Produto, Acao.Alterar);
                            }
                            else if (Item.Produto?.Fornecedor3 == null)
                            {
                                Item.Produto.Fornecedor3 = fornecedor;
                                Item.Produto.BarraFornecedor3 = Item.CodEan;
                                ProdutoController.Instancia.Salvar(Item.Produto, Acao.Alterar);
                            }
                            else if (Item.Produto?.Fornecedor4 == null)
                            {
                                Item.Produto.Fornecedor4 = fornecedor;
                                Item.Produto.BarraFornecedor4 = Item.CodEan;
                                ProdutoController.Instancia.Salvar(Item.Produto, Acao.Alterar);
                            }
                            else if (Item.Produto?.Fornecedor5 == null)
                            {
                                Item.Produto.Fornecedor5 = fornecedor;
                                Item.Produto.BarraFornecedor5 = Item.CodEan;
                                ProdutoController.Instancia.Salvar(Item.Produto, Acao.Alterar);
                            }
                        }

                        // Ultima Valida��o...
                        if (Item.Produto?.Fornecedor?.ID != pessoa?.ID &&
                            Item.Produto?.Fornecedor2?.ID != pessoa?.ID &&
                            Item.Produto?.Fornecedor3?.ID != pessoa?.ID &&
                            Item.Produto?.Fornecedor4?.ID != pessoa?.ID &&
                            Item.Produto?.Fornecedor5?.ID != pessoa?.ID)
                        {
                            Item.Produto = null;
                        }
                    }
                }

                if (Item.Produto == null)
                {
                    var NCM = NCMController.Instancia.GetByNcm(Item.Ncm);
                    Item.Produto = new Produto
                    {
                        Nome = Item.descricao,
                        NovoProduto = true,
                        ID_NCM = NCM?.ID ?? 0,
                        NCM = Item.Ncm,
                        TabelaNCM = NCM,
                        ENQ_IPI = objCfg.CodigoEnquadramentoIPI ?? 999,
                        GrupoEstoque = objCfg.IDGrupoEstoqueProduto == null ? null : GrupoEstoqueController.Instancia.LoadObjectById(objCfg.IDGrupoEstoqueProduto.Value),
                        DtCadastroProduto = DateTime.Now,
                        CodigoReferencia = Item.CodProduto
                        //UtilizaIdentificadorEstoque = true
                    };

                    AtribuirBarraEBarraFornecedor(Item.Produto, Item.CodEan);

                    if (Item.Produto.GrupoEstoque == null)
                        throw new Exception("Nenhum grupo de estoque informado para o produto: " + Item.Produto.Nome);

                    Item.Produto.Estoques = new List<Estoque>();
                    foreach (var itemEstoque in estoques)
                    {
                        Item.Produto.Estoques.Add(new Estoque()
                        {
                            LocalEstoque = itemEstoque,
                            Produto = Item.Produto,
                        });
                    }

                    if (pessoa != null)
                        Item.Produto.Fornecedor = pessoa;

                    /*if (NCM != null)
                    {
                        Item.Produto.ID_NCM = NCM.ID;
                        if (objCfg.CodigoEnquadramentoIPI.HasValue)
                            NCM.EnqGeral = objCfg.CodigoEnquadramentoIPI.Value;
                    }*/

                    /* A unidade de medida de entrada deve ser a que vem da NFe de importa��o, caso n�o exista pela sigla, vem de configura��es. */
                    var UnidadeEncontrada = string.IsNullOrEmpty(Item.UnidadeEntradaSigla) ? null : UnidadesMedidas.FirstOrDefault(o => o.Sigla.ToUpper() == Item.UnidadeEntradaSigla.ToUpper());
                    if (UnidadeEncontrada == null && objCfg.IDUnidadeMedidaEntradaProduto.HasValue)
                    {
                        Item.Produto.UnidadeEntrada = UnidadeController.Instancia.LoadObjectById(objCfg.IDUnidadeMedidaEntradaProduto.Value);
                        Item.UnidadeEntrada = Item.Produto.UnidadeEntrada;
                    }
                    else
                    {
                        Item.Produto.UnidadeEntrada = UnidadeEncontrada;
                        Item.UnidadeEntrada = UnidadeEncontrada;
                    }

                    if (objCfg.IDUnidadeMedidaSaidaProduto.HasValue)
                    {
                        Item.Produto.Unidade = UnidadeController.Instancia.LoadObjectById(objCfg.IDUnidadeMedidaSaidaProduto.Value);
                        if (Item.Produto.Unidade != null)
                        {
                            Item.UnidadeSaida = Item.Produto.Unidade;
                            Item.UnidadeSaidaSigla = Item.Produto.Unidade.Sigla;
                        }
                    }
                    else
                    {
                        if (UnidadeEncontrada == null)
                        {
                            UnidadeController.Instancia.Salvar(new Unidade
                            {
                                Codigo = UnidadeController.Instancia.MaxCodigo(),
                                Nome = Item.UnidadeEntradaSigla,
                                Sigla = Item.UnidadeEntradaSigla
                            }, Acao.Incluir);

                            UnidadesMedidas = UnidadeController.Instancia.GetAll();
                            UnidadeEncontrada = UnidadesMedidas.FirstOrDefault(o => o.Sigla == Item.UnidadeEntradaSigla);
                        }

                        Item.Produto.Unidade = UnidadeEncontrada;
                        Item.UnidadeSaida = UnidadeEncontrada;
                        Item.UnidadeSaidaSigla = UnidadeEncontrada?.Sigla;
                    }

                    if (objCfg.IDGrupoEstoqueProduto.HasValue)
                        Item.Produto.GrupoEstoque = GrupoEstoqueController.Instancia.LoadObjectById(objCfg.IDGrupoEstoqueProduto.Value);

                    if (objCfg.IDTabelaPrecoProduto.HasValue)
                    {
                        var Tabela = TabelaPrecoController.Instancia.LoadObjectById(objCfg.IDTabelaPrecoProduto.Value);
                        Item.Produto.TabelaPrecoProdutos = new List<TabelaPrecoProduto>
                        {
                            new TabelaPrecoProduto
                            {
                                Codigo = Tabela.Codigo,
                                TabelaPreco = Tabela,
                                MargemLucro = Tabela.SugestaoMargemLucro,
                                PDesconto = Tabela.SugestaoPDesconto,
                                PAcrescimo = Tabela.SugestaoPAcrescimo,
                                NomeTabelaPreco = Tabela.Nome,
                                Produto = Item.Produto
                            }
                        };
                    }
                }

                if (!string.IsNullOrEmpty(Item.CFOP))
                {
                    var cfop = TabelaCFOPController.Instancia.GetCFOPPorNumero(Item.CFOP);
                    if (cfop == null)
                        throw new Exception($"O CFOP {Item.CFOP} n�o foi encontrado.");

                    Item.IDCFOP = TabelaCFOPController.Instancia.GetCFOPPorNumero(cfop.CfopVinculadoEntrada);
                    Item.CFOP_Cwork = cfop.CfopVinculadoEntrada;
                }
            }

            gcVinculo.DataSource = listaVinculo;
            gcVinculo.RefreshDataSource();
            gcVinculo.Refresh();
            TotalizarItens();
        }

        private void AtribuirBarraEBarraFornecedor(Produto produto, string codigoBarras)
        {
            if (string.IsNullOrEmpty(codigoBarras))
                produto.Barra = produto.BarraFornecedor = ProdutoController.Instancia.GetProximoCodigoBarrasEAN13(null);
            else
                produto.Barra = produto.BarraFornecedor = codigoBarras;
        }

        public FormVinculo(Pessoa Fornecedor)
        {
            listaVinculo = (List<Vinculo>)VinculoController.Instancia.GetAll();
            fornecedor = Fornecedor;

            List<Vinculo> listaTela = new List<Vinculo>();
            foreach (var item in listaVinculo.Where(i => i.Pessoa.ID == Fornecedor.ID))
            {
                var produto = ProdutoController.Instancia.LoadProdutoByCodigo(item.CodProduto) ?? new Produto();
                item.descricao = produto.Nome ?? "";

                item.CFOP_Cwork = item.IDCFOP.CFOP;
                listaTela.Add(item);
            }
            if (listaTela.Count > 0)
            {
                gcVinculo.DataSource = listaTela;
                gcVinculo.RefreshDataSource();
                gcVinculo.Refresh();
                TotalizarItens();
            }
            else
                MessageBox.Show("Nenhum v�nculo encontrado p/ este fornecedor.", "Falha", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            alteracaoVinculos = true;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
        }

        private void FormVinculo_Shown(object sender, EventArgs e)
        {
            if (alteracaoVinculos)
            {
                if (Convert.ToInt32(listaVinculo.Count(i => i.Pessoa.ID == fornecedor.ID)) == 0)
                    this.Close();

                AjustaForm(false);
            }
            else
            {
                AjustaForm(true);

                if (listaVinculo.Count == 0 && listaNotaItem.Count > 0)
                {
                    ChamaNota();
                    this.Close();
                }

                txtSerie.Text = dadosVinculo[1].ToString();
                txtNumero.Text = dadosVinculo[2].ToString();
                txtValor.Text = dadosVinculo[3].ToString();
            }

            txtFornecedor.Text = fornecedor.Nome + " - " + fornecedor.Codigo;
        }

        private void AjustaForm(bool inclusao)
        {
            txtNumero.Visible = txtSerie.Visible = txtValor.Visible = inclusao;

            lblNumero.Visible = lblSerie.Visible = lblValor.Visible = inclusao;

            if (!inclusao)
            {
                pnVinculo.Location = new Point(8, 36);
                //this.Size = new Size(853, 443);
                gvVinculo.Columns["descricao"].Visible = false;
            }
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            listaVinculo = new List<Vinculo>();
            for (int i = 0; i < gvVinculo.RowCount; i++)
                listaVinculo.Add((Vinculo)gvVinculo.GetRow(i));

            PBControle.Visible = true;
            PBControle.Minimum = 0;
            PBControle.Maximum = listaVinculo.Count;

            int cont = 0;
            foreach (var item in listaVinculo)
            {
                PBControle.Value++;

                if (item.ID != 0)
                    continue;

                item.Pessoa = fornecedor;
                if (item.Produto.ID == 0)
                {
                    var NCM = NCMController.Instancia.GetByNcm(item.Ncm);
                    if (NCM == null)
                        NCMController.Instancia.Salvar(new NCM
                        {
                            Codigo = NCMController.Instancia.MaxCodigo(),
                            Ncm = item.Ncm,
                            Descricao = item.Ncm,
                            //EnqGeral = ConfiguracaoController.Instancia.GetConfiguracao().CodigoEnquadramentoIPI ?? 999
                        }, Acao.Incluir);

                    item.Produto.Codigo = ProdutoController.Instancia.MaxCodigo() + "";
                    item.Produto.DtCadastroProduto = DateTime.Now;
                    item.Produto.TabelaNCM = NCMController.Instancia.GetByNcm(item.Ncm);
                    item.Produto.ID_NCM = item.Produto.TabelaNCM?.ID ?? 0;
                    item.Produto.Estoques = new List<Estoque>();
                    foreach (var itemEstoque in LocalEstoqueController.Instancia.GetAll())
                        item.Produto.Estoques.Add(new Estoque()
                        {
                            LocalEstoque = itemEstoque,
                            Produto = item.Produto,

                        });
                    ProdutoController.Instancia.Salvar(item.Produto, Acao.Incluir);
                }

                if (item.IDCFOP != null)
                    VinculoController.Instancia.Salvar(item, !alteracaoVinculos ? Acao.Incluir : Acao.Alterar);
                else
                {
                    MessageBox.Show("CFOP selecionado n�o existe. linha: " + (cont + 1).ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    PBControle.Value = 0;

                    if (!alteracaoVinculos)
                        for (var i = 0; i < cont; i++)
                            VinculoController.Instancia.Salvar(listaVinculo[i], Acao.Excluir);
                    return;
                }
                cont++;
            }

            if (!alteracaoVinculos)
            {
                listaNotaItem = new List<NotaItem>();

                VinculoController.Instancia.getListasXML(ref listaNotaItem, ref listaVinculo, dadosVinculo, listaGeral, true);

                if (listaNotaItem.Count <= 0)
                {
                    MessageBox.Show("Faltam v�nculos entre produtos do XML e produtos cadastrados. Verifique.", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                    ChamaNota();
            }

            this.Close();
        }

        private void ChamaNota()
        {
            var nota = new Nota();
            var form = new FormNota(InOutType.Entrada);
            nota.Pessoa = fornecedor;
            nota.Serie = dadosVinculo[1].ToString();
            nota.Numero = Convert.ToInt32(dadosVinculo[2]);
            nota.TotalNota = dadosVinculo[3];
            nota.ValorDesconto = dadosVinculo[4];
            nota.ChaveNota = outrasInfor.chave_nota;

            form.Operacao = Acao.Incluir;
            form.Selecionado = nota;

            var cont = 0;
            foreach (var notaItem in listaNotaItem)
            {
                notaItem.Nota = nota;
                notaItem.Sequencia = ++cont;
                nota.NotaItems.Add(notaItem);
            }

            form.importacaoXML = true;
            form.ShowDialog();

            PBControle.Value = 0;
        }

        private void EscolherProdutos()
        {
            var grid = new GridProdutoGen(ProdutoController.Instancia.GetAll(), new Produto(), false, false, typeof(FormProduto)) { Selecionando = true };

            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            if (grid.Selecionado.ID > 0)
                if (grid.Selecionado != null)
                {
                    var Prod = ProdutoController.Instancia.LoadObjectById(grid.Selecionado?.ID ?? 0);
                    gvVinculo.SetFocusedRowCellValue("Produto", Prod);

                    if (Prod.Unidade != null)
                    {
                        var vinculo = (Vinculo)gvVinculo.GetFocusedRow();

                        vinculo.UnidadeSaidaSigla = Prod.Unidade.Sigla;
                        vinculo.UnidadeSaida = Prod.Unidade;
                        gvVinculo.SetFocusedRowCellValue("UnidadeSaidaSigla", vinculo.UnidadeSaidaSigla);
                    }
                    else
                        gvVinculo.EditingValue = Prod.Unidade;
                }
        }

        private void FormVinculo_KeyDown(object sender, KeyEventArgs e)
        {
            if (gvVinculo.FocusedColumn.Name == "Produto")
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        EscolherProdutos();
                        break;
                }
            }
        }

        private void SelecionaProdutoECFOP()
        {
            var vinculo = (Vinculo)gvVinculo.GetFocusedRow();

            switch (gvVinculo.FocusedColumn.Name)
            {
                case "UnSaida":
                    if (!string.IsNullOrEmpty(gvVinculo?.EditingValue?.ToString()))
                    {
                        var UnidadeEncontrada = UnidadeController.Instancia.GetAll().FirstOrDefault(o => o.Sigla.ToUpper() == gvVinculo?.EditingValue?.ToString().ToUpper());
                        if (UnidadeEncontrada != null)
                        {
                            vinculo.UnidadeSaidaSigla = UnidadeEncontrada.Sigla;
                            vinculo.UnidadeSaida = UnidadeEncontrada;
                            gvVinculo.SetFocusedRowCellValue("UnidadeSaidaSigla", vinculo.UnidadeSaidaSigla);
                        }
                        else
                            gvVinculo.EditingValue = vinculo.UnidadeSaidaSigla;
                    }
                    break;
                case "Produto":
                    try
                    {
                        if (gvVinculo.EditingValueModified && !string.IsNullOrEmpty(gvVinculo.EditingValue?.ToString()))
                        {
                            var produto = ProdutoController.Instancia.LoadProdutoByCodigo(gvVinculo?.EditingValue?.ToString());
                            if (produto != null)
                            {
                                vinculo.Produto = produto;
                                gvVinculo.SetFocusedRowCellValue("Produto", vinculo.Produto);
                            }
                            else
                            {
                                gvVinculo.EditingValue = vinculo.Produto.Nome;
                                gvVinculo.SetFocusedRowCellValue("Produto", vinculo.Produto);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        gvVinculo.EditingValue = vinculo.Produto.Nome;
                    }
                    break;
                case "IDCFOP":
                    if (gvVinculo.EditingValue != null)
                    {
                        var cfop = TabelaCFOPController.Instancia.GetCFOPPorNumero(gvVinculo.EditingValue.ToString());
                        if (cfop != null)
                        {
                            vinculo.IDCFOP = cfop;
                            vinculo.CFOP_Cwork = cfop.CFOP;
                            gvVinculo.SetFocusedRowCellValue("IDCFOP", cfop.CFOP);
                        }
                        else
                        {
                            gvVinculo.EditingValue = null;
                            MessageBox.Show("CFOP inv�lido", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    break;
            }
        }

        private void gvVinculo_KeyDown(object sender, KeyEventArgs e)
        {
            SelecionaProdutoECFOP();
        }

        private void SbCFOP_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja vincular todos os produtos com o CFOP informado ?", "Aten��o", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (tbCFOP.EditValue == null || tbCFOP.Text == "")
            {
                MessageBox.Show("Informe um CFOP no campo para vincular", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var cfop = TabelaCFOPController.Instancia.GetCFOPPorNumero(tbCFOP.Text);

                if (cfop == null)
                {
                    MessageBox.Show("CFOP inv�lido", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                gvVinculo.MoveFirst();
                for (int i = 0; i < gvVinculo.RowCount; i++)
                {
                    int rowHdl = gvVinculo.FocusedRowHandle;
                    gvVinculo.SetRowCellValue(rowHdl, IDCFOP, tbCFOP.EditValue);
                    var vinculo = (Vinculo)gvVinculo.GetFocusedRow();

                    vinculo.IDCFOP = cfop;
                    vinculo.CFOP_Cwork = cfop.CFOP;

                    gvVinculo.MoveNext();
                }
            }
            catch
            {
                MessageBox.Show("CFOP inv�lido", "Aten��o", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void TotalizarItens()
        {
            txtQuantidadeItens.Text = ((gcVinculo.DataSource as List<Vinculo>)?.Count ?? 0).ToString() + " Itens a ser importados.";
        }

        private void gvVinculo_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var vinculo = gvVinculo.GetRow(e.RowHandle) as Vinculo;
            if (vinculo?.Produto?.ID != 0)
                e.Appearance.BackColor = Color.FromArgb(173, 216, 230);
        }
    }
}
