using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptNotasServico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNotasServico(DateTime dtInicio, DateTime dtFinal, string inFilial, string inTipoNota, string inServico)
        {
            InitializeComponent();
            dataTable1TableAdapter.GetData(dtInicio, dtFinal, inFilial, inTipoNota, inServico);
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
