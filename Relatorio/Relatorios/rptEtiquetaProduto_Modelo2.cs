using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptEtiquetaProduto_Modelo2 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEtiquetaProduto_Modelo2()
        {
            InitializeComponent();
        }

        private void vlrBarra_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var barra = GetCurrentColumnValue("Barra");
#pragma warning disable CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
            if (barra == String.Empty || barra == null)
#pragma warning restore CS0252 // Comparação de referência não intencional possível; para obter uma comparação de valor, converta o lado esquerdo para o tipo "string"
                e.Cancel = true;
        }
    }
}
