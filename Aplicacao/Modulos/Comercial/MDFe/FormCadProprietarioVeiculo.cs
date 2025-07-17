using Aplicacao.Base;
using Aplicacao.IntermediariasTela;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Cadastro
{
    public partial class FormCadProprietarioVeiculo : FormManutProprietarioVeiculo
    {
        private ProprietarioVeiculoMdfe proprietarioVeiculoMdfe = null;
        private List<TipoPropriedade> tipos;
        private Acao ac;

        public FormCadProprietarioVeiculo()
        {
            InitializeComponent();
            proprietarioVeiculoMdfe = new ProprietarioVeiculoMdfe();
            ac = Acao.Incluir;
        }

        public FormCadProprietarioVeiculo(ProprietarioVeiculoMdfe _proprietarioVeiculoMdfe, Acao _ac)
        {
            InitializeComponent();
            proprietarioVeiculoMdfe = _proprietarioVeiculoMdfe;
            ac = _ac;
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckb_fisica.Checked)
                {
                    proprietarioVeiculoMdfe.Cpf = txt_cpfcnpj.Text;
                    proprietarioVeiculoMdfe.Cnpj = null;
                }
                else
                {
                    proprietarioVeiculoMdfe.Cnpj = txt_cpfcnpj.Text;
                    proprietarioVeiculoMdfe.Cpf = null;
                }

                proprietarioVeiculoMdfe.Nome = txt_nome.Text;
                proprietarioVeiculoMdfe.Uf = lkpUF.Selecionado as UF;
                proprietarioVeiculoMdfe.Rntrc = txt_rntc.Text;
                proprietarioVeiculoMdfe.InscricaoEstadual = txt_inscestadual.Text;
                proprietarioVeiculoMdfe.CodigoAgenciaPorto = txt_agenciaporto.Text;
                proprietarioVeiculoMdfe.TipoProprietario = (cmb_tipopropriedade.SelectedItem as TipoPropriedade).IDTipoPropriedade;

                ProprietarioVeiculoMdfeController.Instancia.Salvar(proprietarioVeiculoMdfe, ac);
                Selecionado = proprietarioVeiculoMdfe;
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
            if (proprietarioVeiculoMdfe == null)
                proprietarioVeiculoMdfe = new ProprietarioVeiculoMdfe();

            lkpUF.Exemplo = new UF { };
            lkpUF.CamposRestricoesAND = new List<string> { };
            lkpUF.Sessao = UFController.Instancia.getSession();

            tipos = TipoPropriedade.GetTipos();
            cmb_tipopropriedade.DataSource = tipos;
            cmb_tipopropriedade.DisplayMember = "descricao";
            cmb_tipopropriedade.ValueMember = "idtipopropriedade";
            cmb_tipopropriedade.SelectedItem = null;

            HabilitarCampos(ac != Acao.Consultar);
            PreencherTela();
            txt_nome.Focus();
        }

        private void HabilitarCampos(bool Habilitar)
        {
            txt_nome.Enabled = Habilitar;
            ckb_fisica.Enabled = Habilitar;
            ckb_juridica.Enabled = Habilitar;
            lkpUF.Enabled = Habilitar;
            lkbUF.Enabled = Habilitar;
            txt_rntc.Enabled = Habilitar;
            txt_inscestadual.Enabled = Habilitar;
            txt_cpfcnpj.Enabled = Habilitar;
            txt_agenciaporto.Enabled = Habilitar;
            cmb_tipopropriedade.Enabled = Habilitar;
            sbGravar.Enabled = Habilitar;
        }

        private void PreencherTela()
        {
            txt_nome.Text = proprietarioVeiculoMdfe.Nome;

            if (!string.IsNullOrEmpty(proprietarioVeiculoMdfe.Cpf))
            {
                ckb_fisica.Checked = true;
                txt_cpfcnpj.Text = proprietarioVeiculoMdfe.Cpf;
            }
            else
            {
                ckb_juridica.Checked = true;
                txt_cpfcnpj.Text = proprietarioVeiculoMdfe.Cnpj;
            }

            if ((proprietarioVeiculoMdfe.Uf?.ID ?? 0) != 0)
                lkpUF.Localizar(proprietarioVeiculoMdfe.Uf.ID);

            txt_rntc.Text = proprietarioVeiculoMdfe.Rntrc;
            txt_inscestadual.Text = proprietarioVeiculoMdfe.InscricaoEstadual;
            txt_agenciaporto.Text = proprietarioVeiculoMdfe.CodigoAgenciaPorto;
            cmb_tipopropriedade.SelectedItem = tipos.Where(o => o.IDTipoPropriedade == proprietarioVeiculoMdfe.TipoProprietario).FirstOrDefault();
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

        public class TipoPropriedade
        {
            public int IDTipoPropriedade { get; set; }
            public string Descricao { get; set; }

            public TipoPropriedade() { }
            public static List<TipoPropriedade> GetTipos()
            {
                List<TipoPropriedade> Tipos = new List<TipoPropriedade>();
                Tipos.Add(new TipoPropriedade { IDTipoPropriedade = 0, Descricao = "TAC - Agregado" });
                Tipos.Add(new TipoPropriedade { IDTipoPropriedade = 1, Descricao = "TAC - Independente" });
                Tipos.Add(new TipoPropriedade { IDTipoPropriedade = 2, Descricao = "Outros" });
                return Tipos;
            }
        }
    }
}
