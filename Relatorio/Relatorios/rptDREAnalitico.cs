using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptDREAnalitico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDREAnalitico(DateTime dataIni, DateTime dataFin, string filiais)
        {
            InitializeComponent();
            f_DREAnaliticoTableAdapter.GetData(dataIni, dataFin, filiais);
            rptDREAnaliticoResu relatorioResu = new rptDREAnaliticoResu(dataIni, dataFin, filiais);
            xrSubreport1.ReportSource = relatorioResu;
        }
    }
}
