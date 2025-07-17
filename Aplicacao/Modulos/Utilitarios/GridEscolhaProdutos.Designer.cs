
namespace Aplicacao.Modulos.Utilitarios
{
    partial class GridEscolhaProdutos
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
            this.gcProdutos = new DevExpress.XtraGrid.GridControl();
            this.gvProdutos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Selecionado = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Codigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Barra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Nome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.PrecoBase = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemGridLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemMemoEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.repositoryItemResourcesComboBox1 = new DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox();
            this.repositoryItemSearchLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemRichTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit();
            this.repositoryItemCheckEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProdutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemResourcesComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gcProdutos
            // 
            this.gcProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcProdutos.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcProdutos.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcProdutos.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcProdutos.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcProdutos.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcProdutos.Location = new System.Drawing.Point(12, 12);
            this.gcProdutos.MainView = this.gvProdutos;
            this.gcProdutos.Name = "gcProdutos";
            this.gcProdutos.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2,
            this.repositoryItemComboBox1,
            this.repositoryItemGridLookUpEdit1,
            this.repositoryItemMemoEdit1,
            this.repositoryItemResourcesComboBox1,
            this.repositoryItemSearchLookUpEdit1,
            this.repositoryItemRichTextEdit1,
            this.repositoryItemCheckEdit3,
            this.repositoryItemCheckEdit1,
            this.repositoryItemCheckEdit4,
            this.repositoryItemCalcEdit1,
            this.repositoryItemSpinEdit1});
            this.gcProdutos.Size = new System.Drawing.Size(776, 397);
            this.gcProdutos.TabIndex = 36;
            this.gcProdutos.UseEmbeddedNavigator = true;
            this.gcProdutos.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProdutos,
            this.cardView1});
            // 
            // gvProdutos
            // 
            this.gvProdutos.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvProdutos.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvProdutos.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvProdutos.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvProdutos.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.Empty.Options.UseBackColor = true;
            this.gvProdutos.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvProdutos.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvProdutos.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvProdutos.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvProdutos.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvProdutos.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvProdutos.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvProdutos.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvProdutos.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvProdutos.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvProdutos.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvProdutos.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProdutos.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvProdutos.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvProdutos.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProdutos.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvProdutos.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvProdutos.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvProdutos.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvProdutos.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvProdutos.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvProdutos.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvProdutos.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvProdutos.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvProdutos.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvProdutos.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvProdutos.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvProdutos.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvProdutos.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvProdutos.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvProdutos.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvProdutos.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvProdutos.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvProdutos.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvProdutos.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.OddRow.Options.UseBackColor = true;
            this.gvProdutos.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvProdutos.Appearance.OddRow.Options.UseForeColor = true;
            this.gvProdutos.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvProdutos.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.Preview.Options.UseFont = true;
            this.gvProdutos.Appearance.Preview.Options.UseForeColor = true;
            this.gvProdutos.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvProdutos.Appearance.Row.Options.UseBackColor = true;
            this.gvProdutos.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvProdutos.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvProdutos.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvProdutos.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvProdutos.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvProdutos.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvProdutos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Selecionado,
            this.ID,
            this.Codigo,
            this.Barra,
            this.Nome,
            this.PrecoBase});
            this.gvProdutos.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gvProdutos.GridControl = this.gcProdutos;
            this.gvProdutos.GroupPanelText = "Arraste uma coluna aqui para agrupar os registros";
            this.gvProdutos.Name = "gvProdutos";
            this.gvProdutos.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvProdutos.OptionsCustomization.AllowSort = false;
            this.gvProdutos.OptionsDetail.EnableMasterViewMode = false;
            this.gvProdutos.OptionsView.EnableAppearanceEvenRow = true;
            this.gvProdutos.OptionsView.EnableAppearanceOddRow = true;
            this.gvProdutos.OptionsView.ShowAutoFilterRow = true;
            this.gvProdutos.OptionsView.ShowGroupPanel = false;
            // 
            // Selecionado
            // 
            this.Selecionado.ColumnEdit = this.repositoryItemCheckEdit1;
            this.Selecionado.FieldName = "Selecionado";
            this.Selecionado.MinWidth = 80;
            this.Selecionado.Name = "Selecionado";
            this.Selecionado.Visible = true;
            this.Selecionado.VisibleIndex = 0;
            this.Selecionado.Width = 80;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            this.ID.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            // 
            // Codigo
            // 
            this.Codigo.Caption = "Código";
            this.Codigo.FieldName = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.OptionsColumn.AllowEdit = false;
            this.Codigo.Visible = true;
            this.Codigo.VisibleIndex = 1;
            this.Codigo.Width = 59;
            // 
            // Barra
            // 
            this.Barra.Caption = "Código de Barras";
            this.Barra.FieldName = "Barra";
            this.Barra.Name = "Barra";
            this.Barra.OptionsColumn.AllowEdit = false;
            this.Barra.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.Barra.Visible = true;
            this.Barra.VisibleIndex = 2;
            this.Barra.Width = 134;
            // 
            // Nome
            // 
            this.Nome.Caption = "Descrição";
            this.Nome.FieldName = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.OptionsColumn.AllowEdit = false;
            this.Nome.Visible = true;
            this.Nome.VisibleIndex = 3;
            this.Nome.Width = 439;
            // 
            // PrecoBase
            // 
            this.PrecoBase.Caption = "Preço Base";
            this.PrecoBase.ColumnEdit = this.repositoryItemSpinEdit1;
            this.PrecoBase.FieldName = "PrecoBase";
            this.PrecoBase.Name = "PrecoBase";
            this.PrecoBase.OptionsColumn.AllowEdit = false;
            this.PrecoBase.Visible = true;
            this.PrecoBase.VisibleIndex = 4;
            this.PrecoBase.Width = 120;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // repositoryItemGridLookUpEdit1
            // 
            this.repositoryItemGridLookUpEdit1.AutoHeight = false;
            this.repositoryItemGridLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemGridLookUpEdit1.Name = "repositoryItemGridLookUpEdit1";
            this.repositoryItemGridLookUpEdit1.PopupView = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemMemoEdit1
            // 
            this.repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemResourcesComboBox1
            // 
            this.repositoryItemResourcesComboBox1.AutoHeight = false;
            this.repositoryItemResourcesComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemResourcesComboBox1.Name = "repositoryItemResourcesComboBox1";
            // 
            // repositoryItemSearchLookUpEdit1
            // 
            this.repositoryItemSearchLookUpEdit1.AutoHeight = false;
            this.repositoryItemSearchLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSearchLookUpEdit1.Name = "repositoryItemSearchLookUpEdit1";
            this.repositoryItemSearchLookUpEdit1.PopupView = this.gridView2;
            // 
            // gridView2
            // 
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemRichTextEdit1
            // 
            this.repositoryItemRichTextEdit1.Name = "repositoryItemRichTextEdit1";
            this.repositoryItemRichTextEdit1.ShowCaretInReadOnly = false;
            // 
            // repositoryItemCheckEdit3
            // 
            this.repositoryItemCheckEdit3.AutoHeight = false;
            this.repositoryItemCheckEdit3.Name = "repositoryItemCheckEdit3";
            // 
            // repositoryItemCheckEdit4
            // 
            this.repositoryItemCheckEdit4.AutoHeight = false;
            this.repositoryItemCheckEdit4.Name = "repositoryItemCheckEdit4";
            // 
            // repositoryItemCalcEdit1
            // 
            this.repositoryItemCalcEdit1.AutoHeight = false;
            this.repositoryItemCalcEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemCalcEdit1.Name = "repositoryItemCalcEdit1";
            // 
            // cardView1
            // 
            this.cardView1.GridControl = this.gcProdutos;
            this.cardView1.Name = "cardView1";
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.ImageOptions.Image = global::Aplicacao.Properties.Resources.Imprimir_Cartão_Ponto_copy;
            this.simpleButton2.ImageOptions.ImageIndex = 1;
            this.simpleButton2.Location = new System.Drawing.Point(632, 415);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 23);
            this.simpleButton2.TabIndex = 40;
            this.simpleButton2.Text = "Salvar";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // sbFechar
            // 
            this.sbFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbFechar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Fechar;
            this.sbFechar.ImageOptions.ImageIndex = 2;
            this.sbFechar.Location = new System.Drawing.Point(713, 415);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(75, 23);
            this.sbFechar.TabIndex = 39;
            this.sbFechar.Text = "&Fechar";
            this.sbFechar.Click += new System.EventHandler(this.sbFechar_Click);
            // 
            // GridEscolhaProdutos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.sbFechar);
            this.Controls.Add(this.gcProdutos);
            this.Name = "GridEscolhaProdutos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid Escolha de Produtos";
            ((System.ComponentModel.ISupportInitialize)(this.gcProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvProdutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemMemoEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemResourcesComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSearchLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRichTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gcProdutos;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProdutos;
        private DevExpress.XtraGrid.Columns.GridColumn Selecionado;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn PrecoBase;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraGrid.Columns.GridColumn Codigo;
        private DevExpress.XtraGrid.Columns.GridColumn Barra;
        private DevExpress.XtraGrid.Columns.GridColumn Nome;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit repositoryItemGridLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit repositoryItemMemoEdit1;
        private DevExpress.XtraScheduler.UI.RepositoryItemResourcesComboBox repositoryItemResourcesComboBox1;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repositoryItemSearchLookUpEdit1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraEditors.Repository.RepositoryItemRichTextEdit repositoryItemRichTextEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit3;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit4;
        private DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit repositoryItemCalcEdit1;
        private DevExpress.XtraGrid.Views.Card.CardView cardView1;
        public DevExpress.XtraEditors.SimpleButton simpleButton2;
        public DevExpress.XtraEditors.SimpleButton sbFechar;
    }
}