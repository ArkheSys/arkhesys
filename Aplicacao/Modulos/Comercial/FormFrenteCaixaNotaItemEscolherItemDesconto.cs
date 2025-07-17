
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaNotaItemEscolherItemDesconto : Form
    {
        public bool Salvou = false;
        public FormFrenteCaixaNotaItemEscolherItemDesconto()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Salvou = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Salvou = false;
            Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Salvou = false;
                    Close();
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void FormFrenteCaixaNotaItemEscolherItemDesconto_Shown(object sender, EventArgs e)
        {
            txtSequencia.Focus();
        }

        private void txtSequencia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                button1.Focus();
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightGray;
            button1.ForeColor = Color.Black;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
        }
    }
}