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
            this.txtSegmento = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.gcNcmVinculados = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.NCM = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btIncluirNCM = new Cwork.Utilitarios.Componentes.DevButton();
            this.btAlterarNCM = new Cwork.Utilitarios.Componentes.DevButton();
            this.btExcluirNCM = new Cwork.Utilitarios.Componentes.DevButton();
            this.btConsultarNCM = new Cwork.Utilitarios.Componentes.DevButton();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSegmento.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNcmVinculados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 362);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(619, 362);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(700, 362);
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
            this.tpPrincipal.Controls.Add(this.btIncluirNCM);
            this.tpPrincipal.Controls.Add(this.btAlterarNCM);
            this.tpPrincipal.Controls.Add(this.btExcluirNCM);
            this.tpPrincipal.Controls.Add(this.btConsultarNCM);
            this.tpPrincipal.Controls.Add(this.gcNcmVinculados);
            this.tpPrincipal.Controls.Add(this.labelControl4);
            this.tpPrincipal.Controls.Add(this.labelControl3);
            this.tpPrincipal.Controls.Add(this.txtDescricao);
            this.tpPrincipal.Controls.Add(this.labelControl2);
            this.tpPrincipal.Controls.Add(this.txtCodigo);
            this.tpPrincipal.Controls.Add(this.labelControl1);
            this.tpPrincipal.Controls.Add(this.txtSegmento);
            this.tpPrincipal.Size = new System.Drawing.Size(757, 332);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(763, 338);
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
            // txtSegmento
            // 
            this.txtSegmento.Location = new System.Drawing.Point(258, 11);
            this.txtSegmento.Name = "txtSegmento";
            this.txtSegmento.Size = new System.Drawing.Size(491, 20);
            this.txtSegmento.TabIndex = 73;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(31, 143);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 74;
            this.labelControl4.Text = "NCM:";
            // 
            // gcNcmVinculados
            // 
            this.gcNcmVinculados.Location = new System.Drawing.Point(66, 138);
            this.gcNcmVinculados.MainView = this.gridView1;
            this.gcNcmVinculados.Name = "gcNcmVinculados";
            this.gcNcmVinculados.Size = new System.Drawing.Size(683, 152);
            this.gcNcmVinculados.TabIndex = 75;
            this.gcNcmVinculados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gridView1.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gridView1.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gridView1.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gridView1.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gridView1.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gridView1.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.Empty.Options.UseBackColor = true;
            this.gridView1.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gridView1.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridView1.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.EvenRow.Options.UseForeColor = true;
            this.gridView1.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gridView1.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gridView1.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gridView1.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gridView1.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gridView1.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gridView1.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gridView1.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.FixedLine.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gridView1.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gridView1.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gridView1.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gridView1.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gridView1.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gridView1.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.GroupButton.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gridView1.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gridView1.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gridView1.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gridView1.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.GroupRow.Options.UseForeColor = true;
            this.gridView1.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gridView1.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gridView1.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gridView1.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gridView1.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gridView1.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gridView1.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.OddRow.Options.UseBackColor = true;
            this.gridView1.Appearance.OddRow.Options.UseBorderColor = true;
            this.gridView1.Appearance.OddRow.Options.UseForeColor = true;
            this.gridView1.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gridView1.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.Preview.Options.UseFont = true;
            this.gridView1.Appearance.Preview.Options.UseForeColor = true;
            this.gridView1.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gridView1.Appearance.Row.Options.UseBackColor = true;
            this.gridView1.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gridView1.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gridView1.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gridView1.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gridView1.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gridView1.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gridView1.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.NCM});
            this.gridView1.GridControl = this.gcNcmVinculados;
            this.gridView1.GroupPanelText = " ";
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsBehavior.FocusLeaveOnTab = true;
            this.gridView1.OptionsCustomization.AllowGroup = false;
            this.gridView1.OptionsView.EnableAppearanceEvenRow = true;
            this.gridView1.OptionsView.EnableAppearanceOddRow = true;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // NCM
            // 
            this.NCM.Caption = "NCM";
            this.NCM.FieldName = "NCMCodigo";
            this.NCM.Name = "NCM";
            this.NCM.Visible = true;
            this.NCM.VisibleIndex = 0;
            // 
            // btIncluirNCM
            // 
            this.btIncluirNCM.GridControl = null;
            this.btIncluirNCM.GridControlType = null;
            this.btIncluirNCM.GridControlTypeParams = null;
            this.btIncluirNCM.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.btIncluirNCM.Location = new System.Drawing.Point(512, 296);
            this.btIncluirNCM.Name = "btIncluirNCM";
            this.btIncluirNCM.Size = new System.Drawing.Size(75, 23);
            this.btIncluirNCM.SubForm = null;
            this.btIncluirNCM.SubFormType = null;
            this.btIncluirNCM.SubFormTypeParams = null;
            this.btIncluirNCM.TabIndex = 77;
            this.btIncluirNCM.TabStop = false;
            this.btIncluirNCM.Text = "&Incluir";
            this.btIncluirNCM.Click += new System.EventHandler(this.btSubRegistro_Click);
            // 
            // btAlterarNCM
            // 
            this.btAlterarNCM.GridControl = null;
            this.btAlterarNCM.GridControlType = null;
            this.btAlterarNCM.GridControlTypeParams = null;
            this.btAlterarNCM.ImageOptions.Image = global::Aplicacao.Properties.Resources.Alterar_copy;
            this.btAlterarNCM.Location = new System.Drawing.Point(593, 296);
            this.btAlterarNCM.Name = "btAlterarNCM";
            this.btAlterarNCM.Size = new System.Drawing.Size(75, 23);
            this.btAlterarNCM.SubForm = null;
            this.btAlterarNCM.SubFormType = null;
            this.btAlterarNCM.SubFormTypeParams = null;
            this.btAlterarNCM.TabIndex = 78;
            this.btAlterarNCM.TabStop = false;
            this.btAlterarNCM.Text = "&Alterar";
            // 
            // btExcluirNCM
            // 
            this.btExcluirNCM.GridControl = null;
            this.btExcluirNCM.GridControlType = null;
            this.btExcluirNCM.GridControlTypeParams = null;
            this.btExcluirNCM.ImageOptions.Image = global::Aplicacao.Properties.Resources.Excluir_copy;
            this.btExcluirNCM.Location = new System.Drawing.Point(674, 296);
            this.btExcluirNCM.Name = "btExcluirNCM";
            this.btExcluirNCM.Size = new System.Drawing.Size(75, 23);
            this.btExcluirNCM.SubForm = null;
            this.btExcluirNCM.SubFormType = null;
            this.btExcluirNCM.SubFormTypeParams = null;
            this.btExcluirNCM.TabIndex = 79;
            this.btExcluirNCM.TabStop = false;
            this.btExcluirNCM.Text = "&Excluir";
            // 
            // btConsultarNCM
            // 
            this.btConsultarNCM.GridControl = null;
            this.btConsultarNCM.GridControlType = null;
            this.btConsultarNCM.GridControlTypeParams = null;
            this.btConsultarNCM.ImageOptions.Image = global::Aplicacao.Properties.Resources.Consulta_copy;
            this.btConsultarNCM.Location = new System.Drawing.Point(431, 296);
            this.btConsultarNCM.Name = "btConsultarNCM";
            this.btConsultarNCM.Size = new System.Drawing.Size(75, 23);
            this.btConsultarNCM.SubForm = null;
            this.btConsultarNCM.SubFormType = null;
            this.btConsultarNCM.SubFormTypeParams = null;
            this.btConsultarNCM.TabIndex = 76;
            this.btConsultarNCM.TabStop = false;
            this.btConsultarNCM.Text = "&Consultar";
            // 
            // FormCEST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(787, 397);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCEST";
            this.Text = "Cadastro de CEST";
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSegmento.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcNcmVinculados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private Cwork.Utilitarios.Componentes.DevMemoEdit txtDescricao;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtSegmento;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        public DevExpress.XtraGrid.GridControl gcNcmVinculados;
        public DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn NCM;
        private Cwork.Utilitarios.Componentes.DevButton btIncluirNCM;
        private Cwork.Utilitarios.Componentes.DevButton btAlterarNCM;
        private Cwork.Utilitarios.Componentes.DevButton btExcluirNCM;
        private Cwork.Utilitarios.Componentes.DevButton btConsultarNCM;
    }
}