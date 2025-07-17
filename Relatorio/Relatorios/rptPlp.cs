using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptPlp : DevExpress.XtraReports.UI.XtraReport
    {
        int id;

        public rptPlp(int id)
        {
            InitializeComponent();
            this.id = id;
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\correios.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                xrPictureBox2.Image = Bitmap.FromFile(caminho, true);
            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo dos correios) não encontrado", "Erro");
                return;
            }
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((rptPLPSubReport)((XRSubreport)sender).ReportSource).p_IDPLP.Value = id;
        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((rptPLPSubReport)((XRSubreport)sender).ReportSource).p_IDPLP.Value = id;
        }

        private void xrSubreport2_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((rptPLPSubReport)((XRSubreport)sender).ReportSource).p_IDPLP.Value = id;
        }
    }
}
