using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptImpressaoPedido06SubParcelas : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoPedido06SubParcelas(int PedidoID)
        {
            InitializeComponent();

            pedido06ParcelaTableAdapter.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
        }

    }
}
