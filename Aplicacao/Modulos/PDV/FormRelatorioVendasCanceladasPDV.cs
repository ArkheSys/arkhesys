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

namespace Aplicacao.Modulos.PDV
{
#pragma warning disable CS0612 // "FormBaseEmpresa" está obsoleto
    public partial class FormRelatorioVendasCanceladasPDV : Relatorios.Base.FormBaseEmpresa
#pragma warning restore CS0612 // "FormBaseEmpresa" está obsoleto
    {
        public FormRelatorioVendasCanceladasPDV()
        {
            InitializeComponent();

            //txtDtInicial.EditValue = DateTime.Now.Date;
            //txtDtFinal.EditValue = DateTime.Now.Date;
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


            ValidaSelectManutencao();
            var Empresa = listaEmpresas.FirstOrDefault();

            var vendas = FrenteCaixaNotaController.Instancia.GetVendasCanceladasPDV(Empresa?.ID, DataInicial, DataFinal);

            DataTable dt = new DataTable();
            dt.Columns.Add("Codigo");
            dt.Columns.Add("Gerente");
            dt.Columns.Add("MotivoCancelamento");
            dt.Columns.Add("NumeroPDV");
            dt.Columns.Add("Operador");
            dt.Columns.Add("TotalGeral");

            foreach (var item in vendas)
            {
                DataRow dr = dt.NewRow();
                dr["Codigo"] = item.Codigo;
                dr["Gerente"] = item.Gerente;
                dr["MotivoCancelamento"] = item.MotivoCancelamento;
                dr["NumeroPDV"] = item.NumeroPDV;
                dr["Operador"] = item.Operador;
                dr["TotalGeral"] = item.TotalGeral;
                dt.Rows.Add(dr);
            }

            var Rel = new XtraRelatorioVendasCanceladosPDV(dt, Empresa.Nome, $"{DataInicial.ToString("dd/MM/yyyy")} até {DataFinal.ToString("dd/MM/yyyy")}");
            var tool = new ReportPrintTool(Rel);
            tool.ShowPreviewDialog();
        }

        private void FormRelatorioVendasCanceladasPDV_Load(object sender, EventArgs e)
        {
            base.FormBase_Load(sender, e);
            Carrega();
        }
    }
}
