using System;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaCancelarVenda : Form
    {
        public string MotivoCancelamento { get; set; }
        public FormFrenteCaixaCancelarVenda()
        {
            InitializeComponent();
        }

        private void BtnDescartar_Click(object sender, EventArgs e)
        {
            Descartar();
        }

        private void Descartar()
        {
            MotivoCancelamento = txtMotivo.Text;
            Close();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormFrenteCaixaCancelarVenda_Shown(object sender, EventArgs e)
        {
            txtMotivo.Focus();
        }
    }
}
