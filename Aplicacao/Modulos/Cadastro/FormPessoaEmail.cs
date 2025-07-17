using Aplicacao.Base;
using cwkGestao.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Aplicacao
{
    public partial class FormPessoaEmail : Aplicacao.IntermediariasTela.FormManutPessoaEmailIntermediaria
    {
        public FormPessoaEmail()
        {
            
        }

        public FormPessoaEmail(PessoaEmail Email)
        {
            Selecionado = Email;
        }

        protected override void SetarMascaras()
        {
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
        }

        protected override void AcoesAntesBase_Load(object sender, EventArgs e)
        {
            grupoTipo.Tag = Selecionado;
        }

        public static bool IsEmail(string strEmail)
        {
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (System.Text.RegularExpressions.Regex.IsMatch(strEmail, strModelo))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void OK()
        {
            try
            {
                txtEmail.Text = txtEmail.Text.Trim();
                if (!IsEmail(txtEmail.Text))
                {
                    MessageBox.Show(this, "Email inválido.", "Atenção!");
                    txtEmail.Focus();
                    return;
                }

                if (ValidarFormulario())
                {
                    TelaProObjeto(tcPrincipal);
                    if (Operacao < Acao.Consultar)
                    {
                        AcoesAntesSalvar();
                        controller.Salvar(Selecionado, Operacao);
                        AcoesDepoisSalvar();
                    }
                    this.Close();
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Não é possível"))
                    MessageBox.Show(e.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    new FormErro(e).ShowDialog();
            }
        }
    }
}
