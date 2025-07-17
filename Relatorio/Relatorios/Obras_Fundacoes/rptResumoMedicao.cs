using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios.Obras_Fundacoes
{
    public partial class rptResumoMedicao : DevExpress.XtraReports.UI.XtraReport
    {
        public rptResumoMedicao(ParametroResumo parms)
        {
            InitializeComponent();
            string arquivoExe = Assembly.GetEntryAssembly().Location;
            string caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                rptMedicaoServico1.SetTotais(parms.ValorMobilizacao, parms.ValorProjetoConsultoria, parms.ValorTotal);
                lblConstrutora.Text = parms.Construtora;
                lblResponsavel.Text = parms.Responsavel;
                lblAssunto.Text = parms.Assunto;
                lblObra.Text = parms.Obra;

                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Arquivo de imagem (Logotipo da empresa) não encontrado", "Atenção");
                return;
            }
            

        }

        public struct ParametroResumo
        {
            public string Construtora { get; set; }
            public string Responsavel { get; set; }
            public string Assunto { get; set; }
            public string Obra { get; set; }
            public string Data { get; set; }

            public decimal ValorMobilizacao { get; set; }
            public decimal ValorProjetoConsultoria { get; set; }
            public decimal ValorTotal { get; set; }
        }
    }
}
