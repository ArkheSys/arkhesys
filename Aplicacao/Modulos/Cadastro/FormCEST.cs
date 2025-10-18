using System;
using System.Windows.Forms;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using Cwork.Utilitarios.Componentes;

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
            Type formType = typeof(FormNCMCEST);

            btIncluirNCM.SubFormType = btAlterarNCM.SubFormType = btExcluirNCM.SubFormType = btConsultarNCM.SubFormType = formType;

            btIncluirNCM.GridControl = gcNcmVinculados;
            btAlterarNCM.GridControl = gcNcmVinculados;
            btExcluirNCM.GridControl = gcNcmVinculados;
            btConsultarNCM.GridControl = gcNcmVinculados;
        }

        protected override void btSubRegistro_Click(object sender, EventArgs e)
        {
            TelaProObjeto(tcPrincipal);

            if (sender is DevButton botao)
            {
                object[] parms = new[] { new NCMCEST() { CEST = Selecionado } };
                botao.SubFormTypeParams = parms;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (Operacao == Acao.Incluir)
            {
                ControlaBotoesSubGrid(false);
            }
        }

        private void ControlaBotoesSubGrid(bool habilitar)
        {
            btIncluirNCM.Enabled = habilitar;
            btAlterarNCM.Enabled = habilitar;
            btExcluirNCM.Enabled = habilitar;
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            base.sbGravar_Click(sender, e);
            if (this.Operacao == Acao.Alterar)
            {
                ControlaBotoesSubGrid(true);
            }
        }
    }
}