using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Relatorio.Relatorios
{
    public partial class rptTroca : DevExpress.XtraReports.UI.XtraReport
    {
        public rptTroca()
        {
            InitializeComponent();
        }

        public rptTroca(string idsTroca)
        {
            InitializeComponent();
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                 dsTrocaTableAdapter.GetData(idsTroca);
                
            }
            catch (Exception)
            {
                MessageBox.Show("Imagem da Empresa não encontrada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                dsTrocaTableAdapter.GetData(idsTroca);
            }
            
        }
    }
}
