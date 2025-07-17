using cwkGestao.Modelo;
using cwkGestao.Negocio;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Utilitarios
{
    public partial class GridEscolhaProdutos : Form
    {
        public List<Produto> Selecionados = null;

        public GridEscolhaProdutos()
        {
            InitializeComponent();

            gcProdutos.DataSource = ProdutoController.Instancia.GetAll().Where(o => !string.IsNullOrEmpty(o.Barra)).OrderBy(prod => prod.NomeOrdenado).Distinct().ToList();
            gcProdutos.RefreshDataSource();
            gcProdutos.Refresh();
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Selecionados = null;
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Selecionados = (gcProdutos.DataSource as IList<Produto>).Where(o => o.Selecionado).ToList();
            Close();
        }
    }
}
