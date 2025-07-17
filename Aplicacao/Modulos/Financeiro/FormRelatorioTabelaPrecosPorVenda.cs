using Aplicacao.Modulos.Rel;
using Aplicacao.Util;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

using DevExpress.XtraReports.UI;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Financeiro
{
    public partial class FormRelatorioTabelaPrecosPorVenda : Form
    {
        public FormRelatorioTabelaPrecosPorVenda()
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
            string codigoInicial = txtProdutoInicial.Text;
            string codigoFinal = txtProdutoFinal.Text;

            string operadorInicial = txtOperadorInicial.Text;
            string operadorFinal = txtOperadorFinal.Text;

            DateTime? dataInicial = null;
            if (txtDtInicial.EditValue != null)
            {
                dataInicial = Convert.ToDateTime(txtDtInicial.EditValue);
                if (dataInicial.Value.Year < 2000)
                    dataInicial = null;
            }

            DateTime? dataFinal = null;
            if (txtDtFinal.EditValue != null)
            {
                dataFinal = Convert.ToDateTime(txtDtFinal.EditValue);
                if (dataFinal.Value.Year < 2000)
                    dataFinal = null;
            }

            var tabelas = FrenteCaixaNotaController.Instancia.GetItensPorFilial(tabelaSelecionada?.ID, dataInicial, dataFinal, codigoInicial, codigoFinal, operadorInicial, operadorFinal);
            var report = new XtraRelatorioTabelaPrecosVenda(FilialController.Instancia.GetFilialPrincipal().EmpresaNome, tabelas.ToList(), dataInicial, dataFinal);

            ReportPrintTool tool = new ReportPrintTool(report);
            tool.ShowPreviewDialog();
        }

        private void lkbTabelaPreco_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaPreco>(lkpTabelaPreco, typeof(FormTabelaPreco));
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

        private void ckbTodosProdutos_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTodosProdutos.Checked)
            {
                txtProdutoInicial.Text = "";
                txtProdutoFinal.Text = "";

                txtProdutoInicial.Enabled = false;
                txtProdutoFinal.Enabled = false;
            }
            else
            {
                txtProdutoInicial.Enabled = true;
                txtProdutoFinal.Enabled = true;
            }
        }

        private void ckb_TodosOperadores_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_TodosOperadores.Checked)
            {
                txtOperadorInicial.Text = "";
                txtOperadorFinal.Text = "";

                txtOperadorInicial.Enabled = false;
                txtOperadorFinal.Enabled = false;
            }
            else
            {
                txtOperadorInicial.Enabled = true;
                txtOperadorFinal.Enabled = true;
            }
        }

        private void lkbProdutoInicial_Click(object sender, EventArgs e)
        {

        }
    }
}
