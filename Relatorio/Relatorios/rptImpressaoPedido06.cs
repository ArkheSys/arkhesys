using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptImpressaoPedido06 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoPedido06(int PedidoID)
        {
            InitializeComponent();

            pedido06TableAdapter.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            rptImpressaoPedido06Sub rptImpressaopedido06sub = new rptImpressaoPedido06Sub(PedidoID);
            SubImpressaoPedido06.ReportSource = rptImpressaopedido06sub;
            rptImpressaoPedido06Sub rptImpressaopedido06sub2 = new rptImpressaoPedido06Sub(PedidoID);
            SubImpressaoPedido06_2.ReportSource = rptImpressaopedido06sub2;
            rptImpressaoPedido06SubParcelas rptImpressaopedido06subParcelas = new rptImpressaoPedido06SubParcelas(PedidoID);
            SubImpressaoPedido06Parcelas.ReportSource = rptImpressaopedido06subParcelas;
            rptImpressaoPedido06SubParcelas rptImpressaopedido06sub2Parcelas = new rptImpressaoPedido06SubParcelas(PedidoID);
            SubImpressaoPedido06Parcelas_2.ReportSource = rptImpressaopedido06sub2Parcelas;

        }

    }
}
