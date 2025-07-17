namespace Aplicacao.Modulos.Cadastro
{
    partial class FormImprimeEtiquetasProdutosPRN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImprimeEtiquetasProdutosPRN));
            this.gcProduto = new DevExpress.XtraGrid.GridControl();
            this.gvProduto = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridProdutoColumnCodigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnDescReduzida = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnUnidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnNomeGrupo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnCodBarra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnPreco = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridProdutoColumnQuantidade = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbLayoutEtiqueta = new DevExpress.XtraEditors.LabelControl();
            this.cbListaPRN = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lbProduto = new DevExpress.XtraEditors.LabelControl();
            this.btnAbrePastaLayout = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lkpImpresora = new Cwork.Utilitarios.Componentes.Lookup();
            this.lkbImpressora = new Cwork.Utilitarios.Componentes.LookupButton();
            this.cb_TabelaPreco = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.gridProdutoColumnEstoque = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbListaPRN.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpImpresora.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_TabelaPreco.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(827, 514);
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.labelControl2);
            this.xtraTabPage1.Controls.Add(this.cb_TabelaPreco);
            this.xtraTabPage1.Controls.Add(this.lkbImpressora);
            this.xtraTabPage1.Controls.Add(this.lkpImpresora);
            this.xtraTabPage1.Controls.Add(this.labelControl1);
            this.xtraTabPage1.Controls.Add(this.btnAbrePastaLayout);
            this.xtraTabPage1.Controls.Add(this.lbProduto);
            this.xtraTabPage1.Controls.Add(this.gcProduto);
            this.xtraTabPage1.Controls.Add(this.lbLayoutEtiqueta);
            this.xtraTabPage1.Controls.Add(this.cbListaPRN);
            this.xtraTabPage1.Size = new System.Drawing.Size(821, 508);
            // 
            // sbCancelar
            // 
            this.sbCancelar.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbCancelar.ImageOptions.Image")));
            this.sbCancelar.ImageOptions.ImageIndex = 2;
            this.sbCancelar.Location = new System.Drawing.Point(759, 527);
            this.sbCancelar.TabIndex = 9;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.Images.SetKeyName(0, "Help copy.ico");
            this.imageList1.Images.SetKeyName(1, "Gravar copy.ico");
            this.imageList1.Images.SetKeyName(2, "cancelar copy.ico");
            this.imageList1.Images.SetKeyName(3, "Inserir copy.ico");
            this.imageList1.Images.SetKeyName(4, "Alterar copy.ico");
            this.imageList1.Images.SetKeyName(5, "Excluir copy.ico");
            this.imageList1.Images.SetKeyName(6, "Consulta copy.ico");
            this.imageList1.Images.SetKeyName(7, "Selecionar copy.ico");
            // 
            // sbGravar
            // 
            this.sbGravar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Imprimir_Cartão_Ponto_copy;
            this.sbGravar.ImageOptions.ImageIndex = 1;
            this.sbGravar.Location = new System.Drawing.Point(678, 527);
            this.sbGravar.TabIndex = 8;
            this.sbGravar.Text = "&Imprimir";
            // 
            // sbAjuda
            // 
            this.sbAjuda.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("sbAjuda.ImageOptions.Image")));
            this.sbAjuda.ImageOptions.ImageIndex = 0;
            this.sbAjuda.Location = new System.Drawing.Point(12, 527);
            // 
            // gcProduto
            // 
            this.gcProduto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcProduto.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcProduto.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcProduto.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcProduto.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcProduto.EmbeddedNavigator.Buttons.First.Hint = "Primeiro registro";
            this.gcProduto.EmbeddedNavigator.Buttons.Last.Hint = "Último registro";
            this.gcProduto.EmbeddedNavigator.Buttons.Next.Hint = "Próximo registro";
            this.gcProduto.EmbeddedNavigator.Buttons.NextPage.Hint = "Próxima página";
            this.gcProduto.EmbeddedNavigator.Buttons.Prev.Hint = "Registro anterior";
            this.gcProduto.EmbeddedNavigator.Buttons.PrevPage.Hint = "Página anterior";
            this.gcProduto.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcProduto.EmbeddedNavigator.TextStringFormat = "Registro {0} de {1}";
            this.gcProduto.Location = new System.Drawing.Point(76, 85);
            this.gcProduto.MainView = this.gvProduto;
            this.gcProduto.Name = "gcProduto";
            this.gcProduto.Size = new System.Drawing.Size(730, 420);
            this.gcProduto.TabIndex = 7;
            this.gcProduto.UseEmbeddedNavigator = true;
            this.gcProduto.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProduto});
            // 
            // gvProduto
            // 
            this.gvProduto.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvProduto.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvProduto.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvProduto.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvProduto.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvProduto.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvProduto.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvProduto.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvProduto.Appearance.Empty.Options.UseBackColor = true;
            this.gvProduto.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvProduto.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvProduto.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvProduto.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvProduto.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvProduto.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvProduto.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvProduto.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvProduto.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvProduto.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvProduto.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvProduto.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvProduto.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvProduto.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvProduto.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvProduto.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvProduto.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProduto.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvProduto.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvProduto.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProduto.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProduto.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvProduto.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvProduto.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvProduto.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvProduto.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvProduto.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvProduto.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvProduto.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvProduto.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvProduto.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvProduto.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvProduto.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvProduto.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvProduto.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvProduto.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvProduto.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvProduto.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvProduto.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvProduto.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvProduto.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvProduto.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvProduto.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProduto.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvProduto.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gvProduto.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvProduto.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.OddRow.Options.UseBackColor = true;
            this.gvProduto.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvProduto.Appearance.OddRow.Options.UseForeColor = true;
            this.gvProduto.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvProduto.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.Preview.Options.UseFont = true;
            this.gvProduto.Appearance.Preview.Options.UseForeColor = true;
            this.gvProduto.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProduto.Appearance.Row.Options.UseBackColor = true;
            this.gvProduto.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvProduto.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvProduto.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProduto.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvProduto.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvProduto.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvProduto.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvProduto.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvProduto.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridProdutoColumnEstoque,
            this.gridProdutoColumnCodigo,
            this.gridProdutoColumnNome,
            this.gridProdutoColumnDescReduzida,
            this.gridProdutoColumnUnidade,
            this.gridProdutoColumnNomeGrupo,
            this.gridProdutoColumnCodBarra,
            this.gridProdutoColumnPreco,
            this.gridProdutoColumnQuantidade});
            this.gvProduto.GridControl = this.gcProduto;
            this.gvProduto.Name = "gvProduto";
            this.gvProduto.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvProduto.OptionsSelection.MultiSelect = true;
            this.gvProduto.OptionsView.ColumnAutoWidth = false;
            this.gvProduto.OptionsView.EnableAppearanceEvenRow = true;
            this.gvProduto.OptionsView.EnableAppearanceOddRow = true;
            this.gvProduto.OptionsView.ShowAutoFilterRow = true;
            this.gvProduto.CustomDrawGroupPanel += new DevExpress.XtraGrid.Views.Base.CustomDrawEventHandler(this.gvProduto_CustomDrawGroupPanel);
            // 
            // gridProdutoColumnCodigo
            // 
            this.gridProdutoColumnCodigo.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnCodigo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnCodigo.Caption = "Código";
            this.gridProdutoColumnCodigo.FieldName = "Codigo";
            this.gridProdutoColumnCodigo.Name = "gridProdutoColumnCodigo";
            this.gridProdutoColumnCodigo.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnCodigo.Visible = true;
            this.gridProdutoColumnCodigo.VisibleIndex = 2;
            this.gridProdutoColumnCodigo.Width = 110;
            // 
            // gridProdutoColumnNome
            // 
            this.gridProdutoColumnNome.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnNome.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnNome.Caption = "Nome";
            this.gridProdutoColumnNome.FieldName = "NomeProduto";
            this.gridProdutoColumnNome.Name = "gridProdutoColumnNome";
            this.gridProdutoColumnNome.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnNome.Visible = true;
            this.gridProdutoColumnNome.VisibleIndex = 3;
            this.gridProdutoColumnNome.Width = 241;
            // 
            // gridProdutoColumnDescReduzida
            // 
            this.gridProdutoColumnDescReduzida.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnDescReduzida.AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gridProdutoColumnDescReduzida.Caption = "Descrição Reduzida";
            this.gridProdutoColumnDescReduzida.FieldName = "Reduzida";
            this.gridProdutoColumnDescReduzida.Name = "gridProdutoColumnDescReduzida";
            this.gridProdutoColumnDescReduzida.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnDescReduzida.Visible = true;
            this.gridProdutoColumnDescReduzida.VisibleIndex = 4;
            this.gridProdutoColumnDescReduzida.Width = 224;
            // 
            // gridProdutoColumnUnidade
            // 
            this.gridProdutoColumnUnidade.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnUnidade.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnUnidade.Caption = "Unidade";
            this.gridProdutoColumnUnidade.FieldName = "Unidade";
            this.gridProdutoColumnUnidade.Name = "gridProdutoColumnUnidade";
            this.gridProdutoColumnUnidade.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnUnidade.Visible = true;
            this.gridProdutoColumnUnidade.VisibleIndex = 5;
            this.gridProdutoColumnUnidade.Width = 54;
            // 
            // gridProdutoColumnNomeGrupo
            // 
            this.gridProdutoColumnNomeGrupo.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnNomeGrupo.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnNomeGrupo.Caption = "Grupo Estoque";
            this.gridProdutoColumnNomeGrupo.FieldName = "NomeGrupoEstoque";
            this.gridProdutoColumnNomeGrupo.Name = "gridProdutoColumnNomeGrupo";
            this.gridProdutoColumnNomeGrupo.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnNomeGrupo.Visible = true;
            this.gridProdutoColumnNomeGrupo.VisibleIndex = 6;
            this.gridProdutoColumnNomeGrupo.Width = 154;
            // 
            // gridProdutoColumnCodBarra
            // 
            this.gridProdutoColumnCodBarra.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnCodBarra.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnCodBarra.Caption = "Barra";
            this.gridProdutoColumnCodBarra.FieldName = "CodBarra";
            this.gridProdutoColumnCodBarra.Name = "gridProdutoColumnCodBarra";
            this.gridProdutoColumnCodBarra.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnCodBarra.Visible = true;
            this.gridProdutoColumnCodBarra.VisibleIndex = 7;
            this.gridProdutoColumnCodBarra.Width = 88;
            // 
            // gridProdutoColumnPreco
            // 
            this.gridProdutoColumnPreco.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnPreco.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnPreco.Caption = "Preço";
            this.gridProdutoColumnPreco.DisplayFormat.FormatString = "c2";
            this.gridProdutoColumnPreco.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridProdutoColumnPreco.FieldName = "Preco";
            this.gridProdutoColumnPreco.Name = "gridProdutoColumnPreco";
            this.gridProdutoColumnPreco.OptionsColumn.AllowEdit = false;
            this.gridProdutoColumnPreco.Visible = true;
            this.gridProdutoColumnPreco.VisibleIndex = 8;
            this.gridProdutoColumnPreco.Width = 100;
            // 
            // gridProdutoColumnQuantidade
            // 
            this.gridProdutoColumnQuantidade.AppearanceHeader.Options.UseTextOptions = true;
            this.gridProdutoColumnQuantidade.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridProdutoColumnQuantidade.Caption = "Quantidade";
            this.gridProdutoColumnQuantidade.FieldName = "QtdEtiquetas";
            this.gridProdutoColumnQuantidade.Name = "gridProdutoColumnQuantidade";
            this.gridProdutoColumnQuantidade.Visible = true;
            this.gridProdutoColumnQuantidade.VisibleIndex = 1;
            this.gridProdutoColumnQuantidade.Width = 68;
            // 
            // lbLayoutEtiqueta
            // 
            this.lbLayoutEtiqueta.Location = new System.Drawing.Point(33, 10);
            this.lbLayoutEtiqueta.Name = "lbLayoutEtiqueta";
            this.lbLayoutEtiqueta.Size = new System.Drawing.Size(37, 13);
            this.lbLayoutEtiqueta.TabIndex = 1;
            this.lbLayoutEtiqueta.Text = "Layout:";
            // 
            // cbListaPRN
            // 
            this.cbListaPRN.Location = new System.Drawing.Point(76, 7);
            this.cbListaPRN.Name = "cbListaPRN";
            this.cbListaPRN.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbListaPRN.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbListaPRN.Size = new System.Drawing.Size(698, 20);
            this.cbListaPRN.TabIndex = 2;
            this.cbListaPRN.Enter += new System.EventHandler(this.cbListaPRN_Enter);
            // 
            // lbProduto
            // 
            this.lbProduto.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProduto.Appearance.Options.UseFont = true;
            this.lbProduto.Location = new System.Drawing.Point(76, 62);
            this.lbProduto.Name = "lbProduto";
            this.lbProduto.Size = new System.Drawing.Size(69, 17);
            this.lbProduto.TabIndex = 6;
            this.lbProduto.Text = "Produtos:";
            // 
            // btnAbrePastaLayout
            // 
            this.btnAbrePastaLayout.ImageOptions.Image = global::Aplicacao.Properties.Resources.folder1;
            this.btnAbrePastaLayout.Location = new System.Drawing.Point(780, 5);
            this.btnAbrePastaLayout.Name = "btnAbrePastaLayout";
            this.btnAbrePastaLayout.Size = new System.Drawing.Size(26, 23);
            this.btnAbrePastaLayout.TabIndex = 3;
            this.btnAbrePastaLayout.Click += new System.EventHandler(this.btnAbrePastaLayout_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(371, 39);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(58, 13);
            this.labelControl1.TabIndex = 8;
            this.labelControl1.Text = "Impressora:";
            // 
            // lkpImpresora
            // 
            this.lkpImpresora.ButtonLookup = null;
            this.lkpImpresora.CamposPesquisa = new string[] {
        "Codigo",
        "Nome"};
            this.lkpImpresora.CamposRestricoesAND = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpImpresora.CamposRestricoesAND")));
            this.lkpImpresora.CamposRestricoesNOT = null;
            this.lkpImpresora.CamposRestricoesOR = ((System.Collections.Generic.List<string>)(resources.GetObject("lkpImpresora.CamposRestricoesOR")));
            this.lkpImpresora.ColunaDescricao = new string[] {
        "Codigo",
        "Nome"};
            this.lkpImpresora.ColunaTamanho = new string[] {
        null,
        null,
        null,
        null,
        null};
            this.lkpImpresora.ContextoLinq = null;
            this.lkpImpresora.CwkFuncaoValidacao = null;
            this.lkpImpresora.CwkMascara = null;
            this.lkpImpresora.CwkValidacao = null;
            this.lkpImpresora.Exemplo = null;
            this.lkpImpresora.ID = 0;
            this.lkpImpresora.Join = null;
            this.lkpImpresora.Key = System.Windows.Forms.Keys.F5;
            this.lkpImpresora.Location = new System.Drawing.Point(435, 36);
            this.lkpImpresora.Name = "lkpImpresora";
            this.lkpImpresora.OnIDChanged = null;
            this.lkpImpresora.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpImpresora.Properties.Appearance.Options.UseBackColor = true;
            this.lkpImpresora.SelecionarTextoOnEnter = true;
            this.lkpImpresora.Size = new System.Drawing.Size(339, 20);
            this.lkpImpresora.Tabela = null;
            this.lkpImpresora.TabIndex = 9;
            this.lkpImpresora.TituloTelaPesquisa = null;
            this.lkpImpresora.ToolTip = "Campos pesquisados: Codigo, Nome.";
            this.lkpImpresora.Where = null;
            // 
            // lkbImpressora
            // 
            this.lkbImpressora.Location = new System.Drawing.Point(780, 36);
            this.lkbImpressora.Lookup = null;
            this.lkbImpressora.Name = "lkbImpressora";
            this.lkbImpressora.Size = new System.Drawing.Size(24, 20);
            this.lkbImpressora.SubForm = null;
            this.lkbImpressora.SubFormType = null;
            this.lkbImpressora.SubFormTypeParams = null;
            this.lkbImpressora.TabIndex = 10;
            this.lkbImpressora.TabStop = false;
            this.lkbImpressora.Text = "...";
            this.lkbImpressora.Click += new System.EventHandler(this.lkbImpressora_Click);
            // 
            // cb_TabelaPreco
            // 
            this.cb_TabelaPreco.Location = new System.Drawing.Point(76, 36);
            this.cb_TabelaPreco.Name = "cb_TabelaPreco";
            this.cb_TabelaPreco.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cb_TabelaPreco.Size = new System.Drawing.Size(270, 20);
            this.cb_TabelaPreco.TabIndex = 39;
            this.cb_TabelaPreco.SelectedIndexChanged += new System.EventHandler(this.cb_TabelaPreco_SelectedIndexChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(4, 39);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(66, 13);
            this.labelControl2.TabIndex = 40;
            this.labelControl2.Text = "Tabela Preço:";
            // 
            // gridProdutoColumnEstoque
            // 
            this.gridProdutoColumnEstoque.Caption = "Selecionar Tudo";
            this.gridProdutoColumnEstoque.FieldName = "Selecionado";
            this.gridProdutoColumnEstoque.Name = "gridProdutoColumnEstoque";
            this.gridProdutoColumnEstoque.Visible = true;
            this.gridProdutoColumnEstoque.VisibleIndex = 0;
            this.gridProdutoColumnEstoque.Width = 90;
            // 
            // FormImprimeEtiquetasProdutosPRN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 557);
            this.Name = "FormImprimeEtiquetasProdutosPRN";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Imprimir Etiqueta Formato PRN";
            this.Shown += new System.EventHandler(this.FormImprimeEtiquetasProdutosPRN_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            this.xtraTabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dxErroProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbListaPRN.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpImpresora.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cb_TabelaPreco.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gcProduto;
        public DevExpress.XtraGrid.Views.Grid.GridView gvProduto;
        private DevExpress.XtraEditors.LabelControl lbLayoutEtiqueta;
        private DevExpress.XtraEditors.ComboBoxEdit cbListaPRN;
        private DevExpress.XtraEditors.LabelControl lbProduto;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnCodigo;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnNome;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnUnidade;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnNomeGrupo;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnCodBarra;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnPreco;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnQuantidade;
        private DevExpress.XtraEditors.SimpleButton btnAbrePastaLayout;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnDescReduzida;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private Cwork.Utilitarios.Componentes.Lookup lkpImpresora;
        private Cwork.Utilitarios.Componentes.LookupButton lkbImpressora;
        private DevExpress.XtraEditors.ComboBoxEdit cb_TabelaPreco;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraGrid.Columns.GridColumn gridProdutoColumnEstoque;
    }
}