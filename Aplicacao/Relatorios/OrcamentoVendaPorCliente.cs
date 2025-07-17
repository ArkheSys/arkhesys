﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Aplicacao.Relatorios
{
#pragma warning disable CS0612 // "FormBaseEmpresa" está obsoleto
    public partial class OrcamentoVendaPorCliente : Aplicacao.Relatorios.Base.FormBaseEmpresa
#pragma warning restore CS0612 // "FormBaseEmpresa" está obsoleto
    {
        Modelo.Cliente objCliente = new Modelo.Cliente();
        public OrcamentoVendaPorCliente()
        {
            InitializeComponent();
            Name = "OrcamentoVendaPorCliente";

            cbPessoa.Properties.DisplayMember = "Nome";
            cbPessoa.Properties.ValueMember = "ID";
            cbPessoa.EditValue = 0;
            cbPessoa.Enabled = false;
            btAdPessoa.Enabled = false;
        }

        protected override void FormBase_Load(object sender, System.EventArgs e)
        {
            base.FormBase_Load(sender, e);
            Carrega();                        
        }

        protected override void btOk_Click(object sender, EventArgs e)
        {
            if (ValidaCampos())
            {
                if (!cbPessoa.Enabled)
                {
                    cbPessoa.EditValue = 0;
                }
                base.btOk_Click(sender, e);
                List<Microsoft.Reporting.WinForms.ReportParameter> parametros = new List<Microsoft.Reporting.WinForms.ReportParameter>();
                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("DataInicial", txtDtInicial.DateTime.ToString());
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("DataFinal", txtDtFinal.DateTime.ToString());
                Microsoft.Reporting.WinForms.ReportParameter p3 = new Microsoft.Reporting.WinForms.ReportParameter("Status", cbStatus.Text);
                Microsoft.Reporting.WinForms.ReportParameter p4 = new Microsoft.Reporting.WinForms.ReportParameter("Titulo", Titulo);
                Microsoft.Reporting.WinForms.ReportParameter p5 = new Microsoft.Reporting.WinForms.ReportParameter("Tipo", "Venda por Cliente");
                parametros.Add(p1);
                parametros.Add(p2);
                parametros.Add(p3);
                parametros.Add(p4);
                parametros.Add(p5);
                int status = 1;
                switch (cbStatus.Text)
                {
                    case "Aberto":
                        status = 1;
                        break;
                    case "Faturado":
                        status = 2;
                        break;
                    case "Cancelado":
                        status = 3;
                        break;
                }
                Relatorio.cwkGestaoDataSet dt = new Relatorio.cwkGestaoDataSet();
                Relatorio.cwkGestaoDataSetTableAdapters.p_PedidosTableAdapter p_PedidosTableAdapter = new Relatorio.cwkGestaoDataSetTableAdapters.p_PedidosTableAdapter();
                Aplicacao.Base.FormRelatorioBase form = new Aplicacao.Base.FormRelatorioBase("rptOrcamentoPorCliFor.rdlc", "cwkGestaoDataSet_p_Pedidos", p_PedidosTableAdapter.GetData(status, txtDtInicial.DateTime, txtDtFinal.DateTime, MontaStringEmpresas(), (int)cbPessoa.EditValue, "CLI", (int)Modelo.InOutType.Saída), parametros);
                form.Show();
                //new FormRelatorioOrcamentoPorCliFor(Titulo, cbStatus.Text, (DateTime)txtDtInicial.EditValue, (DateTime)txtDtFinal.EditValue, MontaStringEmpresas(), (int)cbPessoa.EditValue, Modelo.InOutType.Saída).Show();
            }
            else
            {
                MessageBox.Show("Preencha os campos corretamente.");
            }
        }

        protected bool ValidaCampos()
        {
            bool ret = true;
            foreach (Control c in tabPage1.Controls)
            {
                errorProvider1.SetError(c, "");
                switch (c.Name)
                {
                    case "cbGrupo":
                        if (c.Text == "")
                        {
                            errorProvider1.SetError(c, "Escolha um grupo.");
                            ret = false;
                        }
                        break;
                    case "cbStatus":
                        if (c.Text == "")
                        {
                            errorProvider1.SetError(c, "Campo não pode ficar em branco.");
                            ret = false;
                        }
                        else
                        {
                            if (!((DevExpress.XtraEditors.ComboBoxEdit)c).Properties.Items.Contains(c.Text))
                            {
                                errorProvider1.SetError(c, "Valor incorreto.");
                                ret = false;
                            }
                        }
                        break;
                    case "txtDtInicial":
                        if (c.Text == "")
                        {
                            errorProvider1.SetError(c, "Selecione a data inicial.");
                            ret = false;
                        }
                        break;
                    case "txtDtFinal":
                        if (c.Text == "")
                        {
                            errorProvider1.SetError(c, "Selecione a data final.");
                            ret = false;
                        }
                        break;
                }
            }
            return ret;
        }

        /// <summary>
        /// Método que chama o grid para selecionar um elemento
        /// </summary>
        /// <param name="ptitulo">Título do grid</param>
        /// <param name="pCb">combobox que irá chamar o grid</param>
        /// <param name="gl">objeto que irá retornar a lista para preencher o combo</param>
#pragma warning disable CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning disable CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
        private void GridSelecao(string ptitulo, Componentes.devexpress.cwk_DevLookup pCb, Modelo.IMetodosCwork gl)
#pragma warning restore CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
#pragma warning restore CS0246 // O nome do tipo ou do namespace "Componentes" não pode ser encontrado (está faltando uma diretiva using ou uma referência de assembly?)
        {
            Aplicacao.GridPadrao pGp = new Aplicacao.GridPadrao(ptitulo, true, (int)pCb.EditValue);
            if (cwkControleUsuario.Facade.ControleAcesso(pGp))
            {
                pGp.ShowDialog();
                if (pGp.atualiza == true)
                {
                    pCb.Properties.DataSource = gl.getLista(db);
                }
                if (pGp.Retorno.Length != 0)
                {
                    pCb.EditValue = int.Parse(pGp.Retorno);
                }
            }
            pCb.Focus();
        }

        private void btAdPessoa_Click(object sender, EventArgs e)
        {
            GridSelecao("Tabela de Cliente", cbPessoa, objCliente);
        }

        private void chbFiltraCli_CheckedChanged(object sender, EventArgs e)
        {
            cbPessoa.Enabled = chbFiltraCliente.Checked;
            btAdPessoa.Enabled = chbFiltraCliente.Checked;
            if (!chbFiltraCliente.Checked)
            {
                cbPessoa.EditValue = 0;
            }
            else
            {
                if (cbPessoa.Properties.DataSource == null)
                {
                    cbPessoa.Properties.DataSource = objCliente.getListaCombo(db);
                    cbPessoa.EditValue = 0;
                }
            }
        }
    }
}
