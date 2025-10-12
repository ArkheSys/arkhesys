using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

namespace Aplicacao.Utilitarios
{
    public partial class FormAtualizaProdutosPreçoBase : Form
    {
        private IList<Produto> Produtos;
        public FormAtualizaProdutosPreçoBase()
        {
            InitializeComponent();

            Produtos = ProdutoController.Instancia.GetAll();
            foreach (var item in Produtos)
                item.PrecoBaseNovo = 0;

            lkpFornecedor.Sessao = PessoaController.Instancia.getSession();
            lkpFornecedor.Exemplo = new Pessoa() { BFornecedor = true };
            lkpFornecedor.CamposRestricoesAND = new List<String>() { "bFornecedor" };

            lkpGrupo1.Exemplo = new Grupo1();
            lkpGrupo1.Sessao = Grupo1Controller.Instancia.getSession();

            gcAtualizaPrecoBaseProdutos.DataSource = Produtos.Where(o => !string.IsNullOrEmpty(o.Barra)).OrderBy(prod => prod.NomeOrdenado).Distinct().ToList();
            gcAtualizaPrecoBaseProdutos.RefreshDataSource();
            gcAtualizaPrecoBaseProdutos.Refresh();
        }

        private IList<Produto> GetSelecionados()
        {
            return ((IList<Produto>)gvAtualizaPrecoBaseProdutos.DataSource).Where(o => o.Selecionado).ToList();
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Deseja Atualizar o preço Base dos Produtos?", "ATENCAO!", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            foreach (var item in GetSelecionados())
            {
                var Prod = ProdutoController.Instancia.LoadObjectById(item.ID);
                Prod.PrecoBase = item.PrecoBaseNovo;
                Prod.PrecoFornecedor = item.PrecoBaseNovo;
                Prod.DtPrecoFornecedor = DateTime.Now;
                Prod.DtPrecoBase = DateTime.Now;
                ProdutoController.Instancia.Salvar(Prod, Acao.Alterar);
            }
        }

        private void lkbGrupo1_Click(object sender, EventArgs e)
        {
            GridGenerica<Grupo1> a = new GridGenerica<Grupo1>(Grupo1Controller.Instancia.GetAll(), new FormGrupo1(), (Grupo1)lkpGrupo1.Selecionado, false);
            a.Selecionando = true;
            a.Text = "Tabela de " + (ConfiguracaoController.Instancia.GetConfiguracao().NomeGrupo1 ?? "Grupo1");
            a.ShowDialog();
            if (a.Selecionado != null)
                lkpGrupo1.EditValue = a.Selecionado;
        }

        private void btnlkpFornecedor_Click(object sender, EventArgs e)
        {
            GridGenerica<Pessoa> grid = new GridGenerica<Pessoa>(PessoaController.Instancia.GetAllFornecedores(), new FormPessoa(), (Pessoa)lkpFornecedor.Selecionado, false);
            grid.Selecionando = true;
            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            lkpFornecedor.EditValue = grid.Selecionado;
        }

        private void chbbTodos_CheckedChanged(object sender, EventArgs e)
        {
            if (chbbTodos.Checked)
                lkpFornecedor.EditValue = null;

            lkpFornecedor.Enabled = !chbbTodos.Checked;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void AtualizarFiltros()
        {
            var Grupo = lkpGrupo1.Selecionado as Grupo1;
            var Fornecedor = lkpFornecedor.Selecionado as Pessoa;

            gcAtualizaPrecoBaseProdutos.DataSource = Produtos
                .Where(o => Grupo != null ? o.Grupo1?.Nome == Grupo.Nome : true)
                .Where(o => Fornecedor != null ? o.Fornecedor?.Nome == Fornecedor.Nome : true)
                .Where(o => !string.IsNullOrEmpty(o.Barra))
                .OrderBy(prod => prod.NomeOrdenado)
                .Distinct()
                .ToList();

            gcAtualizaPrecoBaseProdutos.RefreshDataSource();
            gcAtualizaPrecoBaseProdutos.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AtualizarFiltros();
        }

        private void chkAtualizarNCM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lookupButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
