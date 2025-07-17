
namespace Aplicacao.Modulos.Financeiro
{
    partial class FormRelatorioTabelaPrecosGeral
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lkbTabelaPreco = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpTabelaPreco = new Cwork.Utilitarios.Componentes.Lookup();
            this.ckbTodasTabelasPreco = new System.Windows.Forms.CheckBox();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtProdutoFinal = new System.Windows.Forms.TextBox();
            this.ckbTodosProdutos = new System.Windows.Forms.CheckBox();
            this.txtProdutoInicial = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbSituacao = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEtqBloco = new DevExpress.XtraEditors.SimpleButton();
            this.chbSalvarFiltro = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPreco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSituacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbSalvarFiltro.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lkbTabelaPreco);
            this.groupBox1.Controls.Add(this.lkpTabelaPreco);
            this.groupBox1.Controls.Add(this.ckbTodasTabelasPreco);
            this.groupBox1.Controls.Add(this.comboBoxEdit1);
            this.groupBox1.Controls.Add(this.labelControl1);
            this.groupBox1.Controls.Add(this.txtProdutoFinal);
            this.groupBox1.Controls.Add(this.ckbTodosProdutos);
            this.groupBox1.Controls.Add(this.txtProdutoInicial);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbSituacao);
            this.groupBox1.Controls.Add(this.labelControl21);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 131);
            this.groupBox1.TabIndex = 44;
            this.groupBox1.TabStop = false;
            // 
            // lkbTabelaPreco
            // 
            this.lkbTabelaPreco.Location = new System.Drawing.Point(588, 19);
            this.lkbTabelaPreco.Lookup = null;
            this.lkbTabelaPreco.Name = "lkbTabelaPreco";
            this.lkbTabelaPreco.Size = new System.Drawing.Size(24, 20);
            this.lkbTabelaPreco.SubForm = null;
            this.lkbTabelaPreco.SubFormType = null;
            this.lkbTabelaPreco.SubFormTypeParams = null;
            this.lkbTabelaPreco.TabIndex = 63;
            this.lkbTabelaPreco.TabStop = false;
            this.lkbTabelaPreco.Text = "...";
            this.lkbTabelaPreco.Click += new System.EventHandler(this.lkbTabelaPreco_Click);
            // 
            // lkpTabelaPreco
            // 
            this.lkpTabelaPreco.ButtonLookup = this.lkbTabelaPreco;
            this.lkpTabelaPreco.CamposPesquisa = new string[] {
        "Nome",
        "Codigo"};
            this.lkpTabelaPreco.CamposRestricoesAND = null;
            this.lkpTabelaPreco.CamposRestricoesNOT = null;
            this.lkpTabelaPreco.CamposRestricoesOR = null;
            this.lkpTabelaPreco.ColunaDescricao = new string[] {
        "Nome",
        "Código"};
            this.lkpTabelaPreco.ColunaTamanho = new string[] {
        "150",
        "50"};
            this.lkpTabelaPreco.ContextoLinq = null;
            this.lkpTabelaPreco.CwkFuncaoValidacao = null;
            this.lkpTabelaPreco.CwkMascara = null;
            this.lkpTabelaPreco.CwkValidacao = null;
            this.lkpTabelaPreco.Exemplo = null;
            this.lkpTabelaPreco.ID = 0;
            this.lkpTabelaPreco.Join = null;
            this.lkpTabelaPreco.Key = System.Windows.Forms.Keys.F5;
            this.lkpTabelaPreco.Location = new System.Drawing.Point(94, 19);
            this.lkpTabelaPreco.Name = "lkpTabelaPreco";
            this.lkpTabelaPreco.OnIDChanged = null;
            this.lkpTabelaPreco.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpTabelaPreco.Properties.Appearance.Options.UseBackColor = true;
            this.lkpTabelaPreco.SelecionarTextoOnEnter = false;
            this.lkpTabelaPreco.Size = new System.Drawing.Size(488, 20);
            this.lkpTabelaPreco.Tabela = "TabelaPreco";
            this.lkpTabelaPreco.TabIndex = 62;
            this.lkpTabelaPreco.TituloTelaPesquisa = "Pesquisa - Tabela de Preços";
            this.lkpTabelaPreco.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpTabelaPreco.Where = null;
            // 
            // ckbTodasTabelasPreco
            // 
            this.ckbTodasTabelasPreco.AutoSize = true;
            this.ckbTodasTabelasPreco.Location = new System.Drawing.Point(618, 22);
            this.ckbTodasTabelasPreco.Name = "ckbTodasTabelasPreco";
            this.ckbTodasTabelasPreco.Size = new System.Drawing.Size(56, 17);
            this.ckbTodasTabelasPreco.TabIndex = 61;
            this.ckbTodasTabelasPreco.Text = "Todas";
            this.ckbTodasTabelasPreco.UseVisualStyleBackColor = true;
            this.ckbTodasTabelasPreco.CheckedChanged += new System.EventHandler(this.ckbTodasTabelasPreco_CheckedChanged);
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(94, 97);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Properties.ImmediatePopup = true;
            this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "Código",
            "Descrição"});
            this.comboBoxEdit1.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEdit1.Size = new System.Drawing.Size(100, 20);
            this.comboBoxEdit1.TabIndex = 60;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(25, 100);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(63, 13);
            this.labelControl1.TabIndex = 59;
            this.labelControl1.Text = "Ordenar por:";
            // 
            // txtProdutoFinal
            // 
            this.txtProdutoFinal.Location = new System.Drawing.Point(278, 45);
            this.txtProdutoFinal.Name = "txtProdutoFinal";
            this.txtProdutoFinal.Size = new System.Drawing.Size(100, 20);
            this.txtProdutoFinal.TabIndex = 50;
            // 
            // ckbTodosProdutos
            // 
            this.ckbTodosProdutos.AutoSize = true;
            this.ckbTodosProdutos.Location = new System.Drawing.Point(384, 47);
            this.ckbTodosProdutos.Name = "ckbTodosProdutos";
            this.ckbTodosProdutos.Size = new System.Drawing.Size(101, 17);
            this.ckbTodosProdutos.TabIndex = 46;
            this.ckbTodosProdutos.Text = "Todos Produtos";
            this.ckbTodosProdutos.UseVisualStyleBackColor = true;
            // 
            // txtProdutoInicial
            // 
            this.txtProdutoInicial.Location = new System.Drawing.Point(94, 45);
            this.txtProdutoInicial.Name = "txtProdutoInicial";
            this.txtProdutoInicial.Size = new System.Drawing.Size(100, 20);
            this.txtProdutoInicial.TabIndex = 49;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(200, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 48;
            this.label5.Text = "Produto Final:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Produto Inicial:";
            // 
            // cbSituacao
            // 
            this.cbSituacao.Location = new System.Drawing.Point(94, 71);
            this.cbSituacao.Name = "cbSituacao";
            this.cbSituacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbSituacao.Properties.ImmediatePopup = true;
            this.cbSituacao.Properties.Items.AddRange(new object[] {
            "Todos",
            "Ativos",
            "Inativos"});
            this.cbSituacao.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbSituacao.Size = new System.Drawing.Size(100, 20);
            this.cbSituacao.TabIndex = 42;
            // 
            // labelControl21
            // 
            this.labelControl21.Location = new System.Drawing.Point(43, 74);
            this.labelControl21.Name = "labelControl21";
            this.labelControl21.Size = new System.Drawing.Size(45, 13);
            this.labelControl21.TabIndex = 41;
            this.labelControl21.Text = "Situação:";
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(7, 22);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(81, 13);
            this.labelControl9.TabIndex = 44;
            this.labelControl9.Text = "Tabela de Preço:";
            // 
            // sbFechar
            // 
            this.sbFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbFechar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Fechar;
            this.sbFechar.ImageOptions.ImageIndex = 2;
            this.sbFechar.Location = new System.Drawing.Point(630, 158);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(75, 23);
            this.sbFechar.TabIndex = 43;
            this.sbFechar.Text = "&Fechar";
            this.sbFechar.Click += new System.EventHandler(this.sbFechar_Click);
            // 
            // buttonEtqBloco
            // 
            this.buttonEtqBloco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEtqBloco.ImageOptions.Image = global::Aplicacao.Properties.Resources.Imprimir_Cartão_Ponto_copy;
            this.buttonEtqBloco.ImageOptions.ImageIndex = 1;
            this.buttonEtqBloco.Location = new System.Drawing.Point(549, 158);
            this.buttonEtqBloco.Name = "buttonEtqBloco";
            this.buttonEtqBloco.Size = new System.Drawing.Size(75, 23);
            this.buttonEtqBloco.TabIndex = 42;
            this.buttonEtqBloco.Text = "Imprimir";
            this.buttonEtqBloco.Click += new System.EventHandler(this.buttonEtqBloco_Click);
            // 
            // chbSalvarFiltro
            // 
            this.chbSalvarFiltro.Location = new System.Drawing.Point(18, 151);
            this.chbSalvarFiltro.Name = "chbSalvarFiltro";
            this.chbSalvarFiltro.Properties.Caption = "Salvar Filtro";
            this.chbSalvarFiltro.Size = new System.Drawing.Size(85, 19);
            this.chbSalvarFiltro.TabIndex = 41;
            // 
            // FormRelatorioTabelaPrecosGeral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 193);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sbFechar);
            this.Controls.Add(this.buttonEtqBloco);
            this.Controls.Add(this.chbSalvarFiltro);
            this.Name = "FormRelatorioTabelaPrecosGeral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de Tabela Preços Geral";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPreco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbSituacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbSalvarFiltro.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProdutoFinal;
        private System.Windows.Forms.CheckBox ckbTodosProdutos;
        private System.Windows.Forms.TextBox txtProdutoInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.ComboBoxEdit cbSituacao;
        private DevExpress.XtraEditors.LabelControl labelControl21;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        public DevExpress.XtraEditors.SimpleButton sbFechar;
        private DevExpress.XtraEditors.SimpleButton buttonEtqBloco;
        private DevExpress.XtraEditors.CheckEdit chbSalvarFiltro;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.CheckBox ckbTodasTabelasPreco;
        private Cwork.Utilitarios.Componentes.LookupButton lkbTabelaPreco;
        private Cwork.Utilitarios.Componentes.Lookup lkpTabelaPreco;
    }
}