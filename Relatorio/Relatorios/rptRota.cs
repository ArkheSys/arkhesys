using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptRota : DevExpress.XtraReports.UI.XtraReport
    {
        public rptRota(int tipoPeriodo, DateTime dataInicio, DateTime datafim, int situacao, string idResponsavel, string idOperador, string idCliente, string idEmpresa)
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

            try
            {
                rotaTableAdapter.GetData(tipoPeriodo, dataInicio, datafim, situacao, idResponsavel, idOperador, idCliente, idEmpresa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            if (String.IsNullOrEmpty(idResponsavel) && String.IsNullOrEmpty(idOperador))
            {
                GHRota.Visible = false;
                GHRota.GroupFields.Clear();
                GFRota.Visible = false;
                GHSeparador.Visible = true;
                GFSeparador.Visible = true;
            }
            else
            {
                GHSeparador.Visible = false;
                GFSeparador.Visible = false;
            }
        }

    }
}
