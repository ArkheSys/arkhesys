using Aplicacao.Modulos.Cadastro;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using System;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class GridVeiculo : Form
    {
        public Veiculo Selecionado = null;
        //public bool VemDeSelecao = false;
        //public FormCotacaoDeMoedas form = null;

        private void AtualizarGrid()
        {
            var lista = VeiculoController.Instancia.GetAll();
            ovGRD_Veiculos.DataSource = lista;
            ovGRD_Veiculos.RefreshDataSource();
            ovGRD_Veiculos.Refresh();
        }

        public GridVeiculo() //bool VemDeSelecao = false)
        {
            InitializeComponent();
            //this.VemDeSelecao = VemDeSelecao;
            //sbSelecionar.Visible = VemDeSelecao;

            AtualizarGrid();
        }

        private void btn_Incluir_Click(object sender, EventArgs e)
        {
            FormCadVeiculo Form = new FormCadVeiculo(null, cwkGestao.Modelo.Acao.Incluir);
            Form.ShowDialog(this);
            AtualizarGrid();
        }

        private void btn_Alterar_Click(object sender, EventArgs e)
        {
            FormCadVeiculo Form = new FormCadVeiculo((Veiculo)ovGV_Veiculos.GetFocusedRow(), cwkGestao.Modelo.Acao.Alterar);
            Form.ShowDialog(this);
            AtualizarGrid();
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o registro selecionado?", "ATENÇÃO!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                VeiculoController.Instancia.RegisterDeleteIntoTransaction((Veiculo)ovGV_Veiculos.GetFocusedRow());
                VeiculoController.Instancia.CommitUnitOfWorkTransaction();
                AtualizarGrid();
            }
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Consulta_Click(object sender, EventArgs e)
        {
            FormCadVeiculo Form = new FormCadVeiculo((Veiculo)ovGV_Veiculos.GetFocusedRow(), cwkGestao.Modelo.Acao.Consultar);
            Form.ShowDialog(this);
            AtualizarGrid();
        }

        private void ovGRD_Moeda_DoubleClick(object sender, EventArgs e)
        {
            //if (VemDeSelecao)
            //    sbSelecionar_Click(sender, e);
            //else
            //    btn_Alterar_Click(sender, e);
        }

        private void GridMoeda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(27)) // Esc
                Close();
        }

        private void sbSelecionar_Click(object sender, EventArgs e)
        {
            //Selecionado = (Moeda)ovGV_Veiculos.GetFocusedRow();
            //form.SelecionarMoeda(Selecionado);
            //Close();
        }
    }
}