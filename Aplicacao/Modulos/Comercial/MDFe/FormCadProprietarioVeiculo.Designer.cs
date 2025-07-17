namespace Aplicacao.Modulos.Cadastro
{
    partial class FormCadProprietarioVeiculo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadProprietarioVeiculo));
            this.btn_Incluir = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_nome = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_rntc = new System.Windows.Forms.TextBox();
            this.txt_inscestadual = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_cpfcnpj = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_tipopropriedade = new System.Windows.Forms.ComboBox();
            this.txt_agenciaporto = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckb_fisica = new System.Windows.Forms.RadioButton();
            this.ckb_juridica = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.lkbUF = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpUF = new Cwork.Utilitarios.Componentes.Lookup();
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
            this.sbAjuda.Location = new System.Drawing.Point(12, 129);
            // 
            // sbGravar
            // 
            this.sbGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(93, 129);
            this.sbGravar.Visible = false;
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(626, 129);
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
            this.tpPrincipal.Controls.Add(this.label4);
            this.tpPrincipal.Controls.Add(this.lkbUF);
            this.tpPrincipal.Controls.Add(this.lkpUF);
            this.tpPrincipal.Controls.Add(this.ckb_juridica);
            this.tpPrincipal.Controls.Add(this.ckb_fisica);
            this.tpPrincipal.Controls.Add(this.txt_agenciaporto);
            this.tpPrincipal.Controls.Add(this.label3);
            this.tpPrincipal.Controls.Add(this.cmb_tipopropriedade);
            this.tpPrincipal.Controls.Add(this.label10);
            this.tpPrincipal.Controls.Add(this.txt_cpfcnpj);
            this.tpPrincipal.Controls.Add(this.label8);
            this.tpPrincipal.Controls.Add(this.txt_inscestadual);
            this.tpPrincipal.Controls.Add(this.label6);
            this.tpPrincipal.Controls.Add(this.txt_rntc);
            this.tpPrincipal.Controls.Add(this.label5);
            this.tpPrincipal.Controls.Add(this.txt_nome);
            this.tpPrincipal.Controls.Add(this.label2);
            this.tpPrincipal.Size = new System.Drawing.Size(683, 105);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(689, 111);
            // 
            // btn_Incluir
            // 
            this.btn_Incluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Incluir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Incluir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Incluir.ImageOptions.Image")));
            this.btn_Incluir.ImageOptions.ImageIndex = 2;
            this.btn_Incluir.Location = new System.Drawing.Point(545, 129);
            this.btn_Incluir.Name = "btn_Incluir";
            this.btn_Incluir.Size = new System.Drawing.Size(75, 23);
            this.btn_Incluir.TabIndex = 29;
            this.btn_Incluir.Text = "&Gravar";
            this.btn_Incluir.Click += new System.EventHandler(this.sbGravar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "Nome:";
            // 
            // txt_nome
            // 
            this.txt_nome.Location = new System.Drawing.Point(75, 14);
            this.txt_nome.Name = "txt_nome";
            this.txt_nome.Size = new System.Drawing.Size(465, 21);
            this.txt_nome.TabIndex = 105;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 117;
            this.label5.Text = "RNTC:";
            // 
            // txt_rntc
            // 
            this.txt_rntc.Location = new System.Drawing.Point(75, 41);
            this.txt_rntc.Name = "txt_rntc";
            this.txt_rntc.Size = new System.Drawing.Size(131, 21);
            this.txt_rntc.TabIndex = 118;
            // 
            // txt_inscestadual
            // 
            this.txt_inscestadual.Location = new System.Drawing.Point(297, 41);
            this.txt_inscestadual.Name = "txt_inscestadual";
            this.txt_inscestadual.Size = new System.Drawing.Size(173, 21);
            this.txt_inscestadual.TabIndex = 120;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(212, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 13);
            this.label6.TabIndex = 119;
            this.label6.Text = "Insc. Estadual:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 123;
            this.label8.Text = "CPF/CNPJ:";
            // 
            // txt_cpfcnpj
            // 
            this.txt_cpfcnpj.Location = new System.Drawing.Point(75, 67);
            this.txt_cpfcnpj.Name = "txt_cpfcnpj";
            this.txt_cpfcnpj.Size = new System.Drawing.Size(131, 21);
            this.txt_cpfcnpj.TabIndex = 124;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(396, 70);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(92, 13);
            this.label10.TabIndex = 126;
            this.label10.Text = "Tipo Propriedade:";
            // 
            // cmb_tipopropriedade
            // 
            this.cmb_tipopropriedade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_tipopropriedade.DropDownWidth = 133;
            this.cmb_tipopropriedade.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmb_tipopropriedade.FormattingEnabled = true;
            this.cmb_tipopropriedade.ItemHeight = 13;
            this.cmb_tipopropriedade.Location = new System.Drawing.Point(494, 67);
            this.cmb_tipopropriedade.Name = "cmb_tipopropriedade";
            this.cmb_tipopropriedade.Size = new System.Drawing.Size(173, 21);
            this.cmb_tipopropriedade.TabIndex = 129;
            // 
            // txt_agenciaporto
            // 
            this.txt_agenciaporto.Location = new System.Drawing.Point(297, 67);
            this.txt_agenciaporto.Name = "txt_agenciaporto";
            this.txt_agenciaporto.Size = new System.Drawing.Size(93, 21);
            this.txt_agenciaporto.TabIndex = 131;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(212, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 130;
            this.label3.Text = "Agência Porto:";
            // 
            // ckb_fisica
            // 
            this.ckb_fisica.AutoSize = true;
            this.ckb_fisica.Location = new System.Drawing.Point(549, 15);
            this.ckb_fisica.Name = "ckb_fisica";
            this.ckb_fisica.Size = new System.Drawing.Size(51, 17);
            this.ckb_fisica.TabIndex = 136;
            this.ckb_fisica.TabStop = true;
            this.ckb_fisica.Text = "Física";
            this.ckb_fisica.UseVisualStyleBackColor = true;
            // 
            // ckb_juridica
            // 
            this.ckb_juridica.AutoSize = true;
            this.ckb_juridica.Location = new System.Drawing.Point(606, 15);
            this.ckb_juridica.Name = "ckb_juridica";
            this.ckb_juridica.Size = new System.Drawing.Size(61, 17);
            this.ckb_juridica.TabIndex = 137;
            this.ckb_juridica.TabStop = true;
            this.ckb_juridica.Text = "Juridica";
            this.ckb_juridica.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(476, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 140;
            this.label4.Text = "UF:";
            // 
            // lkbUF
            // 
            this.lkbUF.Location = new System.Drawing.Point(643, 41);
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
            this.lkpUF.Location = new System.Drawing.Point(506, 41);
            this.lkpUF.Name = "lkpUF";
            this.lkpUF.OnIDChanged = null;
            this.lkpUF.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpUF.Properties.Appearance.Options.UseBackColor = true;
            this.lkpUF.SelecionarTextoOnEnter = true;
            this.lkpUF.Size = new System.Drawing.Size(131, 20);
            this.lkpUF.Tabela = "TipoNota";
            this.lkpUF.TabIndex = 138;
            this.lkpUF.TituloTelaPesquisa = "Pesquisa - Tipo de Movimentação";
            this.lkpUF.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpUF.Where = null;
            // 
            // FormCadProprietarioVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 164);
            this.Controls.Add(this.btn_Incluir);
            this.Name = "FormCadProprietarioVeiculo";
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_rntc;
        private System.Windows.Forms.TextBox txt_cpfcnpj;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_inscestadual;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_tipopropriedade;
        private System.Windows.Forms.TextBox txt_agenciaporto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton ckb_juridica;
        private System.Windows.Forms.RadioButton ckb_fisica;
        private System.Windows.Forms.Label label4;
        private Cwork.Utilitarios.Componentes.LookupButton lkbUF;
        private Cwork.Utilitarios.Componentes.Lookup lkpUF;
    }
}