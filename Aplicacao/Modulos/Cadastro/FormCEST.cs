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
            InitializeComponent();
        }
        private void FormCEST_Load(object sender, EventArgs e)
        {
            if (Selecionado != null)
            {
                // Carrega os campos de texto simples
                txtCodigo.EditValue = Selecionado.Codigo;
                txtDescricao.EditValue = Selecionado.Descricao;
                txtNCM.EditValue = Selecionado.NCM;

                int indexParaSelecionar = Selecionado.Segmento;

                // Garante que o índice (valor salvo) seja válido para a lista de itens.
                if (indexParaSelecionar >= 0 && indexParaSelecionar < cbSegmento.Properties.Items.Count)
                {
                    cbSegmento.SelectedIndex = indexParaSelecionar;
                }
                else
                {
                    cbSegmento.SelectedIndex = -1;
                }
            }
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            // Suas validações existentes (estão corretas)
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
                dxErroProvider.SetError(txtCodigo, "Código CEST já cadastrado.");
                return;
            }

            Selecionado.Codigo = txtCodigo.Text;
            Selecionado.Descricao = txtDescricao.Text;

            if (txtNCM.EditValue != null)
            {
                // Remove pontos e traços que possam vir da máscara
                Selecionado.NCM = txtNCM.Text.Replace(".", "").Replace("-", "");
            }
            else
            {
                Selecionado.NCM = null; // Garante que o campo fique nulo se estiver vazio
            }

            Selecionado.Segmento = Math.Max(0, cbSegmento.SelectedIndex);

            base.sbGravar_Click(sender, e);

        }
    }
}