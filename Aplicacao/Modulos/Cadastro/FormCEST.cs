using System;
using System.Windows.Forms;
using cwkGestao.Negocio;
using cwkGestao.Modelo;

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
            dxErroProvider.ClearErrors();

            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                dxErroProvider.SetError(txtCodigo, "O campo código é obrigatório.");
                return;
            }
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                dxErroProvider.SetError(txtDescricao, "O campo descrição é obrigatório.");
                return;
            }

            string codigoDigitado = txtCodigo.Text;
            CEST cestExistente = CESTController.Instancia.GetByCodigo(codigoDigitado);

            if (cestExistente != null && cestExistente.ID != Selecionado.ID)
            {
                dxErroProvider.SetError(txtCodigo, "Código CEST já cadasrtado.");
                return;
            }

            Selecionado.Codigo = txtCodigo.Text;
            Selecionado.Descricao = txtDescricao.Text;

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
