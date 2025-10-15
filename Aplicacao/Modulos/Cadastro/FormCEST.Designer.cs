namespace Aplicacao
{
    partial class FormCEST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCEST));
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtCodigo = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDescricao = new Cwork.Utilitarios.Componentes.DevMemoEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtNCM = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtSegmento = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSegmento.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 162);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(622, 162);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(703, 162);
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
            this.tpPrincipal.Controls.Add(this.txtNCM);
            this.tpPrincipal.Controls.Add(this.labelControl4);
            this.tpPrincipal.Controls.Add(this.labelControl3);
            this.tpPrincipal.Controls.Add(this.txtDescricao);
            this.tpPrincipal.Controls.Add(this.labelControl2);
            this.tpPrincipal.Controls.Add(this.txtCodigo);
            this.tpPrincipal.Controls.Add(this.labelControl1);
            this.tpPrincipal.Controls.Add(this.txtSegmento);
            this.tpPrincipal.Size = new System.Drawing.Size(760, 132);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(766, 138);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(23, 14);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(37, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.CwkFuncaoValidacao = null;
            this.txtCodigo.CwkMascara = null;
            this.txtCodigo.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.SomenteNumeral;
            this.txtCodigo.Location = new System.Drawing.Point(66, 11);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtCodigo.SelecionarTextoOnEnter = true;
            this.txtCodigo.Size = new System.Drawing.Size(115, 20);
            this.txtCodigo.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(10, 40);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(50, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "Descrição:";
            // 
            // txtDescricao
            // 
            this.txtDescricao.Location = new System.Drawing.Point(66, 39);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.MaxLength = 2000;
            this.txtDescricao.Size = new System.Drawing.Size(683, 90);
            this.txtDescricao.TabIndex = 71;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(200, 14);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(52, 13);
            this.labelControl3.TabIndex = 72;
            this.labelControl3.Text = "Segmento:";
            // 
            // txtNCM
            // 
            this.txtNCM.CwkFuncaoValidacao = null;
            this.txtNCM.CwkMascara = null;
            this.txtNCM.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.SomenteNumeral;
            this.txtNCM.Location = new System.Drawing.Point(634, 11);
            this.txtNCM.Name = "txtNCM";
            this.txtNCM.SelecionarTextoOnEnter = true;
            this.txtNCM.Size = new System.Drawing.Size(115, 20);
            this.txtNCM.TabIndex = 75;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(602, 14);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 74;
            this.labelControl4.Text = "NCM:";
            // 
            // txtSegmento
            // 
            this.txtSegmento.Location = new System.Drawing.Point(258, 11);
            this.txtSegmento.Name = "txtSegmento";
            this.txtSegmento.Size = new System.Drawing.Size(313, 20);
            this.txtSegmento.TabIndex = 73;
            // 
            // FormCEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(790, 197);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCEST";
            this.Text = "Cadastro de CEST";
            this.Load += new System.EventHandler(this.FormCEST_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNCM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSegmento.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Cwork.Utilitarios.Componentes.DevMemoEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtNCM;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtSegmento;
    }
}