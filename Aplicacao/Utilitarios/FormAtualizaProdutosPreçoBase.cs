using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Aplicacao.Util;
using Aplicacao.Modulos.Tributacao;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Padroes;

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

            lkpNCM.Sessao = NCMController.Instancia.getSession();
            lkpNCM.Exemplo = new NCM();

            lkpCEST.Sessao = CESTController.Instancia.getSession();
            lkpCEST.Exemplo = new CEST();

            lkpPerfilPisCofins.Sessao = PerfilPisCofinsController.Instancia.getSession();
            lkpPerfilPisCofins.Exemplo = new PerfilPisCofins();

            lkpNaturezaReceita.Sessao = PisCofinsNaturezaOperacaoController.Instancia.getSession();
            lkpNaturezaReceita.Exemplo = new PisCofinsNaturezaOperacao();

            lkpClassificacaoFiscal.Sessao = ClassificacaoFiscalController.Instancia.getSession();
            lkpClassificacaoFiscal.Exemplo = new ClassificacaoFiscal();          

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

        private void lkbNCM_Click(object sender, EventArgs e)
        {
            {
                var TodosOsNCM = NCMController.Instancia.GetAll();
                NCM NCMSelecionado = null;

                int.TryParse(lkpNCM.EditValue?.ToString(), out var idSelecionado);
                if (!idSelecionado.Equals(0))
                    NCMSelecionado = TodosOsNCM.Where(o => o.ID == idSelecionado).FirstOrDefault();

                var grid = new GridGenerica<NCM>(TodosOsNCM, new FormNCM(), NCMSelecionado, false);
                grid.Selecionando = true;
                if (cwkControleUsuario.Facade.ControleAcesso(grid))
                    grid.ShowDialog();

                if (grid.Selecionado != null)
                {
                    lkpNCM.EditValue = grid.Selecionado;
                }
            }
        }

        private void lkbCEST_Click(object sender, EventArgs e)
        {
            var todosOsCests = CESTController.Instancia.GetAll();

            LookupUtil.GridLookup<CEST>(lkpCEST, typeof(FormCEST), todosOsCests);
        }

        private void lkbPerfilPisCofins_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<PerfilPisCofins>(lkpPerfilPisCofins, typeof(FormPerfilPisCofins));
        }

        private void lkbClassificacaoFiscal_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<ClassificacaoFiscal>(lkpClassificacaoFiscal, typeof(FormClassificacaoFiscal));
        }

        private void lkbNaturezaReceita_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<PisCofinsNaturezaOperacao>(lkpNaturezaReceita, typeof(FormPisCofinsNaturezaOperacao));
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            AtualizarFiltros();
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            var produtosSelecionados = GetSelecionados();
            if (produtosSelecionados.Count == 0)
            {
                MessageBox.Show("Nenhum produto selecionado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validação: Verifica se algum campo foi selecionado para atualização
            if (!chkAtualizarNCM.Checked && !chkAtualizarCEST.Checked &&
        !chkAtualizarPisCofins.Checked && !chkAtualizarClassificacaoFiscal.Checked && !chkAtualizarNatureza.Checked)
            {
                MessageBox.Show("Selecione pelo menos um campo para atualizar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Valida se os campos marcados foram preenchidos
            
            if (chkAtualizarNCM.Checked && lkpNCM.Selecionado == null)
            {
                MessageBox.Show("Selecione um NCM.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chkAtualizarCEST.Checked && lkpCEST.Selecionado == null)
            {
                MessageBox.Show("Selecione um CEST.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chkAtualizarPisCofins.Checked && lkpPerfilPisCofins.Selecionado == null)
            {
                MessageBox.Show("Selecione um Perfil PIS/COFINS.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chkAtualizarClassificacaoFiscal.Checked && lkpClassificacaoFiscal.Selecionado == null)
            {
                MessageBox.Show("Selecione uma Classificação Fiscal.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (chkAtualizarNatureza.Checked && lkpNaturezaReceita.Selecionado == null)
            {
                MessageBox.Show("Selecione uma Natureza da Receita.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string msg = $"Deseja atualizar os {produtosSelecionados.Count} produtos selecionados?\nEsta operação não pode ser desfeita.";
            if (MessageBox.Show(this, msg, "CONFIRMAÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            var novoNCM = lkpNCM.Selecionado as NCM;
            var novoCEST = lkpCEST.Selecionado as CEST;
            var novoPerfilPisCofins = lkpPerfilPisCofins.Selecionado as PerfilPisCofins;
            var novaClassificacao = lkpClassificacaoFiscal.Selecionado as ClassificacaoFiscal;
            var novaNatureza = lkpNaturezaReceita.Selecionado as PisCofinsNaturezaOperacao; 

            var session = ProdutoController.Instancia.getSession();
            //var transacao = session.BeginTransaction();

            try
            {
                foreach (var produtoDaGrid in produtosSelecionados)
                {
                    var produtoParaSalvar = ProdutoController.Instancia.LoadObjectById(produtoDaGrid.ID);

                    if (chkAtualizarNCM.Checked)
                    {
                        produtoParaSalvar.TabelaNCM = novoNCM;
                        produtoParaSalvar.NCM = novoNCM?.Ncm;
                        produtoParaSalvar.ID_NCM = novoNCM?.ID ?? 0;
                    }
                    if (chkAtualizarCEST.Checked)
                    {
                        produtoParaSalvar.CEST = novoCEST?.Codigo;
                    }
                    if (chkAtualizarPisCofins.Checked)
                    {
                        produtoParaSalvar.PerfilPisCofins = novoPerfilPisCofins;
                    }
                    if (chkAtualizarClassificacaoFiscal.Checked)
                    {
                        produtoParaSalvar.ClassificacaoFiscal = novaClassificacao;
                    }
                    if (chkAtualizarNatureza.Checked)
                    {
                        produtoParaSalvar.SaiNaturezaReceita = novaNatureza?.Codigo.ToString();
                    }

                    ProdutoController.Instancia.Salvar(produtoParaSalvar, Acao.Alterar);
                }

                //transacao.Commit();
                MessageBox.Show("Produtos atualizados com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Produtos = ProdutoController.Instancia.GetAll();
                AtualizarFiltros();
            }
            catch (Exception ex)
            {
                //transacao.Rollback();
                MessageBox.Show("Ocorreu um erro ao atualizar os produtos.\n\nDetalhes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chbbMarca_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = sender as DevExpress.XtraEditors.CheckEdit;
            if (checkBox == null) return;

            bool selecionar = checkBox.Checked;

            gvAtualizaPrecoBaseProdutos.BeginUpdate();
            try
            {
                for (int i = 0; i < gvAtualizaPrecoBaseProdutos.DataRowCount; i++)
                {
                    int rowHandle = gvAtualizaPrecoBaseProdutos.GetRowHandle(i);

                    gvAtualizaPrecoBaseProdutos.SetRowCellValue(rowHandle, "Selecionado", selecionar);
                }
            }
            finally
            {
                gvAtualizaPrecoBaseProdutos.EndUpdate();
            }
        }

        private void btnInverterSelecao_Click(object sender, EventArgs e)
        {
            gvAtualizaPrecoBaseProdutos.BeginUpdate();
            try
            {
                for (int i = 0; i < gvAtualizaPrecoBaseProdutos.DataRowCount; i++)
                {
                    int rowHandle = gvAtualizaPrecoBaseProdutos.GetRowHandle(i);

                    bool valorAtual = Convert.ToBoolean(gvAtualizaPrecoBaseProdutos.GetRowCellValue(rowHandle, "Selecionado"));

                    bool novoValor = !valorAtual;

                    gvAtualizaPrecoBaseProdutos.SetRowCellValue(rowHandle, "Selecionado", novoValor);
                }
            }
            finally
            {
                gvAtualizaPrecoBaseProdutos.EndUpdate();
            }
        }
    }
}
