using Aplicacao.Base;
using Aplicacao.Util;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Utilitarios
{
    public partial class FormPromocao : Form
    {
        private Promocao _Promocao = null;
        private Acao _Acao = Acao.Incluir;


        public FormPromocao(Promocao p, Acao a)
        {
            InitializeComponent();
            _Promocao = p;
            _Acao = a;

            lkpTabelaPreco.Sessao = TabelaPrecoController.Instancia.getSession();
            lkpTabelaPreco.Exemplo = new TabelaPreco();
            lkbTabelaPreco.SubFormType = typeof(FormTabelaPreco);

            PreencherTela();
        }

        private void PreencherTela()
        {
            if (_Promocao == null)
                _Promocao = new Promocao();

            txtNome.Text = _Promocao.Nome;

            if (_Promocao.TabelaPreco != null)
                lkpTabelaPreco.Localizar(_Promocao.TabelaPreco.ID);

            txtPercentual.EditValue = _Promocao.Percentual;
            txtDataInicial.EditValue = _Promocao.DataInicio;
            txtDataFinal.EditValue = _Promocao.DataFim;
            switch (_Promocao.Tipo)
            {
                case 0:
                    rbDesconto.Checked = true;
                    break;
                case 1:
                    rbAumento.Checked = true;
                    break;
            }

            CarregarItens();
        }

        private void sbSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                _Promocao.Nome = txtNome.Text;
                _Promocao.TabelaPreco = lkpTabelaPreco.Selecionado as TabelaPreco;
                _Promocao.Percentual = Convert.ToDecimal(txtPercentual.EditValue);
                _Promocao.DataInicio = Convert.ToDateTime(txtDataInicial.EditValue);
                _Promocao.DataFim = Convert.ToDateTime(txtDataFinal.EditValue);
                _Promocao.Tipo = rbDesconto.Checked ? 0 : 1;

                foreach(var item in _Promocao.Itens)
                {
                    if (item.Removido)
                        PromocaoProdutoController.Instancia.RegisterDeleteIntoTransaction(item);
                }
                PromocaoProdutoController.Instancia.CommitUnitOfWorkTransaction();

                _Promocao.Itens = _Promocao.Itens.Where(o => !o.Removido).ToList();
                PromocaoController.Instancia.Salvar(_Promocao, _Acao);
                Close();
            }
            catch (Exception Ex)
            {
                new FormErro(Ex).ShowDialog(this);
            }
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lkbTabelaPreco_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaPreco>(lkpTabelaPreco, typeof(FormTabelaPreco));
        }

        private void FormPromocao_Shown(object sender, EventArgs e)
        {
            txtNome.Focus();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var form = new GridEscolhaProdutos();
            form.ShowDialog();
            if (form.Selecionados?.Count > 0)
            {
                foreach (var item in form.Selecionados)
                {
                    var precoPromocional = item.PrecoBase - (item.PrecoBase * (Convert.ToDecimal(txtPercentual.EditValue) / 100));
                    if (rbAumento.Checked)
                        precoPromocional = item.PrecoBase + (item.PrecoBase * (Convert.ToDecimal(txtPercentual.EditValue) / 100));

                    _Promocao.Itens.Add(new PromocaoProduto
                    {
                        Guid = Guid.NewGuid().ToString(),
                        PrecoBase = item.PrecoBase,
                        PrecoPromocao = precoPromocional,
                        Produto = item,
                        Promocao = _Promocao
                    });
                }
                CarregarItens();
            }
        }

        private void CarregarItens()
        {
            if (_Promocao.Itens == null)
                _Promocao.Itens = new List<PromocaoProduto>();

            foreach (var item in _Promocao.Itens)
                item.Guid = Guid.NewGuid().ToString();

            ovGRD_Promocoes.DataSource = _Promocao.Itens.Where(o => !o.Removido).ToList();
            ovGRD_Promocoes.RefreshDataSource();
            ovGRD_Promocoes.Refresh();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var selecionado = (PromocaoProduto)ovGV_Promocoes.GetFocusedRow();
            if (selecionado != null)
            {
                foreach (var item in _Promocao.Itens)
                    if (selecionado.Guid == item.Guid)
                        item.Removido = true;
                
                CarregarItens();
            }
        }

        private void txtPercentual_Leave(object sender, EventArgs e)
        {
            RecalcularPromocao();
        }

        private void RecalcularPromocao()
        {
            if (_Promocao == null)
                return;

            if (_Promocao.Itens == null)
                return;

            foreach (var item in _Promocao.Itens)
            {
                var precoPromocional = item.PrecoBase - (item.PrecoBase * (Convert.ToDecimal(txtPercentual.EditValue) / 100));
                if (rbAumento.Checked)
                    precoPromocional = item.PrecoBase + (item.PrecoBase * (Convert.ToDecimal(txtPercentual.EditValue) / 100));

                item.PrecoPromocao = precoPromocional;
            }

            CarregarItens();
        }

        private void rbAumento_CheckedChanged(object sender, EventArgs e)
        {
            RecalcularPromocao();
        }

        private void rbDesconto_CheckedChanged(object sender, EventArgs e)
        {
            RecalcularPromocao();
        }
    }
}
