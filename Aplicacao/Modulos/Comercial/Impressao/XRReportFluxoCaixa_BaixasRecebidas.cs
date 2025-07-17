using cwkGestao.Negocio;

using System.Linq;

namespace Aplicacao.Modulos.Comercial.Impressao
{
    public partial class XRReportFluxoCaixa_BaixasRecebidas : DevExpress.XtraReports.UI.XtraReport
    {
        public XRReportFluxoCaixa_BaixasRecebidas()
        {
            InitializeComponent();
        }

        public XRReportFluxoCaixa_BaixasRecebidas(int IDFluxoCaixa)
        {
            InitializeComponent();

            var movimentos = MovCaixaController.Instancia.GetMovimentosPorFluxoCaixa(IDFluxoCaixa).ToList();
            foreach (var mov in movimentos)
            {
                var DataRow = ovDS_Dados.Tables[0].NewRow();
                DataRow["FORMAPAGAMENTO"] = mov.FormaPagamento;
                DataRow["VALOR"] = mov.Valor;
                ovDS_Dados.Tables[0].Rows.Add(DataRow);
            }
        }

        private void XRReportFluxoCaixa_Movimentos_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
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
