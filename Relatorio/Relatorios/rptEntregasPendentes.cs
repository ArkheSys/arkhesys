using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptEntregasPendentes : DevExpress.XtraReports.UI.XtraReport
    {
        public rptEntregasPendentes(DateTime dtInicio, DateTime dtFim, string IDPedido)
        {
            InitializeComponent();

            entregasPendentesTableAdapter.GetData(dtInicio, dtFim, IDPedido);
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
