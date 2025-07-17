using cwkGestao.Modelo;
using cwkGestao.Negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Utilitarios
{
    public partial class GridPromocoes : Form
    {
        private List<Promocao> Promocoes = null;

        public GridPromocoes()
        {
            InitializeComponent();

            Promocoes = PromocaoController.Instancia.GetAll().ToList();
            CarregarTela(false);
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sbIncluir_Click(object sender, EventArgs e)
        {
            var form = new FormPromocao(null, Acao.Incluir);
            form.ShowDialog(this);

            CarregarTela(true);
        }

        private void CarregarTela(bool CarregarBanco)
        {
            if (CarregarBanco)
                Promocoes = PromocaoController.Instancia.GetAll().ToList();
            ovGRD_Promocoes.DataSource = Promocoes;
            ovGRD_Promocoes.RefreshDataSource();
            ovGRD_Promocoes.Refresh();
        }

        private void sbAlterar_Click(object sender, EventArgs e)
        {
            FormPromocao Form = new FormPromocao((Promocao)ovGV_Promocoes.GetFocusedRow(), cwkGestao.Modelo.Acao.Alterar);
            Form.ShowDialog();
            CarregarTela(true);
        }

        private void sbExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja excluir o registro selecionado?", "ATENÇÃO!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                PromocaoController.Instancia.RegisterDeleteIntoTransaction((Promocao)ovGV_Promocoes.GetFocusedRow());
                PromocaoController.Instancia.CommitUnitOfWorkTransaction();
                CarregarTela(true);
            }
        }

        private void ovGRD_Promocoes_DoubleClick(object sender, EventArgs e)
        {
            var selecionado = (Promocao)ovGV_Promocoes.GetFocusedRow();
            if (selecionado == null)
                return;

            FormPromocao Form = new FormPromocao((Promocao)ovGV_Promocoes.GetFocusedRow(), cwkGestao.Modelo.Acao.Alterar);
            Form.ShowDialog();
            CarregarTela(true);
        }
    }
}
