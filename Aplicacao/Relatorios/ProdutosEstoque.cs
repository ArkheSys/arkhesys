﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Aplicacao.Util;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using System.Drawing;
using Aplicacao.Base;

namespace Aplicacao.Relatorios
{
    public partial class ProdutosEstoque : Aplicacao.Relatorios.Base.FormBase
    {
        private cwkGestao.Negocio.Relatorios.ProdutosEstoque produtoEstoqueController;

        public ProdutosEstoque()
        {
            InitializeComponent();
            lkpGrupoEstoque.Sessao = GrupoEstoqueController.Instancia.getSession();
            lkpGrupoEstoque.Exemplo = new cwkGestao.Modelo.GrupoEstoque();

            lkpProduto.Sessao = ProdutoController.Instancia.getSession();
            lkpProduto.Exemplo = new cwkGestao.Modelo.Produto();

            lkpLocalEstoqueTipoNota.Sessao = LocalEstoqueController.Instancia.getSession();
            lkpLocalEstoqueTipoNota.Exemplo = new LocalEstoque();

            ovCKB_TodosLocais.Checked = true;
            HabilitaLocal(false);

            lkpClienteFrenteCaixa.Exemplo = new Pessoa { BFornecedor = true };
            lkpClienteFrenteCaixa.CamposRestricoesAND = new List<string> { "BFornecedor" };
            lkpClienteFrenteCaixa.Sessao = PessoaController.Instancia.getSession();
        }

        public override void CarregaCampos()
        {
            cbGrupo.Properties.DataSource = EmpresaController.Instancia.GetAll();
        }

        private void cbGrupo_EditValueChanged(object sender, EventArgs e)
        {
            gcPrincipal.DataSource = FilialController.Instancia.GetEmpresasDoGrupo(((cwkGestao.Modelo.Empresa)cbGrupo.EditValue).ID);
        }

        private void HabilitaGrupoEstoque(bool habilita)
        {
            lkpGrupoEstoque.Enabled = habilita;
            btnlkpGrupoEstoque.Enabled = habilita;
        }

        private void HabilitaProduto(bool habilita)
        {
            lkpProduto.Enabled = habilita;
            btnlkpProduto.Enabled = habilita;
        }

        private void HabilitaLocal(bool habilita)
        {
            lkpLocalEstoqueTipoNota.Enabled = habilita;
            lkbLocalEstoqueTipoNota.Enabled = habilita;

            lkpLocalEstoqueTipoNota.Text = "";
            lkpLocalEstoqueTipoNota.EditValue = null;
        }

        protected override bool GerarRelatorio()
        {
            if (!ExistemErros())
            {
                IList<object> resultado;
                List<Microsoft.Reporting.WinForms.ReportParameter> parametros = new List<Microsoft.Reporting.WinForms.ReportParameter>();

                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("Empresa", GetEmpresaRelatorio());
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("Tipo", rgpAgrupamento.EditValue.ToString());
                Microsoft.Reporting.WinForms.ReportParameter p3 = new Microsoft.Reporting.WinForms.ReportParameter("Situacao", cbSituacao.Text);
                parametros.Add(p1);
                parametros.Add(p2);
                parametros.Add(p3);
                nomeRelatorio = @"rptEstoquePosicao.rdlc";
                dataSet = "dsEstoque_PorGrupoEstoque";
                produtoEstoqueController = new cwkGestao.Negocio.Relatorios.ProdutosEstoque(MontaStringEmpresas());

                var LocalEstoque = (LocalEstoque)lkpLocalEstoqueTipoNota.Selecionado;

                if (chbGrupo.Checked && !chbProduto.Checked)
                    resultado = produtoEstoqueController.GerarPorProduto((Produto)lkpProduto.Selecionado, false, ovCMB_StatusEstoque.SelectedIndex, LocalEstoque?.ID);
                else
                {
                    Pessoa fornecedor = (Pessoa)lkpClienteFrenteCaixa.Selecionado;
                    int idFornecedor = fornecedor == null ? 0 : fornecedor.ID;

                    resultado = produtoEstoqueController.GerarPorGrupoEstoque((GrupoEstoque)lkpGrupoEstoque.Selecionado, cbSituacao.Text, false, ovCMB_StatusEstoque.SelectedIndex, LocalEstoque?.ID, idFornecedor);
                }

                VisualizarRelatorio(resultado, parametros);
                return true;
            }
            return false;
        }

        private bool ExistemErros()
        {
            StringBuilder erros = new StringBuilder();
            dxErroProvider.ClearErrors();
            if (cbGrupo.EditValue == null)
            {
                dxErroProvider.SetError(cbGrupo, "Campo obrigatório.");
                erros.AppendLine("Grupo: Selecione um grupo.");
            }
            else if (gvPrincipal.GetSelectedRows().Length == 0)
            {
                erros.AppendLine("Empresas: Selecione pelo menos uma empresa.");
            }
            else
            {
                dxErroProvider.SetError(cbGrupo, "");
            }

            if (chbGrupo.Checked && !chbProduto.Checked)
            {
                if (lkpProduto.Selecionado == null)
                {
                    dxErroProvider.SetError(lkpProduto, "Campo obrigatório.");
                    erros.AppendLine("Produto: Selecione o produto.");
                }
                else
                {
                    dxErroProvider.SetError(lkpGrupoEstoque, "");
                    dxErroProvider.SetError(lkpProduto, "");
                }
            }
            else if (!chbGrupo.Checked && lkpGrupoEstoque.Selecionado == null)
            {
                dxErroProvider.SetError(lkpGrupoEstoque, "Campo obrigatório.");
                erros.AppendLine("Grupo Estoque: Selecione o grupo estoque.");
            }
            else
            {
                dxErroProvider.SetError(lkpProduto, "");
                dxErroProvider.SetError(lkpGrupoEstoque, "");
            }

            if (erros.Length > 0)
            {
                MessageBox.Show("Verificar anomalias:" + Environment.NewLine + erros.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return true;
            }
            return false;
        }

        private void VisualizarRelatorio(IList<object> resultado, List<Microsoft.Reporting.WinForms.ReportParameter> parametros)
        {
            Aplicacao.Base.FormRelatorioBase form = new Aplicacao.Base.FormRelatorioBase(nomeRelatorio, dataSet, resultado, parametros);
            form.Text = this.Text;
            form.Show();
        }

        private string GetEmpresaRelatorio()
        {
            if (gvPrincipal.SelectedRowsCount > 1)
                return ((cwkGestao.Modelo.Empresa)cbGrupo.EditValue).Nome;
            else
                return ((cwkGestao.Modelo.Filial)gvPrincipal.GetRow(gvPrincipal.GetSelectedRows()[0])).Nome;
        }

        protected string MontaStringEmpresas()
        {
            StringBuilder empresas = new StringBuilder("(");
            int[] selectedRowHandles = gvPrincipal.GetSelectedRows();
            for (int i = 0; i < selectedRowHandles.Length; i++)
            {
                if (i > 0)
                    empresas.Append(", ");
                empresas.Append(((cwkGestao.Modelo.Filial)gvPrincipal.GetRow(selectedRowHandles[i])).ID);
            }
            empresas.Append(")");
            return empresas.ToString();
        }

        private void btnlkpGrupoEstoque_Click(object sender, EventArgs e)
        {
            GridGenerica<cwkGestao.Modelo.GrupoEstoque> grid = new GridGenerica<cwkGestao.Modelo.GrupoEstoque>(GrupoEstoqueController.Instancia.GetAll(), new FormGrupoEstoque(), (cwkGestao.Modelo.GrupoEstoque)lkpGrupoEstoque.Selecionado, false);
            grid.Selecionando = true;
            grid.ShowDialog();

            lkpGrupoEstoque.EditValue = grid.Selecionado;
        }

        private void btnlkpProduto_Click(object sender, EventArgs e)
        {
            GridProdutoGen grid = new GridProdutoGen(ProdutoController.Instancia.GetAll(), (cwkGestao.Modelo.Produto)lkpProduto.Selecionado, false, false, typeof(FormProduto));
            grid.Selecionando = true;
            grid.ShowDialog();

            lkpProduto.EditValue = grid.Selecionado;
        }

        private void chbGrupo_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaGrupoEstoque(!chbGrupo.Checked);
            if (chbGrupo.Checked)
            {
                chbProduto.Enabled = true;
                HabilitaProduto(!chbProduto.Checked);
            }
            else
            {
                chbProduto.Checked = false;
                chbProduto.Enabled = false;
                HabilitaProduto(false);
            }
        }

        private void chbProduto_CheckedChanged(object sender, EventArgs e)
        {
            if (chbProduto.Enabled)
            {
                HabilitaProduto(!chbProduto.Checked);
            }
        }

        private void lkpProduto_EnabledChanged(object sender, EventArgs e)
        {
            if (!lkpProduto.Enabled)
            {
                lkpProduto.ID = 0;
                lkpProduto.EditValue = null;
            }
        }

        private void lkpGrupoEstoque_EnabledChanged(object sender, EventArgs e)
        {
            if (!lkpGrupoEstoque.Enabled)
            {
                lkpGrupoEstoque.ID = 0;
                lkpGrupoEstoque.EditValue = null;
            }
        }

        private void gvPrincipal_CustomDrawGroupPanel(object sender, DevExpress.XtraGrid.Views.Base.CustomDrawEventArgs e)
        {
            Bitmap groupPanelImage;
            groupPanelImage = (Bitmap)Aplicacao.Properties.Resources.HeaderGrid;
            groupPanelImage.MakeTransparent();
            Brush brush = e.Cache.GetGradientBrush(e.Bounds, Color.White, Color.WhiteSmoke,
              System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
            e.Graphics.FillRectangle(brush, e.Bounds);
            Image img = groupPanelImage;
            Rectangle r = new Rectangle(e.Bounds.X + e.Bounds.Width - img.Size.Width - 5,
              e.Bounds.Y + (e.Bounds.Height - img.Size.Height) / 2, img.Width, img.Height);
            e.Graphics.DrawImageUnscaled(img, r);
            e.Handled = true;
        }

        private void lkbLocalEstoqueTipoNota_Click(object sender, EventArgs e)
        {
            GridGenerica<LocalEstoque> grid = new GridGenerica<LocalEstoque>(LocalEstoqueController.Instancia.GetAll(), false, typeof(FormLocalEstoque));
            grid.Selecionando = true;
            grid.ShowDialog();

            lkpLocalEstoqueTipoNota.EditValue = grid.Selecionado;
        }

        private void ovCKB_TodosLocais_CheckedChanged(object sender, EventArgs e)
        {
            HabilitaLocal(!ovCKB_TodosLocais.Checked);
        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void lkbClienteFrenteCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                lkbClienteFrenteCaixa.SubFormType = typeof(FormPessoa);
                Util.LookupUtil.GridLookup<cwkGestao.Modelo.Pessoa>(lkpClienteFrenteCaixa, lkbClienteFrenteCaixa.SubFormType);
            }
            catch (Exception ex)
            {
                FormErro form = new FormErro(ex.Message, ex.InnerException.ToString());
                form.ShowDialog();
            }
        }
    }
}
