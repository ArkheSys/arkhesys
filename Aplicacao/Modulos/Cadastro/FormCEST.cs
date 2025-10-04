using System;
using System.Windows.Forms;

namespace Aplicacao
{
    public partial class FormCEST : Aplicacao.IntermediariasTela.FormManutCESTIntermediaria
    {
        public FormCEST()
        {
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                dxErroProvider.SetError(txtCodigo, "O campo código é obrigatório.");
                return;
            }
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                dxErroProvider.SetError(txtNome, "O campo descrição é obrigatório.");
                return;
            }
            base.sbGravar_Click(sender, e);
        }

        private void tpPrincipal_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtNome_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
