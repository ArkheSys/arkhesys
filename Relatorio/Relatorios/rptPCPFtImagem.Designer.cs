namespace Relatorio.Relatorios
{
    partial class rptPCPFtImagem
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
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle1 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle2 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            DevExpress.XtraPrinting.Shape.ShapeRectangle shapeRectangle3 = new DevExpress.XtraPrinting.Shape.ShapeRectangle();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel37 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Imprimir = new DevExpress.XtraReports.Parameters.Parameter();
            this.GroupFooter1 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.xrShape4 = new DevExpress.XtraReports.UI.XRShape();
            this.xrLabel34 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrSubreport2 = new DevExpress.XtraReports.UI.XRSubreport();
            this.rptPCPFtRegras1 = new Relatorio.Relatorios.rptPCPFtRegras();
            this.xrLabel58 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrShape5 = new DevExpress.XtraReports.UI.XRShape();
            this.Id = new DevExpress.XtraReports.Parameters.Parameter();
            this.dsPCPFtImagem1 = new Relatorio.dsPCPFtImagem();
            this.dtFTImagemTableAdapter = new Relatorio.dsPCPFtImagemTableAdapters.dtFTImagemTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.rptPCPFtRegras1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPCPFtImagem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel36,
            this.xrLabel37,
            this.xrPictureBox2,
            this.xrShape1});
            this.Detail.HeightF = 511.0417F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel36
            // 
            this.xrLabel36.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(10.99577F, 369.7083F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(98.95833F, 23F);
            this.xrLabel36.StylePriority.UseFont = false;
            this.xrLabel36.Text = "Observações:";
            // 
            // xrLabel37
            // 
            this.xrLabel37.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.FT Observações")});
            this.xrLabel37.LocationFloat = new DevExpress.Utils.PointFloat(23.49577F, 405.2083F);
            this.xrLabel37.Name = "xrLabel37";
            this.xrLabel37.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel37.SizeF = new System.Drawing.SizeF(469.9933F, 90F);
            this.xrLabel37.Text = "xrLabel37";
            // 
            // xrShape1
            // 
            this.xrShape1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrShape1.BorderWidth = 0;
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(10.99577F, 392.7083F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeRectangle1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(496.8748F, 112.5F);
            this.xrShape1.StylePriority.UseBorders = false;
            this.xrShape1.StylePriority.UseBorderWidth = false;
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Image", null, "DataTable1.FT Imagem Produto")});
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 0F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(494.7917F, 367.1667F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 0F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            this.TopMargin.Visible = false;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 0F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // Imprimir
            // 
            this.Imprimir.Name = "Imprimir";
            this.Imprimir.Type = typeof(bool);
            this.Imprimir.Value = false;
            this.Imprimir.Visible = false;
            // 
            // GroupFooter1
            // 
            this.GroupFooter1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrShape4,
            this.xrLabel34,
            this.xrLabel1,
            this.xrSubreport2,
            this.xrLabel58,
            this.xrShape5});
            this.GroupFooter1.HeightF = 795.2917F;
            this.GroupFooter1.Name = "GroupFooter1";
            this.GroupFooter1.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.GroupFooter1_BeforePrint);
            // 
            // xrShape4
            // 
            this.xrShape4.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrShape4.BorderWidth = 0;
            this.xrShape4.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 48.99989F);
            this.xrShape4.Name = "xrShape4";
            this.xrShape4.Shape = shapeRectangle2;
            this.xrShape4.SizeF = new System.Drawing.SizeF(496.7917F, 94.79173F);
            this.xrShape4.StylePriority.UseBorders = false;
            this.xrShape4.StylePriority.UseBorderWidth = false;
            // 
            // xrLabel34
            // 
            this.xrLabel34.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "DataTable1.Formula")});
            this.xrLabel34.LocationFloat = new DevExpress.Utils.PointFloat(15.5F, 61.49992F);
            this.xrLabel34.Name = "xrLabel34";
            this.xrLabel34.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel34.SizeF = new System.Drawing.SizeF(490F, 77.49996F);
            this.xrLabel34.Text = "xrLabel34";
            // 
            // xrLabel1
            // 
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(10F, 149.9583F);
            this.xrLabel1.Multiline = true;
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(77.9329F, 22.99992F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "Regras:";
            // 
            // xrSubreport2
            // 
            this.xrSubreport2.LocationFloat = new DevExpress.Utils.PointFloat(14.2083F, 187.4583F);
            this.xrSubreport2.Name = "xrSubreport2";
            this.xrSubreport2.ReportSource = this.rptPCPFtRegras1;
            this.xrSubreport2.SizeF = new System.Drawing.SizeF(482.489F, 398.4908F);
            this.xrSubreport2.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.xrSubreport2_BeforePrint_1);
            // 
            // xrLabel58
            // 
            this.xrLabel58.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrLabel58.LocationFloat = new DevExpress.Utils.PointFloat(10F, 26F);
            this.xrLabel58.Multiline = true;
            this.xrLabel58.Name = "xrLabel58";
            this.xrLabel58.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel58.SizeF = new System.Drawing.SizeF(77.9329F, 22.99992F);
            this.xrLabel58.StylePriority.UseFont = false;
            this.xrLabel58.Text = "Fórmula:\r\n";
            // 
            // xrShape5
            // 
            this.xrShape5.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)
                        | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrShape5.BorderWidth = 0;
            this.xrShape5.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 174.9583F);
            this.xrShape5.Name = "xrShape5";
            this.xrShape5.Shape = shapeRectangle3;
            this.xrShape5.SizeF = new System.Drawing.SizeF(496.7917F, 423.4908F);
            this.xrShape5.StylePriority.UseBorders = false;
            this.xrShape5.StylePriority.UseBorderWidth = false;
            // 
            // Id
            // 
            this.Id.Name = "Id";
            this.Id.Type = typeof(short);
            this.Id.Value = 0;
            this.Id.Visible = false;
            // 
            // dsPCPFtImagem1
            // 
            this.dsPCPFtImagem1.DataSetName = "dsPCPFtImagem";
            this.dsPCPFtImagem1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtFTImagemTableAdapter
            // 
            this.dtFTImagemTableAdapter.ClearBeforeFill = true;
            // 
            // rptPCPFtImagem
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupFooter1});
            this.DataAdapter = this.dtFTImagemTableAdapter;
            this.DataMember = "dtFTImagem";
            this.DataSource = this.dsPCPFtImagem1;
            this.FilterString = "[FT id] = ?Id";
            this.Margins = new System.Drawing.Printing.Margins(5, 335, 0, 0);
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.Imprimir,
            this.Id});
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.rptPCPFtRegras1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsPCPFtImagem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.XRLabel xrLabel37;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel36;
        public DevExpress.XtraReports.Parameters.Parameter Imprimir;
        public DevExpress.XtraReports.UI.XRShape xrShape5;
        public DevExpress.XtraReports.UI.XRShape xrShape4;
        public DevExpress.XtraReports.UI.XRLabel xrLabel34;
        public DevExpress.XtraReports.UI.XRLabel xrLabel1;
        public DevExpress.XtraReports.UI.XRSubreport xrSubreport2;
        private rptPCPFtRegras rptPCPFtRegras1;
        public DevExpress.XtraReports.UI.XRLabel xrLabel58;
        public DevExpress.XtraReports.UI.GroupFooterBand GroupFooter1;
        public DevExpress.XtraReports.Parameters.Parameter Id;
        private dsPCPFtImagem dsPCPFtImagem1;
        private Relatorio.dsPCPFtImagemTableAdapters.dtFTImagemTableAdapter dtFTImagemTableAdapter;
    }
}
