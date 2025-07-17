using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Aplicacao.Interfaces;
using DevExpress.Data;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using System.Linq;
using DevExpress.XtraGrid;
using Aplicacao.Base;
using Aplicacao.Util;

namespace Aplicacao
{
    public partial class GridProdutoGenMaisDetalhado : Aplicacao.IntermediariasTela.GridProdutoIntermediaria
    {
        ProdutoController controller = ProdutoController.Instancia;
        private bool selecionarVarios;
        private DevExpress.XtraEditors.SimpleButton buttonSelecionado;
        private IList<Produto> ListaOriginal = new List<Produto>();
        public IList<Produto> ProdutosSelecionados { get; private set; }

        #region Construtores
        public GridProdutoGenMaisDetalhado(IList<Produto> lista, bool adicionarFiltro, Type formManut, params object[] parametros)
            : this(lista, null, false, formManut, parametros)
        {
            ListaOriginal = lista;
            this.Selecionando = true;
            sbImprimirFiltro.Visible = false;
            sbImprimirFiltro.Enabled = false;
            gcPrincipal.Location = new Point(gcPrincipal.Location.X, gcPrincipal.Location.Y + 23);
        }

        public GridProdutoGenMaisDetalhado(IList<Produto> lista, Produto selecionado, bool adicionarFiltro, Type formManut, params object[] parametros)
            : base(lista, selecionado, adicionarFiltro, formManut, parametros)
        {
            ListaOriginal = lista;

            CustomSort = new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(CustomColumnSort);
            this.gvPrincipal.CustomColumnSort += new DevExpress.XtraGrid.Views.Base.CustomColumnSortEventHandler(CustomColumnSort);
            base.lista = lista.OrderBy(prod => prod.NomeOrdenado).ToList();


            InitializeComponent();

            this.gvPrincipal.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvPrincipal_FocusedRowChanged);

            if (selecionado != null) this.gvPrincipal.SelectRow(((System.Collections.IList)gvPrincipal.DataSource).IndexOf(selecionado));
            MontaGridTabPreco();
            MontarColunas();
        }

        protected override string GetNomeEntidade()
        {
            return "Seleção de Produto";
        }

        public GridProdutoGenMaisDetalhado(IList<Produto> produtos, Produto selecionado, string textoProcurado, bool selecionarVarios, bool adicionarFiltro, Type formManut, params object[] parametros)
            : this(produtos, adicionarFiltro, formManut, parametros)
        {
            ListaOriginal = produtos;

            this.selecionarVarios = selecionarVarios;
        }

        #endregion

        private void MontaGridTabPreco()
        {
            Produto registroSelecionado = GetRegistroSelecionado();
            if (registroSelecionado != null)
            {
                gcTabelaPreco.DataSource = controller.GetPrecosProduto(registroSelecionado.ID);
            }
            else
            {
                gcTabelaPreco.DataSource = null;
            }
        }

        private void gvPrincipal_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            MontaGridTabPreco();
        }

        void CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            try
            {
                if (e.Column.FieldName == "Nome")
                {
                    cwkGestao.Modelo.Produto valor1 = (cwkGestao.Modelo.Produto)(((List<cwkGestao.Modelo.Produto>)((DevExpress.XtraGrid.Views.Grid.GridView)sender).DataSource))[e.ListSourceRowIndex1];
                    cwkGestao.Modelo.Produto valor2 = (cwkGestao.Modelo.Produto)(((List<cwkGestao.Modelo.Produto>)((DevExpress.XtraGrid.Views.Grid.GridView)sender).DataSource))[e.ListSourceRowIndex2];
                    e.Handled = true;
                    e.Result = valor1.NomeOrdenado.CompareTo(valor2.NomeOrdenado);
                }
                if (e.Column.FieldName == "Codigo")
                {
                    cwkGestao.Modelo.Produto valor1 = (cwkGestao.Modelo.Produto)(((List<cwkGestao.Modelo.Produto>)((DevExpress.XtraGrid.Views.Grid.GridView)sender).DataSource))[e.ListSourceRowIndex1];
                    cwkGestao.Modelo.Produto valor2 = (cwkGestao.Modelo.Produto)(((List<cwkGestao.Modelo.Produto>)((DevExpress.XtraGrid.Views.Grid.GridView)sender).DataSource))[e.ListSourceRowIndex2];
                    e.Handled = true;
                    e.Result = valor1.CodigoOrdenado.CompareTo(valor2.CodigoOrdenado);
                }
            }
            catch (Exception ex)
            {
                e.Result = 0;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            MontaGridTabPreco();
            ProdutosSelecionados = new List<Produto>();
            Text = "Selecionando Produtos";
            
            pnlFiltro.Visible = true;
            pnlFiltro.Enabled = true;
            ucFiltroGridGenerica.Enabled = false;
            ucFiltroGridGenerica.Visible = false;
            base.OnShown(e);

            gvPrincipal.Columns.ColumnByFieldName("Nome").SortMode = ColumnSortMode.Custom;
            ConfigurarColunasGrupos();

            gvPrincipal.KeyDown += (sender, i) =>
            {
                if (i.KeyCode == Keys.F2)
                {
                    i.Handled = true;
                }
            };

            gvPrincipal.OptionsFind.AllowFindPanel = false;
        }

        private void ConfigurarColunasGrupos()
        {
            var configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            var grupo1 = gvPrincipal.Columns.ColumnByFieldName("Grupo1");
            if (grupo1 != null)
            {
                grupo1.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
                if (!String.IsNullOrEmpty(configuracao.NomeGrupo1))
                    grupo1.Caption = configuracao.NomeGrupo1;
            }

            var grupo2 = gvPrincipal.Columns.ColumnByFieldName("Grupo2");
            if (grupo2 != null)
            {
                grupo2.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
                if (!String.IsNullOrEmpty(configuracao.NomeGrupo2))
                    grupo2.Caption = configuracao.NomeGrupo2;
            }

            var grupo3 = gvPrincipal.Columns.ColumnByFieldName("Grupo3");
            if (grupo3 != null)
            {
                grupo3.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
                if (!String.IsNullOrEmpty(configuracao.NomeGrupo3))
                    grupo3.Caption = configuracao.NomeGrupo3;
            }

            if (!configuracao.CodigoProdutoStr)
            {
                var colCod = gvPrincipal.Columns.ColumnByFieldName("Codigo");
                colCod.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
        }

        private void sbFuncao10_Click(object sender, EventArgs e)
        {
            Produto registroSelecionado = GetRegistroSelecionado();
            if (registroSelecionado != null)
            {
                FormConsultaEstoquePorProduto form = new FormConsultaEstoquePorProduto(registroSelecionado);
                form.ShowDialog();
            }
            else
                MessageBox.Show("Nenhum registro selecionado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sbFuncao11_Click(object sender, EventArgs e)
        {
            MostraImagem();
        }

        private void MostraImagem()
        {
            String Caminho = Convert.ToString(gvPrincipal.GetFocusedRowCellValue("CaminhoImagem"));
            FormImagemProduto form = new FormImagemProduto();

            if (Caminho != string.Empty)
                form.CaminhoImagens = Caminho;
            form.ShowDialog();
        }

        protected override void MontarColunas()
        {

            base.MontarColunas();
            var colunaNome = gvPrincipal.Columns.ColumnByName("Nome");
            if (colunaNome != null)
            {
                colunaNome.SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                colunaNome.SortOrder = ColumnSortOrder.Ascending;
            }
            gvPrincipal.OptionsSelection.EnableAppearanceFocusedRow = false;
            var colunaSelecionado = gvPrincipal.Columns.ColumnByName("Selecionado");
            if (colunaSelecionado == null)
            {
                for (int i = 0; i < gvPrincipal.Columns.Count; i++)
                {
                    gvPrincipal.Columns[i].VisibleIndex += 1;
                    gvPrincipal.Columns[i].OptionsColumn.AllowEdit = false;
                    gvPrincipal.Columns[i].OptionsColumn.AllowFocus = false;
                }
                gvPrincipal.Columns.Insert(0);
                gvPrincipal.Columns[0].Caption = "";
                gvPrincipal.Columns[0].VisibleIndex = 0;
                gvPrincipal.Columns[0].FieldName = "Selecionado";
                gvPrincipal.Columns[0].OptionsColumn.AllowEdit = true;
                gvPrincipal.Columns[0].OptionsColumn.AllowFocus = true;
            }
            else
                colunaSelecionado.Visible = true;

            gvPrincipal.OptionsBehavior.Editable = true;
            gvPrincipal.RefreshData();
            gvPrincipal.RefreshEditor(false);
        }

        protected override void sbSelecionar_Click(object sender, EventArgs e)
        {
            IList<Produto> retorno = new List<Produto>();
            foreach (var item in lista)
            {
                if (item.Selecionado)
                    retorno.Add(item);
            }

            ProdutosSelecionados = retorno;
            this.Close();
        }

        private void sbFuncao22_Click(object sender, EventArgs e)
        {
            SelecionarTodosNenhum(true, "Deseja selecionar todos os produtos?");
        }

        private void sbFuncao23_Click(object sender, EventArgs e)
        {
            SelecionarTodosNenhum(false, "Deseja desmarcar todos os produtos?");
        }

        private void SelecionarTodosNenhum(bool tipo, string mensagem)
        {
            if (lista.Count == 0)
            {
                MessageBox.Show("Lista de produtos está vazia.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MessageBox.Show(mensagem, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (var item in lista)
                    {
                        if (item.Selecionado == tipo)
                            continue;

                        item.Selecionado = tipo;
                    }
                    gvPrincipal.RefreshData();
                }
            }
        }

        private void GridProdutoGenMaisDetalhado_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.F2:
                    AtivarBotao(simpleButton1);
                    break;
                case Keys.F3:
                    AtivarBotao(simpleButton2);
                    break;
                case Keys.F4:
                    AtivarBotao(simpleButton5);
                    break;
                case Keys.F5:
                    AtivarBotao(simpleButton4);
                    break;
                case Keys.F6:
                    AtivarBotao(simpleButton3);
                    break;
                case Keys.F7:
                    AtivarBotao(simpleButton6);
                    break;
                case Keys.F8:
                    AtivarBotao(simpleButton7);
                    break;
            }
        }

        private void AtivarBotao(DevExpress.XtraEditors.SimpleButton button)
        {
            DesativarBotao();

            txtBusca.Focus();
            txtBusca.SelectAll();

            button.Appearance.ForeColor = Color.FromArgb(100, 0, 192, 192);
            buttonSelecionado = button;
        }

        private void DesativarBotao()
        {
            if (buttonSelecionado == null)
                return;

            buttonSelecionado.Appearance.ForeColor = Color.FromArgb(100,32,31,53);
        }

        private void txtBusca_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Buscar();
        }

        private void Buscar()
        {
            if (string.IsNullOrEmpty(txtBusca.Text))
            {
                AtualizarLista(ListaOriginal);
            }

            else
            {
                switch(buttonSelecionado.Name)
                {
                    case "simpleButton1":
                        AtulizarListaPeloCodigo();
                        break;
                    case "simpleButton2":
                        AtulizarListaPelaDescricao();
                        break;
                    case "simpleButton5":
                        AtulizarListaPeloCodigoOriginal();
                        break;
                    case "simpleButton4":
                        AtulizarListaPeloCodigoFabricante();
                        break;
                    case "simpleButton3":
                        AtulizarListaPeloCodigoBarras();
                        break;
                    case "simpleButton6":
                        AtulizarListaPelaLocalizacao();
                        break;
                    case "simpleButton7":
                        AtulizarListaPelaMarcaOuDescricao();
                        break;
                }
            }
        }

        private void AtualizarLista(IList<Produto> lista)
        { 
            gcPrincipal.DataSource = lista;
            gcPrincipal.RefreshDataSource();
            gcPrincipal.Refresh();
        }

        private void AtulizarListaPeloCodigo()
        {
            string codigo = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.Codigo.ToLower() == codigo).ToList();
            AtualizarLista(novaLista);
        }

        private void AtulizarListaPelaDescricao()
        {
            string descricao = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.Nome.ToLower().Contains(descricao) || p.AplicacaoStr.ToLower().Contains(descricao)).ToList();
            AtualizarLista(novaLista);
        }

        private void AtulizarListaPeloCodigoOriginal()
        {
            string codigo = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.CodigoOriginalStr.ToLower() == codigo).ToList();
            AtualizarLista(novaLista);
        }

        private void AtulizarListaPeloCodigoFabricante()
        {
            string codigo = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.CodigoFabricanteStr.ToLower() == codigo).ToList();
            AtualizarLista(novaLista);
        }

        private void AtulizarListaPeloCodigoBarras()
        {
            string codigo = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.Barra.ToLower() == codigo).ToList();
            AtualizarLista(novaLista);
        }

        private void AtulizarListaPelaLocalizacao()
        {
            string localizacao = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.LocalizacaoStr.ToLower() == localizacao).ToList();
            AtualizarLista(novaLista);
        }

        private void AtulizarListaPelaMarcaOuDescricao()
        {
            string descricao = txtBusca.Text.ToLower();
            var novaLista = ListaOriginal.Where(p => p.Nome.ToLower().Contains(descricao) || p.NomeGrupo1.ToLower().Contains(descricao)).ToList();
            AtualizarLista(novaLista);
        }

        private void Ativar_Click(object sender, EventArgs e)
        {
            AtivarBotao((DevExpress.XtraEditors.SimpleButton)sender);
        }
    }
}
