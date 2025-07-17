using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;
using Relatorio.dsOfdComparativoPropostaVerificacoesTableAdapters;

namespace Relatorio.Relatorios
{
    public partial class rptAcompanhamentoMaquina : DevExpress.XtraReports.UI.XtraReport
    {
        public rptAcompanhamentoMaquina(DateTime? dtInicial, DateTime? dtFinal, string IDsMaquinas)
        {
            InitializeComponent();
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";           
            try
            {
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                p_AcompanhamentoMaquinaProdutividadeTableAdapter.GetData(dtInicial, dtFinal, IDsMaquinas);
            }
            catch (Exception)
            {
                try
                {
                    p_AcompanhamentoMaquinaProdutividadeTableAdapter.GetData(dtInicial, dtFinal, IDsMaquinas);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
