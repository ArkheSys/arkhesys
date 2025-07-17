namespace Relatorio.Relatorios
{
    partial class rptEtiquetaProduto_Modelo2
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
            DevExpress.XtraPrinting.BarCode.EAN13Generator eaN13Generator1 = new DevExpress.XtraPrinting.BarCode.EAN13Generator();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPanel1 = new DevExpress.XtraReports.UI.XRPanel();
            this.vlrPreco = new DevExpress.XtraReports.UI.XRLabel();
            this.lblPreco = new DevExpress.XtraReports.UI.XRLabel();
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
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPanel1});
            this.Detail.HeightF = 67F;
            this.Detail.MultiColumn.ColumnCount = 4;
            this.Detail.MultiColumn.ColumnSpacing = 13F;
            this.Detail.MultiColumn.ColumnWidth = 174F;
            this.Detail.MultiColumn.Layout = DevExpress.XtraPrinting.ColumnLayout.AcrossThenDown;
            this.Detail.MultiColumn.Mode = DevExpress.XtraReports.UI.MultiColumnMode.UseColumnWidth;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPanel1
            // 
            this.xrPanel1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrPanel1.BorderWidth = 0;
            this.xrPanel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.vlrPreco,
            this.lblPreco,
            this.vlrNome,
            this.vlrBarra});
            this.xrPanel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrPanel1.Name = "xrPanel1";
            this.xrPanel1.SizeF = new System.Drawing.SizeF(174F, 66F);
            this.xrPanel1.StylePriority.UseBorderWidth = false;
            // 
            // vlrPreco
            // 
            this.vlrPreco.BorderWidth = 0;
            this.vlrPreco.CanGrow = false;
            this.vlrPreco.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEtiquetaProduto.Preco")});
            this.vlrPreco.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.vlrPreco.ForeColor = System.Drawing.Color.Red;
            this.vlrPreco.LocationFloat = new DevExpress.Utils.PointFloat(115F, 37F);
            this.vlrPreco.Name = "vlrPreco";
            this.vlrPreco.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 0, 0, 100F);
            this.vlrPreco.SizeF = new System.Drawing.SizeF(52F, 21F);
            this.vlrPreco.StylePriority.UseBorderWidth = false;
            this.vlrPreco.StylePriority.UsePadding = false;
            this.vlrPreco.Text = "vlrPreco";
            this.vlrPreco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // lblPreco
            // 
            this.lblPreco.BorderWidth = 0;
            this.lblPreco.CanGrow = false;
            this.lblPreco.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEtiquetaProduto.LabelPreco")});
            this.lblPreco.Font = new System.Drawing.Font("Calibri", 8F, System.Drawing.FontStyle.Bold);
            this.lblPreco.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblPreco.LocationFloat = new DevExpress.Utils.PointFloat(116F, 21F);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 0, 0, 100F);
            this.lblPreco.SizeF = new System.Drawing.SizeF(52F, 15F);
            this.lblPreco.StylePriority.UseBorderWidth = false;
            this.lblPreco.StylePriority.UsePadding = false;
            this.lblPreco.Text = "lblPreco";
            this.lblPreco.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // vlrNome
            // 
            this.vlrNome.BorderWidth = 0;
            this.vlrNome.CanGrow = false;
            this.vlrNome.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dtEtiquetaProduto.Nome")});
            this.vlrNome.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.vlrNome.LocationFloat = new DevExpress.Utils.PointFloat(8F, 3F);
            this.vlrNome.Name = "vlrNome";
            this.vlrNome.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 0, 0, 100F);
            this.vlrNome.SizeF = new System.Drawing.SizeF(158F, 15F);
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
            this.vlrBarra.Font = new System.Drawing.Font("Times New Roman", 8F);
            this.vlrBarra.LocationFloat = new DevExpress.Utils.PointFloat(0F, 21F);
            this.vlrBarra.Name = "vlrBarra";
            this.vlrBarra.Padding = new DevExpress.XtraPrinting.PaddingInfo(1, 1, 0, 0, 100F);
            this.vlrBarra.SizeF = new System.Drawing.SizeF(117F, 40F);
            this.vlrBarra.StylePriority.UseBorderWidth = false;
            this.vlrBarra.Symbology = eaN13Generator1;
            this.vlrBarra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.BottomCenter;
            this.vlrBarra.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.vlrBarra_BeforePrint);
            // 
            // dsEtiqueta1
            // 
            this.dsEtiqueta1.DataSetName = "dsEtiqueta";
            this.dsEtiqueta1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.HeightF = 50F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.HeightF = 0F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // rptEtiquetaProduto
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.DataSource = this.dsEtiqueta1;
#pragma warning disable CS0618 // "XtraReportBase.DetailPrintCountOnEmptyDataSource" é obsoleto: "You should use the 'ReportPrintOptions.DetailCountOnEmptyDataSource' instead."
            this.DetailPrintCountOnEmptyDataSource = 10;
#pragma warning restore CS0618 // "XtraReportBase.DetailPrintCountOnEmptyDataSource" é obsoleto: "You should use the 'ReportPrintOptions.DetailCountOnEmptyDataSource' instead."
            this.Margins = new System.Drawing.Printing.Margins(57, 0, 50, 0);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.dsEtiqueta1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRPanel xrPanel1;
        private DevExpress.XtraReports.UI.XRBarCode vlrBarra;
        private DevExpress.XtraReports.UI.XRLabel vlrNome;
        private DevExpress.XtraReports.UI.XRLabel lblPreco;
        private DevExpress.XtraReports.UI.XRLabel vlrPreco;
        private dsEtiqueta dsEtiqueta1;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    }
}
