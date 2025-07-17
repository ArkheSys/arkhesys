using System;
using System.Drawing;
using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptImpressaoPedido09 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoPedido09(int PedidoID)
        {
            InitializeComponent();

            pedidoProdutoServicoTableAdapter1.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");

            try
            {
                xrPictureBox1.Image = Image.FromFile(caminho + "\\LogoEmpresa.jpg");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
            }

            SubImpressaoPedido06_2.ReportSource = new rptImpressaoPedido06Sub(PedidoID);
            SubImpressaoPedido07_1.ReportSource = new rptImpressaoPedido07Sub(PedidoID);
        }

    }
}
