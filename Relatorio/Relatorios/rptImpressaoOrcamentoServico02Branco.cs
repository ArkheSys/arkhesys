using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;
using Relatorio;

namespace Relatorio.Relatorios
{
    public partial class rptImpressaoOrcamentoServico02Branco : DevExpress.XtraReports.UI.XtraReport
    {
        public rptImpressaoOrcamentoServico02Branco(int filial)
        {

            InitializeComponent();
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
#pragma warning disable CS0168 // A variável "ex" está declarada, mas nunca é usada
            catch (Exception ex)
#pragma warning restore CS0168 // A variável "ex" está declarada, mas nunca é usada
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Erro");
                return;
            }
            finally
            {
                string arquivoExe = Assembly.GetEntryAssembly().Location;
                try
                {
                    var datatable = relImpressaoOrdemServicoBrancoTableAdapter.GetData(filial);

                }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
                catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
                {

                }
            }
        }
    }
}
