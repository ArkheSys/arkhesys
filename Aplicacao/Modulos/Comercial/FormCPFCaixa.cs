using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;

using System;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormCPFCaixa : Form
    {
        public cwkGestao.Modelo.Pessoa PessoaCadastrada { get; set; }

        public FormCPFCaixa()
        {
            InitializeComponent();
        }

        private void sbCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sbGravar_Click(object sender, EventArgs e)
        {
            Gravar();
        }

        private void Gravar()
        {
            if ((txt2CNPJ_CPF.Text != string.Empty) && (txt2CNPJ_CPF.Text.Length == 14))
            {
                var pessoaEncontrada = PessoaController.Instancia.GetAll().FirstOrDefault(o => o.CNPJ_CPF?.OnlyNumbers() == txt2CNPJ_CPF.Text.OnlyNumbers());
                if (pessoaEncontrada == null)
                {
                    var message = MessageBox.Show(this, "Deseja cadastrar o CPF do cliente?", "ATENÇAO", MessageBoxButtons.YesNo);
                    switch (message)
                    {
                        case DialogResult.Yes:
                            var formCliente = new FormPessoaPDV(txt2CNPJ_CPF.Text);
                            formCliente.ShowDialog(this);
                            if (formCliente.Selecionado != null && formCliente.Selecionado?.ID != 0)
                            {
                                PessoaCadastrada = formCliente.Selecionado;
                                Close();
                            }
                            break;
                        case DialogResult.No:
                            PessoaCadastrada = new cwkGestao.Modelo.Pessoa { CNPJ_CPF = txt2CNPJ_CPF.Text };
                            Close();
                            break;
                    }
                }
                else
                {
                    PessoaCadastrada = pessoaEncontrada;
                    Close();
                }
            }
            else
            {
                MessageBox.Show("CPF não Informado ou CPF inválido.\n Verifique!");
                txt2CNPJ_CPF.Focus();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    Gravar();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
