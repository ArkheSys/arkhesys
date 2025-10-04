namespace Aplicacao
{
    partial class FormNCM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNCM));
            this.btConsultarNCMItens = new Cwork.Utilitarios.Componentes.DevButton();
            this.btExcluirCest = new Cwork.Utilitarios.Componentes.DevButton();
            this.btAlterarNCMItens = new Cwork.Utilitarios.Componentes.DevButton();
            this.btIncluirCest = new Cwork.Utilitarios.Componentes.DevButton();
            this.gcCestsVinculados = new DevExpress.XtraGrid.GridControl();
            this.gvCestsVinculados = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.codigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtCodigo = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.txtNcm = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAliqCupom = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescricao = new Cwork.Utilitarios.Componentes.CwkBaseEditor();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDtRevogacao = new Cwork.Utilitarios.Componentes.DevDateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.tpPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).BeginInit();
            this.tcPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCestsVinculados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCestsVinculados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNcm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliqCupom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtRevogacao.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtRevogacao.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 376);
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbGravar.ImageOptions.Image")));
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(797, 376);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(878, 376);
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
            this.tpPrincipal.Controls.Add(this.txtDtRevogacao);
            this.tpPrincipal.Controls.Add(this.label5);
            this.tpPrincipal.Controls.Add(this.txtAliqCupom);
            this.tpPrincipal.Controls.Add(this.label1);
            this.tpPrincipal.Controls.Add(this.txtDescricao);
            this.tpPrincipal.Controls.Add(this.txtNcm);
            this.tpPrincipal.Controls.Add(this.label4);
            this.tpPrincipal.Controls.Add(this.label3);
            this.tpPrincipal.Controls.Add(this.txtCodigo);
            this.tpPrincipal.Controls.Add(this.labelControl7);
            this.tpPrincipal.Controls.Add(this.btConsultarNCMItens);
            this.tpPrincipal.Controls.Add(this.btExcluirCest);
            this.tpPrincipal.Controls.Add(this.btAlterarNCMItens);
            this.tpPrincipal.Controls.Add(this.btIncluirCest);
            this.tpPrincipal.Controls.Add(this.gcCestsVinculados);
            this.tpPrincipal.Size = new System.Drawing.Size(935, 352);
            // 
            // tcPrincipal
            // 
            this.tcPrincipal.SelectedTabPage = this.tpPrincipal;
            this.tcPrincipal.Size = new System.Drawing.Size(941, 358);
            // 
            // btConsultarNCMItens
            // 
            this.btConsultarNCMItens.GridControl = null;
            this.btConsultarNCMItens.GridControlType = null;
            this.btConsultarNCMItens.GridControlTypeParams = null;
            this.btConsultarNCMItens.ImageOptions.Image = global::Aplicacao.Properties.Resources.Consulta_copy;
            this.btConsultarNCMItens.Location = new System.Drawing.Point(585, 309);
            this.btConsultarNCMItens.Name = "btConsultarNCMItens";
            this.btConsultarNCMItens.Size = new System.Drawing.Size(75, 23);
            this.btConsultarNCMItens.SubForm = null;
            this.btConsultarNCMItens.SubFormType = null;
            this.btConsultarNCMItens.SubFormTypeParams = null;
            this.btConsultarNCMItens.TabIndex = 10;
            this.btConsultarNCMItens.TabStop = false;
            this.btConsultarNCMItens.Text = "&Consultar";
            // 
            // btExcluirCest
            // 
            this.btExcluirCest.GridControl = null;
            this.btExcluirCest.GridControlType = null;
            this.btExcluirCest.GridControlTypeParams = null;
            this.btExcluirCest.ImageOptions.Image = global::Aplicacao.Properties.Resources.Excluir_copy;
            this.btExcluirCest.Location = new System.Drawing.Point(828, 309);
            this.btExcluirCest.Name = "btExcluirCest";
            this.btExcluirCest.Size = new System.Drawing.Size(75, 23);
            this.btExcluirCest.SubForm = null;
            this.btExcluirCest.SubFormType = null;
            this.btExcluirCest.SubFormTypeParams = null;
            this.btExcluirCest.TabIndex = 13;
            this.btExcluirCest.TabStop = false;
            this.btExcluirCest.Text = "&Excluir";
            this.btExcluirCest.Click += new System.EventHandler(this.btExcluirCest_Click);
            // 
            // btAlterarNCMItens
            // 
            this.btAlterarNCMItens.GridControl = null;
            this.btAlterarNCMItens.GridControlType = null;
            this.btAlterarNCMItens.GridControlTypeParams = null;
            this.btAlterarNCMItens.ImageOptions.Image = global::Aplicacao.Properties.Resources.Alterar_copy;
            this.btAlterarNCMItens.Location = new System.Drawing.Point(747, 309);
            this.btAlterarNCMItens.Name = "btAlterarNCMItens";
            this.btAlterarNCMItens.Size = new System.Drawing.Size(75, 23);
            this.btAlterarNCMItens.SubForm = null;
            this.btAlterarNCMItens.SubFormType = null;
            this.btAlterarNCMItens.SubFormTypeParams = null;
            this.btAlterarNCMItens.TabIndex = 12;
            this.btAlterarNCMItens.TabStop = false;
            this.btAlterarNCMItens.Text = "&Alterar";
            // 
            // btIncluirCest
            // 
            this.btIncluirCest.GridControl = null;
            this.btIncluirCest.GridControlType = null;
            this.btIncluirCest.GridControlTypeParams = null;
            this.btIncluirCest.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.btIncluirCest.Location = new System.Drawing.Point(666, 309);
            this.btIncluirCest.Name = "btIncluirCest";
            this.btIncluirCest.Size = new System.Drawing.Size(75, 23);
            this.btIncluirCest.SubForm = null;
            this.btIncluirCest.SubFormType = null;
            this.btIncluirCest.SubFormTypeParams = null;
            this.btIncluirCest.TabIndex = 11;
            this.btIncluirCest.TabStop = false;
            this.btIncluirCest.Text = "&Incluir";
            this.btIncluirCest.Click += new System.EventHandler(this.btIncluirCest_Click);
            // 
            // gcCestsVinculados
            // 
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.First.Hint = "Primeiro registro";
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.Last.Hint = "Último registro";
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.Next.Hint = "Próximo registro";
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.NextPage.Hint = "Próxima página";
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.Prev.Hint = "Registro anterior";
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.PrevPage.Hint = "Página anterior";
            this.gcCestsVinculados.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcCestsVinculados.EmbeddedNavigator.TextStringFormat = "Registro {0} de {1}";
            this.gcCestsVinculados.Location = new System.Drawing.Point(7, 112);
            this.gcCestsVinculados.MainView = this.gvCestsVinculados;
            this.gcCestsVinculados.Name = "gcCestsVinculados";
            this.gcCestsVinculados.Size = new System.Drawing.Size(896, 191);
            this.gcCestsVinculados.TabIndex = 9;
            this.gcCestsVinculados.UseEmbeddedNavigator = true;
            this.gcCestsVinculados.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCestsVinculados});
            // 
            // gvCestsVinculados
            // 
            this.gvCestsVinculados.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.Empty.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvCestsVinculados.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvCestsVinculados.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvCestsVinculados.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvCestsVinculados.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvCestsVinculados.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvCestsVinculados.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvCestsVinculados.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvCestsVinculados.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvCestsVinculados.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvCestsVinculados.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.OddRow.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvCestsVinculados.Appearance.OddRow.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvCestsVinculados.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.Preview.Options.UseFont = true;
            this.gvCestsVinculados.Appearance.Preview.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvCestsVinculados.Appearance.Row.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvCestsVinculados.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvCestsVinculados.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvCestsVinculados.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvCestsVinculados.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvCestsVinculados.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvCestsVinculados.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.codigo});
            this.gvCestsVinculados.GridControl = this.gcCestsVinculados;
            this.gvCestsVinculados.Name = "gvCestsVinculados";
            this.gvCestsVinculados.OptionsBehavior.Editable = false;
            this.gvCestsVinculados.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvCestsVinculados.OptionsPrint.PrintFooter = false;
            this.gvCestsVinculados.OptionsView.EnableAppearanceEvenRow = true;
            this.gvCestsVinculados.OptionsView.EnableAppearanceOddRow = true;
            this.gvCestsVinculados.OptionsView.ShowGroupPanel = false;
            // 
            // codigo
            // 
            this.codigo.Caption = "Código CEST";
            this.codigo.FieldName = "codigo";
            this.codigo.Name = "codigo";
            this.codigo.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.codigo.Visible = true;
            this.codigo.VisibleIndex = 0;
            // 
            // txtCodigo
            // 
            this.txtCodigo.CwkFuncaoValidacao = null;
            this.txtCodigo.CwkMascara = null;
            this.txtCodigo.CwkValidacao = ((Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao)((Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo | Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.MaiorQueZero)));
            this.txtCodigo.EditValue = 0;
            this.txtCodigo.Location = new System.Drawing.Point(67, 5);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Properties.Mask.EditMask = "d";
            this.txtCodigo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.txtCodigo.SelecionarTextoOnEnter = false;
            this.txtCodigo.Size = new System.Drawing.Size(40, 20);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.TabStop = false;
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(24, 8);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(37, 13);
            this.labelControl7.TabIndex = 0;
            this.labelControl7.Text = "Código:";
            // 
            // txtNcm
            // 
            this.txtNcm.CwkFuncaoValidacao = null;
            this.txtNcm.CwkMascara = null;
            this.txtNcm.CwkValidacao = Cwork.Utilitarios.Componentes.FuncoesValidacao.FuncaoValidacao.NaoNulo;
            this.txtNcm.Location = new System.Drawing.Point(67, 32);
            this.txtNcm.Name = "txtNcm";
            this.txtNcm.Properties.Appearance.Options.UseTextOptions = true;
            this.txtNcm.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtNcm.Properties.MaxLength = 8;
            this.txtNcm.SelecionarTextoOnEnter = true;
            this.txtNcm.Size = new System.Drawing.Size(115, 20);
            this.txtNcm.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "NCM:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Aliq. Cupom:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtAliqCupom
            // 
            this.txtAliqCupom.CwkFuncaoValidacao = null;
            this.txtAliqCupom.CwkMascara = null;
            this.txtAliqCupom.CwkValidacao = null;
            this.txtAliqCupom.Location = new System.Drawing.Point(281, 32);
            this.txtAliqCupom.Name = "txtAliqCupom";
            this.txtAliqCupom.Properties.Appearance.Options.UseTextOptions = true;
            this.txtAliqCupom.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.txtAliqCupom.Properties.Mask.EditMask = "[A-Z]{2}";
            this.txtAliqCupom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txtAliqCupom.Properties.Mask.ShowPlaceHolders = false;
            this.txtAliqCupom.Properties.MaxLength = 8;
            this.txtAliqCupom.SelecionarTextoOnEnter = true;
            this.txtAliqCupom.Size = new System.Drawing.Size(34, 20);
            this.txtAliqCupom.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Descrição:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDescricao
            // 
            this.txtDescricao.CwkFuncaoValidacao = null;
            this.txtDescricao.CwkMascara = null;
            this.txtDescricao.CwkValidacao = null;
            this.txtDescricao.EditValue = "";
            this.txtDescricao.Location = new System.Drawing.Point(67, 58);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Properties.MaxLength = 150;
            this.txtDescricao.SelecionarTextoOnEnter = true;
            this.txtDescricao.Size = new System.Drawing.Size(836, 20);
            this.txtDescricao.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(342, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Data Revogação:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtDtRevogacao
            // 
            this.txtDtRevogacao.EditValue = null;
            this.txtDtRevogacao.Location = new System.Drawing.Point(439, 32);
            this.txtDtRevogacao.Name = "txtDtRevogacao";
            this.txtDtRevogacao.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDtRevogacao.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.txtDtRevogacao.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.txtDtRevogacao.Size = new System.Drawing.Size(87, 20);
            this.txtDtRevogacao.TabIndex = 21;
            // 
            // FormNCM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(965, 411);
            this.Name = "FormNCM";
            this.Load += new System.EventHandler(this.FormNCM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.tpPrincipal.ResumeLayout(false);
            this.tpPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tcPrincipal)).EndInit();
            this.tcPrincipal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCestsVinculados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCestsVinculados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodigo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNcm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAliqCupom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescricao.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtRevogacao.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDtRevogacao.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cwork.Utilitarios.Componentes.DevButton btConsultarNCMItens;
        private Cwork.Utilitarios.Componentes.DevButton btExcluirCest;
        private Cwork.Utilitarios.Componentes.DevButton btAlterarNCMItens;
        private Cwork.Utilitarios.Componentes.DevButton btIncluirCest;
        public DevExpress.XtraGrid.GridControl gcCestsVinculados;
        public DevExpress.XtraGrid.Views.Grid.GridView gvCestsVinculados;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtCodigo;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtNcm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtAliqCupom;
        private Cwork.Utilitarios.Componentes.CwkBaseEditor txtDescricao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Cwork.Utilitarios.Componentes.DevDateEdit txtDtRevogacao;
        private DevExpress.XtraGrid.Columns.GridColumn codigo;
    }
}
