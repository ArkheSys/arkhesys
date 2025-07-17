
namespace Aplicacao.Modulos.Financeiro
{
    partial class FormRelatorioTabelaPrecosPorVenda
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
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            this.buttonEtqBloco = new DevExpress.XtraEditors.SimpleButton();
            this.chbSalvarFiltro = new DevExpress.XtraEditors.CheckEdit();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtOperadorFinal = new System.Windows.Forms.TextBox();
            this.ckb_TodosOperadores = new System.Windows.Forms.CheckBox();
            this.txtOperadorInicial = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lkbTabelaPreco = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpTabelaPreco = new Cwork.Utilitarios.Componentes.Lookup();
            this.ckbTodasTabelasPreco = new System.Windows.Forms.CheckBox();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDtFinal = new DevExpress.XtraEditors.DateEdit();
            this.txtDtInicial = new DevExpress.XtraEditors.DateEdit();
            this.txtProdutoFinal = new System.Windows.Forms.TextBox();
            this.ckbTodosProdutos = new System.Windows.Forms.CheckBox();
            this.txtProdutoInicial = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lkbProdutoInicial = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkbOperadorInicial = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkbOperadorFinal = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkbProdutoFinal = new Cwork.Utilitarios.Componentes.LookupButton();
            ((System.ComponentModel.ISupportInitialize)(this.chbSalvarFiltro.Properties)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPreco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtInicial.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbFechar
            // 
            this.sbFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbFechar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Fechar;
            this.sbFechar.ImageOptions.ImageIndex = 2;
            this.sbFechar.Location = new System.Drawing.Point(624, 145);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(75, 23);
            this.sbFechar.TabIndex = 39;
            this.sbFechar.Text = "&Fechar";
            this.sbFechar.Click += new System.EventHandler(this.sbFechar_Click);
            // 
            // buttonEtqBloco
            // 
            this.buttonEtqBloco.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEtqBloco.ImageOptions.Image = global::Aplicacao.Properties.Resources.Imprimir_Cartão_Ponto_copy;
            this.buttonEtqBloco.ImageOptions.ImageIndex = 1;
            this.buttonEtqBloco.Location = new System.Drawing.Point(543, 145);
            this.buttonEtqBloco.Name = "buttonEtqBloco";
            this.buttonEtqBloco.Size = new System.Drawing.Size(75, 23);
            this.buttonEtqBloco.TabIndex = 38;
            this.buttonEtqBloco.Text = "Imprimir";
            this.buttonEtqBloco.Click += new System.EventHandler(this.buttonEtqBloco_Click);
            // 
            // chbSalvarFiltro
            // 
            this.chbSalvarFiltro.Location = new System.Drawing.Point(12, 149);
            this.chbSalvarFiltro.Name = "chbSalvarFiltro";
            this.chbSalvarFiltro.Properties.Caption = "Salvar Filtro";
            this.chbSalvarFiltro.Size = new System.Drawing.Size(85, 19);
            this.chbSalvarFiltro.TabIndex = 37;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lkbOperadorFinal);
            this.groupBox1.Controls.Add(this.lkbProdutoFinal);
            this.groupBox1.Controls.Add(this.lkbOperadorInicial);
            this.groupBox1.Controls.Add(this.lkbProdutoInicial);
            this.groupBox1.Controls.Add(this.txtOperadorFinal);
            this.groupBox1.Controls.Add(this.ckb_TodosOperadores);
            this.groupBox1.Controls.Add(this.txtOperadorInicial);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lkbTabelaPreco);
            this.groupBox1.Controls.Add(this.lkpTabelaPreco);
            this.groupBox1.Controls.Add(this.ckbTodasTabelasPreco);
            this.groupBox1.Controls.Add(this.labelControl9);
            this.groupBox1.Controls.Add(this.labelControl3);
            this.groupBox1.Controls.Add(this.labelControl2);
            this.groupBox1.Controls.Add(this.txtDtFinal);
            this.groupBox1.Controls.Add(this.txtDtInicial);
            this.groupBox1.Controls.Add(this.txtProdutoFinal);
            this.groupBox1.Controls.Add(this.ckbTodosProdutos);
            this.groupBox1.Controls.Add(this.txtProdutoInicial);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(687, 124);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // txtOperadorFinal
            // 
            this.txtOperadorFinal.Location = new System.Drawing.Point(302, 71);
            this.txtOperadorFinal.Name = "txtOperadorFinal";
            this.txtOperadorFinal.Size = new System.Drawing.Size(100, 20);
            this.txtOperadorFinal.TabIndex = 72;
            // 
            // ckb_TodosOperadores
            // 
            this.ckb_TodosOperadores.AutoSize = true;
            this.ckb_TodosOperadores.Location = new System.Drawing.Point(443, 73);
            this.ckb_TodosOperadores.Name = "ckb_TodosOperadores";
            this.ckb_TodosOperadores.Size = new System.Drawing.Size(56, 17);
            this.ckb_TodosOperadores.TabIndex = 68;
            this.ckb_TodosOperadores.Text = "Todos";
            this.ckb_TodosOperadores.UseVisualStyleBackColor = true;
            this.ckb_TodosOperadores.CheckedChanged += new System.EventHandler(this.ckb_TodosOperadores_CheckedChanged);
            // 
            // txtOperadorInicial
            // 
            this.txtOperadorInicial.Location = new System.Drawing.Point(94, 71);
            this.txtOperadorInicial.Name = "txtOperadorInicial";
            this.txtOperadorInicial.Size = new System.Drawing.Size(100, 20);
            this.txtOperadorInicial.TabIndex = 71;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(224, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 70;
            this.label1.Text = "Operador Final:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 69;
            this.label2.Text = "Operador Inicial:";
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
            this.lkbTabelaPreco.TabIndex = 67;
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
            this.lkpTabelaPreco.TabIndex = 66;
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
            this.ckbTodasTabelasPreco.TabIndex = 65;
            this.ckbTodasTabelasPreco.Text = "Todas";
            this.ckbTodasTabelasPreco.UseVisualStyleBackColor = true;
            this.ckbTodasTabelasPreco.CheckedChanged += new System.EventHandler(this.ckbTodasTabelasPreco_CheckedChanged);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(7, 22);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(81, 13);
            this.labelControl9.TabIndex = 64;
            this.labelControl9.Text = "Tabela de Preço:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(244, 99);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 55;
            this.labelControl3.Text = "Data Final:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(31, 99);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(57, 13);
            this.labelControl2.TabIndex = 53;
            this.labelControl2.Text = "Data Inicial:";
            // 
            // txtDtFinal
            // 
            this.txtDtFinal.EditValue = null;
            this.txtDtFinal.Location = new System.Drawing.Point(302, 96);
            this.txtDtFinal.Name = "txtDtFinal";
            this.txtDtFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDtFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDtFinal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtDtFinal.Size = new System.Drawing.Size(100, 20);
            this.txtDtFinal.TabIndex = 56;
            // 
            // txtDtInicial
            // 
            this.txtDtInicial.EditValue = null;
            this.txtDtInicial.Location = new System.Drawing.Point(94, 96);
            this.txtDtInicial.Name = "txtDtInicial";
            this.txtDtInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDtInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDtInicial.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtDtInicial.Size = new System.Drawing.Size(100, 20);
            this.txtDtInicial.TabIndex = 54;
            // 
            // txtProdutoFinal
            // 
            this.txtProdutoFinal.Location = new System.Drawing.Point(302, 45);
            this.txtProdutoFinal.Name = "txtProdutoFinal";
            this.txtProdutoFinal.Size = new System.Drawing.Size(100, 20);
            this.txtProdutoFinal.TabIndex = 50;
            // 
            // ckbTodosProdutos
            // 
            this.ckbTodosProdutos.AutoSize = true;
            this.ckbTodosProdutos.Location = new System.Drawing.Point(443, 47);
            this.ckbTodosProdutos.Name = "ckbTodosProdutos";
            this.ckbTodosProdutos.Size = new System.Drawing.Size(101, 17);
            this.ckbTodosProdutos.TabIndex = 46;
            this.ckbTodosProdutos.Text = "Todos Produtos";
            this.ckbTodosProdutos.UseVisualStyleBackColor = true;
            this.ckbTodosProdutos.CheckedChanged += new System.EventHandler(this.ckbTodosProdutos_CheckedChanged);
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
            this.label5.Location = new System.Drawing.Point(224, 48);
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
            // lkbProdutoInicial
            // 
            this.lkbProdutoInicial.Location = new System.Drawing.Point(200, 45);
            this.lkbProdutoInicial.Lookup = null;
            this.lkbProdutoInicial.Name = "lkbProdutoInicial";
            this.lkbProdutoInicial.Size = new System.Drawing.Size(24, 20);
            this.lkbProdutoInicial.SubForm = null;
            this.lkbProdutoInicial.SubFormType = null;
            this.lkbProdutoInicial.SubFormTypeParams = null;
            this.lkbProdutoInicial.TabIndex = 73;
            this.lkbProdutoInicial.TabStop = false;
            this.lkbProdutoInicial.Text = "...";
            this.lkbProdutoInicial.Click += new System.EventHandler(this.lkbProdutoInicial_Click);
            // 
            // lkbOperadorInicial
            // 
            this.lkbOperadorInicial.Location = new System.Drawing.Point(200, 71);
            this.lkbOperadorInicial.Lookup = null;
            this.lkbOperadorInicial.Name = "lkbOperadorInicial";
            this.lkbOperadorInicial.Size = new System.Drawing.Size(24, 20);
            this.lkbOperadorInicial.SubForm = null;
            this.lkbOperadorInicial.SubFormType = null;
            this.lkbOperadorInicial.SubFormTypeParams = null;
            this.lkbOperadorInicial.TabIndex = 74;
            this.lkbOperadorInicial.TabStop = false;
            this.lkbOperadorInicial.Text = "...";
            // 
            // lkbOperadorFinal
            // 
            this.lkbOperadorFinal.Location = new System.Drawing.Point(408, 71);
            this.lkbOperadorFinal.Lookup = null;
            this.lkbOperadorFinal.Name = "lkbOperadorFinal";
            this.lkbOperadorFinal.Size = new System.Drawing.Size(24, 20);
            this.lkbOperadorFinal.SubForm = null;
            this.lkbOperadorFinal.SubFormType = null;
            this.lkbOperadorFinal.SubFormTypeParams = null;
            this.lkbOperadorFinal.TabIndex = 76;
            this.lkbOperadorFinal.TabStop = false;
            this.lkbOperadorFinal.Text = "...";
            // 
            // lkbProdutoFinal
            // 
            this.lkbProdutoFinal.Location = new System.Drawing.Point(408, 45);
            this.lkbProdutoFinal.Lookup = null;
            this.lkbProdutoFinal.Name = "lkbProdutoFinal";
            this.lkbProdutoFinal.Size = new System.Drawing.Size(24, 20);
            this.lkbProdutoFinal.SubForm = null;
            this.lkbProdutoFinal.SubFormType = null;
            this.lkbProdutoFinal.SubFormTypeParams = null;
            this.lkbProdutoFinal.TabIndex = 75;
            this.lkbProdutoFinal.TabStop = false;
            this.lkbProdutoFinal.Text = "...";
            // 
            // FormRelatorioTabelaPrecosPorVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 180);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.sbFechar);
            this.Controls.Add(this.buttonEtqBloco);
            this.Controls.Add(this.chbSalvarFiltro);
            this.Name = "FormRelatorioTabelaPrecosPorVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório de Tabela de Preços Por Venda";
            ((System.ComponentModel.ISupportInitialize)(this.chbSalvarFiltro.Properties)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPreco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtInicial.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton sbFechar;
        private DevExpress.XtraEditors.SimpleButton buttonEtqBloco;
        private DevExpress.XtraEditors.CheckEdit chbSalvarFiltro;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtProdutoFinal;
        private System.Windows.Forms.CheckBox ckbTodosProdutos;
        private System.Windows.Forms.TextBox txtProdutoInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit txtDtFinal;
        private DevExpress.XtraEditors.DateEdit txtDtInicial;
        private Cwork.Utilitarios.Componentes.LookupButton lkbTabelaPreco;
        private Cwork.Utilitarios.Componentes.Lookup lkpTabelaPreco;
        private System.Windows.Forms.CheckBox ckbTodasTabelasPreco;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private System.Windows.Forms.TextBox txtOperadorFinal;
        private System.Windows.Forms.CheckBox ckb_TodosOperadores;
        private System.Windows.Forms.TextBox txtOperadorInicial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Cwork.Utilitarios.Componentes.LookupButton lkbOperadorFinal;
        private Cwork.Utilitarios.Componentes.LookupButton lkbProdutoFinal;
        private Cwork.Utilitarios.Componentes.LookupButton lkbOperadorInicial;
        private Cwork.Utilitarios.Componentes.LookupButton lkbProdutoInicial;
    }
}