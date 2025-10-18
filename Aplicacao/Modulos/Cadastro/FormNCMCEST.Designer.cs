namespace Aplicacao
{
    public partial class FormNCMCEST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNCMCEST));
            this.txtNCM = new Cwork.Utilitarios.Componentes.Lookup();
            this.labelControl30 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCM.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 95);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(113, 95);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(194, 95);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.ImageStream = null;
            // 
            // tpPrincipal
            // 
            this.tpPrincipal.Controls.Add(this.txtNCM);
            this.tpPrincipal.Controls.Add(this.labelControl30);
            this.tpPrincipal.Size = new System.Drawing.Size(251, 71);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(257, 77);
            // 
            // txtNCM
            // 
            this.txtNCM.ButtonLookup = null;
            this.txtNCM.CamposPesquisa = new string[] {
        "Codigo",
        "Nome"};
            this.txtNCM.CamposRestricoesAND = null;
            this.txtNCM.CamposRestricoesNOT = null;
            this.txtNCM.CamposRestricoesOR = null;
            this.txtNCM.ColunaDescricao = new string[] {
        "Código",
        "Nome",
        null,
        null,
        null};
            this.txtNCM.ColunaTamanho = new string[] {
        "100",
        "400",
        null,
        null,
        null};
            this.txtNCM.ContextoLinq = null;
            this.txtNCM.CwkFuncaoValidacao = null;
            this.txtNCM.CwkMascara = null;
            this.txtNCM.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtNCM.Exemplo = null;
            this.txtNCM.ID = 0;
            this.txtNCM.Join = null;
            this.txtNCM.Key = System.Windows.Forms.Keys.F5;
            this.txtNCM.Location = new System.Drawing.Point(48, 27);
            this.txtNCM.Name = "txtNCM";
            this.txtNCM.OnIDChanged = null;
            this.txtNCM.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txtNCM.Properties.Appearance.Options.UseBackColor = true;
            this.txtNCM.SelecionarTextoOnEnter = true;
            this.txtNCM.Size = new System.Drawing.Size(192, 20);
            this.txtNCM.Tabela = null;
            this.txtNCM.TabIndex = 12;
            this.txtNCM.TituloTelaPesquisa = null;
            this.txtNCM.ToolTip = "Campos pesquisados: Codigo, Nome.";
            this.txtNCM.Where = null;
            // 
            // labelControl30
            // 
            this.labelControl30.Location = new System.Drawing.Point(16, 30);
            this.labelControl30.Name = "labelControl30";
            this.labelControl30.Size = new System.Drawing.Size(26, 13);
            this.labelControl30.TabIndex = 11;
            this.labelControl30.Text = "NCM:";
            // 
            // FormNCMCEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(281, 130);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNCMCEST";
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNCM.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Cwork.Utilitarios.Componentes.Lookup txtNCM;
        private DevExpress.XtraEditors.LabelControl labelControl30;
    }
}
