using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using System.Reflection;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptDuplicataDev : DevExpress.XtraReports.UI.XtraReport
    {
        public rptDuplicataDev(bool ContraAprentacao, string caminho)
        {
            try
            {
                InitializeComponent();
                
                xrPictureBox1.Image = Bitmap.FromFile(caminho, true);
                xrPictureBox2.Image = Bitmap.FromFile(caminho, true);

                if (ContraAprentacao == true)
                {
                    xrLabel13.Visible = false;
                    xrLabel93.Visible = true;
                    xrLabel66.Visible = false;
                    xrLabel94.Visible = true;
                }
                else
                {
                    xrLabel13.Visible = true;
                    xrLabel93.Visible = false;
                    xrLabel66.Visible = true;
                    xrLabel94.Visible = false;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("LogoEmpresa"))
                    MessageBox.Show("Arquivo logotipo da empresa não encontrado " + ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                    MessageBox.Show(ex.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
