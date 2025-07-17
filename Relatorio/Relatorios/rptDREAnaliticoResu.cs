using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptDREAnaliticoResu : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDREAnaliticoResu(DateTime dataIni, DateTime dataFin, string filiais)
        {
            InitializeComponent();
            f_DREAnaliticoResumoTableAdapter.GetData(dataIni, dataFin, filiais);
        }

    }
}
