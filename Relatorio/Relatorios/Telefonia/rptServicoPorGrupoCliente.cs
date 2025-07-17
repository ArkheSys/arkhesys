using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios.Telefonia
{
    public partial class rptServicoPorGrupoCliente : DevExpress.XtraReports.UI.XtraReport
    {
        public rptServicoPorGrupoCliente(decimal mes, decimal ano, string in_tipoCliente, string in_Servico)
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
                tel_GrupoClienteTableAdapter.GetData(mes, ano, in_tipoCliente, in_Servico);
            }
        }
    }
}
