using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptImpressaoNotaCompra02 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoNotaCompra02(int PedidoID)
        {

            InitializeComponent();
            pedidoCompraTableAdapter.GetData(PedidoID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            try
            {
                xrPictureBox1.Image = Image.FromFile(caminho + "\\LogoEmpresa.jpg");
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
                
            }
        }

    }
}
