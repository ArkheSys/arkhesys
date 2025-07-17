using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptPLPSubReport : DevExpress.XtraReports.UI.XtraReport
    {
        public rptPLPSubReport()
        {
            InitializeComponent();
        }

        private void rptPLPSubReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }
    }
}
