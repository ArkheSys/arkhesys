namespace Relatorio.Relatorios
{
    partial class rptEtiquetaProdutoGs1
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
            DevExpress.XtraPrinting.BarCode.Code128Generator code128Generator1 = new DevExpress.XtraPrinting.BarCode.Code128Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.vlrNome = new DevExpress.XtraReports.UI.XRLabel();
            this.vlrBarra = new DevExpress.XtraReports.UI.XRBarCode();
            this.dsEtiqueta1 = new Relatorio.dsEtiqueta();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiqueta1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.BorderWidth = 1;
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 127F;
            this.Detail.MultiColumn.ColumnCount = 4;
            this.Detail.MultiColumn.ColumnSpacing = 30F;
            this.Detail.MultiColumn.ColumnWidth = 380F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnCount;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.StylePriority.UseBorderWidth = false;
            this.Detail.StylePriority.UsePadding = false;
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.BorderWidth = 1;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.vlrNome,
            this.vlrBarra});
            this.xrPanel1.Dpi = 254F;
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(444F, 127F);
            this.xrPanel1.StylePriority.UseBorderWidth = false;
            // 
            // vlrNome
            // 
            this.vlrNome.BorderWidth = 0;
            this.vlrNome.CanGrow = false;
            this.vlrNome.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEtiquetaProduto.Nome")});
            this.vlrNome.Dpi = 254F;
            this.vlrNome.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vlrNome.LocationFloat = new DevExpress.Utils.PointFloat(25F, 3F);
            this.vlrNome.Name = "vlrNome";
            this.vlrNome.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 254F);
            this.vlrNome.SizeF = new System.Drawing.SizeF(320F, 38.1F);
            this.vlrNome.StylePriority.UseBorderWidth = false;
            this.vlrNome.StylePriority.UseFont = false;
            this.vlrNome.StylePriority.UsePadding = false;
            this.vlrNome.StylePriority.UseTextAlignment = false;
            this.vlrNome.Text = "vlrNome";
            this.vlrNome.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // vlrBarra
            // 
            this.vlrBarra.AutoModule = true;
            this.vlrBarra.BorderWidth = 0;
            this.vlrBarra.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEtiquetaProduto.Barra")});
            this.vlrBarra.Dpi = 254F;
            this.vlrBarra.Font = new System.Drawing.Font("Times New Roman", 7F);
            this.vlrBarra.LocationFloat = new DevExpress.Utils.PointFloat(14F, 42.1F);
            this.vlrBarra.Module = 5.08F;
            this.vlrBarra.Name = "vlrBarra";
            this.vlrBarra.Padding = new DevExpress.XtraPrinting.PaddingInfo(3, 3, 0, 0, 254F);
            this.vlrBarra.SizeF = new System.Drawing.SizeF(398F, 77.12917F);
            this.vlrBarra.StylePriority.UseBorderWidth = false;
            this.vlrBarra.StylePriority.UseFont = false;
            this.vlrBarra.StylePriority.UseTextAlignment = false;
            this.vlrBarra.Symbology = code128Generator1;
            this.vlrBarra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.vlrBarra.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.vlrBarra_BeforePrint);
            // 
            // dsEtiqueta1
            // 
            this.dsEtiqueta1.DataSetName = "dsEtiqueta";
            this.dsEtiqueta1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 254F;
            this.topMarginBand1.HeightF = 120F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 254F;
            this.bottomMarginBand1.HeightF = 110F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // rptEtiquetaProdutoGs1
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.BorderColor = System.Drawing.Color.Transparent;
            this.DataSource = this.dsEtiqueta1;
#pragma warning disable CS0618 // "XtraReportBase.DetailPrintCountOnEmptyDataSource" é obsoleto: "You should use the 'ReportPrintOptions.DetailCountOnEmptyDataSource' instead."
            this.DetailPrintCountOnEmptyDataSource = 10;
#pragma warning restore CS0618 // "XtraReportBase.DetailPrintCountOnEmptyDataSource" é obsoleto: "You should use the 'ReportPrintOptions.DetailCountOnEmptyDataSource' instead."
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(145, 110, 120, 110);
            this.PageHeight = 2794;
            this.PageWidth = 2159;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiqueta1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRBarCode vlrBarra;
        private DevExpress.XtraReports.UI.XRLabel vlrNome;
        private dsEtiqueta dsEtiqueta1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
