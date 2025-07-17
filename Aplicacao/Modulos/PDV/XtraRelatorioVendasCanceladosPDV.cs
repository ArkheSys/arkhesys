using System.Data;

namespace Aplicacao.Modulos.PDV
{
    public partial class XtraRelatorioVendasCanceladosPDV : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraRelatorioVendasCanceladosPDV()
        {
            InitializeComponent();
        }

        public XtraRelatorioVendasCanceladosPDV(DataTable dt, string NomeEmpresa, string Periodo)
        {
            InitializeComponent();
            dsDados.Tables.Clear();
            dt.TableName = "DADOS";
            dsDados.Tables.Add(dt);

            ovTXT_NomeEmpresa.Text = NomeEmpresa;
            ovTXT_PeriodoRelatorio.Text = Periodo;
        }
    }
}
