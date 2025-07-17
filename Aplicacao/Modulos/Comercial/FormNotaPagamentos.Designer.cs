
namespace Aplicacao.Modulos.Comercial
{
    partial class FormNotaPagamentos
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
            this.gcNotaParcelas = new DevExpress.XtraGrid.GridControl();
            this.gvNotaParcelas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gcNotaParcelas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotaParcelas)).BeginInit();
            this.SuspendLayout();
            // 
            // gcNotaParcelas
            // 
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.First.Hint = "Primeiro registro";
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.Last.Hint = "Último registro";
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.Next.Hint = "Próximo registro";
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.NextPage.Hint = "Próxima página";
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.Prev.Hint = "Registro anterior";
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.PrevPage.Hint = "Página anterior";
            this.gcNotaParcelas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcNotaParcelas.EmbeddedNavigator.TextStringFormat = "Registro {0} de {1}";
            this.gcNotaParcelas.Location = new System.Drawing.Point(12, 12);
            this.gcNotaParcelas.MainView = this.gvNotaParcelas;
            this.gcNotaParcelas.Name = "gcNotaParcelas";
            this.gcNotaParcelas.Size = new System.Drawing.Size(508, 186);
            this.gcNotaParcelas.TabIndex = 1;
            this.gcNotaParcelas.TabStop = false;
            this.gcNotaParcelas.UseEmbeddedNavigator = true;
            this.gcNotaParcelas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNotaParcelas});
            // 
            // gvNotaParcelas
            // 
            this.gvNotaParcelas.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.Empty.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvNotaParcelas.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvNotaParcelas.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvNotaParcelas.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNotaParcelas.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNotaParcelas.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNotaParcelas.Appearance.FocusedRow.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvNotaParcelas.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvNotaParcelas.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvNotaParcelas.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNotaParcelas.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.OddRow.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvNotaParcelas.Appearance.OddRow.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvNotaParcelas.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.Preview.Options.UseFont = true;
            this.gvNotaParcelas.Appearance.Preview.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNotaParcelas.Appearance.Row.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNotaParcelas.Appearance.SelectedRow.ForeColor = System.Drawing.Color.Black;
            this.gvNotaParcelas.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvNotaParcelas.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvNotaParcelas.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvNotaParcelas.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvNotaParcelas.GridControl = this.gcNotaParcelas;
            this.gvNotaParcelas.Name = "gvNotaParcelas";
            this.gvNotaParcelas.OptionsBehavior.Editable = false;
            this.gvNotaParcelas.OptionsView.EnableAppearanceEvenRow = true;
            this.gvNotaParcelas.OptionsView.EnableAppearanceOddRow = true;
            this.gvNotaParcelas.OptionsView.ShowGroupPanel = false;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(445, 204);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 2;
            this.simpleButton1.Text = "Fechar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // FormNotaPagamentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 239);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.gcNotaParcelas);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(548, 278);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(548, 278);
            this.Name = "FormNotaPagamentos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pagamentos da Nota";
            this.Shown += new System.EventHandler(this.FormNotaPagamentos_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gcNotaParcelas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNotaParcelas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevExpress.XtraGrid.GridControl gcNotaParcelas;
        public DevExpress.XtraGrid.Views.Grid.GridView gvNotaParcelas;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}