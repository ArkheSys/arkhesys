using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoMultiDetalhe : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoMultiDetalhe()
        {
            InitializeComponent();

            xrPictureBox1.Image = Relatorio.Properties.Resources.tesoura; 
            xrPictureBox3.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox4.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox5.Image = Relatorio.Properties.Resources.tesoura;
            xrPictureBox6.Image = Relatorio.Properties.Resources.tesoura;
        }
    }
}
