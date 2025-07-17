
namespace Aplicacao.Modulos.Utilitarios
{
    partial class FormPromocao
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
            this.sbSalvar = new DevExpress.XtraEditors.SimpleButton();
            this.sbFechar = new DevExpress.XtraEditors.SimpleButton();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.ovGRD_Promocoes = new DevExpress.XtraGrid.GridControl();
            this.ovGV_Promocoes = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Codigo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Barra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.CodRef = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Nome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.cardView2 = new DevExpress.XtraGrid.Views.Card.CardView();
            this.rbDesconto = new System.Windows.Forms.RadioButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtDataFinal = new DevExpress.XtraEditors.DateEdit();
            this.lkbTabelaPreco = new Cwork.Utilitarios.Componentes.LookupButton();
            this.lkpTabelaPreco = new Cwork.Utilitarios.Componentes.Lookup();
            this.lblTabelaPreco = new DevExpress.XtraEditors.LabelControl();
            this.txtPercentual = new Cwork.Utilitarios.Componentes.DevCalcEdit();
            this.rbAumento = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDataInicial = new DevExpress.XtraEditors.DateEdit();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ovGRD_Promocoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ovGV_Promocoes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPreco.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPercentual.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sbSalvar
            // 
            this.sbSalvar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbSalvar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.sbSalvar.Location = new System.Drawing.Point(699, 401);
            this.sbSalvar.Name = "sbSalvar";
            this.sbSalvar.Size = new System.Drawing.Size(75, 23);
            this.sbSalvar.TabIndex = 56;
            this.sbSalvar.Text = "Salvar";
            this.sbSalvar.Click += new System.EventHandler(this.sbSalvar_Click);
            // 
            // sbFechar
            // 
            this.sbFechar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sbFechar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sbFechar.ImageOptions.Image = global::Aplicacao.Properties.Resources.Fechar;
            this.sbFechar.ImageOptions.ImageIndex = 2;
            this.sbFechar.Location = new System.Drawing.Point(780, 401);
            this.sbFechar.Name = "sbFechar";
            this.sbFechar.Size = new System.Drawing.Size(75, 23);
            this.sbFechar.TabIndex = 55;
            this.sbFechar.Text = "&Fechar";
            this.sbFechar.Click += new System.EventHandler(this.sbFechar_Click);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.simpleButton2);
            this.metroPanel1.Controls.Add(this.simpleButton1);
            this.metroPanel1.Controls.Add(this.ovGRD_Promocoes);
            this.metroPanel1.Controls.Add(this.rbDesconto);
            this.metroPanel1.Controls.Add(this.labelControl2);
            this.metroPanel1.Controls.Add(this.labelControl1);
            this.metroPanel1.Controls.Add(this.txtDataFinal);
            this.metroPanel1.Controls.Add(this.lkbTabelaPreco);
            this.metroPanel1.Controls.Add(this.lkpTabelaPreco);
            this.metroPanel1.Controls.Add(this.lblTabelaPreco);
            this.metroPanel1.Controls.Add(this.txtPercentual);
            this.metroPanel1.Controls.Add(this.rbAumento);
            this.metroPanel1.Controls.Add(this.label1);
            this.metroPanel1.Controls.Add(this.txtDataInicial);
            this.metroPanel1.Controls.Add(this.txtNome);
            this.metroPanel1.CustomBackground = false;
            this.metroPanel1.HorizontalScrollbar = false;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(12, 12);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(843, 383);
            this.metroPanel1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroPanel1.StyleManager = null;
            this.metroPanel1.TabIndex = 57;
            this.metroPanel1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroPanel1.VerticalScrollbar = false;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.simpleButton2.Location = new System.Drawing.Point(750, 92);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(86, 23);
            this.simpleButton2.TabIndex = 58;
            this.simpleButton2.Text = "Remover";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.simpleButton1.Location = new System.Drawing.Point(608, 92);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(136, 23);
            this.simpleButton1.TabIndex = 57;
            this.simpleButton1.Text = "Adicionar Produtos";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
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
            this.ovGRD_Promocoes.Location = new System.Drawing.Point(3, 121);
            this.ovGRD_Promocoes.MainView = this.ovGV_Promocoes;
            this.ovGRD_Promocoes.Name = "ovGRD_Promocoes";
            this.ovGRD_Promocoes.Size = new System.Drawing.Size(837, 259);
            this.ovGRD_Promocoes.TabIndex = 50;
            this.ovGRD_Promocoes.UseEmbeddedNavigator = true;
            this.ovGRD_Promocoes.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.ovGV_Promocoes,
            this.cardView2});
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
            this.Codigo,
            this.Barra,
            this.CodRef,
            this.Nome,
            this.gridColumn2,
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
            // Codigo
            // 
            this.Codigo.Caption = "Código";
            this.Codigo.FieldName = "Codigo";
            this.Codigo.Name = "Codigo";
            this.Codigo.OptionsColumn.AllowEdit = false;
            this.Codigo.Visible = true;
            this.Codigo.VisibleIndex = 0;
            this.Codigo.Width = 60;
            // 
            // Barra
            // 
            this.Barra.Caption = "Código de Barras";
            this.Barra.FieldName = "Barra";
            this.Barra.Name = "Barra";
            this.Barra.OptionsColumn.AllowEdit = false;
            this.Barra.Visible = true;
            this.Barra.VisibleIndex = 1;
            this.Barra.Width = 110;
            // 
            // CodRef
            // 
            this.CodRef.Caption = "Referencia";
            this.CodRef.FieldName = "CodRef";
            this.CodRef.Name = "CodRef";
            this.CodRef.OptionsColumn.AllowEdit = false;
            this.CodRef.Visible = true;
            this.CodRef.VisibleIndex = 2;
            this.CodRef.Width = 101;
            // 
            // Nome
            // 
            this.Nome.Caption = "Nome";
            this.Nome.FieldName = "Nome";
            this.Nome.Name = "Nome";
            this.Nome.OptionsColumn.AllowEdit = false;
            this.Nome.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.Nome.OptionsColumn.ReadOnly = true;
            this.Nome.Visible = true;
            this.Nome.VisibleIndex = 3;
            this.Nome.Width = 272;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Preço Base";
            this.gridColumn2.DisplayFormat.FormatString = "{0:c2}";
            this.gridColumn2.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn2.FieldName = "PrecoBase";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn2.OptionsColumn.ReadOnly = true;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 4;
            this.gridColumn2.Width = 110;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Preço Promocional";
            this.gridColumn5.DisplayFormat.FormatString = "{0:c2}";
            this.gridColumn5.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.gridColumn5.FieldName = "PrecoPromocao";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 110;
            // 
            // cardView2
            // 
            this.cardView2.GridControl = this.ovGRD_Promocoes;
            this.cardView2.Name = "cardView2";
            // 
            // rbDesconto
            // 
            this.rbDesconto.AutoSize = true;
            this.rbDesconto.BackColor = System.Drawing.Color.White;
            this.rbDesconto.Location = new System.Drawing.Point(446, 65);
            this.rbDesconto.Name = "rbDesconto";
            this.rbDesconto.Size = new System.Drawing.Size(97, 17);
            this.rbDesconto.TabIndex = 8;
            this.rbDesconto.TabStop = true;
            this.rbDesconto.Text = "% de Desconto";
            this.rbDesconto.UseVisualStyleBackColor = false;
            this.rbDesconto.CheckedChanged += new System.EventHandler(this.rbDesconto_CheckedChanged);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(416, 15);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(55, 13);
            this.labelControl2.TabIndex = 49;
            this.labelControl2.Text = "Percentual:";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(13, 67);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(40, 13);
            this.labelControl1.TabIndex = 48;
            this.labelControl1.Text = "Período:";
            // 
            // txtDataFinal
            // 
            this.txtDataFinal.EditValue = null;
            this.txtDataFinal.Location = new System.Drawing.Point(165, 64);
            this.txtDataFinal.Name = "txtDataFinal";
            this.txtDataFinal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataFinal.Size = new System.Drawing.Size(100, 20);
            this.txtDataFinal.TabIndex = 6;
            // 
            // lkbTabelaPreco
            // 
            this.lkbTabelaPreco.Location = new System.Drawing.Point(519, 38);
            this.lkbTabelaPreco.Lookup = null;
            this.lkbTabelaPreco.Name = "lkbTabelaPreco";
            this.lkbTabelaPreco.Size = new System.Drawing.Size(24, 20);
            this.lkbTabelaPreco.SubForm = null;
            this.lkbTabelaPreco.SubFormType = null;
            this.lkbTabelaPreco.SubFormTypeParams = null;
            this.lkbTabelaPreco.TabIndex = 4;
            this.lkbTabelaPreco.TabStop = false;
            this.lkbTabelaPreco.Text = "...";
            this.lkbTabelaPreco.Click += new System.EventHandler(this.lkbTabelaPreco_Click);
            // 
            // lkpTabelaPreco
            // 
            this.lkpTabelaPreco.ButtonLookup = this.lkbTabelaPreco;
            this.lkpTabelaPreco.CamposPesquisa = new string[] {
        "Nome",
        "Codigo"};
            this.lkpTabelaPreco.CamposRestricoesAND = null;
            this.lkpTabelaPreco.CamposRestricoesNOT = null;
            this.lkpTabelaPreco.CamposRestricoesOR = null;
            this.lkpTabelaPreco.ColunaDescricao = new string[] {
        "Nome",
        "Código"};
            this.lkpTabelaPreco.ColunaTamanho = new string[] {
        "150",
        "50"};
            this.lkpTabelaPreco.ContextoLinq = null;
            this.lkpTabelaPreco.CwkFuncaoValidacao = null;
            this.lkpTabelaPreco.CwkMascara = null;
            this.lkpTabelaPreco.CwkValidacao = null;
            this.lkpTabelaPreco.Exemplo = null;
            this.lkpTabelaPreco.ID = 0;
            this.lkpTabelaPreco.Join = null;
            this.lkpTabelaPreco.Key = System.Windows.Forms.Keys.F5;
            this.lkpTabelaPreco.Location = new System.Drawing.Point(59, 38);
            this.lkpTabelaPreco.Name = "lkpTabelaPreco";
            this.lkpTabelaPreco.OnIDChanged = null;
            this.lkpTabelaPreco.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lkpTabelaPreco.Properties.Appearance.Options.UseBackColor = true;
            this.lkpTabelaPreco.SelecionarTextoOnEnter = false;
            this.lkpTabelaPreco.Size = new System.Drawing.Size(454, 20);
            this.lkpTabelaPreco.Tabela = "TabelaPreco";
            this.lkpTabelaPreco.TabIndex = 3;
            this.lkpTabelaPreco.TituloTelaPesquisa = "Pesquisa - Tabela de Preços";
            this.lkpTabelaPreco.ToolTip = "Campos pesquisados: Nome, Codigo.";
            this.lkpTabelaPreco.Where = null;
            // 
            // lblTabelaPreco
            // 
            this.lblTabelaPreco.Location = new System.Drawing.Point(17, 41);
            this.lblTabelaPreco.Name = "lblTabelaPreco";
            this.lblTabelaPreco.Size = new System.Drawing.Size(36, 13);
            this.lblTabelaPreco.TabIndex = 44;
            this.lblTabelaPreco.Text = "Tabela:";
            // 
            // txtPercentual
            // 
            this.txtPercentual.Location = new System.Drawing.Point(477, 12);
            this.txtPercentual.Name = "txtPercentual";
            this.txtPercentual.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.txtPercentual.Properties.Appearance.Options.UseBackColor = true;
            this.txtPercentual.Properties.Mask.EditMask = "n2";
            this.txtPercentual.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.txtPercentual.Properties.Precision = 2;
            this.txtPercentual.Size = new System.Drawing.Size(66, 20);
            this.txtPercentual.TabIndex = 2;
            this.txtPercentual.Leave += new System.EventHandler(this.txtPercentual_Leave);
            // 
            // rbAumento
            // 
            this.rbAumento.AutoSize = true;
            this.rbAumento.BackColor = System.Drawing.Color.White;
            this.rbAumento.Location = new System.Drawing.Point(347, 65);
            this.rbAumento.Name = "rbAumento";
            this.rbAumento.Size = new System.Drawing.Size(93, 17);
            this.rbAumento.TabIndex = 7;
            this.rbAumento.TabStop = true;
            this.rbAumento.Text = "% de Aumento";
            this.rbAumento.UseVisualStyleBackColor = false;
            this.rbAumento.CheckedChanged += new System.EventHandler(this.rbAumento_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Nome:";
            // 
            // txtDataInicial
            // 
            this.txtDataInicial.EditValue = null;
            this.txtDataInicial.Location = new System.Drawing.Point(59, 64);
            this.txtDataInicial.Name = "txtDataInicial";
            this.txtDataInicial.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDataInicial.Size = new System.Drawing.Size(100, 20);
            this.txtDataInicial.TabIndex = 5;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(59, 12);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(351, 20);
            this.txtNome.TabIndex = 1;
            // 
            // FormPromocao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 436);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.sbSalvar);
            this.Controls.Add(this.sbFechar);
            this.Name = "FormPromocao";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cadastro de Promoçoes";
            this.Shown += new System.EventHandler(this.FormPromocao_Shown);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ovGRD_Promocoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ovGV_Promocoes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cardView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataFinal.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpTabelaPreco.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPercentual.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDataInicial.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sbSalvar;
        public DevExpress.XtraEditors.SimpleButton sbFechar;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.RadioButton rbAumento;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit txtDataInicial;
        private System.Windows.Forms.TextBox txtNome;
        private Cwork.Utilitarios.Componentes.DevCalcEdit txtPercentual;
        private Cwork.Utilitarios.Componentes.LookupButton lkbTabelaPreco;
        private Cwork.Utilitarios.Componentes.Lookup lkpTabelaPreco;
        private DevExpress.XtraEditors.LabelControl lblTabelaPreco;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit txtDataFinal;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.RadioButton rbDesconto;
        private DevExpress.XtraGrid.GridControl ovGRD_Promocoes;
        private DevExpress.XtraGrid.Views.Grid.GridView ovGV_Promocoes;
        private DevExpress.XtraGrid.Columns.GridColumn Nome;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Views.Card.CardView cardView2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.Columns.GridColumn Codigo;
        private DevExpress.XtraGrid.Columns.GridColumn Barra;
        private DevExpress.XtraGrid.Columns.GridColumn CodRef;
    }
}