using Aplicacao.Modulos.Cadastro;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using System;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class GridProprietarioVeiculo : Form
    {
        public ProprietarioVeiculoMdfe Selecionado = null;

        private void AtualizarGrid()
        {
            var lista = ProprietarioVeiculoMdfeController.Instancia.GetAll();
            ovGRD_Proprietario.DataSource = lista;
            ovGRD_Proprietario.RefreshDataSource();
            ovGRD_Proprietario.Refresh();
        }

        public GridProprietarioVeiculo()
        {
            InitializeComponent();
            AtualizarGrid();
        }

        private void btn_Incluir_Click(object sender, EventArgs e)
        {
            FormCadProprietarioVeiculo Form = new FormCadProprietarioVeiculo(null, cwkGestao.Modelo.Acao.Incluir);
            Form.ShowDialog(this);
            AtualizarGrid();
        }

        private void btn_Alterar_Click(object sender, EventArgs e)
        {
            FormCadProprietarioVeiculo Form = new FormCadProprietarioVeiculo((ProprietarioVeiculoMdfe)ovGV_Proprietario.GetFocusedRow(), cwkGestao.Modelo.Acao.Alterar);
            Form.ShowDialog(this);
            AtualizarGrid();
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o registro selecionado?", "ATENÇÃO!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ProprietarioVeiculoMdfeController.Instancia.RegisterDeleteIntoTransaction((ProprietarioVeiculoMdfe)ovGV_Proprietario.GetFocusedRow());
                ProprietarioVeiculoMdfeController.Instancia.CommitUnitOfWorkTransaction();
                AtualizarGrid();
            }
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_Consulta_Click(object sender, EventArgs e)
        {
            FormCadProprietarioVeiculo Form = new FormCadProprietarioVeiculo((ProprietarioVeiculoMdfe)ovGV_Proprietario.GetFocusedRow(), cwkGestao.Modelo.Acao.Consultar);
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