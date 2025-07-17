using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios.Telefonia
{
    public partial class rptLigacaoPorGrupoCliente : DevExpress.XtraReports.UI.XtraReport
    {
        public rptLigacaoPorGrupoCliente(decimal mes, decimal ano, string in_tipoCliente, string in_tipoLigacao)
        {
            InitializeComponent();

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
            finally
            {
                dataTable1TableAdapter.GetData(mes, ano, in_tipoCliente, in_tipoLigacao);
            }
        }
    }
}
