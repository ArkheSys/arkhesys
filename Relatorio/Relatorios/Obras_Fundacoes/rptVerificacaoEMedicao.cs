using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios.Obras_Fundacoes
{
    public partial class rptVerificacaoEMedicao : DevExpress.XtraReports.UI.XtraReport
    {
        public rptVerificacaoEMedicao(string nomeEmpresa)
        {
            InitializeComponent();
            lblTitulo.Text = nomeEmpresa;
        }

        private void xrChart1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            xrChart1.Series[0].DataSource = DataSource;
        }

    }
}
