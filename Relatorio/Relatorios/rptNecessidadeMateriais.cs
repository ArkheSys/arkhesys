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
    public partial class rptNecessidadeMateriais : DevExpress.XtraReports.UI.XtraReport
    {
        public rptNecessidadeMateriais(int idLocalEstoque, string dataIni, string dataFim, string TipoOrcamento, string TipoRequisicao, string idGrupoEstoque, string idFilial)
        {
            InitializeComponent();
            dataTable1TableAdapter.GetData(idLocalEstoque, TipoOrcamento, TipoRequisicao, dataIni, dataFim, idGrupoEstoque, idFilial);

            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
            catch (Exception)
            {
                MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Aviso", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
        }

    }
}
