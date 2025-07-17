namespace Relatorio.Relatorios.Obras_Fundacoes
{
    partial class rptResumoMedicao
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
            DevExpress.XtraPrinting.Shape.ShapeLine shapeLine1 = new DevExpress.XtraPrinting.Shape.ShapeLine();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrShape1 = new DevExpress.XtraReports.UI.XRShape();
            this.lblConstrutora = new DevExpress.XtraReports.UI.XRLabel();
            this.lblResponsavel = new DevExpress.XtraReports.UI.XRLabel();
            this.lblAssunto = new DevExpress.XtraReports.UI.XRLabel();
            this.lblObra = new DevExpress.XtraReports.UI.XRLabel();
            this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail1 = new DevExpress.XtraReports.UI.DetailBand();
            this.DetailReport1 = new DevExpress.XtraReports.UI.DetailReportBand();
            this.Detail2 = new DevExpress.XtraReports.UI.DetailBand();
            this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
            this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.subPerfuracoes = new DevExpress.XtraReports.UI.XRSubreport();
            this.rptMedicaoPerfuracao1 = new Relatorio.Relatorios.Obras_Fundacoes.rptMedicaoPerfuracao();
            this.subServicos = new DevExpress.XtraReports.UI.XRSubreport();
            this.rptMedicaoServico1 = new Relatorio.Relatorios.Obras_Fundacoes.rptMedicaoServico();
            ((System.ComponentModel.ISupportInitialize)(this.rptMedicaoPerfuracao1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptMedicaoServico1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Dpi = 254F;
            this.Detail.HeightF = 64F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel3,
            this.xrLabel2,
            this.xrLabel1,
            this.xrShape1,
            this.lblConstrutora,
            this.lblResponsavel,
            this.lblAssunto,
            this.lblObra});
            this.PageHeader.Dpi = 254F;
            this.PageHeader.HeightF = 766F;
            this.PageHeader.Name = "PageHeader";
            this.PageHeader.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 254F);
            this.PageHeader.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.Dpi = 254F;
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 2.539977F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(1897F, 354.0128F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.StretchImage;
            // 
            // xrLabel3
            // 
            this.xrLabel3.CanGrow = false;
            this.xrLabel3.Dpi = 254F;
            this.xrLabel3.Font = new System.Drawing.Font("Arial", 11F);
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 605.0491F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(170.18F, 63.50006F);
            this.xrLabel3.StylePriority.UseFont = false;
            this.xrLabel3.Text = "Assunto:";
            this.xrLabel3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.CanGrow = false;
            this.xrLabel2.Dpi = 254F;
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 541.5491F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(63.5F, 63.5F);
            this.xrLabel2.Text = "a/c";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel1
            // 
            this.xrLabel1.CanGrow = false;
            this.xrLabel1.Dpi = 254F;
            this.xrLabel1.Font = new System.Drawing.Font("Arial", 11F);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 478.0491F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(259.08F, 63.5F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.Text = "À Construtora ";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrShape1
            // 
            this.xrShape1.Angle = 270;
            this.xrShape1.BackColor = System.Drawing.Color.Transparent;
            this.xrShape1.Dpi = 254F;
            this.xrShape1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 395.0759F);
            this.xrShape1.Name = "xrShape1";
            this.xrShape1.Shape = shapeLine1;
            this.xrShape1.SizeF = new System.Drawing.SizeF(1897F, 20.32007F);
            this.xrShape1.StylePriority.UseBackColor = false;
            // 
            // lblConstrutora
            // 
            this.lblConstrutora.CanGrow = false;
            this.lblConstrutora.Dpi = 254F;
            this.lblConstrutora.Font = new System.Drawing.Font("Arial", 11F);
            this.lblConstrutora.LocationFloat = new DevExpress.Utils.PointFloat(259.08F, 478.0491F);
            this.lblConstrutora.Name = "lblConstrutora";
            this.lblConstrutora.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblConstrutora.SizeF = new System.Drawing.SizeF(1637.92F, 63.5F);
            this.lblConstrutora.StylePriority.UseFont = false;
            this.lblConstrutora.Text = "lblConstrutora";
            this.lblConstrutora.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblResponsavel
            // 
            this.lblResponsavel.CanGrow = false;
            this.lblResponsavel.Dpi = 254F;
            this.lblResponsavel.Font = new System.Drawing.Font("Arial", 11F);
            this.lblResponsavel.LocationFloat = new DevExpress.Utils.PointFloat(63.5F, 541.5491F);
            this.lblResponsavel.Name = "lblResponsavel";
            this.lblResponsavel.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblResponsavel.SizeF = new System.Drawing.SizeF(1833.5F, 63.5F);
            this.lblResponsavel.StylePriority.UseFont = false;
            this.lblResponsavel.Text = "lblResponsavel";
            this.lblResponsavel.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblAssunto
            // 
            this.lblAssunto.CanGrow = false;
            this.lblAssunto.Dpi = 254F;
            this.lblAssunto.Font = new System.Drawing.Font("Arial", 11F);
            this.lblAssunto.LocationFloat = new DevExpress.Utils.PointFloat(170.1799F, 605.0491F);
            this.lblAssunto.Name = "lblAssunto";
            this.lblAssunto.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblAssunto.SizeF = new System.Drawing.SizeF(1726.82F, 63.50006F);
            this.lblAssunto.StylePriority.UseFont = false;
            this.lblAssunto.Text = "lblAssunto";
            this.lblAssunto.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // lblObra
            // 
            this.lblObra.CanGrow = false;
            this.lblObra.Dpi = 254F;
            this.lblObra.Font = new System.Drawing.Font("Arial", 11F);
            this.lblObra.LocationFloat = new DevExpress.Utils.PointFloat(0F, 668.5491F);
            this.lblObra.Name = "lblObra";
            this.lblObra.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 5, 0, 0, 254F);
            this.lblObra.SizeF = new System.Drawing.SizeF(1897F, 63.5F);
            this.lblObra.StylePriority.UseFont = false;
            this.lblObra.Text = "lblObra";
            this.lblObra.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // DetailReport
            // 
            this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail1});
            this.DetailReport.Dpi = 254F;
            this.DetailReport.Level = 0;
            this.DetailReport.Name = "DetailReport";
            // 
            // Detail1
            // 
            this.Detail1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subPerfuracoes});
            this.Detail1.Dpi = 254F;
            this.Detail1.HeightF = 168F;
            this.Detail1.Name = "Detail1";
            // 
            // DetailReport1
            // 
            this.DetailReport1.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail2});
            this.DetailReport1.Dpi = 254F;
            this.DetailReport1.Level = 1;
            this.DetailReport1.Name = "DetailReport1";
            // 
            // Detail2
            // 
            this.Detail2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.subServicos});
            this.Detail2.Dpi = 254F;
            this.Detail2.HeightF = 107F;
            this.Detail2.Name = "Detail2";
            // 
            // topMarginBand1
            // 
            this.topMarginBand1.Dpi = 254F;
            this.topMarginBand1.HeightF = 152F;
            this.topMarginBand1.Name = "topMarginBand1";
            // 
            // bottomMarginBand1
            // 
            this.bottomMarginBand1.Dpi = 254F;
            this.bottomMarginBand1.HeightF = 152F;
            this.bottomMarginBand1.Name = "bottomMarginBand1";
            // 
            // subPerfuracoes
            // 
            this.subPerfuracoes.CanShrink = true;
            this.subPerfuracoes.Dpi = 254F;
            this.subPerfuracoes.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subPerfuracoes.Name = "subPerfuracoes";
            this.subPerfuracoes.ReportSource = this.rptMedicaoPerfuracao1;
            this.subPerfuracoes.SizeF = new System.Drawing.SizeF(1897F, 106.68F);
            // 
            // subServicos
            // 
            this.subServicos.CanShrink = true;
            this.subServicos.Dpi = 254F;
            this.subServicos.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.subServicos.Name = "subServicos";
            this.subServicos.ReportSource = this.rptMedicaoServico1;
            this.subServicos.SizeF = new System.Drawing.SizeF(1897F, 106.68F);
            // 
            // rptResumoMedicao
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.PageHeader,
            this.DetailReport,
            this.DetailReport1,
            this.topMarginBand1,
            this.bottomMarginBand1});
            this.Dpi = 254F;
            this.Margins = new System.Drawing.Printing.Margins(102, 102, 152, 152);
            this.PageHeight = 2969;
            this.PageWidth = 2101;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.ReportUnit = DevExpress.XtraReports.UI.ReportUnit.TenthsOfAMillimeter;
            this.SnapGridSize = 31.75F;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.rptMedicaoPerfuracao1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rptMedicaoServico1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.PageHeaderBand PageHeader;
        private DevExpress.XtraReports.UI.XRLabel lblConstrutora;
        private DevExpress.XtraReports.UI.XRLabel lblResponsavel;
        private DevExpress.XtraReports.UI.XRLabel lblAssunto;
        private DevExpress.XtraReports.UI.XRLabel lblObra;
        private DevExpress.XtraReports.UI.XRShape xrShape1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel3;
        private DevExpress.XtraReports.UI.DetailBand Detail1;
        private DevExpress.XtraReports.UI.DetailBand Detail2;
        private rptMedicaoServico rptMedicaoServico1;
        public DevExpress.XtraReports.UI.DetailReportBand DetailReport;
        public DevExpress.XtraReports.UI.DetailReportBand DetailReport1;
        public DevExpress.XtraReports.UI.XRSubreport subServicos;
        private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
        private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
        public DevExpress.XtraReports.UI.XRSubreport subPerfuracoes;
        private rptMedicaoPerfuracao rptMedicaoPerfuracao1;
    }
}
