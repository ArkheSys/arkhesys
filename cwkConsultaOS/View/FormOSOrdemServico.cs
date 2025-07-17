using cwkConsultaOS.Controller;
using Cwork.Utilitarios.Componentes;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cwkConsultaOS
{
    public partial class FormOSOrdemServico : Form
    {
        private OrdemServico _selecionado;
        private OSOrdemServicoController controller;

        public FormOSOrdemServico()
        {
            InitializeComponent();
        }

        public OrdemServico Selecionado
        {
            get { return _selecionado; }
            set
            {
                _selecionado = value;
                tpPrincipal.Tag = _selecionado;
            }
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        private void FormOSOrdemServico_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (Selecionado != null)
                {
                    PreencherTela();
                    
                }
            }
        }

        private void PreencherTela()
        {
            
            txtEmpresa.Text = Selecionado.LOJA.RAZAOSOCIAL;
            txtData.Text = Selecionado.ENTRADA.ToString();
            txtCodigo.Text = Selecionado.IDORDEMSERVICO.ToString();
            txtStatus.Text = Selecionado.SITUACAO.NOME;
            txtCliente.Text = Selecionado.CLIENTES.CLI_NOME;
            txtContato.Text = Selecionado.CLIENTES.TELEFONE_1 + " " + Selecionado.CLIENTES.CELULAR_1 + " " + Selecionado.CLIENTES.CLI_EMAIL;
            txtProblemaRelatado.Text = Selecionado.EQUIPAMENTO.DEFEITO;
            txtAcessorios.Text = Selecionado.EQUIPAMENTO.ACESSORIO;
            txtResultadoTecnico.Text = Selecionado.EQUIPAMENTO.LAUDO;
            txtObservacao.Text = Selecionado.EQUIPAMENTO.OBS;
            txtEquipamento.Text = Selecionado.EQUIPAMENTO.EQUIPAMENTO.DESCRICAO;
            txtServicoSubTotal.Text = Selecionado.SUBTOTALSERV.ToString();
            txtServicoDescontoValor.Text = Selecionado.DESCONTOSERV.ToString();
            txtProdutoSubTotal.Text = (Selecionado.PRODUTOOS.Sum(p => p.VALOR)).ToString();
            txtTotalGeral.Text = Selecionado.TOTALOS.ToString();

            gcProdutos.DataSource = Selecionado.PRODUTOOS.ToList();
            gcServicoItem.DataSource = Selecionado.SERVICOOS.ToList();

        }

        protected virtual OrdemServico GetRegistroSelecionado()
        {
            try
            {
                OrdemServico retorno;
                if (controller != null)
                {
                    try
                    {
                        retorno = controller.LoadObjectById(((EquipamentosOS)gvProdutos.GetRow(gvProdutos.GetSelectedRows()[0])).IDEQUIPOS);
                        return retorno;
                    }
                    catch (Exception)
                    {
                        retorno = (OrdemServico)gvProdutos.GetRow(gvProdutos.GetSelectedRows()[0]);
                        return retorno;
                    }
                }
                retorno = (OrdemServico)gvProdutos.GetRow(gvProdutos.GetSelectedRows()[0]);
                return retorno;
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }
        }

        public void Cancelar()
        {
            Close();
        }

        private void FormOSOrdemServico_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Cancelar();
        }
    }
}
