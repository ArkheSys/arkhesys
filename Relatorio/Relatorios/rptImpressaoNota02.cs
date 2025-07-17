using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptImpressaoNota02 : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoNota02(int PedidoID)
        {

            
            InitializeComponent();
            orcamentoTableAdapter.GetData(PedidoID);
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
                System.Windows.Forms.MessageBox.Show("O Logotipo da Empresa não está definido", 
                    "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

    }
}
