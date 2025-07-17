namespace Relatorio.Relatorios
{
    partial class rptDREAnalitico
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rptDREAnalitico));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLbCodigo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLbValor = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLbDescricao = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLbConta = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLbEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLbPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLbNomeFilial = new DevExpress.XtraReports.UI.XRLabel();
            this.nomeFilial = new DevExpress.XtraReports.Parameters.Parameter();
            this.LbDataImpressao = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrTbNomeColunas = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTCConta = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTcCodigoReduzido = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCDescricao = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTCValor = new DevExpress.XtraReports.UI.XRTableCell();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.lbTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.periodo = new DevExpress.XtraReports.Parameters.Parameter();
            this.dtFim = new DevExpress.XtraReports.Parameters.Parameter();
            this.dtInicio = new DevExpress.XtraReports.Parameters.Parameter();
            this.dsDRE1 = new Relatorio.dsDRE();
            this.f_DREAnaliticoTableAdapter = new Relatorio.dsDRETableAdapters.F_DREAnaliticoTableAdapter();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrSubreport1 = new DevExpress.XtraReports.UI.XRSubreport();
            this.xrLine1 = new DevExpress.XtraReports.UI.XRLine();
            this.idFilial = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.xrTbNomeColunas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRE1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLbCodigo,
            this.xrLbValor,
            this.xrLbDescricao,
            this.xrLbConta});
            this.Detail.HeightF = 19.79167F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLbCodigo
            // 
            this.xrLbCodigo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DREAnalitico.CodigoPCR")});
            this.xrLbCodigo.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLbCodigo.LocationFloat = new DevExpress.Utils.PointFloat(132.5F, 0F);
            this.xrLbCodigo.Name = "xrLbCodigo";
            this.xrLbCodigo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbCodigo.SizeF = new System.Drawing.SizeF(46.08337F, 19.125F);
            this.xrLbCodigo.StylePriority.UseFont = false;
            this.xrLbCodigo.Text = "xrLbCodigo";
            // 
            // xrLbValor
            // 
            this.xrLbValor.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DREAnalitico.valor", "{0:c2}")});
            this.xrLbValor.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLbValor.LocationFloat = new DevExpress.Utils.PointFloat(701.2084F, 0F);
            this.xrLbValor.Name = "xrLbValor";
            this.xrLbValor.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbValor.SizeF = new System.Drawing.SizeF(91.79163F, 19.125F);
            this.xrLbValor.StylePriority.UseFont = false;
            this.xrLbValor.StylePriority.UseTextAlignment = false;
            this.xrLbValor.Text = "xrLbValor";
            this.xrLbValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLbDescricao
            // 
            this.xrLbDescricao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DREAnalitico.descricaoPCR")});
            this.xrLbDescricao.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLbDescricao.LocationFloat = new DevExpress.Utils.PointFloat(180.5834F, 0F);
            this.xrLbDescricao.Name = "xrLbDescricao";
            this.xrLbDescricao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbDescricao.SizeF = new System.Drawing.SizeF(519.625F, 19.125F);
            this.xrLbDescricao.StylePriority.UseFont = false;
            this.xrLbDescricao.Text = "xrLbDescricao";
            // 
            // xrLbConta
            // 
            this.xrLbConta.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DREAnalitico.contaPCR")});
            this.xrLbConta.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.xrLbConta.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLbConta.Name = "xrLbConta";
            this.xrLbConta.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbConta.SizeF = new System.Drawing.SizeF(129.5F, 19.125F);
            this.xrLbConta.StylePriority.UseFont = false;
            this.xrLbConta.Text = "xrLbConta";
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.TopMargin.HeightF = 22F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrPageInfo2.Format = "Página {0} de {1}";
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(584.1425F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(208.8575F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLbEmpresa
            // 
            this.xrLbEmpresa.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLbEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(3.125F, 0F);
            this.xrLbEmpresa.Name = "xrLbEmpresa";
            this.xrLbEmpresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbEmpresa.SizeF = new System.Drawing.SizeF(57.85072F, 23F);
            this.xrLbEmpresa.StylePriority.UseFont = false;
            this.xrLbEmpresa.StylePriority.UseTextAlignment = false;
            this.xrLbEmpresa.Text = "Empresa:";
            this.xrLbEmpresa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLbPeriodo
            // 
            this.xrLbPeriodo.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLbPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(3.125F, 23F);
            this.xrLbPeriodo.Name = "xrLbPeriodo";
            this.xrLbPeriodo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbPeriodo.SizeF = new System.Drawing.SizeF(57.85072F, 23F);
            this.xrLbPeriodo.StylePriority.UseFont = false;
            this.xrLbPeriodo.StylePriority.UseTextAlignment = false;
            this.xrLbPeriodo.Text = "Período:";
            this.xrLbPeriodo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLbNomeFilial
            // 
            this.xrLbNomeFilial.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.nomeFilial, "Text", "")});
            this.xrLbNomeFilial.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLbNomeFilial.LocationFloat = new DevExpress.Utils.PointFloat(62.97572F, 0F);
            this.xrLbNomeFilial.Name = "xrLbNomeFilial";
            this.xrLbNomeFilial.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbNomeFilial.SizeF = new System.Drawing.SizeF(511.1668F, 23F);
            this.xrLbNomeFilial.StylePriority.UseFont = false;
            this.xrLbNomeFilial.Text = "[Parameters.nomeFilial]";
            // 
            // nomeFilial
            // 
            this.nomeFilial.Name = "nomeFilial";
            this.nomeFilial.Value = "";
            this.nomeFilial.Visible = false;
            // 
            // LbDataImpressao
            // 
            this.LbDataImpressao.Font = new System.Drawing.Font("Arial", 8F);
            this.LbDataImpressao.LocationFloat = new DevExpress.Utils.PointFloat(584.1425F, 23F);
            this.LbDataImpressao.Name = "LbDataImpressao";
            this.LbDataImpressao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbDataImpressao.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.LbDataImpressao.SizeF = new System.Drawing.SizeF(208.8575F, 23F);
            this.LbDataImpressao.StylePriority.UseFont = false;
            this.LbDataImpressao.StylePriority.UseTextAlignment = false;
            this.LbDataImpressao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrTbNomeColunas
            // 
            this.xrTbNomeColunas.BackColor = System.Drawing.Color.Gainsboro;
            this.xrTbNomeColunas.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTbNomeColunas.BorderWidth = 1;
            this.xrTbNomeColunas.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrTbNomeColunas.LocationFloat = new DevExpress.Utils.PointFloat(0F, 104.6201F);
            this.xrTbNomeColunas.Name = "xrTbNomeColunas";
            this.xrTbNomeColunas.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTbNomeColunas.SizeF = new System.Drawing.SizeF(793F, 19F);
            this.xrTbNomeColunas.StylePriority.UseBackColor = false;
            this.xrTbNomeColunas.StylePriority.UseBorders = false;
            this.xrTbNomeColunas.StylePriority.UseBorderWidth = false;
            this.xrTbNomeColunas.StylePriority.UseFont = false;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTCConta,
            this.xrTcCodigoReduzido,
            this.xrTCDescricao,
            this.xrTCValor});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1;
            // 
            // xrTCConta
            // 
            this.xrTCConta.BackColor = System.Drawing.Color.LightGray;
            this.xrTCConta.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTCConta.Name = "xrTCConta";
            this.xrTCConta.StylePriority.UseBackColor = false;
            this.xrTCConta.StylePriority.UseFont = false;
            this.xrTCConta.StylePriority.UseTextAlignment = false;
            this.xrTCConta.Text = " Conta";
            this.xrTCConta.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            this.xrTCConta.Weight = 0.45944320087571411;
            // 
            // xrTcCodigoReduzido
            // 
            this.xrTcCodigoReduzido.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTcCodigoReduzido.Name = "xrTcCodigoReduzido";
            this.xrTcCodigoReduzido.StylePriority.UseFont = false;
            this.xrTcCodigoReduzido.Text = " Código";
            this.xrTcCodigoReduzido.Weight = 0.17463684543859015;
            // 
            // xrTCDescricao
            // 
            this.xrTCDescricao.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTCDescricao.Name = "xrTCDescricao";
            this.xrTCDescricao.StylePriority.UseFont = false;
            this.xrTCDescricao.Text = " Descrição";
            this.xrTCDescricao.Weight = 1.8908901306965158;
            // 
            // xrTCValor
            // 
            this.xrTCValor.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold);
            this.xrTCValor.Name = "xrTCValor";
            this.xrTCValor.StylePriority.UseFont = false;
            this.xrTCValor.StylePriority.UseTextAlignment = false;
            this.xrTCValor.Text = "Valor";
            this.xrTCValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.xrTCValor.Weight = 0.35517510143954295;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 35F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lbTitulo
            // 
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lbTitulo.LocationFloat = new DevExpress.Utils.PointFloat(1.125F, 66.83331F);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbTitulo.SizeF = new System.Drawing.SizeF(791.875F, 24.78674F);
            this.lbTitulo.StylePriority.UseFont = false;
            this.lbTitulo.StylePriority.UseTextAlignment = false;
            this.lbTitulo.Text = "DRE  - Demonstrativo de Resultado";
            this.lbTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine2,
            this.xrLabel2,
            this.lbTitulo,
            this.xrTbNomeColunas,
            this.LbDataImpressao,
            this.xrLbPeriodo,
            this.xrLbEmpresa,
            this.xrLbNomeFilial,
            this.xrPageInfo2});
            this.PageHeader.HeightF = 126.0417F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 45.99997F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(793F, 2.166679F);
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.periodo, "Text", "")});
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(62.97572F, 22.99999F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(511.1669F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "xrLabel2";
            // 
            // periodo
            // 
            this.periodo.Name = "periodo";
            this.periodo.Value = "";
            this.periodo.Visible = false;
            // 
            // dtFim
            // 
            this.dtFim.Name = "dtFim";
            this.dtFim.Type = typeof(System.DateTime);
            this.dtFim.Value = new System.DateTime(2013, 4, 11, 17, 13, 5, 41);
            this.dtFim.Visible = false;
            // 
            // dtInicio
            // 
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Type = typeof(System.DateTime);
            this.dtInicio.Value = new System.DateTime(2013, 4, 11, 17, 12, 42, 351);
            this.dtInicio.Visible = false;
            // 
            // dsDRE1
            // 
            this.dsDRE1.DataSetName = "dsDRE";
            this.dsDRE1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // f_DREAnaliticoTableAdapter
            // 
            this.f_DREAnaliticoTableAdapter.ClearBeforeFill = true;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrSubreport1,
            this.xrLine1});
            this.GroupFooter1.HeightF = 33.33333F;
            this.GroupFooter1.Name = "GroupFooter1";
            // 
            // xrSubreport1
            // 
            this.xrSubreport1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 8F);
            this.xrSubreport1.Name = "xrSubreport1";
            this.xrSubreport1.SizeF = new System.Drawing.SizeF(793F, 22.99999F);
            // 
            // xrLine1
            // 
            this.xrLine1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLine1.Name = "xrLine1";
            this.xrLine1.SizeF = new System.Drawing.SizeF(793F, 2F);
            // 
            // idFilial
            // 
            this.idFilial.Name = "idFilial";
            this.idFilial.Value = "";
            this.idFilial.Visible = false;
            // 
            // rptDREAnalitico
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader,
            this.GroupFooter1});
            this.DataAdapter = this.f_DREAnaliticoTableAdapter;
            this.DataMember = "F_DREAnalitico";
            this.DataSource = this.dsDRE1;
            this.DataSourceSchema = resources.GetString("$this.DataSourceSchema");
            this.Margins = new System.Drawing.Printing.Margins(18, 16, 22, 35);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.nomeFilial,
            this.dtInicio,
            this.dtFim,
            this.idFilial,
            this.periodo});
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTbNomeColunas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRE1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLbCodigo;
        private DevExpress.XtraReports.UI.XRLabel xrLbValor;
        private DevExpress.XtraReports.UI.XRLabel xrLbDescricao;
        private DevExpress.XtraReports.UI.XRLabel xrLbConta;
#pragma warning disable CS0169 // O campo "rptDREAnalitico.planoContaReferencialTableAdapter" nunca é usado
        private Relatorio.dsDRETableAdapters.PlanoContaReferencialTableAdapter planoContaReferencialTableAdapter;
#pragma warning restore CS0169 // O campo "rptDREAnalitico.planoContaReferencialTableAdapter" nunca é usado
        private DevExpress.XtraReports.UI.XRTable xrTbNomeColunas;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTCConta;
        private DevExpress.XtraReports.UI.XRTableCell xrTcCodigoReduzido;
        private DevExpress.XtraReports.UI.XRTableCell xrTCDescricao;
        private DevExpress.XtraReports.UI.XRTableCell xrTCValor;
        private DevExpress.XtraReports.UI.XRLabel lbTitulo;
        private DevExpress.XtraReports.UI.XRLabel xrLbNomeFilial;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRPageInfo LbDataImpressao;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel xrLbPeriodo;
        private DevExpress.XtraReports.UI.XRLabel xrLbEmpresa;
        public DevExpress.XtraReports.Parameters.Parameter nomeFilial;
        public DevExpress.XtraReports.Parameters.Parameter dtInicio;
        public DevExpress.XtraReports.Parameters.Parameter dtFim;
        private DevExpress.XtraReports.UI.XRSubreport xrSubreport1;
        private dsDRE dsDRE1;
        private Relatorio.dsDRETableAdapters.F_DREAnaliticoTableAdapter f_DREAnaliticoTableAdapter;
        private DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        private DevExpress.XtraReports.UI.XRLine xrLine1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        public DevExpress.XtraReports.Parameters.Parameter periodo;
        public DevExpress.XtraReports.Parameters.Parameter idFilial;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
    }
}
