namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoMeiaFolha : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoMeiaFolha(string Observacao)
        {
            InitializeComponent();

            xrLabel27.Text = Observacao;
        }

        private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell11.Text == "0,00" || xrTableCell11.Text == "0.00")
            {
                xrTableCell11.Text = "";
            }
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell12.Text == "0,00" || xrTableCell12.Text == "0.00")
            {
                xrTableCell12.Text = "";
            }
        }
    }
}
