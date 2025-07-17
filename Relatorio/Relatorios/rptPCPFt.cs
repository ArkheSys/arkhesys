using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptPCPFt : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPCPFt(string id)
        {
            InitializeComponent();           
            dataTable1TableAdapter.GetData(id);
                string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                caminho = caminho + "\\LogoEmpresa.jpg";
                try
                {
                    xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
                catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
                {
                    System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
                    return;
                }
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((rptPCPFTItens)((XRSubreport)sender).ReportSource).idFichaTecnica.Value = this.GetCurrentColumnValue("FT id");
        }

        private void xrSubreport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((rptPCPFtImagem)((XRSubreport)sender).ReportSource).Id.Value = this.GetCurrentColumnValue("FT id");
            ((rptPCPFtImagem)((XRSubreport)sender).ReportSource).Imprimir.Value = Imprimir.Value;
        }

    }
}
