using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptImpostosPorNota : DevExpress.XtraReports.UI.XtraReport
    {
#pragma warning disable CS0414 // O campo "rptImpostosPorNota.report" é atribuído, mas seu valor nunca é usado
        XtraReport report = null;
#pragma warning restore CS0414 // O campo "rptImpostosPorNota.report" é atribuído, mas seu valor nunca é usado
        public rptImpostosPorNota(DateTime dtInicio, DateTime dtFinal, int idEmpresa, string bBaseRet, string idTipoNota, string idStatus)
        {
            InitializeComponent();
            dtImpostosNFSeTableAdapter.GetData(dtInicio, dtFinal, idEmpresa, bBaseRet, idTipoNota, idStatus);
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
                return;
            }
            finally
            {
                xrSubreport1.ReportSource = new rptSubNFSeImpostos(dtInicio, dtFinal, idEmpresa, bBaseRet, idTipoNota, idStatus);
            }
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

    }
}
