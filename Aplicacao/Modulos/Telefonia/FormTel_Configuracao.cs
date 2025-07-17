using System;
using System.Collections.Generic;
using System.Windows.Forms;

using cwkGestao.Modelo;

namespace Aplicacao.Modulos.Telefonia
{
    public partial class FormTel_Configuracao : Aplicacao.IntermediariasTela.FormManutTel_Configuracao
    {
        public FormTel_Configuracao()
        {
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            SetupSubForms();
        }

        private void SetupSubForms()
        {
            lkpPlanoConta.Exemplo = new PlanoConta() { BTitulo = false };
            lkpPlanoConta.CamposRestricoesAND = new List<string>() { "btitulo" };
            lkbPlanoConta.SubFormType = typeof(FormPlanoConta);
            lkbFilial.SubFormType = typeof(FormFilial);
            lkbAcrescimo.SubFormType = typeof(FormAcrescimo);
            lkbCondicao.SubFormType = typeof(FormCondicao);
            lkbHistorico.SubFormType = typeof(FormHistorico);
            lkbPortador.SubFormType = typeof(FormPortador);
            lkbTipoDocumento.SubFormType = typeof(FormTipoDocumento);
            lkbServicoJuros.SubFormType = typeof(FormTel_Servico);
            lkbServicoMulta.SubFormType = typeof(FormTel_Servico);
        }

        protected override List<Exception> ObjetoPraTela(Control pai)
        {
            chbSolicitaConfirmaEmailTelefonia.Checked = Selecionado.SolicitaConfirmaEmailTelefonia;

            txtUrlBanco.Text = Selecionado.UrlBanco;
            txtClientIDBanco.Text = Selecionado.ClientIDBanco;
            txtSecretIDBanco.Text = Selecionado.SecretIDBanco;
            txtUrlCertificado.Text = Selecionado.UrlCertificado;
            txtChavePix.Text = Selecionado.ChavePix;

            return base.ObjetoPraTela(pai);
        }
        protected override void OK()
        {
            Selecionado.ConsideraFixo = cbFixos.Checked;
            Selecionado.UrlBanco = txtUrlBanco.Text;
            Selecionado.ClientIDBanco = txtClientIDBanco.Text;
            Selecionado.SecretIDBanco = txtSecretIDBanco.Text;
            Selecionado.UrlCertificado = txtUrlCertificado.Text;
            Selecionado.SolicitaConfirmaEmailTelefonia = chbSolicitaConfirmaEmailTelefonia.Checked;
            Selecionado.ChavePix = txtChavePix.Text;

            base.OK();
        }

        private void FormTel_Configuracao_Shown(object sender, EventArgs e)
        {
            cbFixos.Checked = Selecionado.ConsideraFixo;
        }
    }
}
