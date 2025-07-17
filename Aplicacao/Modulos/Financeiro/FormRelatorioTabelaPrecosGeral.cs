using Aplicacao.Modulos.Rel;
using Aplicacao.Util;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

using DevExpress.XtraReports.UI;

using System;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Financeiro
{
    public partial class FormRelatorioTabelaPrecosGeral : Form
    {
        public FormRelatorioTabelaPrecosGeral()
        {
            InitializeComponent();

            lkbTabelaPreco.SubFormType = typeof(FormTabelaPreco);

            lkpTabelaPreco.Exemplo = new TabelaPreco();
            lkpTabelaPreco.Sessao = TabelaPrecoController.Instancia.getSession();
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonEtqBloco_Click(object sender, EventArgs e)
        {
            var tabelaSelecionada = lkpTabelaPreco.Selecionado as TabelaPreco;
            int? codigoInicial = null;
            if (!string.IsNullOrEmpty(txtProdutoInicial.Text))
                codigoInicial = Convert.ToInt32(txtProdutoInicial.Text);

            int? codigoFinal = null;
            if (!string.IsNullOrEmpty(txtProdutoFinal.Text))
                codigoFinal = Convert.ToInt32(txtProdutoFinal.Text);

            bool? produtosAtivos = null;

            switch (cbSituacao.SelectedIndex)
            {
                case 1: produtosAtivos = true; break;
                case 2: produtosAtivos = false; break;
            }

            var tabelas = TabelaPrecoController.Instancia.GetProdutosPorTabelaPreco(tabelaSelecionada?.ID, codigoInicial, codigoFinal, produtosAtivos);
            var report = new XtraRelatorioTabelaPrecosGeral(FilialController.Instancia.GetFilialPrincipal().EmpresaNome, tabelas.ToList());

            ReportPrintTool tool = new ReportPrintTool(report);
            tool.ShowPreviewDialog();
        }

        private void ckbTodasTabelasPreco_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTodasTabelasPreco.Checked)
            {
                lkpTabelaPreco.Enabled = false;
                lkpTabelaPreco.EditValue = null;

                lkbTabelaPreco.Enabled = false;
            }
            else
            {
                lkpTabelaPreco.Enabled = true;
                lkbTabelaPreco.Enabled = true;
            }
        }

        private void lkbTabelaPreco_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaPreco>(lkpTabelaPreco, typeof(FormTabelaPreco));
        }
    }
}
