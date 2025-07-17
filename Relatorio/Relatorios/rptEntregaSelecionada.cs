using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;


namespace Relatorio.Relatorios
{
    public partial class rptEntregaSelecionada : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEntregaSelecionada(int EntregaID)
        {
            InitializeComponent();

            entregaSelecionadaTableAdapter.GetData(EntregaID);
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            try
            {
               xrPictureBox1.Image = Image.FromFile(caminho + "\\LogoEmpresa.jpg");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("O Logotipo da Empresa não está definido",
                    "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

        }

    }
}
