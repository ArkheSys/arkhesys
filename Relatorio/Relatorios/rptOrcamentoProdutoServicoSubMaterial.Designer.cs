namespace Relatorio.Relatorios
{
    partial class rptOrcamentoProdutoServicoSubMaterial
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
            this.xrLabel80 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel79 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel70 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel69 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel67 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel68 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel66 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.cwkGestaoDataSet1 = new Relatorio.cwkGestaoDataSet();
            this.p_PedidosStatusTableAdapter = new Relatorio.cwkGestaoDataSetTableAdapters.p_PedidosStatusTableAdapter();
            this.dsOrcamentoProdutoServico1 = new Relatorio.dsOrcamentoProdutoServico();
            this.dsOrcamentoProdutoServicoEquipamentoTableAdapter = new Relatorio.dsOrcamentoProdutoServicoTableAdapters.dsOrcamentoProdutoServicoEquipamentoTableAdapter();
            this.dsOrcamentoProdutoServicoMaoObraTableAdapter = new Relatorio.dsOrcamentoProdutoServicoTableAdapters.dsOrcamentoProdutoServicoMaoObraTableAdapter();
            this.IDPedido = new DevExpress.XtraReports.Parameters.Parameter();
            this.dsOrcamentoProdutoServicoMaterialTableAdapter = new Relatorio.dsOrcamentoProdutoServicoTableAdapters.dsOrcamentoProdutoServicoMaterialTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.cwkGestaoDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrcamentoProdutoServico1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel80,
            this.xrLabel79,
            this.xrLabel70,
            this.xrLabel69,
            this.xrLabel67,
            this.xrLabel68,
            this.xrLabel63,
            this.xrLabel66});
            this.Detail.HeightF = 34.49997F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel80
            // 
            this.xrLabel80.BorderDashStyle = DevExpress.XtraPrinting.BorderDashStyle.Solid;
            this.xrLabel80.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel80.BorderWidth = 1;
            this.xrLabel80.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel80.LocationFloat = new DevExpress.Utils.PointFloat(58.64F, 0F);
            this.xrLabel80.Name = "xrLabel80";
            this.xrLabel80.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel80.SizeF = new System.Drawing.SizeF(718.31F, 17.25F);
            this.xrLabel80.StylePriority.UseBorderDashStyle = false;
            this.xrLabel80.StylePriority.UseBorders = false;
            this.xrLabel80.StylePriority.UseBorderWidth = false;
            this.xrLabel80.StylePriority.UseFont = false;
            this.xrLabel80.StylePriority.UseTextAlignment = false;
            this.xrLabel80.Text = "Material";
            this.xrLabel80.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // xrLabel79
            // 
            this.xrLabel79.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel79.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel79.LocationFloat = new DevExpress.Utils.PointFloat(9.999999F, 9.349747E-06F);
            this.xrLabel79.Name = "xrLabel79";
            this.xrLabel79.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel79.SizeF = new System.Drawing.SizeF(48.64F, 17.25F);
            this.xrLabel79.StylePriority.UseBorders = false;
            this.xrLabel79.StylePriority.UseFont = false;
            this.xrLabel79.StylePriority.UseTextAlignment = false;
            this.xrLabel79.Text = "2";
            this.xrLabel79.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopJustify;
            // 
            // xrLabel70
            // 
            this.xrLabel70.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel70.BorderWidth = 1;
            this.xrLabel70.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsOrcamentoProdutoServicoMaterial.Total", "{0:R$ 0,0.00}")});
            this.xrLabel70.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel70.LocationFloat = new DevExpress.Utils.PointFloat(668.95F, 17.24997F);
            this.xrLabel70.Name = "xrLabel70";
            this.xrLabel70.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel70.SizeF = new System.Drawing.SizeF(107.99F, 17.25F);
            this.xrLabel70.StylePriority.UseBorders = false;
            this.xrLabel70.StylePriority.UseBorderWidth = false;
            this.xrLabel70.StylePriority.UseFont = false;
            this.xrLabel70.StylePriority.UseTextAlignment = false;
            this.xrLabel70.Text = "[Total]";
            this.xrLabel70.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel69
            // 
            this.xrLabel69.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel69.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsOrcamentoProdutoServicoMaterial.ValorCalculado", "{0:R$ 0,0.00}")});
            this.xrLabel69.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel69.LocationFloat = new DevExpress.Utils.PointFloat(563.34F, 17.24995F);
            this.xrLabel69.Name = "xrLabel69";
            this.xrLabel69.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel69.SizeF = new System.Drawing.SizeF(105.6104F, 17.24998F);
            this.xrLabel69.StylePriority.UseBorders = false;
            this.xrLabel69.StylePriority.UseFont = false;
            this.xrLabel69.StylePriority.UseTextAlignment = false;
            this.xrLabel69.Text = "xrLabel69";
            this.xrLabel69.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel67
            // 
            this.xrLabel67.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel67.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsOrcamentoProdutoServicoMaterial.Unidade")});
            this.xrLabel67.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel67.LocationFloat = new DevExpress.Utils.PointFloat(386.79F, 17.24997F);
            this.xrLabel67.Name = "xrLabel67";
            this.xrLabel67.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel67.SizeF = new System.Drawing.SizeF(61.22F, 17.25F);
            this.xrLabel67.StylePriority.UseBorders = false;
            this.xrLabel67.StylePriority.UseFont = false;
            this.xrLabel67.StylePriority.UseTextAlignment = false;
            this.xrLabel67.Text = "xrLabel67";
            this.xrLabel67.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel68
            // 
            this.xrLabel68.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel68.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsOrcamentoProdutoServicoMaterial.Quantidade")});
            this.xrLabel68.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel68.LocationFloat = new DevExpress.Utils.PointFloat(448.01F, 17.24997F);
            this.xrLabel68.Name = "xrLabel68";
            this.xrLabel68.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel68.SizeF = new System.Drawing.SizeF(115.33F, 17.25F);
            this.xrLabel68.StylePriority.UseBorders = false;
            this.xrLabel68.StylePriority.UseFont = false;
            this.xrLabel68.StylePriority.UseTextAlignment = false;
            this.xrLabel68.Text = "xrLabel68";
            this.xrLabel68.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel63
            // 
            this.xrLabel63.Borders = ((DevExpress.XtraPrinting.BorderSide)((DevExpress.XtraPrinting.BorderSide.Top | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel63.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsOrcamentoProdutoServicoMaterial.ProdutoNome")});
            this.xrLabel63.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(58.64F, 17.24993F);
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel63.SizeF = new System.Drawing.SizeF(328.15F, 17.25F);
            this.xrLabel63.StylePriority.UseBorders = false;
            this.xrLabel63.StylePriority.UseFont = false;
            this.xrLabel63.StylePriority.UseTextAlignment = false;
            this.xrLabel63.Text = "xrLabel63";
            this.xrLabel63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // xrLabel66
            // 
            this.xrLabel66.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top)
                        | DevExpress.XtraPrinting.BorderSide.Right)));
            this.xrLabel66.DataBindings.AddRange(new DevExpress.XtraReports.UI.XRBinding[] {
            new DevExpress.XtraReports.UI.XRBinding("Text", null, "dsOrcamentoProdutoServicoMaterial.Contador")});
            this.xrLabel66.Font = new System.Drawing.Font("Times New Roman", 10F);
            this.xrLabel66.LocationFloat = new DevExpress.Utils.PointFloat(9.999998F, 17.24997F);
            this.xrLabel66.Name = "xrLabel66";
            this.xrLabel66.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel66.SizeF = new System.Drawing.SizeF(48.64F, 17.25F);
            this.xrLabel66.StylePriority.UseBorders = false;
            this.xrLabel66.StylePriority.UseFont = false;
            this.xrLabel66.StylePriority.UseTextAlignment = false;
            this.xrLabel66.Text = "xrLabel66";
            this.xrLabel66.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleJustify;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 1F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 18.76672F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // cwkGestaoDataSet1
            // 
            this.cwkGestaoDataSet1.DataSetName = "cwkGestaoDataSet";
            this.cwkGestaoDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // p_PedidosStatusTableAdapter
            // 
            this.p_PedidosStatusTableAdapter.ClearBeforeFill = true;
            // 
            // dsOrcamentoProdutoServico1
            // 
            this.dsOrcamentoProdutoServico1.DataSetName = "dsOrcamentoProdutoServico";
            this.dsOrcamentoProdutoServico1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dsOrcamentoProdutoServicoEquipamentoTableAdapter
            // 
            this.dsOrcamentoProdutoServicoEquipamentoTableAdapter.ClearBeforeFill = true;
            // 
            // dsOrcamentoProdutoServicoMaoObraTableAdapter
            // 
            this.dsOrcamentoProdutoServicoMaoObraTableAdapter.ClearBeforeFill = true;
            // 
            // IDPedido
            // 
            this.IDPedido.Name = "IDPedido";
            this.IDPedido.Value = "";
            this.IDPedido.Visible = false;
            // 
            // dsOrcamentoProdutoServicoMaterialTableAdapter
            // 
            this.dsOrcamentoProdutoServicoMaterialTableAdapter.ClearBeforeFill = true;
            // 
            // rptOrcamentoProdutoServicoSubMaterial
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin});
            this.DataAdapter = this.dsOrcamentoProdutoServicoMaterialTableAdapter;
            this.DataMember = "dsOrcamentoProdutoServicoMaterial";
            this.DataSource = this.dsOrcamentoProdutoServico1;
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(14, 17, 1, 19);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.IDPedido});
            this.Version = "10.2";
            ((System.ComponentModel.ISupportInitialize)(this.cwkGestaoDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsOrcamentoProdutoServico1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private cwkGestaoDataSet cwkGestaoDataSet1;
        private Relatorio.cwkGestaoDataSetTableAdapters.p_PedidosStatusTableAdapter p_PedidosStatusTableAdapter;
        private dsOrcamentoProdutoServico dsOrcamentoProdutoServico1;
        private Relatorio.dsOrcamentoProdutoServicoTableAdapters.dsOrcamentoProdutoServicoEquipamentoTableAdapter dsOrcamentoProdutoServicoEquipamentoTableAdapter;
        private Relatorio.dsOrcamentoProdutoServicoTableAdapters.dsOrcamentoProdutoServicoMaoObraTableAdapter dsOrcamentoProdutoServicoMaoObraTableAdapter;
        public DevExpress.XtraReports.Parameters.Parameter IDPedido;
        private Relatorio.dsOrcamentoProdutoServicoTableAdapters.dsOrcamentoProdutoServicoMaterialTableAdapter dsOrcamentoProdutoServicoMaterialTableAdapter;
        private DevExpress.XtraReports.UI.XRLabel xrLabel70;
        private DevExpress.XtraReports.UI.XRLabel xrLabel69;
        private DevExpress.XtraReports.UI.XRLabel xrLabel67;
        private DevExpress.XtraReports.UI.XRLabel xrLabel68;
        private DevExpress.XtraReports.UI.XRLabel xrLabel63;
        private DevExpress.XtraReports.UI.XRLabel xrLabel66;
        private DevExpress.XtraReports.UI.XRLabel xrLabel80;
        private DevExpress.XtraReports.UI.XRLabel xrLabel79;
    }
}
