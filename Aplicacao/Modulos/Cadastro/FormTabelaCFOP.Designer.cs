namespace Aplicacao
{
    partial class FormTabelaCFOP
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTabelaCFOP));
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCFOP = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.txtCodigo = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCfopVinculadoEntrada = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNome = new Cwork.Utilitarios.Componentes.DevMemoEdit();
            this.txtAplicacao = new Cwork.Utilitarios.Componentes.DevMemoEdit();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCFOP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCfopVinculadoEntrada.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNome.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAplicacao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 247);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(540, 247);
            this.sbGravar.TabIndex = 0;
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(621, 247);
            this.sbCancelar.TabIndex = 1;
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
            this.tpPrincipal.Controls.Add(this.txtAplicacao);
            this.tpPrincipal.Controls.Add(this.label6);
            this.tpPrincipal.Controls.Add(this.txtNome);
            this.tpPrincipal.Controls.Add(this.txtCfopVinculadoEntrada);
            this.tpPrincipal.Controls.Add(this.label4);
            this.tpPrincipal.Controls.Add(this.txtCodigo);
            this.tpPrincipal.Controls.Add(this.label3);
            this.tpPrincipal.Controls.Add(this.txtCFOP);
            this.tpPrincipal.Controls.Add(this.label2);
            this.tpPrincipal.Controls.Add(this.label1);
            this.tpPrincipal.Size = new System.Drawing.Size(678, 223);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(684, 229);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nome:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(531, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "CFOP:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCFOP
            // 
            this.txtCFOP.CwkFuncaoValidacao = null;
            this.txtCFOP.CwkMascara = null;
            this.txtCFOP.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtCFOP.Location = new System.Drawing.Point(575, 3);
            this.txtCFOP.Name = "txtCFOP";
            this.txtCFOP.Properties.Mask.EditMask = "\\d\\d\\d\\d";
            this.txtCFOP.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCFOP.SelecionarTextoOnEnter = true;
            this.txtCFOP.Size = new System.Drawing.Size(100, 20);
            this.txtCFOP.TabIndex = 3;
            // 
            // txtCodigo
            // 
            this.txtCodigo.CwkFuncaoValidacao = null;
            this.txtCodigo.CwkMascara = Cwork.Utilitarios.Componentes.Mascaras.Mascara.NUMERAL;
            this.txtCodigo.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtCodigo.Location = new System.Drawing.Point(64, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.Appearance.Options.UseTextOptions = true;
            this.txtCodigo.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtCodigo.Properties.Mask.EditMask = "\\d{0,15}";
            this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCodigo.Properties.Mask.ShowPlaceHolders = false;
            this.txtCodigo.SelecionarTextoOnEnter = true;
            this.txtCodigo.Size = new System.Drawing.Size(100, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtCodigo.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Código:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCfopVinculadoEntrada
            // 
            this.txtCfopVinculadoEntrada.CwkFuncaoValidacao = null;
            this.txtCfopVinculadoEntrada.CwkMascara = null;
            this.txtCfopVinculadoEntrada.CwkValidacao = null;
            this.txtCfopVinculadoEntrada.Location = new System.Drawing.Point(575, 189);
            this.txtCfopVinculadoEntrada.Name = "txtCfopVinculadoEntrada";
            this.txtCfopVinculadoEntrada.Properties.Mask.EditMask = "\\d\\d\\d\\d";
            this.txtCfopVinculadoEntrada.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtCfopVinculadoEntrada.SelecionarTextoOnEnter = true;
            this.txtCfopVinculadoEntrada.Size = new System.Drawing.Size(100, 20);
            this.txtCfopVinculadoEntrada.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(442, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "CFOP Vinculado Entrada:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(64, 31);
            this.txtNome.Name = "txtNome";
            this.txtNome.Properties.MaxLength = 2000;
            this.txtNome.Size = new System.Drawing.Size(611, 65);
            this.txtNome.TabIndex = 70;
            // 
            // txtAplicacao
            // 
            this.txtAplicacao.Location = new System.Drawing.Point(64, 118);
            this.txtAplicacao.Name = "txtAplicacao";
            this.txtAplicacao.Properties.MaxLength = 2000;
            this.txtAplicacao.Size = new System.Drawing.Size(611, 65);
            this.txtAplicacao.TabIndex = 72;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 71;
            this.label6.Text = "Aplicação:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormTabelaCFOP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(708, 282);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTabelaCFOP";
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCFOP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCfopVinculadoEntrada.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNome.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAplicacao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCFOP;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCodigo;
        private System.Windows.Forms.Label label3;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCfopVinculadoEntrada;
        private System.Windows.Forms.Label label4;
        private Cwork.Utilitarios.Componentes.DevMemoEdit txtNome;
        private Cwork.Utilitarios.Componentes.DevMemoEdit txtAplicacao;
        private System.Windows.Forms.Label label6;
    }
}
