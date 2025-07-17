using Aplicacao.Base;
using Aplicacao.IntermediariasTela;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Cadastro
{
    public partial class FormCadCondutor : FormManutCondutor
    {
        private Condutor condutor = null;
        private Acao ac;

        public FormCadCondutor()
        {
            InitializeComponent();
            condutor = new Condutor();
            ac = Acao.Incluir;
        }

        public FormCadCondutor(Condutor _condutor, Acao _ac)
        {
            InitializeComponent();
            condutor = _condutor;
            ac = _ac;
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            try
            {
                condutor.Nome = txt_nome.Text;
                condutor.Uf = lkpUF.Selecionado as UF;
                condutor.Cpf = txt_cpfcnpj.Text;
                condutor.Ativo = ckbAtivo.Checked ? 1 : 0;
                CondutorController.Instancia.Salvar(condutor, ac);
                Selecionado = condutor;
                Close();
            }
            catch (Exception Ex)
            {
                FormErro Form = new FormErro(Ex);
                Form.ShowDialog(this);
            }

            //base.sbGravar_Click(sender, e);
        }

        private void sbCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormCadVeiculo_Shown(object sender, EventArgs e)
        {
            if (condutor == null)
                condutor= new Condutor();

            lkpUF.Exemplo = new UF { };
            lkpUF.CamposRestricoesAND = new List<string> { };
            lkpUF.Sessao = UFController.Instancia.getSession();

            HabilitarCampos(ac != Acao.Consultar);
            PreencherTela();
            txt_nome.Focus();
        }

        private void HabilitarCampos(bool Habilitar)
        {
            txt_nome.Enabled = Habilitar;
            txt_cpfcnpj.Enabled = Habilitar;
            ckbAtivo.Enabled = Habilitar;
            lkpUF.Enabled = Habilitar;
            lkbUF.Enabled = Habilitar;
            sbGravar.Enabled = Habilitar;
        }

        private void PreencherTela()
        {
            txt_cpfcnpj.Text = condutor.Cpf;
            txt_nome.Text = condutor.Nome;

            if ((condutor.Uf?.ID ?? 0) != 0)
                lkpUF.Localizar(condutor.Uf.ID);

            ckbAtivo.Checked = condutor.Ativo == 1;
        }

        private void lkbUF_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<UF>(UFController.Instancia.GetAll(), new FormUF(), (UF)lkpUF.Selecionado, false);
            grid.Selecionando = true;
            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            if (grid.Selecionado != null)
                lkpUF.Localizar(grid.Selecionado.ID);
        }

        private void FormCadVeiculo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(27))
                Close();
        }
    }
}
