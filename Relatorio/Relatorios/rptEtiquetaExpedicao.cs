using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptEtiquetaExpedicao : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEtiquetaExpedicao()
        {
            InitializeComponent();
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            xrPictureBox1.Image = Bitmap.FromFile(caminho + "\\LogoEmpresa.jpg", true);
        }

        private void xrLabel7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrLabel2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e) 
        {
   
          XRLabel label = (XRLabel)sender;
             
          if (Convert.ToString(GetCurrentColumnValue("Remetente")).Contains("SAMUGIM"))
	      {
		        xrLabel2.Text = "0010273964";
          }
          else
                xrLabel2.Text = "0060570610";
	    }     
        

    }
}
