
namespace Aplicacao.Modulos.Telefonia
{
    partial class FormBoletoD8
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
            this.lkbFilial = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpFilial = new Cwork.Utilitarios.Componentes.Lookup();
            this.txtObservacao = new DevExpress.XtraEditors.MemoEdit();
            this.lblObservacao = new DevExpress.XtraEditors.LabelControl();
            this.lkpServico = new Cwork.Utilitarios.Componentes.Lookup();
            this.lkbServico = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lblServico = new DevExpress.XtraEditors.LabelControl();
            this.txtValor = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.txtDataVencimento = new Cwork.Utilitarios.Componentes.CwkDateEditor();
            this.lblValor = new DevExpress.XtraEditors.LabelControl();
            this.txtNumeroDocumento = new Cwork.Utilitarios.Componentes.DevTextEdit();
            this.txtDataCriacao = new Cwork.Utilitarios.Componentes.CwkDateEditor();
            this.lblFilial = new DevExpress.XtraEditors.LabelControl();
            this.lblEspecieDocumento = new DevExpress.XtraEditors.LabelControl();
            this.lblDataCriacao = new DevExpress.XtraEditors.LabelControl();
            this.lblNumeroDocumento = new DevExpress.XtraEditors.LabelControl();
            this.lblDataVencimento = new DevExpress.XtraEditors.LabelControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCodigo = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.labelControl45 = new DevExpress.XtraEditors.LabelControl();
            this.txtEspecieDocumento = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.lkbCliente = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpCliente = new Cwork.Utilitarios.Componentes.Lookup();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGravarFatura = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lkpFilial.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpServico.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCriacao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCriacao.Properties)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEspecieDocumento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCliente.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lkbFilial
            // 
            this.lkbFilial.Location = new System.Drawing.Point(603, 16);
            this.lkbFilial.Lookup = null;
            this.lkbFilial.Name = "lkbFilial";
            this.lkbFilial.Size = new System.Drawing.Size(24, 20);
            this.lkbFilial.SubForm = null;
            this.lkbFilial.SubFormType = null;
            this.lkbFilial.SubFormTypeParams = null;
            this.lkbFilial.TabIndex = 80;
            this.lkbFilial.TabStop = false;
            this.lkbFilial.Text = "...";
            this.lkbFilial.Click += new System.EventHandler(this.lkbFilial_Click);
            // 
            // lkpFilial
            // 
            this.lkpFilial.ButtonLookup = this.lkbFilial;
            this.lkpFilial.CamposPesquisa = new string[] {
        "Nome",
        "Codigo"};
            this.lkpFilial.CamposRestricoesAND = null;
            this.lkpFilial.CamposRestricoesNOT = null;
            this.lkpFilial.CamposRestricoesOR = null;
            this.lkpFilial.ColunaDescricao = new string[] {
        "Nome",
        "Código"};
            this.lkpFilial.ColunaTamanho = new string[] {
        "150",
        "50"};
            this.lkpFilial.ContextoLinq = null;
            this.lkpFilial.CwkFuncaoValidacao = null;
            this.lkpFilial.CwkMascara = null;
            this.lkpFilial.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpFilial.Exemplo = null;
            this.lkpFilial.ID = 0;
            this.lkpFilial.Join = null;
            this.lkpFilial.Key = System.Windows.Forms.Keys.F5;
            this.lkpFilial.Location = new System.Drawing.Point(87, 16);
            this.lkpFilial.Name = "lkpFilial";
            this.lkpFilial.OnIDChanged = null;
            this.lkpFilial.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpFilial.Properties.Appearance.Options.UseBackColor = true;
            this.lkpFilial.SelecionarTextoOnEnter = true;
            this.lkpFilial.Size = new System.Drawing.Size(510, 20);
            this.lkpFilial.Tabela = "Filial";
            this.lkpFilial.TabIndex = 0;
            this.lkpFilial.TituloTelaPesquisa = "Pesquisa - Empresa";
            this.lkpFilial.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpFilial.Where = null;
            // 
            // txtObservacao
            // 
            this.txtObservacao.Location = new System.Drawing.Point(87, 172);
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.Size = new System.Drawing.Size(540, 189);
            this.txtObservacao.TabIndex = 8;
            // 
            // lblObservacao
            // 
            this.lblObservacao.Location = new System.Drawing.Point(17, 174);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Size = new System.Drawing.Size(62, 13);
            this.lblObservacao.TabIndex = 74;
            this.lblObservacao.Text = "Observação:";
            // 
            // lkpServico
            // 
            this.lkpServico.ButtonLookup = this.lkbServico;
            this.lkpServico.CamposPesquisa = new string[] {
        "Codigo",
        "Descricao"};
            this.lkpServico.CamposRestricoesAND = null;
            this.lkpServico.CamposRestricoesNOT = null;
            this.lkpServico.CamposRestricoesOR = null;
            this.lkpServico.ColunaDescricao = new string[] {
        "Código",
        "Descrição",
        "Agencia",
        "Conta Corrente",
        "Cedente"};
            this.lkpServico.ColunaTamanho = new string[] {
        "20",
        "120",
        "60",
        "60",
        "60"};
            this.lkpServico.ContextoLinq = null;
            this.lkpServico.CwkFuncaoValidacao = null;
            this.lkpServico.CwkMascara = null;
            this.lkpServico.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpServico.Exemplo = null;
            this.lkpServico.ID = 0;
            this.lkpServico.Join = null;
            this.lkpServico.Key = System.Windows.Forms.Keys.F5;
            this.lkpServico.Location = new System.Drawing.Point(87, 146);
            this.lkpServico.Name = "lkpServico";
            this.lkpServico.OnIDChanged = null;
            this.lkpServico.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpServico.Properties.Appearance.Options.UseBackColor = true;
            this.lkpServico.SelecionarTextoOnEnter = false;
            this.lkpServico.Size = new System.Drawing.Size(510, 20);
            this.lkpServico.Tabela = "Tel_Servico";
            this.lkpServico.TabIndex = 7;
            this.lkpServico.TituloTelaPesquisa = null;
            this.lkpServico.ToolTip = "Código, Descrição";
            this.lkpServico.Where = null;
            // 
            // lkbServico
            // 
            this.lkbServico.Location = new System.Drawing.Point(603, 145);
            this.lkbServico.Lookup = null;
            this.lkbServico.Name = "lkbServico";
            this.lkbServico.Size = new System.Drawing.Size(24, 20);
            this.lkbServico.SubForm = null;
            this.lkbServico.SubFormType = null;
            this.lkbServico.SubFormTypeParams = null;
            this.lkbServico.TabIndex = 77;
            this.lkbServico.TabStop = false;
            this.lkbServico.Text = "...";
            this.lkbServico.Click += new System.EventHandler(this.lkbServico_Click);
            // 
            // lblServico
            // 
            this.lblServico.Location = new System.Drawing.Point(40, 145);
            this.lblServico.Name = "lblServico";
            this.lblServico.Size = new System.Drawing.Size(39, 13);
            this.lblServico.TabIndex = 75;
            this.lblServico.Text = "Serviço:";
            // 
            // txtValor
            // 
            this.txtValor.CwkFuncaoValidacao = null;
            this.txtValor.CwkMascara = Cwork.Utilitarios.Componentes.Mascaras.Mascara.MONETARIO;
            this.txtValor.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtValor.Location = new System.Drawing.Point(85, 94);
            this.txtValor.Name = "txtValor";
            this.txtValor.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtValor.Properties.Appearance.Options.UseBackColor = true;
            this.txtValor.Properties.Appearance.Options.UseTextOptions = true;
            this.txtValor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtValor.Properties.Mask.EditMask = "c2";
            this.txtValor.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtValor.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtValor.SelecionarTextoOnEnter = false;
            this.txtValor.Size = new System.Drawing.Size(100, 20);
            this.txtValor.TabIndex = 5;
            // 
            // txtDataVencimento
            // 
            this.txtDataVencimento.CwkFuncaoValidacao = null;
            this.txtDataVencimento.CwkValidacao = null;
            this.txtDataVencimento.EditValue = null;
            this.txtDataVencimento.Location = new System.Drawing.Point(527, 42);
            this.txtDataVencimento.Name = "txtDataVencimento";
            this.txtDataVencimento.Obrigatorio = true;
            this.txtDataVencimento.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataVencimento.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataVencimento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDataVencimento.Size = new System.Drawing.Size(100, 20);
            this.txtDataVencimento.TabIndex = 2;
            // 
            // lblValor
            // 
            this.lblValor.Location = new System.Drawing.Point(51, 97);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(28, 13);
            this.lblValor.TabIndex = 69;
            this.lblValor.Text = "Valor:";
            // 
            // txtNumeroDocumento
            // 
            this.txtNumeroDocumento.CwkFuncaoValidacao = null;
            this.txtNumeroDocumento.CwkMascara = null;
            this.txtNumeroDocumento.CwkValidacao = null;
            this.txtNumeroDocumento.Location = new System.Drawing.Point(87, 68);
            this.txtNumeroDocumento.Name = "txtNumeroDocumento";
            this.txtNumeroDocumento.Properties.MaxLength = 10;
            this.txtNumeroDocumento.SelecionarTextoOnEnter = false;
            this.txtNumeroDocumento.Size = new System.Drawing.Size(280, 20);
            this.txtNumeroDocumento.TabIndex = 3;
            // 
            // txtDataCriacao
            // 
            this.txtDataCriacao.CwkFuncaoValidacao = null;
            this.txtDataCriacao.CwkValidacao = null;
            this.txtDataCriacao.EditValue = null;
            this.txtDataCriacao.Location = new System.Drawing.Point(87, 42);
            this.txtDataCriacao.Name = "txtDataCriacao";
            this.txtDataCriacao.Obrigatorio = true;
            this.txtDataCriacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataCriacao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDataCriacao.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtDataCriacao.Size = new System.Drawing.Size(100, 20);
            this.txtDataCriacao.TabIndex = 1;
            // 
            // lblFilial
            // 
            this.lblFilial.Location = new System.Drawing.Point(34, 19);
            this.lblFilial.Name = "lblFilial";
            this.lblFilial.Size = new System.Drawing.Size(45, 13);
            this.lblFilial.TabIndex = 60;
            this.lblFilial.Text = "Empresa:";
            // 
            // lblEspecieDocumento
            // 
            this.lblEspecieDocumento.Location = new System.Drawing.Point(424, 71);
            this.lblEspecieDocumento.Name = "lblEspecieDocumento";
            this.lblEspecieDocumento.Size = new System.Drawing.Size(97, 13);
            this.lblEspecieDocumento.TabIndex = 64;
            this.lblEspecieDocumento.Text = "Espécie Documento:";
            // 
            // lblDataCriacao
            // 
            this.lblDataCriacao.Location = new System.Drawing.Point(13, 45);
            this.lblDataCriacao.Name = "lblDataCriacao";
            this.lblDataCriacao.Size = new System.Drawing.Size(66, 13);
            this.lblDataCriacao.TabIndex = 61;
            this.lblDataCriacao.Text = "Data Criação:";
            // 
            // lblNumeroDocumento
            // 
            this.lblNumeroDocumento.Location = new System.Drawing.Point(7, 71);
            this.lblNumeroDocumento.Name = "lblNumeroDocumento";
            this.lblNumeroDocumento.Size = new System.Drawing.Size(72, 13);
            this.lblNumeroDocumento.TabIndex = 63;
            this.lblNumeroDocumento.Text = "N. Documento:";
            // 
            // lblDataVencimento
            // 
            this.lblDataVencimento.Location = new System.Drawing.Point(436, 45);
            this.lblDataVencimento.Name = "lblDataVencimento";
            this.lblDataVencimento.Size = new System.Drawing.Size(85, 13);
            this.lblDataVencimento.TabIndex = 62;
            this.lblDataVencimento.Text = "Data Vencimento:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtCodigo);
            this.panel1.Controls.Add(this.labelControl45);
            this.panel1.Controls.Add(this.txtEspecieDocumento);
            this.panel1.Controls.Add(this.labelControl4);
            this.panel1.Controls.Add(this.lkbCliente);
            this.panel1.Controls.Add(this.lkpCliente);
            this.panel1.Controls.Add(this.lkpFilial);
            this.panel1.Controls.Add(this.lkbFilial);
            this.panel1.Controls.Add(this.lblDataVencimento);
            this.panel1.Controls.Add(this.lblNumeroDocumento);
            this.panel1.Controls.Add(this.txtObservacao);
            this.panel1.Controls.Add(this.lblDataCriacao);
            this.panel1.Controls.Add(this.lblObservacao);
            this.panel1.Controls.Add(this.lblEspecieDocumento);
            this.panel1.Controls.Add(this.lkpServico);
            this.panel1.Controls.Add(this.lblFilial);
            this.panel1.Controls.Add(this.lblServico);
            this.panel1.Controls.Add(this.txtDataCriacao);
            this.panel1.Controls.Add(this.lkbServico);
            this.panel1.Controls.Add(this.txtNumeroDocumento);
            this.panel1.Controls.Add(this.lblValor);
            this.panel1.Controls.Add(this.txtDataVencimento);
            this.panel1.Controls.Add(this.txtValor);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 382);
            this.panel1.TabIndex = 0;
            // 
            // txtCodigo
            // 
            this.txtCodigo.CwkFuncaoValidacao = null;
            this.txtCodigo.CwkMascara = Cwork.Utilitarios.Componentes.Mascaras.Mascara.CODIGO;
            this.txtCodigo.CwkValidacao = null;
            this.txtCodigo.Location = new System.Drawing.Point(546, 94);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodigo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCodigo.Properties.Mask.EditMask = "\\d{0,9}";
            this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCodigo.Properties.ReadOnly = true;
            this.txtCodigo.SelecionarTextoOnEnter = false;
            this.txtCodigo.Size = new System.Drawing.Size(81, 20);
            this.txtCodigo.TabIndex = 85;
            // 
            // labelControl45
            // 
            this.labelControl45.Location = new System.Drawing.Point(503, 97);
            this.labelControl45.Name = "labelControl45";
            this.labelControl45.Size = new System.Drawing.Size(37, 13);
            this.labelControl45.TabIndex = 84;
            this.labelControl45.Text = "Código:";
            // 
            // txtEspecieDocumento
            // 
            this.txtEspecieDocumento.CwkFuncaoValidacao = null;
            this.txtEspecieDocumento.CwkMascara = null;
            this.txtEspecieDocumento.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtEspecieDocumento.Location = new System.Drawing.Point(527, 68);
            this.txtEspecieDocumento.Name = "txtEspecieDocumento";
            this.txtEspecieDocumento.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtEspecieDocumento.Properties.Appearance.Options.UseBackColor = true;
            this.txtEspecieDocumento.Properties.Appearance.Options.UseTextOptions = true;
            this.txtEspecieDocumento.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtEspecieDocumento.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEspecieDocumento.SelecionarTextoOnEnter = false;
            this.txtEspecieDocumento.Size = new System.Drawing.Size(100, 20);
            this.txtEspecieDocumento.TabIndex = 4;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(44, 123);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(37, 13);
            this.labelControl4.TabIndex = 81;
            this.labelControl4.Text = "Cliente:";
            // 
            // lkbCliente
            // 
            this.lkbCliente.Location = new System.Drawing.Point(603, 120);
            this.lkbCliente.Lookup = null;
            this.lkbCliente.Name = "lkbCliente";
            this.lkbCliente.Size = new System.Drawing.Size(24, 20);
            this.lkbCliente.SubForm = null;
            this.lkbCliente.SubFormType = null;
            this.lkbCliente.SubFormTypeParams = null;
            this.lkbCliente.TabIndex = 83;
            this.lkbCliente.TabStop = false;
            this.lkbCliente.Text = "...";
            this.lkbCliente.Click += new System.EventHandler(this.lkbCliente_Click);
            // 
            // lkpCliente
            // 
            this.lkpCliente.ButtonLookup = this.lkbCliente;
            this.lkpCliente.CamposPesquisa = new string[] {
        "Pessoa.Nome",
        "Pessoa.Codigo"};
            this.lkpCliente.CamposRestricoesAND = null;
            this.lkpCliente.CamposRestricoesNOT = null;
            this.lkpCliente.CamposRestricoesOR = null;
            this.lkpCliente.ColunaDescricao = new string[] {
        "Nome",
        "Código"};
            this.lkpCliente.ColunaTamanho = new string[] {
        "280",
        "200"};
            this.lkpCliente.ContextoLinq = null;
            this.lkpCliente.CwkFuncaoValidacao = null;
            this.lkpCliente.CwkMascara = null;
            this.lkpCliente.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpCliente.Exemplo = null;
            this.lkpCliente.ID = 0;
            this.lkpCliente.Join = " ";
            this.lkpCliente.Key = System.Windows.Forms.Keys.F5;
            this.lkpCliente.Location = new System.Drawing.Point(87, 120);
            this.lkpCliente.Name = "lkpCliente";
            this.lkpCliente.OnIDChanged = null;
            this.lkpCliente.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpCliente.Properties.Appearance.Options.UseBackColor = true;
            this.lkpCliente.SelecionarTextoOnEnter = true;
            this.lkpCliente.Size = new System.Drawing.Size(510, 20);
            this.lkpCliente.Tabela = "Pessoa";
            this.lkpCliente.TabIndex = 6;
            this.lkpCliente.TituloTelaPesquisa = "Pesquisa - Cliente";
            this.lkpCliente.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpCliente.Where = null;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(459, 400);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(96, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGravarFatura
            // 
            this.btnGravarFatura.Location = new System.Drawing.Point(561, 400);
            this.btnGravarFatura.Name = "btnGravarFatura";
            this.btnGravarFatura.Size = new System.Drawing.Size(96, 40);
            this.btnGravarFatura.TabIndex = 1;
            this.btnGravarFatura.Text = "Gravar";
            this.btnGravarFatura.UseVisualStyleBackColor = true;
            this.btnGravarFatura.Click += new System.EventHandler(this.btnGravarFatura_ClickAsync);
            // 
            // FormBoletoD8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 452);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGravarFatura);
            this.Controls.Add(this.panel1);
            this.Name = "FormBoletoD8";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fatura de Arrecadação";
            this.Load += new System.EventHandler(this.FormBoletoD8_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lkpFilial.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtObservacao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpServico.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtValor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataVencimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNumeroDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCriacao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataCriacao.Properties)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEspecieDocumento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpCliente.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cwork.Utilitarios.Componentes.LookupButton lkbFilial;
        private Cwork.Utilitarios.Componentes.Lookup lkpFilial;
        private DevExpress.XtraEditors.MemoEdit txtObservacao;
        private DevExpress.XtraEditors.LabelControl lblObservacao;
        private Cwork.Utilitarios.Componentes.Lookup lkpServico;
        private Cwork.Utilitarios.Componentes.LookupButton lkbServico;
        private DevExpress.XtraEditors.LabelControl lblServico;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtValor;
        private Cwork.Utilitarios.Componentes.CwkDateEditor txtDataVencimento;
        private DevExpress.XtraEditors.LabelControl lblValor;
        private Cwork.Utilitarios.Componentes.DevTextEdit txtNumeroDocumento;
        private Cwork.Utilitarios.Componentes.CwkDateEditor txtDataCriacao;
        private DevExpress.XtraEditors.LabelControl lblFilial;
        private DevExpress.XtraEditors.LabelControl lblEspecieDocumento;
        private DevExpress.XtraEditors.LabelControl lblDataCriacao;
        private DevExpress.XtraEditors.LabelControl lblNumeroDocumento;
        private DevExpress.XtraEditors.LabelControl lblDataVencimento;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGravarFatura;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Cwork.Utilitarios.Componentes.LookupButton lkbCliente;
        private Cwork.Utilitarios.Componentes.Lookup lkpCliente;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtEspecieDocumento;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl45;
    }
}