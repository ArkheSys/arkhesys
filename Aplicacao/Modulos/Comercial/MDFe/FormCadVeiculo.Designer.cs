namespace Aplicacao.Modulos.Cadastro
{
    partial class FormCadVeiculo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCadVeiculo));
            this.btn_Incluir = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPlaca = new System.Windows.Forms.TextBox();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ckbAtivo = new System.Windows.Forms.CheckBox();
            this.lkbUF = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpUF = new Cwork.Utilitarios.Componentes.Lookup();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.txtAnoFabricacao = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAnoModelo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRenavam = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbTipoRodado = new System.Windows.Forms.ComboBox();
            this.cmbCarroceria = new System.Windows.Forms.ComboBox();
            this.txtTaraKG = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCapacidadeKG = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCapacidadeM3 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
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
            this.sbAjuda.Location = new System.Drawing.Point(12, 176);
            // 
            // sbGravar
            // 
            this.sbGravar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(93, 176);
            this.sbGravar.Visible = false;
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(626, 176);
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
            this.tpPrincipal.Controls.Add(this.txtCapacidadeM3);
            this.tpPrincipal.Controls.Add(this.label12);
            this.tpPrincipal.Controls.Add(this.txtCapacidadeKG);
            this.tpPrincipal.Controls.Add(this.label11);
            this.tpPrincipal.Controls.Add(this.txtTaraKG);
            this.tpPrincipal.Controls.Add(this.label3);
            this.tpPrincipal.Controls.Add(this.cmbCarroceria);
            this.tpPrincipal.Controls.Add(this.cmbTipoRodado);
            this.tpPrincipal.Controls.Add(this.label10);
            this.tpPrincipal.Controls.Add(this.label9);
            this.tpPrincipal.Controls.Add(this.txtRenavam);
            this.tpPrincipal.Controls.Add(this.label8);
            this.tpPrincipal.Controls.Add(this.txtAnoModelo);
            this.tpPrincipal.Controls.Add(this.label7);
            this.tpPrincipal.Controls.Add(this.txtAnoFabricacao);
            this.tpPrincipal.Controls.Add(this.label6);
            this.tpPrincipal.Controls.Add(this.txtMarca);
            this.tpPrincipal.Controls.Add(this.label5);
            this.tpPrincipal.Controls.Add(this.label4);
            this.tpPrincipal.Controls.Add(this.lkbUF);
            this.tpPrincipal.Controls.Add(this.lkpUF);
            this.tpPrincipal.Controls.Add(this.ckbAtivo);
            this.tpPrincipal.Controls.Add(this.txtModelo);
            this.tpPrincipal.Controls.Add(this.label1);
            this.tpPrincipal.Controls.Add(this.txtPlaca);
            this.tpPrincipal.Controls.Add(this.label2);
            this.tpPrincipal.Size = new System.Drawing.Size(683, 152);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(689, 158);
            // 
            // btn_Incluir
            // 
            this.btn_Incluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Incluir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Incluir.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btn_Incluir.ImageOptions.Image")));
            this.btn_Incluir.ImageOptions.ImageIndex = 2;
            this.btn_Incluir.Location = new System.Drawing.Point(545, 176);
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
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 104;
            this.label2.Text = "Placa:";
            // 
            // txtPlaca
            // 
            this.txtPlaca.Location = new System.Drawing.Point(75, 14);
            this.txtPlaca.Name = "txtPlaca";
            this.txtPlaca.Size = new System.Drawing.Size(80, 21);
            this.txtPlaca.TabIndex = 105;
            // 
            // txtModelo
            // 
            this.txtModelo.Location = new System.Drawing.Point(212, 14);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.Size = new System.Drawing.Size(395, 21);
            this.txtModelo.TabIndex = 107;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(161, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 106;
            this.label1.Text = "Modelo:";
            // 
            // ckbAtivo
            // 
            this.ckbAtivo.AutoSize = true;
            this.ckbAtivo.Location = new System.Drawing.Point(613, 17);
            this.ckbAtivo.Name = "ckbAtivo";
            this.ckbAtivo.Size = new System.Drawing.Size(51, 17);
            this.ckbAtivo.TabIndex = 108;
            this.ckbAtivo.Text = "Ativo";
            this.ckbAtivo.UseVisualStyleBackColor = true;
            // 
            // lkbUF
            // 
            this.lkbUF.Location = new System.Drawing.Point(212, 41);
            this.lkbUF.Lookup = null;
            this.lkbUF.Name = "lkbUF";
            this.lkbUF.Size = new System.Drawing.Size(24, 20);
            this.lkbUF.SubForm = null;
            this.lkbUF.SubFormType = null;
            this.lkbUF.SubFormTypeParams = null;
            this.lkbUF.TabIndex = 115;
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
            this.lkpUF.Location = new System.Drawing.Point(75, 41);
            this.lkpUF.Name = "lkpUF";
            this.lkpUF.OnIDChanged = null;
            this.lkpUF.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpUF.Properties.Appearance.Options.UseBackColor = true;
            this.lkpUF.SelecionarTextoOnEnter = true;
            this.lkpUF.Size = new System.Drawing.Size(131, 20);
            this.lkpUF.Tabela = "TipoNota";
            this.lkpUF.TabIndex = 114;
            this.lkpUF.TituloTelaPesquisa = "Pesquisa - Tipo de Movimentação";
            this.lkpUF.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpUF.Where = null;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 116;
            this.label4.Text = "UF:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(242, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 117;
            this.label5.Text = "Marca:";
            // 
            // txtMarca
            // 
            this.txtMarca.Location = new System.Drawing.Point(288, 40);
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(376, 21);
            this.txtMarca.TabIndex = 118;
            // 
            // txtAnoFabricacao
            // 
            this.txtAnoFabricacao.Location = new System.Drawing.Point(288, 67);
            this.txtAnoFabricacao.Name = "txtAnoFabricacao";
            this.txtAnoFabricacao.Size = new System.Drawing.Size(96, 21);
            this.txtAnoFabricacao.TabIndex = 120;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(220, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 119;
            this.label6.Text = "Ano Fabric:";
            // 
            // txtAnoModelo
            // 
            this.txtAnoModelo.Location = new System.Drawing.Point(463, 67);
            this.txtAnoModelo.Name = "txtAnoModelo";
            this.txtAnoModelo.Size = new System.Drawing.Size(96, 21);
            this.txtAnoModelo.TabIndex = 122;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(390, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 121;
            this.label7.Text = "Ano Modelo:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 13);
            this.label8.TabIndex = 123;
            this.label8.Text = "Renavam:";
            // 
            // txtRenavam
            // 
            this.txtRenavam.Location = new System.Drawing.Point(75, 67);
            this.txtRenavam.Name = "txtRenavam";
            this.txtRenavam.Size = new System.Drawing.Size(131, 21);
            this.txtRenavam.TabIndex = 124;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 125;
            this.label9.Text = "Rodado:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(221, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 13);
            this.label10.TabIndex = 126;
            this.label10.Text = "Carroceria:";
            // 
            // cmbTipoRodado
            // 
            this.cmbTipoRodado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoRodado.DropDownWidth = 133;
            this.cmbTipoRodado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbTipoRodado.FormattingEnabled = true;
            this.cmbTipoRodado.ItemHeight = 13;
            this.cmbTipoRodado.Location = new System.Drawing.Point(75, 94);
            this.cmbTipoRodado.Name = "cmbTipoRodado";
            this.cmbTipoRodado.Size = new System.Drawing.Size(135, 21);
            this.cmbTipoRodado.TabIndex = 127;
            // 
            // cmbCarroceria
            // 
            this.cmbCarroceria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCarroceria.DropDownWidth = 133;
            this.cmbCarroceria.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbCarroceria.FormattingEnabled = true;
            this.cmbCarroceria.ItemHeight = 13;
            this.cmbCarroceria.Location = new System.Drawing.Point(288, 94);
            this.cmbCarroceria.Name = "cmbCarroceria";
            this.cmbCarroceria.Size = new System.Drawing.Size(135, 21);
            this.cmbCarroceria.TabIndex = 129;
            // 
            // txtTaraKG
            // 
            this.txtTaraKG.Location = new System.Drawing.Point(75, 121);
            this.txtTaraKG.Name = "txtTaraKG";
            this.txtTaraKG.Size = new System.Drawing.Size(80, 21);
            this.txtTaraKG.TabIndex = 131;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 130;
            this.label3.Text = "Tara/KG:";
            // 
            // txtCapacidadeKG
            // 
            this.txtCapacidadeKG.Location = new System.Drawing.Point(251, 121);
            this.txtCapacidadeKG.Name = "txtCapacidadeKG";
            this.txtCapacidadeKG.Size = new System.Drawing.Size(80, 21);
            this.txtCapacidadeKG.TabIndex = 133;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(161, 124);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(84, 13);
            this.label11.TabIndex = 132;
            this.label11.Text = "Capacidade/KG:";
            // 
            // txtCapacidadeM3
            // 
            this.txtCapacidadeM3.Location = new System.Drawing.Point(427, 121);
            this.txtCapacidadeM3.Name = "txtCapacidadeM3";
            this.txtCapacidadeM3.Size = new System.Drawing.Size(80, 21);
            this.txtCapacidadeM3.TabIndex = 135;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 124);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 134;
            this.label12.Text = "Capacidade/M3:";
            // 
            // FormCadVeiculo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 211);
            this.Controls.Add(this.btn_Incluir);
            this.Name = "FormCadVeiculo";
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
        private System.Windows.Forms.CheckBox ckbAtivo;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlaca;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private Cwork.Utilitarios.Componentes.LookupButton lkbUF;
        private Cwork.Utilitarios.Componentes.Lookup lkpUF;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMarca;
        private System.Windows.Forms.TextBox txtRenavam;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAnoModelo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAnoFabricacao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCarroceria;
        private System.Windows.Forms.ComboBox cmbTipoRodado;
        private System.Windows.Forms.TextBox txtTaraKG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCapacidadeM3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCapacidadeKG;
        private System.Windows.Forms.Label label11;
    }
}