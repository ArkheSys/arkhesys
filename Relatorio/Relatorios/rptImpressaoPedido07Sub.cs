using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptImpressaoPedido07Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoPedido07Sub(int PedidoID)
        {
            InitializeComponent();

            servicosTableAdapter.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
        }

    }
}
