using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoDimaggioSubRel : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoDimaggioSubRel(int IDPedido)
        {
            InitializeComponent();
            var dt = dttaPedidoItemDimaggio.GetData(IDPedido);
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrLabel1.Text.ToLower().Contains("gerenciamento"))
            {
                DetailBand db = (DetailBand)sender;
                db.Visible = false;
            }
        }

        private void GroupHeader2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            if (xrLabel1.Text.ToLower().Contains("gerenciamento"))
            {
                GroupHeader2.Visible = false;
            }
        }

        private void GroupFooter2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                string img = this.GetCurrentColumnValue("Imagem").ToString();
                if (String.IsNullOrEmpty(img))
                {
                    GroupFooter2.Visible = false;
                }
            }
            catch (Exception)
            {
                GroupFooter2.Visible = false;
            }
        }

        private void xrPictureBox1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            byte[] imageBytes = Convert.FromBase64String(this.GetCurrentColumnValue("Imagem").ToString());
            MemoryStream ms = new MemoryStream(imageBytes);
            xrPictureBox1.Image = Image.FromStream(ms, true);
        }

    }
}
