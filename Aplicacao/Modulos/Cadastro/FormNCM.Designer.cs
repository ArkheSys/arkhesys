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
            this.btExcluirNCMItens = new Cwork.Utilitarios.Componentes.DevButton();
            this.btAlterarNCMItens = new Cwork.Utilitarios.Componentes.DevButton();
            this.btIncluirNCMItens = new Cwork.Utilitarios.Componentes.DevButton();
            this.gcNCMItens = new DevExpress.XtraGrid.GridControl();
            this.gvNCMItens = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcNCMItens)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNCMItens)).BeginInit();
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
            this.tpPrincipal.Controls.Add(this.btExcluirNCMItens);
            this.tpPrincipal.Controls.Add(this.btAlterarNCMItens);
            this.tpPrincipal.Controls.Add(this.btIncluirNCMItens);
            this.tpPrincipal.Controls.Add(this.gcNCMItens);
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
            // btExcluirNCMItens
            // 
            this.btExcluirNCMItens.GridControl = null;
            this.btExcluirNCMItens.GridControlType = null;
            this.btExcluirNCMItens.GridControlTypeParams = null;
            this.btExcluirNCMItens.ImageOptions.Image = global::Aplicacao.Properties.Resources.Excluir_copy;
            this.btExcluirNCMItens.Location = new System.Drawing.Point(828, 309);
            this.btExcluirNCMItens.Name = "btExcluirNCMItens";
            this.btExcluirNCMItens.Size = new System.Drawing.Size(75, 23);
            this.btExcluirNCMItens.SubForm = null;
            this.btExcluirNCMItens.SubFormType = null;
            this.btExcluirNCMItens.SubFormTypeParams = null;
            this.btExcluirNCMItens.TabIndex = 13;
            this.btExcluirNCMItens.TabStop = false;
            this.btExcluirNCMItens.Text = "&Excluir";
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
            // btIncluirNCMItens
            // 
            this.btIncluirNCMItens.GridControl = null;
            this.btIncluirNCMItens.GridControlType = null;
            this.btIncluirNCMItens.GridControlTypeParams = null;
            this.btIncluirNCMItens.ImageOptions.Image = global::Aplicacao.Properties.Resources.Inserir_copy;
            this.btIncluirNCMItens.Location = new System.Drawing.Point(666, 309);
            this.btIncluirNCMItens.Name = "btIncluirNCMItens";
            this.btIncluirNCMItens.Size = new System.Drawing.Size(75, 23);
            this.btIncluirNCMItens.SubForm = null;
            this.btIncluirNCMItens.SubFormType = null;
            this.btIncluirNCMItens.SubFormTypeParams = null;
            this.btIncluirNCMItens.TabIndex = 11;
            this.btIncluirNCMItens.TabStop = false;
            this.btIncluirNCMItens.Text = "&Incluir";
            // 
            // gcNCMItens
            // 
            this.gcNCMItens.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.gcNCMItens.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.gcNCMItens.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.gcNCMItens.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.gcNCMItens.EmbeddedNavigator.Buttons.First.Hint = "Primeiro registro";
            this.gcNCMItens.EmbeddedNavigator.Buttons.Last.Hint = "Último registro";
            this.gcNCMItens.EmbeddedNavigator.Buttons.Next.Hint = "Próximo registro";
            this.gcNCMItens.EmbeddedNavigator.Buttons.NextPage.Hint = "Próxima página";
            this.gcNCMItens.EmbeddedNavigator.Buttons.Prev.Hint = "Registro anterior";
            this.gcNCMItens.EmbeddedNavigator.Buttons.PrevPage.Hint = "Página anterior";
            this.gcNCMItens.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.gcNCMItens.EmbeddedNavigator.TextStringFormat = "Registro {0} de {1}";
            this.gcNCMItens.Location = new System.Drawing.Point(7, 112);
            this.gcNCMItens.MainView = this.gvNCMItens;
            this.gcNCMItens.Name = "gcNCMItens";
            this.gcNCMItens.Size = new System.Drawing.Size(896, 191);
            this.gcNCMItens.TabIndex = 9;
            this.gcNCMItens.UseEmbeddedNavigator = true;
            this.gcNCMItens.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvNCMItens});
            // 
            // gvNCMItens
            // 
            this.gvNCMItens.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.Empty.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.EvenRow.BackColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.EvenRow.BorderColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.EvenRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvNCMItens.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(184)))), ((int)(((byte)(251)))));
            this.gvNCMItens.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvNCMItens.Appearance.FilterPanel.BackColor2 = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FilterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FocusedCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.FocusedCell.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.FocusedRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FocusedRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
            this.gvNCMItens.Appearance.GroupPanel.BackColor2 = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.GroupPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvNCMItens.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(201)))), ((int)(((byte)(254)))));
            this.gvNCMItens.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.HideSelectionRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.HideSelectionRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.OddRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.OddRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.OddRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.OddRow.Options.UseBorderColor = true;
            this.gvNCMItens.Appearance.OddRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.Preview.Font = new System.Drawing.Font("Verdana", 7.5F);
            this.gvNCMItens.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.Preview.Options.UseFont = true;
            this.gvNCMItens.Appearance.Preview.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.Row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.Row.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(229)))), ((int)(((byte)(231)))));
            this.gvNCMItens.Appearance.Row.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.RowSeparator.BackColor = System.Drawing.Color.Black;
            this.gvNCMItens.Appearance.RowSeparator.BackColor2 = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(206)))), ((int)(((byte)(57)))));
            this.gvNCMItens.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvNCMItens.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvNCMItens.Appearance.TopNewRow.BackColor = System.Drawing.Color.White;
            this.gvNCMItens.Appearance.TopNewRow.Options.UseBackColor = true;
            this.gvNCMItens.GridControl = this.gcNCMItens;
            this.gvNCMItens.Name = "gvNCMItens";
            this.gvNCMItens.OptionsBehavior.Editable = false;
            this.gvNCMItens.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvNCMItens.OptionsView.EnableAppearanceEvenRow = true;
            this.gvNCMItens.OptionsView.EnableAppearanceOddRow = true;
            this.gvNCMItens.OptionsView.ShowGroupPanel = false;
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
            ((System.ComponentModel.ISupportInitialize)(this.gcNCMItens)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvNCMItens)).EndInit();
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
        private Cwork.Utilitarios.Componentes.DevButton btExcluirNCMItens;
        private Cwork.Utilitarios.Componentes.DevButton btAlterarNCMItens;
        private Cwork.Utilitarios.Componentes.DevButton btIncluirNCMItens;
        public DevExpress.XtraGrid.GridControl gcNCMItens;
        public DevExpress.XtraGrid.Views.Grid.GridView gvNCMItens;
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
    }
}
