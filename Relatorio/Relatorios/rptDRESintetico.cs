using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptDRESintetico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDRESintetico(DateTime dataIni, DateTime dataFin, string filiais)
        {
            InitializeComponent();
            f_DRESinteticoTableAdapter.GetData(dataIni, dataFin, filiais);

        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if ((Convert.ToString(this.GetCurrentColumnValue("tipo"))).Equals("R"))
            {
                xrTableCellDesricao.BackColor = Color.Silver;
                xrTableCellValor.BackColor = Color.Silver;
            }
            else
            {
                xrTableCellDesricao.BackColor = Color.Transparent;
                xrTableCellValor.BackColor = Color.Transparent;
            }

            if ((Convert.ToString(this.GetCurrentColumnValue("tipo"))).Equals("R") ||
                (Convert.ToString(this.GetCurrentColumnValue("tipo"))).Equals("V"))
            {
              //  xrTableCellValor.BackColor = Color.Gainsboro;
                xrTableCellValor.Font = new Font(xrTableCellValor.Font, FontStyle.Bold);
                xrTableCellDesricao.Font = new Font(xrTableCellDesricao.Font, FontStyle.Bold);
            }
            else
            {
             //   xrTableCellValor.BackColor = Color.Transparent;
                xrTableCellValor.Font = new Font(xrTableCellValor.Font, FontStyle.Regular);
                xrTableCellDesricao.Font = new Font(xrTableCellDesricao.Font, FontStyle.Regular);
            }
        }

        private void rptDRESintetico_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime i = (DateTime)dtInicio.Value;
            DateTime f = (DateTime)dtFim.Value;
            f_DRESinteticoTableAdapter.GetData(i, f, (String)idFilial.Value);
        }
    }
}
