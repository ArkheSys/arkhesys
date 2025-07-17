using MetroFramework.Forms;

using System;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaNotaVincularPOS : MetroForm
    {
        public string NumeroPOS = null;
        public FormFrenteCaixaNotaVincularPOS()
        {
            InitializeComponent();
        }

        private void FormFrenteCaixaNotaVincularPOS_Shown(object sender, EventArgs e)
        {
            txtTransacaoPos.Focus();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtTransacaoPos.Text))
                {
                    txtTransacaoPos.Focus();
                    return false;
                }

                NumeroPOS = txtTransacaoPos.Text;
                Close();
            }

            return base.ProcessDialogKey(keyData);
        }
    }
}
