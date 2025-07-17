using cwkGestao.Modelo;
using cwkGestao.Negocio;
using DevExpress.XtraReports.UI;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Rel
{
    public partial class FormRelatorioMargemLucro : Form
    {
        public FormRelatorioMargemLucro()
        {
            InitializeComponent();

            lkpProduto.Exemplo = new cwkGestao.Modelo.Produto();
            lkpProduto.Sessao = ProdutoController.Instancia.getSession();
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //Imprimi Relatório de Margem de Lucro
            DateTime DataInicial = Convert.ToDateTime(txtDtInicial.EditValue);
            DataInicial = new DateTime(DataInicial.Year, DataInicial.Month, DataInicial.Day, 0, 0, 0);

            DateTime DataFinal = Convert.ToDateTime(txtDtFinal.EditValue);
            DataFinal = new DateTime(DataFinal.Year, DataFinal.Month, DataFinal.Day, 23, 59, 59);

            string Ativos = string.Empty;
            switch (cbSituacao.SelectedIndex)
            {
                case 1:
                    Ativos = " AND PRODUTO.INATIVO = 0 ";
                    break;
                case 2:
                    Ativos = " AND  PRODUTO.INATIVO = 1 ";
                    break;
            }
            var ModeloDocumento = "";
            switch (cbDocumento.Text)
            {
                case "Pedidos":
                    ModeloDocumento = null;
                    break;
                case "Nfe":
                    ModeloDocumento = " AND Nota.ModeloDocto = 55 ";
                    break;
                case "NFCe":
                    ModeloDocumento = " AND Nota.ModeloDocto = 65 ";
                    break;
            }

            var IDEmpresaSelecionada = GetEmpresaRelatorio();
            if (!IDEmpresaSelecionada.HasValue)
            {
                MessageBox.Show(this, "Selecione uma Empresa.", "Atenção!");
                return;
            }

            string idProduto = "";
            if (lkpProduto.Selecionado != null)
                idProduto = $" AND PRODUTO.ID = {((Produto)lkpProduto.Selecionado).ID } ";

            var ListaProdutos = NotaController.Instancia.GetMargemdeLucroProdutos(DataInicial, DataFinal, IDEmpresaSelecionada.Value, Ativos, ModeloDocumento, idProduto);

            DataTable dt = new DataTable();
            dt.Columns.Add("idproduto");
            dt.Columns.Add("codigo");
            dt.Columns.Add("nome");
            dt.Columns.Add("unidade");

            dt.Columns.Add("precovenda", typeof(decimal));
            dt.Columns.Add("ultimocusto", typeof(decimal));
            dt.Columns.Add("quantidade", typeof(decimal));
            dt.Columns.Add("valordelucro", typeof(decimal));
            dt.Columns.Add("total", typeof(decimal));
            dt.Columns.Add("margemlucro", typeof(decimal));

            if (ListaProdutos != null)
            {
                ListaProdutos = ListaProdutos.OrderByDescending(o => o.Quantidade).ToList();
                foreach (var item in ListaProdutos)
                {
                    var Row = dt.NewRow();
                    Row["idproduto"] = item.IDProduto;
                    Row["codigo"] = item.Codigo;
                    Row["nome"] = item.Nome;
                    Row["unidade"] = item.Unidade;

                    Row["quantidade"] = item.Quantidade;
                    Row["precovenda"] = item.PrecoVenda;
                    Row["ultimocusto"] = item.UltimoCusto;
                    Row["valordelucro"] = item.ValorDeLucro;
                    Row["total"] = item.Total;
                    Row["margemlucro"] = item.MargemLucro;
                    dt.Rows.Add(Row);
                }

                XtraRelatorioProdutosMargemDeLucro Rel = new XtraRelatorioProdutosMargemDeLucro(dt, ((cwkGestao.Modelo.Filial)gvPrincipal.GetRow(gvPrincipal.GetSelectedRows()[0])).Nome, DataInicial, DataFinal);
                ReportPrintTool tool = new ReportPrintTool(Rel);
                tool.ShowPreviewDialog();
            }
        }
        private int? GetEmpresaRelatorio()
        {
            if (gvPrincipal.GetSelectedRows() != null && gvPrincipal.GetSelectedRows().Count() > 0)
                return ((cwkGestao.Modelo.Filial)gvPrincipal.GetRow(gvPrincipal.GetSelectedRows()[0])).ID;
            return null;
        }

        public void cbGrupo_EditValueChanged(object sender, EventArgs e)
        {
            gcPrincipal.DataSource = FilialController.Instancia.GetEmpresasDoGrupo(((cwkGestao.Modelo.Empresa)cbGrupo.EditValue).ID);
        }

        private void FormRelatorioMargemLucro_Load(object sender, EventArgs e)
        {
            txtDtInicial.EditValue = DateTime.Now.AddDays(-30);
            txtDtFinal.EditValue = DateTime.Now;
            cbSituacao.Text = "Todos";
            cbDocumento.Text = "Todos";
            cbGrupo.Properties.DataSource = EmpresaController.Instancia.GetAll();
        }

        private void btnlkpProduto_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<cwkGestao.Modelo.Produto>(ProdutoController.Instancia.GetAll(), new FormProduto(), (cwkGestao.Modelo.Produto)lkpProduto.Selecionado, false);
            grid.Selecionando = true;
            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            lkpProduto.EditValue = grid.Selecionado;
        }
    }
}
