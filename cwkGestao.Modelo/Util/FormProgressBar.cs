using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cwkGestao.Modelo.Util
{
    public partial class FormProgressBar : Form
    {
        public string path;
        public FormProgressBar(string path)
        {
            InitializeComponent();
            this.path = path;
        }

        private void FormProgressBar_Load(object sender, EventArgs e)
        {
            pbStream.Value = 0;
            try
            {    
                using (FileStream f = File.Create(path))
                {
                    //pbStream.Value = Convert.ToInt32(f.Position);
                    //lblProgress.Text = Convert.ToString(Convert.ToInt64((100 * f.Position) / Convert.ToDouble(f.Length)) + "%");
                    //pbStream.Refresh();
                    lblProgress.Refresh();
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch(Exception ex)
            {
                string message = ex.Message;
                MessageBox.Show("O Arquivo não foi salvo!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
    }
}
