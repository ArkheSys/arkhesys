using cwkGestao.Modelo;
using System.Collections.Generic;
using System.Data;

namespace Aplicacao.Modulos.Comercial.Impressao
{
    public partial class XRReportFluxoCaixa_SangriaA4 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRReportFluxoCaixa_SangriaA4()
        {
            InitializeComponent();
        }

        public XRReportFluxoCaixa_SangriaA4(List<SangriaCaixa> Sangrias)
        {
            InitializeComponent();

            foreach (var Sangria in Sangrias)
            {
                var DataRow = ovDS_Dados.Tables[0].NewRow();
                DataRow["DATASANGRIA"] = Sangria.DataSangria;
                DataRow["VALOR"] = Sangria.Valor;
                ovDS_Dados.Tables[0].Rows.Add(DataRow);
            }
        }

        private void XRReportFluxoCaixa_Sangria_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (ovDS_Dados.Tables[0] == null)
            {
                e.Cancel = true;
                return;
            }

            if (ovDS_Dados.Tables[0].Rows.Count == 0)
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
