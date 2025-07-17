
namespace Aplicacao.Modulos.Utilitarios
{
    partial class GridPromocoes
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
            this.ovGRD_Promocoes = new DevExpress.XtraGrid.GridControl();
            this.ovGV_Promocoes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cardView2 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.sbExcluir = new DevExpress.XtraEditors.SimpleButton();
            this.sbAlterar = new DevExpress.XtraEditors.SimpleButton();
            this.sbIncluir = new DevExpress.XtraEditors.SimpleButton();
            this.sbConsultar = new DevExpress.XtraEditors.SimpleButton();
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.ovGRD_Promocoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ovGV_Promocoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView2)).BeginInit();
            this.SuspendLayout();
            // 
            // ovGRD_Promocoes
            // 
            this.ovGRD_Promocoes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ovGRD_Promocoes.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.ovGRD_Promocoes.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.ovGRD_Promocoes.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.ovGRD_Promocoes.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.ovGRD_Promocoes.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.ovGRD_Promocoes.Location = new System.Drawing.Point(12, 12);
            this.ovGRD_Promocoes.MainView = this.ovGV_Promocoes;
            this.ovGRD_Promocoes.Name = "ovGRD_Promocoes";
            this.ovGRD_Promocoes.Size = new System.Drawing.Size(776, 368);
            this.ovGRD_Promocoes.TabIndex = 44;
            this.ovGRD_Promocoes.UseEmbeddedNavigator = true;
            this.ovGRD_Promocoes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ovGV_Promocoes,
            this.cardView2});
            this.ovGRD_Promocoes.DoubleClick += new System.EventHandler(this.ovGRD_Promocoes_DoubleClick);
            // 
            // ovGV_Promocoes
            // 
            this.ovGV_Promocoes.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.Empty.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.EvenRow.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.EvenRow.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.EvenRow.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.ovGV_Promocoes.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.ovGV_Promocoes.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.ovGV_Promocoes.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.FilterPanel.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.FilterPanel.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.FixedLine.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.ovGV_Promocoes.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.FocusedCell.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.FocusedCell.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.ovGV_Promocoes.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.FocusedRow.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.FocusedRow.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.FooterPanel.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.FooterPanel.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.GroupButton.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.GroupButton.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.GroupFooter.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.GroupFooter.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.ovGV_Promocoes.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.GroupPanel.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.GroupPanel.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.GroupRow.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.ovGV_Promocoes.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.ovGV_Promocoes.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.ovGV_Promocoes.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.OddRow.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.OddRow.Options.UseBorderColor = true;
            this.ovGV_Promocoes.Appearance.OddRow.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.ovGV_Promocoes.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.Preview.Options.UseFont = true;
            this.ovGV_Promocoes.Appearance.Preview.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.ovGV_Promocoes.Appearance.Row.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.ovGV_Promocoes.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.RowSeparator.Options.UseBackColor = true;
            this.ovGV_Promocoes.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.SelectedRow.Options.UseForeColor = true;
            this.ovGV_Promocoes.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.ovGV_Promocoes.Appearance.TopNewRow.Options.UseBackColor = true;
            this.ovGV_Promocoes.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5});
            this.ovGV_Promocoes.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.ovGV_Promocoes.GridControl = this.ovGRD_Promocoes;
            this.ovGV_Promocoes.GroupPanelText = "Arraste uma coluna aqui para agrupar os registros";
            this.ovGV_Promocoes.Name = "ovGV_Promocoes";
            this.ovGV_Promocoes.OptionsBehavior.FocusLeaveOnTab = true;
            this.ovGV_Promocoes.OptionsDetail.EnableMasterViewMode = false;
            this.ovGV_Promocoes.OptionsView.EnableAppearanceEvenRow = true;
            this.ovGV_Promocoes.OptionsView.EnableAppearanceOddRow = true;
            this.ovGV_Promocoes.OptionsView.ShowAutoFilterRow = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "Nome";
            this.gridColumn1.FieldName = "Nome";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn1.OptionsColumn.ReadOnly = true;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 200;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tabela Preço";
            this.gridColumn2.FieldName = "TabelaPrecoFormatada";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 326;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Data Início";
            this.gridColumn3.FieldName = "DataInicio";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn3.OptionsColumn.ReadOnly = true;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 2;
            this.gridColumn3.Width = 86;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Data Fim";
            this.gridColumn4.FieldName = "DataFim";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn4.OptionsColumn.ReadOnly = true;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 86;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Percentual";
            this.gridColumn5.FieldName = "Percentual";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn5.OptionsColumn.ReadOnly = true;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 4;
            this.gridColumn5.Width = 60;
            // 
            // cardView2
            // 
            this.cardView2.GridControl = this.ovGRD_Promocoes;
            this.cardView2.Name = "cardView2";
            // 
            // sbExcluir
            // 
            this.sbExcluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbExcluir.ImageOptions.Image = global::Aplicacao.Properties.Resources.Excluir_copy;
            this.sbExcluir.Location = new System.Drawing.Point(713, 386);
            this.sbExcluir.Name = "sbExcluir";
            this.sbExcluir.Size = new System.Drawing.Size(75, 23);
            this.sbExcluir.TabIndex = 56;
            this.sbExcluir.Text = "Excluir";
            this.sbExcluir.Click += new System.EventHandler(this.sbExcluir_Click);
            // 
            // sbAlterar
            // 
            this.sbAlterar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbAlterar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Alterar_copy;
            this.sbAlterar.Location = new System.Drawing.Point(632, 386);
            this.sbAlterar.Name = "sbAlterar";
            this.sbAlterar.Size = new System.Drawing.Size(75, 23);
            this.sbAlterar.TabIndex = 55;
            this.sbAlterar.Text = "Alterar";
            this.sbAlterar.Click += new System.EventHandler(this.sbAlterar_Click);
            // 
            // sbIncluir
            // 
            this.sbIncluir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbIncluir.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.sbIncluir.Location = new System.Drawing.Point(551, 386);
            this.sbIncluir.Name = "sbIncluir";
            this.sbIncluir.Size = new System.Drawing.Size(75, 23);
            this.sbIncluir.TabIndex = 54;
            this.sbIncluir.Text = "Incluir";
            this.sbIncluir.Click += new System.EventHandler(this.sbIncluir_Click);
            // 
            // sbConsultar
            // 
            this.sbConsultar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbConsultar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Consulta_copy;
            this.sbConsultar.Location = new System.Drawing.Point(470, 386);
            this.sbConsultar.Name = "sbConsultar";
            this.sbConsultar.Size = new System.Drawing.Size(75, 23);
            this.sbConsultar.TabIndex = 53;
            this.sbConsultar.Text = "Consultar";
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
            this.sbFechar.TabIndex = 52;
            this.sbFechar.Text = "&Fechar";
            this.sbFechar.Click += new System.EventHandler(this.sbFechar_Click);
            // 
            // GridPromocoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.sbExcluir);
            this.Controls.Add(this.sbAlterar);
            this.Controls.Add(this.sbIncluir);
            this.Controls.Add(this.sbConsultar);
            this.Controls.Add(this.sbFechar);
            this.Controls.Add(this.ovGRD_Promocoes);
            this.Name = "GridPromocoes";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grid de Promoçoes";
            ((System.ComponentModel.ISupportInitialize)(this.ovGRD_Promocoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ovGV_Promocoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl ovGRD_Promocoes;
        private DevExpress.XtraGrid.Views.Grid.GridView ovGV_Promocoes;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.Card.CardView cardView2;
        private DevExpress.XtraEditors.SimpleButton sbExcluir;
        private DevExpress.XtraEditors.SimpleButton sbAlterar;
        private DevExpress.XtraEditors.SimpleButton sbIncluir;
        private DevExpress.XtraEditors.SimpleButton sbConsultar;
        public DevExpress.XtraEditors.SimpleButton sbFechar;
    }
}