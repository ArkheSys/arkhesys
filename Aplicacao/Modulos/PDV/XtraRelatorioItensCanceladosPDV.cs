using System.Data;

namespace Aplicacao.Modulos.PDV
{
    public partial class XtraRelatorioItensCanceladosPDV : DevExpress.XtraReports.UI.XtraReport
    {

        public XtraRelatorioItensCanceladosPDV()
        {
            InitializeComponent();
        }

        public XtraRelatorioItensCanceladosPDV(DataTable dt, string NomeEmpresa, string Periodo)
        {
            InitializeComponent();
            dataSet1.Tables.Clear();
            dataSet1.Tables.Add(dt);

            ovTXT_NomeEmpresa.Text = NomeEmpresa;
            ovTXT_PeriodoRelatorio.Text = Periodo;
        }
    }
}
