using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using Aplicacao.Modulos.Comercial.Impressao;
using Aplicacao.Util;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio;

using DBUtils.Classes;

using DevExpress.XtraReports.UI;

using MetroFramework.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaNotaCarregarPedido : MetroForm
    {
        public Pedido Selecionado = null;
        public IniFile ArquivoIni => new IniFile(Debugger.IsAttached
    ? $"{Path.GetFullPath(".").Substring(0, Path.GetFullPath(".").IndexOf("cwkGestao"))}cwkGestao\\App_Data\\Start.ini"
    : $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\App_Data\\Start.ini");

        private bool Reservas = false;
        public FormFrenteCaixaNotaCarregarPedido(bool Reservas)
        {
            InitializeComponent();
            this.Reservas = Reservas;
            InitGrid();
        }

        private void InitGrid()
        {
            var List = PedidoController.Instancia.GetListaPedidosFrenteCaixaParaCarregar(Reservas);
            if (Reservas)
                List = List.Where(o => o.Status != 2).ToList();

            ovGRD_Pedidos.DataSource = List;
            ovGRD_Pedidos.Refresh();
            AjustaHeaderItens();
        }

        private void AjustaHeaderItens()
        {
            var style = new DataGridViewCellStyle
            {
                Font = new Font("Open Sans", 10, FontStyle.Regular),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            foreach (DataGridViewColumn column in ovGRD_Pedidos.Columns)
            {
                switch (column.Name)
                {
                    case "Codigo":
                        column.DisplayIndex = 0;
                        column.FillWeight = Convert.ToInt32(ovGRD_Pedidos.Width * 0.15);
                        column.HeaderText = "Código";
                        column.HeaderCell.Style = style.Clone();
                        break;
                    case "Dt":
                        column.DisplayIndex = 1;
                        column.FillWeight = Convert.ToInt32(ovGRD_Pedidos.Width * 0.20);
                        column.HeaderText = "Data Cadastro";
                        column.HeaderCell.Style = style.Clone();
                        break;
                    case "PessoaNome":
                        column.DisplayIndex = 2;
                        column.FillWeight = Convert.ToInt32(ovGRD_Pedidos.Width * 0.45);
                        column.HeaderText = "Pessoa";
                        column.HeaderCell.Style = style.Clone();
                        break;
                    case "Status":
                        column.DisplayIndex = 3;
                        column.FillWeight = Convert.ToInt32(ovGRD_Pedidos.Width * 0.13);
                        column.HeaderText = "Status";
                        column.HeaderCell.Style = style.Clone();
                        break;
                    case "StatusNFCe":
                        column.DisplayIndex = 4;
                        column.FillWeight = Convert.ToInt32(ovGRD_Pedidos.Width * 0.17);
                        column.HeaderText = "NFCe";
                        column.HeaderCell.Style = style.Clone();
                        break;
                    case "TotalPedido":
                        column.DisplayIndex = 5;
                        column.FillWeight = Convert.ToInt32(ovGRD_Pedidos.Width * 0.17);
                        column.HeaderText = "Total";
                        column.HeaderCell.Style = style.Clone();
                        break;
                    default:
                        column.DisplayIndex = 0;
                        column.Visible = false;
                        break;
                }
            }
        }

        private void ovGRD_Pedidos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (ovGRD_Pedidos.Columns[e.ColumnIndex].Name)
            {
                case "TotalPedido":
                    e.Value = Convert.ToDecimal(e.Value).ToString("c2");
                    break;
                case "Status":
                    e.Value = "1".Equals(e.Value.ToString()) ? "Em Aberto" : "Faturado";
                    break;
            }
        }

        private void SelecionarNFCe()
        {
            if (ovGRD_Pedidos?.SelectedRows[0].DataBoundItem is pxyPedidoFrenteCaixaCarregar LinhaSelecionada)
                Selecionado = PedidoController.Instancia.LoadObjectById(LinhaSelecionada.ID);

            Close();
        }

        private void ovGRD_Pedidos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelecionarNFCe();
        }

        private void FormFrenteCaixaNotaCarregarPedido_Shown(object sender, EventArgs e)
        {
            //TXT_Pesquisa.Focus();

            ovGRD_Pedidos.Focus();

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Control | Keys.X:
                    CancelarPedido();
                    break;
                case Keys.Control | Keys.P:
                    ImprimirPedido();
                    break;
                case Keys.Control | Keys.N:
                    ImprimirNFCe();
                    break;
                case Keys.Control | Keys.F:
                    SelecionarNFCe();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void smbImprimirPedido_Click(object sender, EventArgs e)
        {
            ImprimirPedido();
        }

        private string CaminhoIni => Debugger.IsAttached ?
           $"{Path.GetFullPath(".").Substring(0, Path.GetFullPath(".").IndexOf("cwkGestao"))}cwkGestao\\App_Data\\Start.ini" :
           $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\App_Data\\Start.ini";

        private string NomeImpressora
        {
            get
            {
                IniFile ArquivoIni = new IniFile(CaminhoIni);
                return ArquivoIni.GetValue("Configuracoes_PDV", "NOME_IMPRESSORA", ConfiguracaoController.Instancia.GetConfiguracao().NomeImpressora);
            }
        }

        public void ImprimirNFCe()
        {
            pxyPedidoFrenteCaixaCarregar LinhaSelecionada = ovGRD_Pedidos.SelectedRows[0].DataBoundItem as pxyPedidoFrenteCaixaCarregar;
            Pedido Ped = PedidoController.Instancia.GetByCodigo(LinhaSelecionada.Codigo);
            var NotaAutorizada = NotaController.Instancia.GetNotaAutorizadaPorPedido(LinhaSelecionada.Codigo, true).FirstOrDefault();
            if (NotaAutorizada == null)
            {
                MessageBox.Show("Nenhuma NFCe encontrada para este pedido!", "ATENÇÃO");
                return;
            }

            var Config = (from c in new Modelo.DataClassesDataContext().Configuracaos select c).FirstOrDefault();
            FormNFe frmNFe = new FormNFe(Convert.ToInt32(ArquivoIni.GetValue("Configuracoes_PDV", "SERIE", "1")), Ped.Pessoa?.CNPJ_CPF);

            if (MessageBox.Show("Deseja Imprimir a NFCe?", "Atenção!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                frmNFe.IDNota = NotaAutorizada.ID;
                frmNFe.FillObjNFe();
                if (1 == Config?.VisualizarNFCe)
                    frmNFe.nfeController.VisualizarDANFE(false, NotaAutorizada.Filial?.CaminhoLogoNfe);
                else
                    frmNFe.nfeController.ImprimirNFCe(NomeImpressora, string.IsNullOrEmpty(NomeImpressora), NotaAutorizada.Filial?.CaminhoLogoNfe);
            }
        }

        public void ImprimirPedido()
        {
            pxyPedidoFrenteCaixaCarregar LinhaSelecionada = ovGRD_Pedidos.SelectedRows[0].DataBoundItem as pxyPedidoFrenteCaixaCarregar;
            var Ped = PedidoController.Instancia.GetByCodigo(LinhaSelecionada.Codigo);

            if (Ped.BDelivery == 1)
            {
                var RelatorioFluxoCaixa = new XRReportPedidoDelivery(Ped, PedidoController.Instancia.GetFrenteCaixaNotaPorPedido(LinhaSelecionada.ID)?.Troco ?? 0);
                var tool = new ReportPrintTool(RelatorioFluxoCaixa);

                var Config = (from c in new Modelo.DataClassesDataContext().Configuracaos select c).FirstOrDefault();
                if (1 == Config?.VisualizarNFCe)
                    tool.ShowPreviewDialog();
                else
                    tool.Print(NomeImpressora);
            }
            else
            {
                var _Ped = PedidoController.Instancia.GetFrenteCaixaNotaPorPedido(LinhaSelecionada.ID);
                var RelatorioFluxoCaixa = new XRReportPedidoVenda(null, Ped, _Ped?.Troco ?? 0, _Ped?.TotalPago ?? 0, null);
                var tool = new ReportPrintTool(RelatorioFluxoCaixa);

                var Config = (from c in new Modelo.DataClassesDataContext().Configuracaos select c).FirstOrDefault();
                if (1 == Config?.VisualizarNFCe)
                    tool.ShowPreviewDialog();
                else
                    tool.Print(NomeImpressora);
            }
        }
        private void smbImprimirNFCe_Click(object sender, EventArgs e)
        {
            //NFCe
            ImprimirNFCe();
        }

        private void SbQuantidade_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelarPedido()
        {
            try
            {
                pxyPedidoFrenteCaixaCarregar LinhaSelecionada = ovGRD_Pedidos.SelectedRows[0].DataBoundItem as pxyPedidoFrenteCaixaCarregar;
                Pedido p = PedidoController.Instancia.GetByCodigo(LinhaSelecionada.Codigo);
                if (SHLUtil.UsarSHL())
                {
                    string retorno = SHLUtil.EnviarCancelamentoPedidoSHL(p);
                    if (retorno == "Sucesso")
                        CancelarPedido(p);
                    else
                    {
                        if (p.CodShl != 0)
                            MessageBox.Show("Este pedido já foi cancelado no SHL e portanto não é possivel mudar seu status", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else
                            CancelarPedido(p);
                    }
                }
                else
                    CancelarPedido(p);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CancelarPedido();
        }

        private void CancelarPedido(Pedido p)
        {
            if (p.Status != StatusOrcamento.Cancelado)
            {
                //if (p.Status != StatusOrcamento.Fechado
                //    && p.Status != StatusOrcamento.Faturado)
                //{
                string motivo;
                string usuarioAlteracao = cwkControleUsuario.Facade.getUsuarioLogado.Nome;
                DateTime dataAlteracao = DateTime.Now;

                Aplicacao.InputBox ib = new InputBox();
                ib.Show("Motivo do Cancelamento", "Informe o motivo do cancelamento:", out motivo);

                if (ib.confirmado == true)
                {
                    p.MotivoCancelamento = motivo;
                    p.UsuarioCancelamento = usuarioAlteracao;
                    p.DtCancelamento = dataAlteracao;
                    p.Status = StatusOrcamento.Cancelado;

                    PedidoController.Instancia.Salvar(p, Acao.Alterar);

                    // Cancelando documentos
                    DocumentoController.Instancia.CancelarDocumentos(DocumentoController.Instancia.GetDocumentosPorNumeroPedido(p.Numero));
                    DocumentoController.Instancia.CommitUnitOfWorkTransaction();

                    // BAtualizaEstoque do item da nota pra false
                    var nota = NotaController.Instancia.LoadObjectById(NotaController.Instancia.GetByPedido(p).ID);
                    var itens = nota.NotaItems.ToList();
                    foreach (var ni in itens)
                        ni.BAtualizaEstoque = false;
                    nota.NotaItems = itens;

                    NotaController.Instancia.Salvar(nota, Acao.Alterar);
                }
                //}
                //else
                //    MessageBox.Show("Não é possível cancelar este orçamento !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult resultado = MessageBox.Show("Tem certeza que deseja mudar o status do orçamento", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (resultado == DialogResult.Yes)
                {
                    if (p.TipoPedido.Status == 0)
                        p.Status = (StatusOrcamento)1;
                    else
                        p.Status = (StatusOrcamento)p.TipoPedido.Status;

                    PedidoController.Instancia.Salvar(p, Acao.Alterar);
                }
            }
            InitGrid();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SelecionarNFCe();
        }
    }
}
