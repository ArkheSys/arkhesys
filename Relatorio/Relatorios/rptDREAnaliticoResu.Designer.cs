namespace Relatorio.Relatorios
{
    partial class rptDREAnaliticoResu
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
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.dsDRE1 = new Relatorio.dsDRE();
            this.f_DREAnaliticoTableAdapter = new Relatorio.dsDRETableAdapters.F_DREAnaliticoTableAdapter();
            this.f_DREAnaliticoResumoTableAdapter = new Relatorio.dsDRETableAdapters.F_DREAnaliticoResumoTableAdapter();
            this.dtInicio = new DevExpress.XtraReports.Parameters.Parameter();
            this.dtFim = new DevExpress.XtraReports.Parameters.Parameter();
            this.idFilial = new DevExpress.XtraReports.Parameters.Parameter();
            ((System.ComponentModel.ISupportInitialize)(this.dsDRE1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel1});
            this.Detail.HeightF = 23F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel2
            // 
            this.xrLabel2.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DREAnaliticoResumo.valor", "{0:c2}")});
            this.xrLabel2.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(695.875F, 0F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(97.125F, 23F);
            this.xrLabel2.StylePriority.UseFont = false;
            this.xrLabel2.StylePriority.UseTextAlignment = false;
            this.xrLabel2.Text = "xrLabel2";
            this.xrLabel2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // xrLabel1
            // 
            this.xrLabel1.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "F_DREAnaliticoResumo.DescTipoConta")});
            this.xrLabel1.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(394.8333F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(298.75F, 23F);
            this.xrLabel1.StylePriority.UseFont = false;
            this.xrLabel1.StylePriority.UseTextAlignment = false;
            this.xrLabel1.Text = "xrLabel1";
            this.xrLabel1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 22F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.BottomMargin.HeightF = 35F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.StylePriority.UseFont = false;
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
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
            // f_DREAnaliticoResumoTableAdapter
            // 
            this.f_DREAnaliticoResumoTableAdapter.ClearBeforeFill = true;
            // 
            // dtInicio
            // 
            this.dtInicio.Name = "dtInicio";
            this.dtInicio.Type = typeof(System.DateTime);
            this.dtInicio.Value = new System.DateTime(2013, 4, 12, 17, 15, 24, 366);
            this.dtInicio.Visible = false;
            // 
            // dtFim
            // 
            this.dtFim.Name = "dtFim";
            this.dtFim.Type = typeof(System.DateTime);
            this.dtFim.Value = new System.DateTime(2013, 4, 12, 17, 15, 50, 228);
            this.dtFim.Visible = false;
            // 
            // idFilial
            // 
            this.idFilial.Name = "idFilial";
            this.idFilial.Value = "";
            this.idFilial.Visible = false;
            // 
            // rptDREAnaliticoResu
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataAdapter = this.f_DREAnaliticoResumoTableAdapter;
            this.DataMember = "F_DREAnaliticoResumo";
            this.DataSource = this.dsDRE1;
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(18, 16, 22, 35);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.dtInicio,
            this.dtFim,
            this.idFilial});
            this.RequestParameters = false;
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.dsDRE1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private dsDRE dsDRE1;
        private Relatorio.dsDRETableAdapters.F_DREAnaliticoTableAdapter f_DREAnaliticoTableAdapter;
        private Relatorio.dsDRETableAdapters.F_DREAnaliticoResumoTableAdapter f_DREAnaliticoResumoTableAdapter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel2;
        private DevExpress.XtraReports.UI.XRLabel xrLabel1;
        private DevExpress.XtraReports.Parameters.Parameter dtInicio;
        private DevExpress.XtraReports.Parameters.Parameter dtFim;
        private DevExpress.XtraReports.Parameters.Parameter idFilial;
    }
}
