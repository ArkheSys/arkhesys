using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoMulti : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoMulti(string Forma, bool exibirObs)
        {
            InitializeComponent();

            xrPictureBox1.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox3.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox4.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox5.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox6.Image = Relatorio.Properties.Resources.tesoura;

            xrLabel67.Text = "Formas de Pagamento: " + Forma;
            xrLabel68.Text = "Formas de Pagamento: " + Forma;

            xrTableCell16.Multiline = exibirObs;
        }

        public rptOrcamentoMulti()
        {
            InitializeComponent();

            xrPictureBox1.Image = Relatorio.Properties.Resources.tesoura; 
            xrPictureBox3.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox4.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox5.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox6.Image = Relatorio.Properties.Resources.tesoura;
        }

        private void xrTableCell12_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell12.Text == "0,00" || xrTableCell12.Text == "0.00")
            {
                xrTableCell12.Text = "";
            }
        }

        private void xrTableCell11_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell11.Text == "0,00" || xrTableCell11.Text == "0.00")
            {
                xrTableCell11.Text = "";
            }
        }

        private void xrTableCell23_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell23.Text == "0,00" || xrTableCell23.Text == "0.00")
            {
                xrTableCell23.Text = "";
            }
        }

        private void xrTableCell24_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrTableCell24.Text == "0,00" || xrTableCell24.Text == "0.00")
            {
                xrTableCell24.Text = "";
            }
        }
    }
}
