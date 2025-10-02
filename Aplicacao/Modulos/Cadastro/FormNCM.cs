using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using cwkGestao.Modelo;

namespace Aplicacao
{
    public partial class FormNCM : Aplicacao.IntermediariasTela.FormManutNCMIntermediaria
    {
        public FormNCM()
        {

        }
        public void CarregarCampos()
        {
            
            txtDtRevogacao.EditValue = Selecionado.DtRevogacao;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            object[] parms = new[] { new NCMItem() { NCM = Selecionado } };
            Type formType = typeof(FormNCMItem);

            btIncluirNCMItens.SubFormType = btAlterarNCMItens.SubFormType = btExcluirNCMItens.SubFormType = btConsultarNCMItens.SubFormType = formType;
            btIncluirNCMItens.SubFormTypeParams = btAlterarNCMItens.SubFormTypeParams = btExcluirNCMItens.SubFormTypeParams = btConsultarNCMItens.SubFormTypeParams = parms;

            //var formNCMItem = new FormNCMItem() { Selecionado = new NCMItem() { NCM = Selecionado } };
            //btIncluirNCMItens.SubForm = formNCMItem;
            //btAlterarNCMItens.SubForm = formNCMItem;
            //btExcluirNCMItens.SubForm = formNCMItem;
            //btConsultarNCMItens.SubForm = formNCMItem;
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            if (Obrigatório() == false)
            {
                return;
            }

            Selecionado.DtRevogacao = Convert.ToDateTime(txtDtRevogacao.EditValue);

            base.sbGravar_Click(sender, e);
        }

        private bool Obrigatório()
        {
            /*if (txtEnqGeral.Text.Length == 0 || Convert.ToInt32(txtEnqGeral.Text) == 0)
            {
                dxErroProvider.SetError(txtEnqGeral, "Valor não pode ser nulo ou igual a 0 ");
                return false;
            }*/

            return true;
        }

        private void FormNCM_Load(object sender, EventArgs e)
        {
            CarregarCampos();
        }

        private void TxtEnqGeral_Enter(object sender, EventArgs e)
        {
            dxErroProvider.ClearErrors();
        }
    }
}
