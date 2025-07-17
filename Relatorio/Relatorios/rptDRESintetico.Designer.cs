namespace Relatorio.Relatorios
{
    partial class rptDRESintetico
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
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCellTipo = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellDesricao = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCellValor = new DevExpress.XtraReports.UI.XRTableCell();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.dtInicio = new DevExpress.XtraReports.Parameters.Parameter();
            this.dtFim = new DevExpress.XtraReports.Parameters.Parameter();
            this.dsDRESintetico1 = new Relatorio.dsDRESintetico();
            this.f_DRESinteticoTableAdapter = new Relatorio.dsDRESinteticoTableAdapters.F_DRESinteticoTableAdapter();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.lbTitulo = new DevExpress.XtraReports.UI.XRLabel();
            this.LbDataImpressao = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.periodo = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLine2 = new DevExpress.XtraReports.UI.XRLine();
            this.xrLbPeriodo = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.xrLbNomeFilial = new DevExpress.XtraReports.UI.XRLabel();
            this.nomeFilial = new DevExpress.XtraReports.Parameters.Parameter();
            this.xrLbEmpresa = new DevExpress.XtraReports.UI.XRLabel();
            this.idFilial = new DevExpress.XtraReports.Parameters.Parameter();
            this.dsDRESintetico2 = new Relatorio.dsDRESintetico();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRESintetico1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRESintetico2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
            this.Detail.HeightF = 22F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.Detail.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.Detail_BeforePrint);
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.BorderWidth = 1F;
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(2.500026F, 0F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(790.5F, 22F);
            this.xrTable1.StylePriority.UseBorders = false;
            this.xrTable1.StylePriority.UseBorderWidth = false;
            this.xrTable1.StylePriority.UseTextAlignment = false;
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCellTipo,
            this.xrTableCellDesricao,
            this.xrTableCellValor});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCellTipo
            // 
            this.xrTableCellTipo.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellTipo.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DRESintetico.tipo")});
            this.xrTableCellTipo.Name = "xrTableCellTipo";
            this.xrTableCellTipo.StylePriority.UseBorders = false;
            this.xrTableCellTipo.Text = "xrTableCellTipo";
            this.xrTableCellTipo.Visible = false;
            this.xrTableCellTipo.Weight = 0.019772298304811864D;
            // 
            // xrTableCellDesricao
            // 
            this.xrTableCellDesricao.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTableCellDesricao.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DRESintetico.Descricao")});
            this.xrTableCellDesricao.Name = "xrTableCellDesricao";
            this.xrTableCellDesricao.StylePriority.UseBorders = false;
            this.xrTableCellDesricao.Text = "xrTableCellDesricao";
            this.xrTableCellDesricao.Weight = 5.7966531742227811D;
            // 
            // xrTableCellValor
            // 
            this.xrTableCellValor.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DRESintetico.valor", "{0:c2}")});
            this.xrTableCellValor.Name = "xrTableCellValor";
            this.xrTableCellValor.StylePriority.UseTextAlignment = false;
            this.xrTableCellValor.Text = "xrTableCellValor";
            this.xrTableCellValor.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            this.xrTableCellValor.Weight = 1.9985754324492713D;
            // 
            // TopMargin
            // 
            this.TopMargin.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopMargin.HeightF = 22F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.StylePriority.UseFont = false;
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 35F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // dtInicio
            // 
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Type = typeof(System.DateTime);
            this.dtInicio.ValueInfo = "03/21/2013 10:21:13";
            this.dtInicio.Visible = false;
            // 
            // dtFim
            // 
            this.dtFim.Name = "dtFim";
            this.dtFim.Type = typeof(System.DateTime);
            this.dtFim.ValueInfo = "03/21/2013 10:21:13";
            this.dtFim.Visible = false;
            // 
            // dsDRESintetico1
            // 
            this.dsDRESintetico1.DataSetName = "dsDRESintetico";
            this.dsDRESintetico1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // f_DRESinteticoTableAdapter
            // 
            this.f_DRESinteticoTableAdapter.ClearBeforeFill = true;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lbTitulo,
            this.LbDataImpressao,
            this.xrLabel2,
            this.xrLine2,
            this.xrLbPeriodo,
            this.xrPageInfo2,
            this.xrLbNomeFilial,
            this.xrLbEmpresa});
            this.PageHeader.HeightF = 121.875F;
            this.PageHeader.Name = "PageHeader";
            // 
            // lbTitulo
            // 
            this.lbTitulo.Font = new System.Drawing.Font("Arial", 14F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lbTitulo.LocationFloat = new DevExpress.Utils.PointFloat(0.6425173F, 77.08826F);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.lbTitulo.SizeF = new System.Drawing.SizeF(792.3575F, 24.78674F);
            this.lbTitulo.StylePriority.UseFont = false;
            this.lbTitulo.StylePriority.UseTextAlignment = false;
            this.lbTitulo.Text = "DRE  - Demonstração do Resultado do Exercício";
            this.lbTitulo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // LbDataImpressao
            // 
            this.LbDataImpressao.Font = new System.Drawing.Font("Arial", 8F);
            this.LbDataImpressao.LocationFloat = new DevExpress.Utils.PointFloat(584.1425F, 27F);
            this.LbDataImpressao.Name = "LbDataImpressao";
            this.LbDataImpressao.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.LbDataImpressao.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
            this.LbDataImpressao.SizeF = new System.Drawing.SizeF(206.8575F, 23F);
            this.LbDataImpressao.StylePriority.UseFont = false;
            this.LbDataImpressao.StylePriority.UseTextAlignment = false;
            this.LbDataImpressao.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.periodo, "Text", "")});
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(65F, 27F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(514F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.Text = "xrLabel2";
            // 
            // periodo
            // 
            this.periodo.Name = "periodo";
            this.periodo.Visible = false;
            // 
            // xrLine2
            // 
            this.xrLine2.LocationFloat = new DevExpress.Utils.PointFloat(0.6425173F, 50F);
            this.xrLine2.Name = "xrLine2";
            this.xrLine2.SizeF = new System.Drawing.SizeF(793.3575F, 4.166683F);
            // 
            // xrLbPeriodo
            // 
            this.xrLbPeriodo.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLbPeriodo.LocationFloat = new DevExpress.Utils.PointFloat(2.5F, 27F);
            this.xrLbPeriodo.Name = "xrLbPeriodo";
            this.xrLbPeriodo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbPeriodo.SizeF = new System.Drawing.SizeF(57.85072F, 23F);
            this.xrLbPeriodo.StylePriority.UseFont = false;
            this.xrLbPeriodo.StylePriority.UseTextAlignment = false;
            this.xrLbPeriodo.Text = "Período:";
            this.xrLbPeriodo.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.Font = new System.Drawing.Font("Arial", 8F);
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(584.1425F, 2F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(206.8575F, 23F);
            this.xrPageInfo2.StylePriority.UseFont = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            this.xrPageInfo2.TextFormatString = "Página {0} de {1}";
            // 
            // xrLbNomeFilial
            // 
            this.xrLbNomeFilial.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding(this.nomeFilial, "Text", "")});
            this.xrLbNomeFilial.Font = new System.Drawing.Font("Times New Roman", 9F);
            this.xrLbNomeFilial.LocationFloat = new DevExpress.Utils.PointFloat(65F, 2F);
            this.xrLbNomeFilial.Name = "xrLbNomeFilial";
            this.xrLbNomeFilial.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbNomeFilial.SizeF = new System.Drawing.SizeF(514F, 23F);
            this.xrLbNomeFilial.StylePriority.UseFont = false;
            this.xrLbNomeFilial.Text = "[Parameters.nomeFilial]";
            // 
            // nomeFilial
            // 
            this.nomeFilial.Name = "nomeFilial";
            this.nomeFilial.Visible = false;
            // 
            // xrLbEmpresa
            // 
            this.xrLbEmpresa.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.xrLbEmpresa.LocationFloat = new DevExpress.Utils.PointFloat(2.5F, 2F);
            this.xrLbEmpresa.Name = "xrLbEmpresa";
            this.xrLbEmpresa.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLbEmpresa.SizeF = new System.Drawing.SizeF(57.85072F, 23F);
            this.xrLbEmpresa.StylePriority.UseFont = false;
            this.xrLbEmpresa.StylePriority.UseTextAlignment = false;
            this.xrLbEmpresa.Text = "Empresa:";
            this.xrLbEmpresa.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // idFilial
            // 
            this.idFilial.Name = "idFilial";
            this.idFilial.Visible = false;
            // 
            // dsDRESintetico2
            // 
            this.dsDRESintetico2.DataSetName = "dsDRESintetico";
            this.dsDRESintetico2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // rptDRESintetico
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.PageHeader});
            this.BorderWidth = 2F;
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.dsDRESintetico2});
            this.DataAdapter = this.f_DRESinteticoTableAdapter;
            this.DataMember = "F_DRESintetico";
            this.DataSource = this.dsDRESintetico1;
            this.Margins = new System.Drawing.Printing.Margins(18, 15, 22, 35);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.dtInicio,
            this.dtFim,
            this.idFilial,
            this.nomeFilial,
            this.periodo});
            this.Version = "19.1";
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.rptDRESintetico_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRESintetico1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRESintetico2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private dsDRESintetico dsDRESintetico1;
        private Relatorio.dsDRESinteticoTableAdapters.F_DRESinteticoTableAdapter f_DRESinteticoTableAdapter;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        public DevExpress.XtraReports.Parameters.Parameter idFilial;
        public DevExpress.XtraReports.Parameters.Parameter nomeFilial;
        public DevExpress.XtraReports.Parameters.Parameter dtInicio;
        public DevExpress.XtraReports.Parameters.Parameter dtFim;
        public DevExpress.XtraReports.Parameters.Parameter periodo;
        private DevExpress.XtraReports.UI.XRPageInfo LbDataImpressao;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLine xrLine2;
        private DevExpress.XtraReports.UI.XRLabel xrLbPeriodo;
        private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo2;
        private DevExpress.XtraReports.UI.XRLabel xrLbNomeFilial;
        private DevExpress.XtraReports.UI.XRLabel xrLbEmpresa;
        private DevExpress.XtraReports.UI.XRLabel lbTitulo;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCellTipo;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCellDesricao;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCellValor;
        private dsDRESintetico dsDRESintetico2;
    }
}
