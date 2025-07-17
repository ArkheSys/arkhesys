namespace Aplicacao.Modulos.Cadastro
{
    partial class FormCadCondutor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadCondutor));
            this.btn_Incluir = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_nome = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_cpfcnpj = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lkbUF = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpUF = new Cwork.Utilitarios.Componentes.Lookup();
            this.ckbAtivo = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUF.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 94);
            // 
            // sbGravar
            // 
            this.sbGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(93, 94);
            this.sbGravar.Visible = false;
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(626, 94);
            this.sbCancelar.Click += new System.EventHandler(this.sbCancelar_Click);
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
            this.tpPrincipal.Controls.Add(this.ckbAtivo);
            this.tpPrincipal.Controls.Add(this.label4);
            this.tpPrincipal.Controls.Add(this.lkbUF);
            this.tpPrincipal.Controls.Add(this.lkpUF);
            this.tpPrincipal.Controls.Add(this.txt_cpfcnpj);
            this.tpPrincipal.Controls.Add(this.label8);
            this.tpPrincipal.Controls.Add(this.txt_nome);
            this.tpPrincipal.Controls.Add(this.label2);
            this.tpPrincipal.Size = new System.Drawing.Size(683, 70);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(689, 76);
            // 
            // btn_Incluir
            // 
            this.btn_Incluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Incluir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Incluir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Incluir.ImageOptions.Image")));
            this.btn_Incluir.ImageOptions.ImageIndex = 2;
            this.btn_Incluir.Location = new System.Drawing.Point(545, 94);
            this.btn_Incluir.Name = "btn_Incluir";
            this.btn_Incluir.Size = new System.Drawing.Size(75, 23);
            this.btn_Incluir.TabIndex = 29;
            this.btn_Incluir.Text = "&Gravar";
            this.btn_Incluir.Click += new System.EventHandler(this.sbGravar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "Nome:";
            // 
            // txt_nome
            // 
            this.txt_nome.Location = new System.Drawing.Point(57, 36);
            this.txt_nome.Name = "txt_nome";
            this.txt_nome.Size = new System.Drawing.Size(550, 21);
            this.txt_nome.TabIndex = 105;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 123;
            this.label8.Text = "CPF:";
            // 
            // txt_cpfcnpj
            // 
            this.txt_cpfcnpj.Location = new System.Drawing.Point(57, 9);
            this.txt_cpfcnpj.Name = "txt_cpfcnpj";
            this.txt_cpfcnpj.Size = new System.Drawing.Size(149, 21);
            this.txt_cpfcnpj.TabIndex = 124;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(212, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 140;
            this.label4.Text = "UF:";
            // 
            // lkbUF
            // 
            this.lkbUF.Location = new System.Drawing.Point(516, 9);
            this.lkbUF.Lookup = null;
            this.lkbUF.Name = "lkbUF";
            this.lkbUF.Size = new System.Drawing.Size(24, 20);
            this.lkbUF.SubForm = null;
            this.lkbUF.SubFormType = null;
            this.lkbUF.SubFormTypeParams = null;
            this.lkbUF.TabIndex = 139;
            this.lkbUF.TabStop = false;
            this.lkbUF.Text = "...";
            this.lkbUF.Click += new System.EventHandler(this.lkbUF_Click);
            // 
            // lkpUF
            // 
            this.lkpUF.ButtonLookup = this.lkbUF;
            this.lkpUF.CamposPesquisa = new string[] {
        "Nome",
        "Codigo"};
            this.lkpUF.CamposRestricoesAND = null;
            this.lkpUF.CamposRestricoesNOT = null;
            this.lkpUF.CamposRestricoesOR = null;
            this.lkpUF.ColunaDescricao = new string[] {
        "Nome",
        "Código"};
            this.lkpUF.ColunaTamanho = new string[] {
        "150",
        "50"};
            this.lkpUF.ContextoLinq = null;
            this.lkpUF.CwkFuncaoValidacao = null;
            this.lkpUF.CwkMascara = null;
            this.lkpUF.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpUF.Exemplo = null;
            this.lkpUF.ID = 0;
            this.lkpUF.Join = null;
            this.lkpUF.Key = System.Windows.Forms.Keys.F5;
            this.lkpUF.Location = new System.Drawing.Point(242, 9);
            this.lkpUF.Name = "lkpUF";
            this.lkpUF.OnIDChanged = null;
            this.lkpUF.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpUF.Properties.Appearance.Options.UseBackColor = true;
            this.lkpUF.SelecionarTextoOnEnter = true;
            this.lkpUF.Size = new System.Drawing.Size(268, 20);
            this.lkpUF.Tabela = "TipoNota";
            this.lkpUF.TabIndex = 138;
            this.lkpUF.TituloTelaPesquisa = "Pesquisa - Tipo de Movimentação";
            this.lkpUF.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpUF.Where = null;
            // 
            // ckbAtivo
            // 
            this.ckbAtivo.AutoSize = true;
            this.ckbAtivo.Location = new System.Drawing.Point(613, 38);
            this.ckbAtivo.Name = "ckbAtivo";
            this.ckbAtivo.Size = new System.Drawing.Size(51, 17);
            this.ckbAtivo.TabIndex = 141;
            this.ckbAtivo.Text = "Ativo";
            this.ckbAtivo.UseVisualStyleBackColor = true;
            // 
            // FormCadCondutor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 129);
            this.Controls.Add(this.btn_Incluir);
            this.Name = "FormCadCondutor";
            this.Text = "Cadastro de Veiculo";
            this.Shown += new System.EventHandler(this.FormCadVeiculo_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormCadVeiculo_KeyDown);
            this.Controls.SetChildIndex(this.sbCancelar, 0);
            this.Controls.SetChildIndex(this.sbGravar, 0);
            this.Controls.SetChildIndex(this.sbAjuda, 0);
            this.Controls.SetChildIndex(this.tcPrincipal, 0);
            this.Controls.SetChildIndex(this.btn_Incluir, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lkpUF.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        public DevExpress.XtraEditors.SimpleButton btn_Incluir;
        private System.Windows.Forms.TextBox txt_nome;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_cpfcnpj;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private Cwork.Utilitarios.Componentes.LookupButton lkbUF;
        private Cwork.Utilitarios.Componentes.Lookup lkpUF;
        private System.Windows.Forms.CheckBox ckbAtivo;
    }
}