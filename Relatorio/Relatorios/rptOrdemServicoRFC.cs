using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptOrdemServicoRFC : DevExpress.XtraReports.UI.XtraReport
    {
        bool bPossuiHorasContratadas = false;
        public rptOrdemServicoRFC(int idPessoa, DateTime dtInicial, DateTime dtFinal, string horasContratadas)
        {
            InitializeComponent();
            string caminho = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\LogoEmpresa.jpg";
            try
            {
                PicBoxLogoEmpresa.Image = Bitmap.FromFile(caminho, true);
            }
            catch (Exception)
            {
            }


            
            string caminhoAssinatura = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            caminho = caminho + "\\Assinatura.jpg";
            try
            {
                PicBoxAssinatura.Image = Bitmap.FromFile(caminho, true);
            }
            catch (Exception)
            {
            }
            dsOrdemServicoRFCTableAdapter.GetData(idPessoa, dtInicial, dtFinal);
            if (!string.IsNullOrEmpty(horasContratadas))
            {
                if (horasContratadas != "000:00")
                {
                    bPossuiHorasContratadas = true;
                }
            }
        }

        private void rptOrdemServicoRFC_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (!bPossuiHorasContratadas)
            {
                lblTotalHoras.Visible = false;
                lblTotHorasContrato.Visible = false;
                txtTotalHoras.Visible = false;
                txtTotalHorasContrato.Visible = false;
            }
            else
            {
                lblTotalHoras.Visible = true;
                lblTotHorasContrato.Visible = true;
                txtTotalHoras.Visible = true;
                txtTotalHorasContrato.Visible = true;
            }
        }

    }
}
