using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptCCEImpressao : DevExpress.XtraReports.UI.XtraReport
    {
        public rptCCEImpressao(int id)
        {
            InitializeComponent();
            dataTable1TableAdapter.Fill(dsCCE1.DataTable1, id);

            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
                return;
            }
        }

    }
}
