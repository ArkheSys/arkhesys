using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptPCPFtImagem : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPCPFtImagem()
        {
            InitializeComponent();            
        }
        private void xrSubreport2_BeforePrint_1(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

            ((rptPCPFtRegras)((XRSubreport)sender).ReportSource).id.Value = Id.Value;
        }

        private void GroupFooter1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((bool)Imprimir.Value == true)
            {
                GroupFooter1.Visible = true;
            }
            else
            {
                GroupFooter1.Visible = false;
            }

        }

    }
}
