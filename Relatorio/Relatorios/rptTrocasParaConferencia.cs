using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptTrocasParaConferencia : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTrocasParaConferencia(string IDTipoPedido, string IDFilial, string IDCliente, DateTime dataInicial, DateTime dataFinal)
        {
            InitializeComponent();

            trocasParaConferenciaTableAdapter.GetData(dataInicial, dataFinal, IDTipoPedido, IDFilial, IDCliente);
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
                return;
            }
        }

    }
}
