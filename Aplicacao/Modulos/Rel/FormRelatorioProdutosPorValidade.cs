using cwkGestao.Negocio;

using DevExpress.XtraReports.UI;

using System;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Rel
{
    public partial class FormRelatorioProdutosPorValidade : Form
    {
        public FormRelatorioProdutosPorValidade()
        {
            InitializeComponent();
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonEtqBloco_Click(object sender, EventArgs e)
        {
            var report = new XtraRelatorioProdutosPorDataValidade(FilialController.Instancia.GetFilialPrincipal().Nome)
            {
                ShowPrintMarginsWarning = false
            };
            var tool = new ReportPrintTool(report);
            tool.ShowPreviewDialog();
        }
    }
}
