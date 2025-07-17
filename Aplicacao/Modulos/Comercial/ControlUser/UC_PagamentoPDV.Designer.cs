namespace Aplicacao.Modulos.Comercial.ControlUser
{
    partial class UC_PagamentoPDV
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.TXT_FormaPagamento = new System.Windows.Forms.Label();
            this.TXT_Valor = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.txtParcelas = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.LBL_Total = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TXT_Valor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParcelas.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TXT_FormaPagamento
            // 
            this.TXT_FormaPagamento.BackColor = System.Drawing.Color.Transparent;
            this.TXT_FormaPagamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_FormaPagamento.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.TXT_FormaPagamento.Location = new System.Drawing.Point(4, 4);
            this.TXT_FormaPagamento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TXT_FormaPagamento.Name = "TXT_FormaPagamento";
            this.TXT_FormaPagamento.Size = new System.Drawing.Size(218, 61);
            this.TXT_FormaPagamento.TabIndex = 8;
            this.TXT_FormaPagamento.Text = "Forma de Pagamento";
            this.TXT_FormaPagamento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TXT_FormaPagamento.Click += new System.EventHandler(this.TXT_FormaPagamento_Click);
            // 
            // TXT_Valor
            // 
            this.TXT_Valor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TXT_Valor.CwkFuncaoValidacao = null;
            this.TXT_Valor.CwkMascara = Cwork.Utilitarios.Componentes.Mascaras.Mascara.MONETARIO;
            this.TXT_Valor.CwkValidacao = null;
            this.TXT_Valor.EditValue = "0,00";
            this.TXT_Valor.Location = new System.Drawing.Point(8, 86);
            this.TXT_Valor.Margin = new System.Windows.Forms.Padding(0, 4, 4, 4);
            this.TXT_Valor.Name = "TXT_Valor";
            this.TXT_Valor.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.TXT_Valor.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TXT_Valor.Properties.Appearance.ForeColor = System.Drawing.Color.Green;
            this.TXT_Valor.Properties.Appearance.Options.UseBackColor = true;
            this.TXT_Valor.Properties.Appearance.Options.UseFont = true;
            this.TXT_Valor.Properties.Appearance.Options.UseForeColor = true;
            this.TXT_Valor.Properties.Appearance.Options.UseTextOptions = true;
            this.TXT_Valor.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.TXT_Valor.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.TXT_Valor.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.TXT_Valor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Transparent;
            this.TXT_Valor.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.TXT_Valor.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Transparent;
            this.TXT_Valor.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.TXT_Valor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.TXT_Valor.Properties.Mask.EditMask = "c2";
            this.TXT_Valor.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.TXT_Valor.Properties.Mask.SaveLiteral = false;
            this.TXT_Valor.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.TXT_Valor.SelecionarTextoOnEnter = true;
            this.TXT_Valor.Size = new System.Drawing.Size(214, 32);
            this.TXT_Valor.TabIndex = 1;
            this.TXT_Valor.Click += new System.EventHandler(this.TXT_Valor_Click);
            this.TXT_Valor.Enter += new System.EventHandler(this.TXT_Valor_Enter);
            // 
            // txtParcelas
            // 
            this.txtParcelas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtParcelas.CwkFuncaoValidacao = null;
            this.txtParcelas.CwkMascara = Cwork.Utilitarios.Componentes.Mascaras.Mascara.NUMERAL;
            this.txtParcelas.CwkValidacao = null;
            this.txtParcelas.EditValue = "0";
            this.txtParcelas.EnterMoveNextControl = true;
            this.txtParcelas.Location = new System.Drawing.Point(164, 86);
            this.txtParcelas.Margin = new System.Windows.Forms.Padding(4);
            this.txtParcelas.Name = "txtParcelas";
            this.txtParcelas.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtParcelas.Properties.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtParcelas.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.txtParcelas.Properties.Appearance.Options.UseBackColor = true;
            this.txtParcelas.Properties.Appearance.Options.UseFont = true;
            this.txtParcelas.Properties.Appearance.Options.UseForeColor = true;
            this.txtParcelas.Properties.Appearance.Options.UseTextOptions = true;
            this.txtParcelas.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.txtParcelas.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.Transparent;
            this.txtParcelas.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.txtParcelas.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Transparent;
            this.txtParcelas.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtParcelas.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.Transparent;
            this.txtParcelas.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtParcelas.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.txtParcelas.Properties.Mask.EditMask = "\\d{0,15}";
            this.txtParcelas.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtParcelas.Properties.Mask.SaveLiteral = false;
            this.txtParcelas.Properties.Mask.ShowPlaceHolders = false;
            this.txtParcelas.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtParcelas.SelecionarTextoOnEnter = false;
            this.txtParcelas.Size = new System.Drawing.Size(58, 32);
            this.txtParcelas.TabIndex = 2;
            this.txtParcelas.Leave += new System.EventHandler(this.txtParcelas_Leave);
            // 
            // LBL_Total
            // 
            this.LBL_Total.BackColor = System.Drawing.Color.Transparent;
            this.LBL_Total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LBL_Total.ForeColor = System.Drawing.Color.Blue;
            this.LBL_Total.Location = new System.Drawing.Point(8, 65);
            this.LBL_Total.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LBL_Total.Name = "LBL_Total";
            this.LBL_Total.Size = new System.Drawing.Size(214, 22);
            this.LBL_Total.TabIndex = 9;
            this.LBL_Total.Text = "0,00";
            this.LBL_Total.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LBL_Total.Click += new System.EventHandler(this.LBL_Total_Click);
            this.LBL_Total.Leave += new System.EventHandler(this.LBL_Total_Leave);
            // 
            // UC_PagamentoPDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.LBL_Total);
            this.Controls.Add(this.txtParcelas);
            this.Controls.Add(this.TXT_Valor);
            this.Controls.Add(this.TXT_FormaPagamento);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximumSize = new System.Drawing.Size(538, 56);
            this.MinimumSize = new System.Drawing.Size(162, 125);
            this.Name = "UC_PagamentoPDV";
            this.Size = new System.Drawing.Size(225, 125);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormFinalizarVendaFrenteCaixa_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.TXT_Valor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtParcelas.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label TXT_FormaPagamento;
        public Cwork.Utilitarios.Componentes.CwkBaseEditor TXT_Valor;
        public Cwork.Utilitarios.Componentes.CwkBaseEditor txtParcelas;
        private System.Windows.Forms.Label LBL_Total;
    }
}
