using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptImpressaoPedido06Sub : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoPedido06Sub(int PedidoID)
        {
            InitializeComponent();

            pedido06TableAdapter.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
        }
    }
}
