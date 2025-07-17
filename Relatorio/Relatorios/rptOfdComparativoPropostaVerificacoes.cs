using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptOfdComparativoPropostaVerificacoes : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOfdComparativoPropostaVerificacoes(int idProposta)
        {
            InitializeComponent();
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
            catch (Exception)
            {
            }
            dsOfdComparativoPropostaVerificacoesTableAdapter.GetData(idProposta);
        }

    }
}
