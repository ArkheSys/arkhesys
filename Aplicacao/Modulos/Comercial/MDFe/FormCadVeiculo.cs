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
    public partial class FormCadVeiculo : FormManutVeiculo
    {
        private Veiculo veiculo = null;
        private List<TipoRodado> rodados;
        private List<TipoCarroceria> carrocerias;
        private Acao ac;

        public FormCadVeiculo()
        {
            InitializeComponent();
            veiculo = new Veiculo();
            ac = Acao.Incluir;
        }

        public FormCadVeiculo(Veiculo _veiculo, Acao _ac)
        {
            InitializeComponent();
            veiculo = _veiculo;
            ac = _ac;
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            try
            {
                veiculo.Placa = txtPlaca.Text;
                veiculo.Modelo = txtModelo.Text;
                veiculo.Ativo = ckbAtivo.Checked ? 1 : 0;
                veiculo.Uf = lkpUF.Selecionado as UF;
                veiculo.Marca = txtMarca.Text;
                veiculo.Renavam = txtRenavam.Text;
                veiculo.AnoFabricacao = Convert.ToInt32(txtAnoFabricacao.Text);
                veiculo.AnoFabricacao = Convert.ToInt32(txtAnoModelo.Text);
                veiculo.TipoCarroceria = (cmbCarroceria.SelectedItem as TipoCarroceria).IDTipoCarroceria;
                veiculo.TipoRodado = (cmbTipoRodado.SelectedItem as TipoRodado).IDTipoRodado;
                veiculo.TaraEmKg = Convert.ToDecimal(txtTaraKG.Text);
                veiculo.CapacidadeEmKg = Convert.ToDecimal(txtCapacidadeKG.Text);
                veiculo.CapacidadeEmM3 = Convert.ToDecimal(txtCapacidadeM3.Text);

                VeiculoController.Instancia.Salvar(veiculo, ac);
                Selecionado = veiculo;
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
            if (veiculo == null)
                veiculo = new Veiculo();

            lkpUF.Exemplo = new UF { };
            lkpUF.CamposRestricoesAND = new List<string> { };
            lkpUF.Sessao = UFController.Instancia.getSession();

            rodados = TipoRodado.GetTipos();
            cmbTipoRodado.DataSource = rodados;
            cmbTipoRodado.DisplayMember = "descricao";
            cmbTipoRodado.ValueMember = "idtiporodado";
            cmbTipoRodado.SelectedItem = null;

            carrocerias = TipoCarroceria.GetTipos();
            cmbCarroceria.DataSource = carrocerias;
            cmbCarroceria.DisplayMember = "descricao";
            cmbCarroceria.ValueMember = "idtipocarroceria";
            cmbCarroceria.SelectedItem = null;

            HabilitarCampos(ac != Acao.Consultar);
            PreencherTela();
            txtPlaca.Focus();
        }

        private void HabilitarCampos(bool Habilitar)
        {
            txtPlaca.Enabled = Habilitar;
            txtModelo.Enabled = Habilitar;
            ckbAtivo.Enabled = Habilitar;
            lkpUF.Enabled = Habilitar;
            lkbUF.Enabled = Habilitar;
            txtMarca.Enabled = Habilitar;
            txtRenavam.Enabled = Habilitar;
            txtAnoFabricacao.Enabled = Habilitar;
            txtAnoModelo.Enabled = Habilitar;
            cmbCarroceria.Enabled = Habilitar;
            cmbTipoRodado.Enabled = Habilitar;
            txtTaraKG.Enabled = Habilitar;
            txtCapacidadeKG.Enabled = Habilitar;
            txtCapacidadeM3.Enabled = Habilitar;
            sbGravar.Enabled = Habilitar;
        }

        private void PreencherTela()
        {
            txtPlaca.Text = veiculo.Placa;
            txtModelo.Text = veiculo.Modelo;
            ckbAtivo.Checked = veiculo.Ativo == 1;

            if ((veiculo.Uf?.ID ?? 0) != 0)
                lkpUF.Localizar(veiculo.Uf.ID);

            txtMarca.Text = veiculo.Marca;
            txtRenavam.Text = veiculo.Renavam;
            txtAnoFabricacao.Text = veiculo.AnoFabricacao.ToString();
            txtAnoModelo.Text = veiculo.AnoFabricacao.ToString();
            cmbCarroceria.SelectedItem = carrocerias.Where(o => o.IDTipoCarroceria == veiculo.TipoCarroceria).FirstOrDefault();
            cmbTipoRodado.SelectedItem = rodados.Where(o => o.IDTipoRodado == veiculo.TipoRodado).FirstOrDefault();
            txtTaraKG.Text = veiculo.TaraEmKg.ToString("n2");
            txtCapacidadeKG.Text = veiculo.CapacidadeEmKg.ToString("n2");
            txtCapacidadeM3.Text = veiculo.CapacidadeEmM3.ToString("n2");
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

        public class TipoCarroceria
        {
            public int IDTipoCarroceria { get; set; }
            public string Descricao { get; set; }

            public static List<TipoCarroceria> GetTipos()
            {
                List<TipoCarroceria> Tipos = new List<TipoCarroceria>();
                Tipos.Add(new TipoCarroceria { IDTipoCarroceria = 0, Descricao = "Não Aplicavel" });
                Tipos.Add(new TipoCarroceria { IDTipoCarroceria = 1, Descricao = "Aberta" });
                Tipos.Add(new TipoCarroceria { IDTipoCarroceria = 2, Descricao = "Fechada/Bau" });
                Tipos.Add(new TipoCarroceria { IDTipoCarroceria = 3, Descricao = "Graneleira" });
                Tipos.Add(new TipoCarroceria { IDTipoCarroceria = 4, Descricao = "Porta/Container" });
                Tipos.Add(new TipoCarroceria { IDTipoCarroceria = 5, Descricao = "Sider" });
                return Tipos;
            }
        }

        public class TipoRodado
        {
            public int IDTipoRodado { get; set; }
            public string Descricao { get; set; }

            public TipoRodado() { }

            public static List<TipoRodado> GetTipos()
            {
                List<TipoRodado> Tipos = new List<TipoRodado>();
                Tipos.Add(new TipoRodado { IDTipoRodado = 1, Descricao = "Truck" });
                Tipos.Add(new TipoRodado { IDTipoRodado = 2, Descricao = "Toco" });
                Tipos.Add(new TipoRodado { IDTipoRodado = 3, Descricao = "Cavalo Mecânico" });
                Tipos.Add(new TipoRodado { IDTipoRodado = 4, Descricao = "VAN" });
                Tipos.Add(new TipoRodado { IDTipoRodado = 5, Descricao = "Utilitário" });
                Tipos.Add(new TipoRodado { IDTipoRodado = 6, Descricao = "Outros" });
                return Tipos;
            }
        }
    }
}
