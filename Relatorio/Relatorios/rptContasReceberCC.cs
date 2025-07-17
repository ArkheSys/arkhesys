using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptContasReceberCC : DevExpress.XtraReports.UI.XtraReport
    {
        public rptContasReceberCC(int es, string bVenc, DateTime dtInicial, DateTime dtFinal, string filial, string tipoDoc, string situacao, string cC, string pessoas)
        {
            //es = entrada e saída, bVenc = baixa por vencimento ou pelo outro negócio (:
            InitializeComponent();
            dtCCTableAdapter.Fill(dsRelCentroCusto1.dtCC, bVenc, dtInicial, dtFinal, es, filial, tipoDoc, situacao, cC, pessoas);
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

        private void rptContasReceberCC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!((Boolean)bMostraQuebraPessoa.Value))
            {
                GroupHeader3.GroupFields.Clear();
                GroupHeader3.Visible = false;
                GroupFooter3.Visible = false;
            }
            else
            {
                GroupHeader3.Visible = true;
                GroupFooter3.Visible = true;
            }
        }
    }
}
