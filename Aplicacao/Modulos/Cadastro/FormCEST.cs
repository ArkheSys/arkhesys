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

        private void FormCEST_Load(object sender, EventArgs e)
        {
            // Preenche o lookup com todos os segmentos cadastrados
            lkpSegmento.Sessao = CEST_SegmentoController.Instancia.getSession();
            lkpSegmento.Exemplo = new CEST_Segmento();
            lkpSegmento.CamposPesquisa = new[] { "Codigo", "Descricao" };
            lkpSegmento.ColunaDescricao = new[] { "Código", "Descrição" };
            lkpSegmento.ColunaTamanho = new[] { "50", "350" };

            if (Operacao != Acao.Incluir)
            {
                lkpSegmento.Localizar(Selecionado.Segmento.ID);
            }

        }
    }
}
