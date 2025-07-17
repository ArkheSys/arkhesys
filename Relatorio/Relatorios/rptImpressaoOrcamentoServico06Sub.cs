using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptImpressaoOrcamentoServico06Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoOrcamentoServico06Sub(int IDPedido, out decimal totalprod)
        {
            InitializeComponent();
            var datatable = relImpressaoOrdemServicoSubTableAdapter.GetData(IDPedido);
            totalprod = 0;
            for (int i = 0; i < datatable.Rows.Count; i++)
                totalprod += Convert.ToDecimal(datatable.Rows[i]["OSProdutoTotal"]);


        }

    }
}
