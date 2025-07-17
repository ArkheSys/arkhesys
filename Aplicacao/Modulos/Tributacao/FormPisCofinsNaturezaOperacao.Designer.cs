
namespace Aplicacao.Modulos.Tributacao
{
    partial class FormPisCofinsNaturezaOperacao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPisCofinsNaturezaOperacao));
            this.txtCodigo = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label1 = new System.Windows.Forms.Label();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label9 = new System.Windows.Forms.Label();
            this.btnlkpTabelaPIS = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpTabelaPIS = new Cwork.Utilitarios.Componentes.Lookup();
            this.btnlkpTabelaCOFINS = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpTabelaCOFINS = new Cwork.Utilitarios.Componentes.Lookup();
            this.btConsultarNaturezaOperacao = new Cwork.Utilitarios.Componentes.DevButton();
            this.btExcluirNaturezaOperacao = new Cwork.Utilitarios.Componentes.DevButton();
            this.btAlterarNaturezaOperacao = new Cwork.Utilitarios.Componentes.DevButton();
            this.btIncluirNaturezaOperacao = new Cwork.Utilitarios.Componentes.DevButton();
            this.gcNaturezaOperacao = new DevExpress.XtraGrid.GridControl();
            this.gvNCMItens = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPIS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaCOFINS.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNaturezaOperacao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNCMItens)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 436);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(632, 436);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(713, 436);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            this.imageList1.Images.SetKeyName(6, "");
            this.imageList1.Images.SetKeyName(7, "");
            // 
            // tpPrincipal
            // 
            this.tpPrincipal.Controls.Add(this.btConsultarNaturezaOperacao);
            this.tpPrincipal.Controls.Add(this.btExcluirNaturezaOperacao);
            this.tpPrincipal.Controls.Add(this.btAlterarNaturezaOperacao);
            this.tpPrincipal.Controls.Add(this.btIncluirNaturezaOperacao);
            this.tpPrincipal.Controls.Add(this.gcNaturezaOperacao);
            this.tpPrincipal.Controls.Add(this.btnlkpTabelaCOFINS);
            this.tpPrincipal.Controls.Add(this.lkpTabelaCOFINS);
            this.tpPrincipal.Controls.Add(this.btnlkpTabelaPIS);
            this.tpPrincipal.Controls.Add(this.lkpTabelaPIS);
            this.tpPrincipal.Controls.Add(this.txtDescricao);
            this.tpPrincipal.Controls.Add(this.label9);
            this.tpPrincipal.Controls.Add(this.labelControl1);
            this.tpPrincipal.Controls.Add(this.labelControl4);
            this.tpPrincipal.Controls.Add(this.txtCodigo);
            this.tpPrincipal.Controls.Add(this.label1);
            this.tpPrincipal.Size = new System.Drawing.Size(770, 412);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(776, 418);
            // 
            // txtCodigo
            // 
            this.txtCodigo.CwkFuncaoValidacao = null;
            this.txtCodigo.CwkMascara = null;
            this.txtCodigo.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtCodigo.Location = new System.Drawing.Point(68, 25);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodigo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCodigo.SelecionarTextoOnEnter = true;
            this.txtCodigo.Size = new System.Drawing.Size(125, 20);
            this.txtCodigo.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Código:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(42, 59);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(20, 13);
            this.labelControl4.TabIndex = 13;
            this.labelControl4.Text = "PIS:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(391, 59);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(42, 13);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "COFINS:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.CwkFuncaoValidacao = null;
            this.txtDescricao.CwkMascara = null;
            this.txtDescricao.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtDescricao.EditValue = "";
            this.txtDescricao.Location = new System.Drawing.Point(262, 25);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.MaxLength = 200;
            this.txtDescricao.SelecionarTextoOnEnter = true;
            this.txtDescricao.Size = new System.Drawing.Size(449, 20);
            this.txtDescricao.TabIndex = 24;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(199, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Descrição:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnlkpTabelaPIS
            // 
            this.btnlkpTabelaPIS.Location = new System.Drawing.Point(346, 58);
            this.btnlkpTabelaPIS.Lookup = null;
            this.btnlkpTabelaPIS.Name = "btnlkpTabelaPIS";
            this.btnlkpTabelaPIS.Size = new System.Drawing.Size(24, 20);
            this.btnlkpTabelaPIS.SubForm = null;
            this.btnlkpTabelaPIS.SubFormType = null;
            this.btnlkpTabelaPIS.SubFormTypeParams = null;
            this.btnlkpTabelaPIS.TabIndex = 72;
            this.btnlkpTabelaPIS.TabStop = false;
            this.btnlkpTabelaPIS.Text = "...";
            this.btnlkpTabelaPIS.Click += new System.EventHandler(this.btnlkpPis_Click);
            // 
            // lkpTabelaPIS
            // 
            this.lkpTabelaPIS.ButtonLookup = this.btnlkpTabelaPIS;
            this.lkpTabelaPIS.CamposPesquisa = new string[] {
        "Codigo",
        "Descricao"};
            this.lkpTabelaPIS.CamposRestricoesAND = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpTabelaPIS.CamposRestricoesAND")));
            this.lkpTabelaPIS.CamposRestricoesNOT = null;
            this.lkpTabelaPIS.CamposRestricoesOR = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpTabelaPIS.CamposRestricoesOR")));
            this.lkpTabelaPIS.ColunaDescricao = new string[] {
        "Descrição",
        "Codigo"};
            this.lkpTabelaPIS.ColunaTamanho = new string[] {
        "150",
        "50"};
            this.lkpTabelaPIS.ContextoLinq = null;
            this.lkpTabelaPIS.CwkFuncaoValidacao = null;
            this.lkpTabelaPIS.CwkMascara = null;
            this.lkpTabelaPIS.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpTabelaPIS.Exemplo = null;
            this.lkpTabelaPIS.ID = 0;
            this.lkpTabelaPIS.Join = null;
            this.lkpTabelaPIS.Key = System.Windows.Forms.Keys.F5;
            this.lkpTabelaPIS.Location = new System.Drawing.Point(68, 56);
            this.lkpTabelaPIS.Name = "lkpTabelaPIS";
            this.lkpTabelaPIS.OnIDChanged = null;
            this.lkpTabelaPIS.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpTabelaPIS.Properties.Appearance.Options.UseBackColor = true;
            this.lkpTabelaPIS.SelecionarTextoOnEnter = false;
            this.lkpTabelaPIS.Size = new System.Drawing.Size(272, 20);
            this.lkpTabelaPIS.Tabela = null;
            this.lkpTabelaPIS.TabIndex = 71;
            this.lkpTabelaPIS.TituloTelaPesquisa = null;
            this.lkpTabelaPIS.ToolTip = "Campos pesquisados: Codigo, Descricao.";
            this.lkpTabelaPIS.Where = null;
            // 
            // btnlkpTabelaCOFINS
            // 
            this.btnlkpTabelaCOFINS.Location = new System.Drawing.Point(717, 60);
            this.btnlkpTabelaCOFINS.Lookup = null;
            this.btnlkpTabelaCOFINS.Name = "btnlkpTabelaCOFINS";
            this.btnlkpTabelaCOFINS.Size = new System.Drawing.Size(24, 20);
            this.btnlkpTabelaCOFINS.SubForm = null;
            this.btnlkpTabelaCOFINS.SubFormType = null;
            this.btnlkpTabelaCOFINS.SubFormTypeParams = null;
            this.btnlkpTabelaCOFINS.TabIndex = 74;
            this.btnlkpTabelaCOFINS.TabStop = false;
            this.btnlkpTabelaCOFINS.Text = "...";
            this.btnlkpTabelaCOFINS.Click += new System.EventHandler(this.btnlkpCofins_Click);
            // 
            // lkpTabelaCOFINS
            // 
            this.lkpTabelaCOFINS.ButtonLookup = this.btnlkpTabelaCOFINS;
            this.lkpTabelaCOFINS.CamposPesquisa = new string[] {
        "Codigo",
        "Descricao"};
            this.lkpTabelaCOFINS.CamposRestricoesAND = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpTabelaCOFINS.CamposRestricoesAND")));
            this.lkpTabelaCOFINS.CamposRestricoesNOT = null;
            this.lkpTabelaCOFINS.CamposRestricoesOR = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpTabelaCOFINS.CamposRestricoesOR")));
            this.lkpTabelaCOFINS.ColunaDescricao = new string[] {
        "Descrição",
        "Código"};
            this.lkpTabelaCOFINS.ColunaTamanho = new string[] {
        "100",
        "20"};
            this.lkpTabelaCOFINS.ContextoLinq = null;
            this.lkpTabelaCOFINS.CwkFuncaoValidacao = null;
            this.lkpTabelaCOFINS.CwkMascara = null;
            this.lkpTabelaCOFINS.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpTabelaCOFINS.Exemplo = null;
            this.lkpTabelaCOFINS.ID = 0;
            this.lkpTabelaCOFINS.Join = null;
            this.lkpTabelaCOFINS.Key = System.Windows.Forms.Keys.F5;
            this.lkpTabelaCOFINS.Location = new System.Drawing.Point(439, 58);
            this.lkpTabelaCOFINS.Name = "lkpTabelaCOFINS";
            this.lkpTabelaCOFINS.OnIDChanged = null;
            this.lkpTabelaCOFINS.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpTabelaCOFINS.Properties.Appearance.Options.UseBackColor = true;
            this.lkpTabelaCOFINS.SelecionarTextoOnEnter = false;
            this.lkpTabelaCOFINS.Size = new System.Drawing.Size(272, 20);
            this.lkpTabelaCOFINS.Tabela = null;
            this.lkpTabelaCOFINS.TabIndex = 73;
            this.lkpTabelaCOFINS.TituloTelaPesquisa = null;
            this.lkpTabelaCOFINS.ToolTip = "Campos pesquisados: Codigo, Descricao.";
            this.lkpTabelaCOFINS.Where = null;
            // 
            // btConsultarNaturezaOperacao
            // 
            this.btConsultarNaturezaOperacao.GridControl = null;
            this.btConsultarNaturezaOperacao.GridControlType = null;
            this.btConsultarNaturezaOperacao.GridControlTypeParams = null;
            this.btConsultarNaturezaOperacao.ImageOptions.Image = global::Aplicacao.Properties.Resources.Consulta_copy;
            this.btConsultarNaturezaOperacao.Location = new System.Drawing.Point(404, 318);
            this.btConsultarNaturezaOperacao.Name = "btConsultarNaturezaOperacao";
            this.btConsultarNaturezaOperacao.Size = new System.Drawing.Size(75, 23);
            this.btConsultarNaturezaOperacao.SubForm = null;
            this.btConsultarNaturezaOperacao.SubFormType = null;
            this.btConsultarNaturezaOperacao.SubFormTypeParams = null;
            this.btConsultarNaturezaOperacao.TabIndex = 76;
            this.btConsultarNaturezaOperacao.TabStop = false;
            this.btConsultarNaturezaOperacao.Text = "&Consultar";
            // 
            // btExcluirNaturezaOperacao
            // 
            this.btExcluirNaturezaOperacao.GridControl = null;
            this.btExcluirNaturezaOperacao.GridControlType = null;
            this.btExcluirNaturezaOperacao.GridControlTypeParams = null;
            this.btExcluirNaturezaOperacao.ImageOptions.Image = global::Aplicacao.Properties.Resources.Excluir_copy;
            this.btExcluirNaturezaOperacao.Location = new System.Drawing.Point(638, 318);
            this.btExcluirNaturezaOperacao.Name = "btExcluirNaturezaOperacao";
            this.btExcluirNaturezaOperacao.Size = new System.Drawing.Size(75, 23);
            this.btExcluirNaturezaOperacao.SubForm = null;
            this.btExcluirNaturezaOperacao.SubFormType = null;
            this.btExcluirNaturezaOperacao.SubFormTypeParams = null;
            this.btExcluirNaturezaOperacao.TabIndex = 79;
            this.btExcluirNaturezaOperacao.TabStop = false;
            this.btExcluirNaturezaOperacao.Text = "&Excluir";
            // 
            // btAlterarNaturezaOperacao
            // 
            this.btAlterarNaturezaOperacao.GridControl = null;
            this.btAlterarNaturezaOperacao.GridControlType = null;
            this.btAlterarNaturezaOperacao.GridControlTypeParams = null;
            this.btAlterarNaturezaOperacao.ImageOptions.Image = global::Aplicacao.Properties.Resources.Alterar_copy;
            this.btAlterarNaturezaOperacao.Location = new System.Drawing.Point(560, 318);
            this.btAlterarNaturezaOperacao.Name = "btAlterarNaturezaOperacao";
            this.btAlterarNaturezaOperacao.Size = new System.Drawing.Size(75, 23);
            this.btAlterarNaturezaOperacao.SubForm = null;
            this.btAlterarNaturezaOperacao.SubFormType = null;
            this.btAlterarNaturezaOperacao.SubFormTypeParams = null;
            this.btAlterarNaturezaOperacao.TabIndex = 78;
            this.btAlterarNaturezaOperacao.TabStop = false;
            this.btAlterarNaturezaOperacao.Text = "&Alterar";
            // 
            // btIncluirNaturezaOperacao
            // 
            this.btIncluirNaturezaOperacao.GridControl = null;
            this.btIncluirNaturezaOperacao.GridControlType = null;
            this.btIncluirNaturezaOperacao.GridControlTypeParams = null;
            this.btIncluirNaturezaOperacao.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.btIncluirNaturezaOperacao.Location = new System.Drawing.Point(482, 318);
            this.btIncluirNaturezaOperacao.Name = "btIncluirNaturezaOperacao";
            this.btIncluirNaturezaOperacao.Size = new System.Drawing.Size(75, 23);
            this.btIncluirNaturezaOperacao.SubForm = null;
            this.btIncluirNaturezaOperacao.SubFormType = null;
            this.btIncluirNaturezaOperacao.SubFormTypeParams = null;
            this.btIncluirNaturezaOperacao.TabIndex = 77;
            this.btIncluirNaturezaOperacao.TabStop = false;
            this.btIncluirNaturezaOperacao.Text = "&Incluir";
            // 
            // gcNaturezaOperacao
            // 
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.First.Hint = "Primeiro registro";
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.Last.Hint = "Último registro";
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.Next.Hint = "Próximo registro";
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.NextPage.Hint = "Próxima página";
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.Prev.Hint = "Registro anterior";
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.PrevPage.Hint = "Página anterior";
            this.gcNaturezaOperacao.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcNaturezaOperacao.EmbeddedNavigator.TextStringFormat = "Registro {0} de {1}";
            this.gcNaturezaOperacao.Location = new System.Drawing.Point(42, 121);
            this.gcNaturezaOperacao.MainView = this.gvNCMItens;
            this.gcNaturezaOperacao.Name = "gcNaturezaOperacao";
            this.gcNaturezaOperacao.Size = new System.Drawing.Size(672, 191);
            this.gcNaturezaOperacao.TabIndex = 75;
            this.gcNaturezaOperacao.UseEmbeddedNavigator = true;
            this.gcNaturezaOperacao.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNCMItens});
            // 
            // gvNCMItens
            // 
            this.gvNCMItens.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.Empty.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvNCMItens.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvNCMItens.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvNCMItens.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvNCMItens.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvNCMItens.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvNCMItens.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.OddRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.OddRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvNCMItens.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.Preview.Options.UseFont = true;
            this.gvNCMItens.Appearance.Preview.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.Row.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvNCMItens.GridControl = this.gcNaturezaOperacao;
            this.gvNCMItens.Name = "gvNCMItens";
            this.gvNCMItens.OptionsBehavior.Editable = false;
            this.gvNCMItens.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvNCMItens.OptionsView.EnableAppearanceEvenRow = true;
            this.gvNCMItens.OptionsView.EnableAppearanceOddRow = true;
            this.gvNCMItens.OptionsView.ShowGroupPanel = false;
            // 
            // FormPisCofinsNaturezaOperacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 471);
            this.Name = "FormPisCofinsNaturezaOperacao";
            this.Text = "FormPisCofinsNaturezaOperacao";
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPIS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaCOFINS.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNaturezaOperacao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNCMItens)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCodigo;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtDescricao;
        private System.Windows.Forms.Label label9;
        private Cwork.Utilitarios.Componentes.LookupButton btnlkpTabelaCOFINS;
        private Cwork.Utilitarios.Componentes.Lookup lkpTabelaCOFINS;
        private Cwork.Utilitarios.Componentes.LookupButton btnlkpTabelaPIS;
        private Cwork.Utilitarios.Componentes.Lookup lkpTabelaPIS;
        private Cwork.Utilitarios.Componentes.DevButton btConsultarNaturezaOperacao;
        private Cwork.Utilitarios.Componentes.DevButton btExcluirNaturezaOperacao;
        private Cwork.Utilitarios.Componentes.DevButton btAlterarNaturezaOperacao;
        private Cwork.Utilitarios.Componentes.DevButton btIncluirNaturezaOperacao;
        public DevExpress.XtraGrid.GridControl gcNaturezaOperacao;
        public DevExpress.XtraGrid.Views.Grid.GridView gvNCMItens;
    }
}