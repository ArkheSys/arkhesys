using System;
using System.Drawing;
using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptImpressaoPedido07 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoPedido07(int PedidoID)
        {
            InitializeComponent();

            pedidoProdutoServicoTableAdapter1.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");

            try
            {
                xrPictureBox1.Image = xrPictureBox2.Image = Image.FromFile(caminho + "\\LogoEmpresa.jpg");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
            }

            rptImpressaoPedido06Sub rptImpressaopedido06sub1 = new rptImpressaoPedido06Sub(PedidoID);
            SubImpressaoPedido06_2.ReportSource = rptImpressaopedido06sub1;
            rptImpressaoPedido06Sub rptImpressaopedido06sub2 = new rptImpressaoPedido06Sub(PedidoID);
            SubImpressaoPedido06_1.ReportSource = rptImpressaopedido06sub2;
            rptImpressaoPedido07Sub rptImpressaopedido07sub1 = new rptImpressaoPedido07Sub(PedidoID);
            SubImpressaoPedido07_1.ReportSource = rptImpressaopedido07sub1;
            rptImpressaoPedido07Sub rptImpressaopedido07sub2 = new rptImpressaoPedido07Sub(PedidoID);
            SubImpressaoPedido07_2.ReportSource = rptImpressaopedido07sub2;
        }

    }
}
