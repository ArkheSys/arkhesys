
namespace Aplicacao.Modulos.Comercial
{
    partial class FormConsultaCreditoCliente
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
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            this.gcValoresCreditoClientes = new DevExpress.XtraGrid.GridControl();
            this.gvValoresCreditoClientes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
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
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCheckEdit4 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.repositoryItemCalcEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCalcEdit();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.cardView1 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.chbbTodos = new DevExpress.XtraEditors.CheckEdit();
            this.btAdPessoa = new Componentes.devexpress.cwk_DevButton();
            this.cbPessoa = new Componentes.devexpress.cwk_DevLookup();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.gcValoresCreditoClientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvValoresCreditoClientes)).BeginInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbbTodos.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPessoa.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbFechar
            // 
            this.sbFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbFechar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Fechar;
            this.sbFechar.ImageOptions.ImageIndex = 2;
            this.sbFechar.Location = new System.Drawing.Point(801, 531);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(75, 23);
            this.sbFechar.TabIndex = 27;
            this.sbFechar.Text = "&Fechar";
            // 
            // gcValoresCreditoClientes
            // 
            this.gcValoresCreditoClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcValoresCreditoClientes.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcValoresCreditoClientes.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcValoresCreditoClientes.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcValoresCreditoClientes.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcValoresCreditoClientes.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcValoresCreditoClientes.Location = new System.Drawing.Point(12, 38);
            this.gcValoresCreditoClientes.MainView = this.gvValoresCreditoClientes;
            this.gcValoresCreditoClientes.Name = "gcValoresCreditoClientes";
            this.gcValoresCreditoClientes.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
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
            this.gcValoresCreditoClientes.Size = new System.Drawing.Size(864, 487);
            this.gcValoresCreditoClientes.TabIndex = 37;
            this.gcValoresCreditoClientes.UseEmbeddedNavigator = true;
            this.gcValoresCreditoClientes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvValoresCreditoClientes,
            this.cardView1});
            // 
            // gvValoresCreditoClientes
            // 
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.Empty.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvValoresCreditoClientes.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvValoresCreditoClientes.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvValoresCreditoClientes.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvValoresCreditoClientes.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvValoresCreditoClientes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvValoresCreditoClientes.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvValoresCreditoClientes.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvValoresCreditoClientes.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvValoresCreditoClientes.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.OddRow.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvValoresCreditoClientes.Appearance.OddRow.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvValoresCreditoClientes.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.Preview.Options.UseFont = true;
            this.gvValoresCreditoClientes.Appearance.Preview.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvValoresCreditoClientes.Appearance.Row.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvValoresCreditoClientes.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvValoresCreditoClientes.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvValoresCreditoClientes.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvValoresCreditoClientes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4});
            this.gvValoresCreditoClientes.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gvValoresCreditoClientes.GridControl = this.gcValoresCreditoClientes;
            this.gvValoresCreditoClientes.GroupPanelText = "Arraste uma coluna aqui para agrupar os registros";
            this.gvValoresCreditoClientes.Name = "gvValoresCreditoClientes";
            this.gvValoresCreditoClientes.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvValoresCreditoClientes.OptionsCustomization.AllowSort = false;
            this.gvValoresCreditoClientes.OptionsDetail.EnableMasterViewMode = false;
            this.gvValoresCreditoClientes.OptionsView.EnableAppearanceEvenRow = true;
            this.gvValoresCreditoClientes.OptionsView.EnableAppearanceOddRow = true;
            this.gvValoresCreditoClientes.OptionsView.ShowAutoFilterRow = true;
            this.gvValoresCreditoClientes.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Codigo";
            this.gridColumn1.FieldName = "Codigo";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 136;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Nome";
            this.gridColumn2.FieldName = "Nome";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 476;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "CPF/CNPJ";
            this.gridColumn3.FieldName = "CNPJ_CPF";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 150;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Saldo";
            this.gridColumn4.FieldName = "SaldoFormatado";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 84;
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
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
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
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // cardView1
            // 
            this.cardView1.GridControl = this.gcValoresCreditoClientes;
            this.cardView1.Name = "cardView1";
            // 
            // chbbTodos
            // 
            this.chbbTodos.Location = new System.Drawing.Point(822, 13);
            this.chbbTodos.Name = "chbbTodos";
            this.chbbTodos.Properties.Caption = "Todos";
            this.chbbTodos.Size = new System.Drawing.Size(54, 19);
            this.chbbTodos.TabIndex = 41;
            // 
            // btAdPessoa
            // 
            this.btAdPessoa.Location = new System.Drawing.Point(792, 12);
            this.btAdPessoa.Name = "btAdPessoa";
            this.btAdPessoa.Size = new System.Drawing.Size(24, 20);
            this.btAdPessoa.TabIndex = 40;
            this.btAdPessoa.TabStop = false;
            this.btAdPessoa.Text = "...";
            // 
            // cbPessoa
            // 
            this.cbPessoa.ButtonLookup = this.btAdPessoa;
            this.cbPessoa.Key = System.Windows.Forms.Keys.F5;
            this.cbPessoa.Location = new System.Drawing.Point(55, 12);
            this.cbPessoa.Name = "cbPessoa";
            this.cbPessoa.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPessoa.Properties.NullText = "";
            this.cbPessoa.Size = new System.Drawing.Size(731, 20);
            this.cbPessoa.TabIndex = 39;
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(12, 15);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(37, 13);
            this.labelControl8.TabIndex = 38;
            this.labelControl8.Text = "Cliente:";
            // 
            // FormConsultaCreditoCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 566);
            this.Controls.Add(this.chbbTodos);
            this.Controls.Add(this.btAdPessoa);
            this.Controls.Add(this.cbPessoa);
            this.Controls.Add(this.labelControl8);
            this.Controls.Add(this.gcValoresCreditoClientes);
            this.Controls.Add(this.sbFechar);
            this.Name = "FormConsultaCreditoCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta Valores em Crédito de Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.gcValoresCreditoClientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvValoresCreditoClientes)).EndInit();
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
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCalcEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chbbTodos.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbPessoa.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.SimpleButton sbFechar;
        private DevExpress.XtraGrid.GridControl gcValoresCreditoClientes;
        private DevExpress.XtraGrid.Views.Grid.GridView gvValoresCreditoClientes;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
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
        private DevExpress.XtraEditors.CheckEdit chbbTodos;
#pragma warning disable CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning disable CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
        private Componentes.devexpress.cwk_DevButton btAdPessoa;
#pragma warning restore CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning restore CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning disable CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning disable CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
        private Componentes.devexpress.cwk_DevLookup cbPessoa;
#pragma warning restore CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning restore CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
    }
}