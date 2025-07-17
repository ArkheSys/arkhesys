using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using cwkGestao.Negocio;

using DevExpress.XtraReports.UI;

namespace Aplicacao.Modulos.PDV
{
#pragma warning disable CS0612 // "FormBaseEmpresa" está obsoleto
    public partial class FormRelatorioItensCanceladosPDV : Relatorios.Base.FormBaseEmpresa
#pragma warning restore CS0612 // "FormBaseEmpresa" está obsoleto
    {
        public FormRelatorioItensCanceladosPDV()
        {
            InitializeComponent();
            ckbTodos.Checked = true;
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            base.btOk_Click(sender, e);

            DateTime DataInicial = Convert.ToDateTime(txtDtInicial.EditValue);
            if (DataInicial.Year <= 1800)
            {
                MessageBox.Show(this, "Atençao!", "Selecione a Data Inicial");
                return;
            }

            DateTime DataFinal = Convert.ToDateTime(txtDtFinal.EditValue);
            if (DataFinal.Year <= 1800)
            {
                MessageBox.Show(this, "Atençao!", "Selecione a Data Final");
                return;
            }

            int? CodigoInicial = null;
            if (!string.IsNullOrEmpty(txtUsuarioInicial.Text))
                CodigoInicial = Convert.ToInt32(txtUsuarioInicial.Text);

            int? CodigoFinal = null;
            if (!string.IsNullOrEmpty(txtUsuarioFinal.Text))
                CodigoFinal = Convert.ToInt32(txtUsuarioFinal.Text);

            ValidaSelectManutencao();
            var Empresa = listaEmpresas.FirstOrDefault();
            var ListProdutos = NotaController.Instancia.GetItensCanceladosPDV(Empresa?.ID ?? 0, DataInicial, DataFinal, CodigoInicial, CodigoFinal);

            DataTable dt = new DataTable();
            dt.Columns.Add("usuario");
            dt.Columns.Add("idproduto");
            dt.Columns.Add("codigo");
            dt.Columns.Add("nome");
            dt.Columns.Add("motivocancelamento");
            dt.Columns.Add("quantidade");
            dt.Columns.Add("valor");
            dt.Columns.Add("total");

            foreach (var item in ListProdutos)
            {
                DataRow dr = dt.NewRow();
                dr["usuario"] = item.Usuario;
                dr["idproduto"] = item.IDProduto;
                dr["codigo"] = item.Codigo;
                dr["nome"] = item.Produto;
                dr["motivocancelamento"] = item.Motivo;
                dr["quantidade"] = item.Quantidade;
                dr["valor"] = item.ValorUnitario;
                dr["total"] = item.ValorTotal;
                dt.Rows.Add(dr);
            }

            var Rel = new XtraRelatorioItensCanceladosPDV(dt, Empresa.Nome, $"{DataInicial.ToString("dd/MM/yyyy")} até {DataFinal.ToString("dd/MM/yyyy")}");
            var tool = new ReportPrintTool(Rel);
            tool.ShowPreviewDialog();
        }

        private void FormRelatorioItensCanceladosPDV_Load(object sender, EventArgs e)
        {
            base.FormBase_Load(sender, e);
            Carrega();
        }

        private void ckbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (!ckbTodos.Checked)
            {
                txtUsuarioInicial.Text = "";
                txtUsuarioFinal.Text = "";
            }
        }
    }
}
