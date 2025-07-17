
namespace Aplicacao.Modulos.Tributacao
{
    partial class FormImpostosTributosExcessoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImpostosTributosExcessoes));
            this.txtCodigo = new Cwork.Utilitarios.Componentes.DevTextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnlkpUF = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpUF = new Cwork.Utilitarios.Componentes.Lookup();
            this.labelControl22 = new DevExpress.XtraEditors.LabelControl();
            this.txtPIcms = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.txtPReducaoIcms = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtPIcmsSt = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtPReducaoIcmsSt = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.txtPMva = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtPDiferimento = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.txtPDesoneracao = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.mInfAdicionais = new Cwork.Utilitarios.Componentes.DevMemoEdit();
            this.lblInfAdicionais = new System.Windows.Forms.Label();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.cbeCSTCSOSNIcms = new Cwork.Utilitarios.Componentes.DevComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnlkpTabelaCFOP = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpTabelaCFOP = new Cwork.Utilitarios.Componentes.Lookup();
            this.txtDescricao = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUF.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPIcms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPReducaoIcms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPIcmsSt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPReducaoIcmsSt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPMva.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPDiferimento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPDesoneracao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mInfAdicionais.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeCSTCSOSNIcms.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaCFOP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 195);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(799, 195);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(880, 195);
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
            this.tpPrincipal.Controls.Add(this.txtDescricao);
            this.tpPrincipal.Controls.Add(this.label9);
            this.tpPrincipal.Controls.Add(this.btnlkpTabelaCFOP);
            this.tpPrincipal.Controls.Add(this.lkpTabelaCFOP);
            this.tpPrincipal.Controls.Add(this.labelControl2);
            this.tpPrincipal.Controls.Add(this.labelControl10);
            this.tpPrincipal.Controls.Add(this.cbeCSTCSOSNIcms);
            this.tpPrincipal.Controls.Add(this.mInfAdicionais);
            this.tpPrincipal.Controls.Add(this.lblInfAdicionais);
            this.tpPrincipal.Controls.Add(this.txtPDesoneracao);
            this.tpPrincipal.Controls.Add(this.labelControl9);
            this.tpPrincipal.Controls.Add(this.txtPDiferimento);
            this.tpPrincipal.Controls.Add(this.labelControl8);
            this.tpPrincipal.Controls.Add(this.txtPMva);
            this.tpPrincipal.Controls.Add(this.labelControl7);
            this.tpPrincipal.Controls.Add(this.txtPReducaoIcmsSt);
            this.tpPrincipal.Controls.Add(this.labelControl5);
            this.tpPrincipal.Controls.Add(this.txtPIcmsSt);
            this.tpPrincipal.Controls.Add(this.labelControl4);
            this.tpPrincipal.Controls.Add(this.txtPReducaoIcms);
            this.tpPrincipal.Controls.Add(this.labelControl3);
            this.tpPrincipal.Controls.Add(this.txtPIcms);
            this.tpPrincipal.Controls.Add(this.labelControl6);
            this.tpPrincipal.Controls.Add(this.btnlkpUF);
            this.tpPrincipal.Controls.Add(this.lkpUF);
            this.tpPrincipal.Controls.Add(this.labelControl22);
            this.tpPrincipal.Controls.Add(this.txtCodigo);
            this.tpPrincipal.Controls.Add(this.labelControl1);
            this.tpPrincipal.Size = new System.Drawing.Size(937, 171);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(943, 177);
            // 
            // txtCodigo
            // 
            this.txtCodigo.AllowDrop = true;
            this.txtCodigo.CwkFuncaoValidacao = null;
            this.txtCodigo.CwkMascara = Cwork.Utilitarios.Componentes.Mascaras.Mascara.CODIGO;
            this.txtCodigo.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtCodigo.Location = new System.Drawing.Point(66, 19);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodigo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCodigo.Properties.Mask.EditMask = "\\d{0,9}";
            this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCodigo.SelecionarTextoOnEnter = true;
            this.txtCodigo.Size = new System.Drawing.Size(83, 20);
            this.txtCodigo.TabIndex = 7;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 22);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "Código:";
            // 
            // btnlkpUF
            // 
            this.btnlkpUF.Location = new System.Drawing.Point(374, 19);
            this.btnlkpUF.Lookup = null;
            this.btnlkpUF.Name = "btnlkpUF";
            this.btnlkpUF.Size = new System.Drawing.Size(24, 20);
            this.btnlkpUF.SubForm = null;
            this.btnlkpUF.SubFormType = null;
            this.btnlkpUF.SubFormTypeParams = null;
            this.btnlkpUF.TabIndex = 12;
            this.btnlkpUF.TabStop = false;
            this.btnlkpUF.Text = "...";
            this.btnlkpUF.Click += new System.EventHandler(this.btnlkpUF_Click);
            // 
            // lkpUF
            // 
            this.lkpUF.ButtonLookup = this.btnlkpUF;
            this.lkpUF.CamposPesquisa = new string[] {
        "Nome",
        "Sigla",
        "IBGE",
        "Codigo"};
            this.lkpUF.CamposRestricoesAND = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpUF.CamposRestricoesAND")));
            this.lkpUF.CamposRestricoesNOT = null;
            this.lkpUF.CamposRestricoesOR = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpUF.CamposRestricoesOR")));
            this.lkpUF.ColunaDescricao = new string[] {
        "Nome",
        "Sigla",
        "IBGE",
        "Código",
        null};
            this.lkpUF.ColunaTamanho = new string[] {
        "100",
        "20",
        "20",
        "20",
        null};
            this.lkpUF.ContextoLinq = null;
            this.lkpUF.CwkFuncaoValidacao = null;
            this.lkpUF.CwkMascara = null;
            this.lkpUF.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpUF.Exemplo = null;
            this.lkpUF.ID = 0;
            this.lkpUF.Join = null;
            this.lkpUF.Key = System.Windows.Forms.Keys.F5;
            this.lkpUF.Location = new System.Drawing.Point(206, 19);
            this.lkpUF.Name = "lkpUF";
            this.lkpUF.OnIDChanged = null;
            this.lkpUF.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpUF.Properties.Appearance.Options.UseBackColor = true;
            this.lkpUF.SelecionarTextoOnEnter = false;
            this.lkpUF.Size = new System.Drawing.Size(162, 20);
            this.lkpUF.Tabela = null;
            this.lkpUF.TabIndex = 11;
            this.lkpUF.TituloTelaPesquisa = null;
            this.lkpUF.ToolTip = "Campos pesquisados: Nome, Sigla, IBGE, Codigo.";
            this.lkpUF.Where = null;
            // 
            // labelControl22
            // 
            this.labelControl22.Location = new System.Drawing.Point(160, 22);
            this.labelControl22.Name = "labelControl22";
            this.labelControl22.Size = new System.Drawing.Size(37, 13);
            this.labelControl22.TabIndex = 10;
            this.labelControl22.Text = "Estado:";
            // 
            // txtPIcms
            // 
            this.txtPIcms.Location = new System.Drawing.Point(66, 59);
            this.txtPIcms.Name = "txtPIcms";
            this.txtPIcms.Properties.Mask.EditMask = "P2";
            this.txtPIcms.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPIcms.Properties.Precision = 2;
            this.txtPIcms.Size = new System.Drawing.Size(51, 20);
            this.txtPIcms.TabIndex = 14;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(17, 62);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(43, 13);
            this.labelControl6.TabIndex = 13;
            this.labelControl6.Text = "% ICMS:";
            // 
            // txtPReducaoIcms
            // 
            this.txtPReducaoIcms.Location = new System.Drawing.Point(206, 59);
            this.txtPReducaoIcms.Name = "txtPReducaoIcms";
            this.txtPReducaoIcms.Properties.Mask.EditMask = "P2";
            this.txtPReducaoIcms.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPReducaoIcms.Properties.Precision = 2;
            this.txtPReducaoIcms.Size = new System.Drawing.Size(51, 20);
            this.txtPReducaoIcms.TabIndex = 16;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(130, 62);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(69, 13);
            this.labelControl3.TabIndex = 15;
            this.labelControl3.Text = "% Red. ICMS:";
            // 
            // txtPIcmsSt
            // 
            this.txtPIcmsSt.Location = new System.Drawing.Point(331, 59);
            this.txtPIcmsSt.Name = "txtPIcmsSt";
            this.txtPIcmsSt.Properties.Mask.EditMask = "P2";
            this.txtPIcmsSt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPIcmsSt.Properties.Precision = 2;
            this.txtPIcmsSt.Size = new System.Drawing.Size(51, 20);
            this.txtPIcmsSt.TabIndex = 18;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(267, 62);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 13);
            this.labelControl4.TabIndex = 17;
            this.labelControl4.Text = "% ICMS ST:";
            // 
            // txtPReducaoIcmsSt
            // 
            this.txtPReducaoIcmsSt.Location = new System.Drawing.Point(483, 59);
            this.txtPReducaoIcmsSt.Name = "txtPReducaoIcmsSt";
            this.txtPReducaoIcmsSt.Properties.Mask.EditMask = "P2";
            this.txtPReducaoIcmsSt.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPReducaoIcmsSt.Properties.Precision = 2;
            this.txtPReducaoIcmsSt.Size = new System.Drawing.Size(51, 20);
            this.txtPReducaoIcmsSt.TabIndex = 20;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(393, 62);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(84, 13);
            this.labelControl5.TabIndex = 19;
            this.labelControl5.Text = "% Red. ICMS ST:";
            // 
            // txtPMva
            // 
            this.txtPMva.Location = new System.Drawing.Point(588, 59);
            this.txtPMva.Name = "txtPMva";
            this.txtPMva.Properties.Mask.EditMask = "P2";
            this.txtPMva.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPMva.Properties.Precision = 2;
            this.txtPMva.Size = new System.Drawing.Size(51, 20);
            this.txtPMva.TabIndex = 22;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(543, 62);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(39, 13);
            this.labelControl7.TabIndex = 21;
            this.labelControl7.Text = "% MVA:";
            // 
            // txtPDiferimento
            // 
            this.txtPDiferimento.Location = new System.Drawing.Point(723, 59);
            this.txtPDiferimento.Name = "txtPDiferimento";
            this.txtPDiferimento.Properties.Mask.EditMask = "P2";
            this.txtPDiferimento.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPDiferimento.Properties.Precision = 2;
            this.txtPDiferimento.Size = new System.Drawing.Size(51, 20);
            this.txtPDiferimento.TabIndex = 24;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(645, 62);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(73, 13);
            this.labelControl8.TabIndex = 23;
            this.labelControl8.Text = "% Diferimento:";
            // 
            // txtPDesoneracao
            // 
            this.txtPDesoneracao.Location = new System.Drawing.Point(869, 59);
            this.txtPDesoneracao.Name = "txtPDesoneracao";
            this.txtPDesoneracao.Properties.Mask.EditMask = "P2";
            this.txtPDesoneracao.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPDesoneracao.Properties.Precision = 2;
            this.txtPDesoneracao.Size = new System.Drawing.Size(51, 20);
            this.txtPDesoneracao.TabIndex = 26;
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(786, 62);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(81, 13);
            this.labelControl9.TabIndex = 25;
            this.labelControl9.Text = "% Desoneração:";
            // 
            // mInfAdicionais
            // 
            this.mInfAdicionais.Location = new System.Drawing.Point(94, 97);
            this.mInfAdicionais.Name = "mInfAdicionais";
            this.mInfAdicionais.Properties.MaxLength = 500;
            this.mInfAdicionais.Size = new System.Drawing.Size(461, 56);
            this.mInfAdicionais.TabIndex = 53;
            // 
            // lblInfAdicionais
            // 
            this.lblInfAdicionais.AutoSize = true;
            this.lblInfAdicionais.Location = new System.Drawing.Point(12, 99);
            this.lblInfAdicionais.Name = "lblInfAdicionais";
            this.lblInfAdicionais.Size = new System.Drawing.Size(79, 13);
            this.lblInfAdicionais.TabIndex = 52;
            this.lblInfAdicionais.Text = "Inf. Adicionais:";
            this.lblInfAdicionais.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(404, 22);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(86, 13);
            this.labelControl10.TabIndex = 54;
            this.labelControl10.Text = "CST/CSOSN Icms:";
            // 
            // cbeCSTCSOSNIcms
            // 
            this.cbeCSTCSOSNIcms.Location = new System.Drawing.Point(496, 19);
            this.cbeCSTCSOSNIcms.Name = "cbeCSTCSOSNIcms";
            this.cbeCSTCSOSNIcms.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeCSTCSOSNIcms.Properties.Items.AddRange(new object[] {
            "00-Tributada integralmente",
            "10-Tributada e com cobrança do ICMS por substituição tributária",
            "20-Com redução de base de cálculo ",
            "30-Isenta ou não tributada e com cobrança do ICMS por substituição tributária ",
            "40-Isenta ",
            "41-Não tributada ",
            "50-Suspensão ",
            "51-Diferimento ",
            "60-ICMS cobrado anteriormente por substituição tributária ",
            "70-Com redução de base de cálculo e cobrança do ICMS por substituição tributária " +
                "",
            "90-Outras",
            "101-Tributada pelo Simples Nacional com permissão de crédito ",
            "102-Tributada pelo Simples Nacional sem permissão de crédito ",
            "103-Isenção do ICMS no Simples Nacional para faixa de receita bruta",
            "201-Tributada pelo Simples Nacional com permissão de crédito e com cobrança do IC" +
                "MS por substituição tributária ",
            "202-Tributada pelo Simples Nacional sem permissão de crédito e com cobrança do IC" +
                "MS por substituição tributária ",
            "203-Isenção do ICMS no Simples Nacional para faixa de receita bruta e com cobranç" +
                "a do ICMS por substituição tributária ",
            "300-Imune ",
            "400-Não tributada pelo Simples Nacional ",
            "500-ICMS cobrado anteriormente por substituição tributária (substituído) ou por a" +
                "ntecipação ",
            "900-Outros"});
            this.cbeCSTCSOSNIcms.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbeCSTCSOSNIcms.Size = new System.Drawing.Size(424, 20);
            this.cbeCSTCSOSNIcms.TabIndex = 55;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(576, 99);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(31, 13);
            this.labelControl2.TabIndex = 56;
            this.labelControl2.Text = "CFOP:";
            // 
            // btnlkpTabelaCFOP
            // 
            this.btnlkpTabelaCFOP.Location = new System.Drawing.Point(891, 98);
            this.btnlkpTabelaCFOP.Lookup = null;
            this.btnlkpTabelaCFOP.Name = "btnlkpTabelaCFOP";
            this.btnlkpTabelaCFOP.Size = new System.Drawing.Size(24, 20);
            this.btnlkpTabelaCFOP.SubForm = null;
            this.btnlkpTabelaCFOP.SubFormType = null;
            this.btnlkpTabelaCFOP.SubFormTypeParams = null;
            this.btnlkpTabelaCFOP.TabIndex = 74;
            this.btnlkpTabelaCFOP.TabStop = false;
            this.btnlkpTabelaCFOP.Text = "...";
            this.btnlkpTabelaCFOP.Click += new System.EventHandler(this.btnlkpCfop_Click);
            // 
            // lkpTabelaCFOP
            // 
            this.lkpTabelaCFOP.ButtonLookup = this.btnlkpTabelaCFOP;
            this.lkpTabelaCFOP.CamposPesquisa = new string[] {
        "CFOP",
        "Nome",
        "Codigo"};
            this.lkpTabelaCFOP.CamposRestricoesAND = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpTabelaCFOP.CamposRestricoesAND")));
            this.lkpTabelaCFOP.CamposRestricoesNOT = null;
            this.lkpTabelaCFOP.CamposRestricoesOR = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpTabelaCFOP.CamposRestricoesOR")));
            this.lkpTabelaCFOP.ColunaDescricao = new string[] {
        "Nome",
        "Sigla",
        "IBGE",
        "Código",
        null};
            this.lkpTabelaCFOP.ColunaTamanho = new string[] {
        "100",
        "20",
        "20",
        "20",
        null};
            this.lkpTabelaCFOP.ContextoLinq = null;
            this.lkpTabelaCFOP.CwkFuncaoValidacao = null;
            this.lkpTabelaCFOP.CwkMascara = null;
            this.lkpTabelaCFOP.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.lkpTabelaCFOP.Exemplo = null;
            this.lkpTabelaCFOP.ID = 0;
            this.lkpTabelaCFOP.Join = null;
            this.lkpTabelaCFOP.Key = System.Windows.Forms.Keys.F5;
            this.lkpTabelaCFOP.Location = new System.Drawing.Point(613, 96);
            this.lkpTabelaCFOP.Name = "lkpTabelaCFOP";
            this.lkpTabelaCFOP.OnIDChanged = null;
            this.lkpTabelaCFOP.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpTabelaCFOP.Properties.Appearance.Options.UseBackColor = true;
            this.lkpTabelaCFOP.SelecionarTextoOnEnter = false;
            this.lkpTabelaCFOP.Size = new System.Drawing.Size(272, 20);
            this.lkpTabelaCFOP.Tabela = null;
            this.lkpTabelaCFOP.TabIndex = 73;
            this.lkpTabelaCFOP.TituloTelaPesquisa = null;
            this.lkpTabelaCFOP.ToolTip = "Campos pesquisados: CFOP, Nome, Codigo.";
            this.lkpTabelaCFOP.Where = null;
            // 
            // txtDescricao
            // 
            this.txtDescricao.CwkFuncaoValidacao = null;
            this.txtDescricao.CwkMascara = null;
            this.txtDescricao.CwkValidacao = null;
            this.txtDescricao.EditValue = "";
            this.txtDescricao.Location = new System.Drawing.Point(627, 133);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.MaxLength = 200;
            this.txtDescricao.SelecionarTextoOnEnter = true;
            this.txtDescricao.Size = new System.Drawing.Size(288, 20);
            this.txtDescricao.TabIndex = 76;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(564, 136);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 13);
            this.label9.TabIndex = 75;
            this.label9.Text = "Descrição:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // FormImpostosTributosExcessoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 230);
            this.Name = "FormImpostosTributosExcessoes";
            this.Text = "FormImpostosTributosExcessoes";
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpUF.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPIcms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPReducaoIcms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPIcmsSt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPReducaoIcmsSt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPMva.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPDiferimento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPDesoneracao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mInfAdicionais.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeCSTCSOSNIcms.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaCFOP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cwork.Utilitarios.Componentes.DevTextEdit txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Cwork.Utilitarios.Componentes.LookupButton btnlkpUF;
        private Cwork.Utilitarios.Componentes.Lookup lkpUF;
        private DevExpress.XtraEditors.LabelControl labelControl22;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPDesoneracao;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPDiferimento;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPMva;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPReducaoIcmsSt;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPIcmsSt;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPReducaoIcms;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPIcms;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private Cwork.Utilitarios.Componentes.DevMemoEdit mInfAdicionais;
        private System.Windows.Forms.Label lblInfAdicionais;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private Cwork.Utilitarios.Componentes.DevComboBoxEdit cbeCSTCSOSNIcms;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Cwork.Utilitarios.Componentes.LookupButton btnlkpTabelaCFOP;
        private Cwork.Utilitarios.Componentes.Lookup lkpTabelaCFOP;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtDescricao;
        private System.Windows.Forms.Label label9;
    }
}