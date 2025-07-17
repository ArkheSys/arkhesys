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
    public partial class rptImpressaoOrcamentoLote : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoOrcamentoLote(string ids)
        {
            InitializeComponent();
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                dttaImpOrcLote.GetData(ids);
            }
            catch (Exception)
            {
                MessageBox.Show("Imagem da Empresa não encontrada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                dttaImpOrcLote.GetData(ids);
            }
            
        }

    }
}
