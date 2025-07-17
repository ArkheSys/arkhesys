using System;
using cwkGestao.Modelo;
using System.Data;
using cwkGestao.Negocio;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace Aplicacao.Modulos.Comercial.Impressao
{
    public partial class XRReportPedidoVenda : DevExpress.XtraReports.UI.XtraReport
    {
        private Pedido pedidoSelecionado = null;
        private Configuracao objConfiguracao = null;
        private decimal Troco { get; set; }
        private decimal TotalPago { get; set; }
        public XRReportPedidoVenda(decimal Troco, decimal TotalPago, Pedido pedido)
        {
            InitializeComponent();
            this.Troco = Troco;
            this.TotalPago = TotalPago;
            pedidoSelecionado = pedido;
            objConfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();

            // Rotina para carregar foto
            //Pedido.(PedidoID);
            try
            {
                xrPictureBox1.Image = Image.FromFile(pedido.Filial?.CaminhoLogoEmpresa ?? objConfiguracao.Filial?.CaminhoLogoEmpresa);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("O Logotipo da Empresa não está definido",
                    "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            HabilitarCampos();
        }

        private void HabilitarCampos()
        {
            if (objConfiguracao.ImprimirNomeClienteCupom == 0)
            {
                ImprimirNomeClienteCupom.Controls.Clear();
                ImprimirNomeClienteCupom.HeightF = 0;
                SBCliente.Visible = false;
            }

            if (objConfiguracao.ImprimirEnderecoClienteCupom == 0)
            {
                ImprimirEnderecoClienteCupom.Controls.Clear();
                ImprimirEnderecoClienteCupom.HeightF = 0;
                SBEndereco.Visible = false;
            }

            if (objConfiguracao.ImprimirTelefoneClienteCupom == 0)
            {
                ImprimirTelefoneClienteCupom.Controls.Clear();
                ImprimirTelefoneClienteCupom.HeightF = 0;
                SBTelefone.Visible = false;
            }

            if (objConfiguracao.ImprimirCnpjCpfClienteCupom == 0)
            {
                ImprimirCnpjCpfClienteCupom.Controls.Clear();
                ImprimirCnpjCpfClienteCupom.HeightF = 0;
                SBCnpj.Visible = false;
            }

            TXT_Observacao.Text = objConfiguracao.ObservacaoPDV;
        }

        private IList<FrenteCaixaNotaItem> ItensCancelados;

        public XRReportPedidoVenda(Nota objNota, Pedido objPedido, decimal Troco, decimal TotalPago, int? IDFrenteCaixaNota)
        {
            InitializeComponent();
            this.Troco = Troco;
            this.TotalPago = TotalPago;
            objConfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();

            if (objConfiguracao.ImprimirItensCanceladosPDV == 1 && IDFrenteCaixaNota.HasValue)
            {
                var seItensCancelados = FrenteCaixaNotaController.Instancia.SeItensCancelados(IDFrenteCaixaNota.Value);
                if (seItensCancelados)
                {
                    ItensCancelados = FrenteCaixaNotaController.Instancia.GetItensCancelados(IDFrenteCaixaNota.Value);
                }
            }

            try
            {
                if (objNota != null)
                    xrPictureBox1.Image = Image.FromFile(objNota.Filial?.CaminhoLogoEmpresa ?? objConfiguracao.Filial?.CaminhoLogoEmpresa);
                else if (objPedido != null)
                    xrPictureBox1.Image = Image.FromFile(objPedido.Filial?.CaminhoLogoEmpresa ?? objConfiguracao.Filial?.CaminhoLogoEmpresa);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("O Logotipo da Empresa não está definido",
                    "Aviso", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }

            if (objNota != null)
                GerarNota(objNota);
            else
                GerarPedido(objPedido);

            HabilitarCampos();
        }

        private void GerarNota(Nota objNota)
        {
            TXT_EMPRESA.Text = objNota.Filial.Nome.ToUpper();
            TXT_ENDERECO.Text = $"{objNota.Filial.Endereco},{objNota.Filial.Numero} {objNota.Filial.Cidade.Nome}-{objNota.Filial.Cidade.UF.Sigla}".ToUpper();
            TXT_TELEFONE.Text = $"CNPJ/CPF: {objNota.Filial.CNPJ} FONE: {objNota.Filial.Telefone}";
            TXT_INSCRICAOESTADUAL.Text = $"INSC.ESTADUAL: {objNota.Filial.Inscricao}";

            TXT_NUMEROPEDIDO.Text = $"PEDIDO {objNota.CodigoPedido}";
            txt_TipoPedido.Text = "TIPO: NOTA";
            TXT_DATA.Text = objNota.Dt.ToString();

            TXT_CLIENTE.Text = $"{objNota.Pessoa?.Nome} [Cód.: {objNota.Pessoa?.Codigo}]";
            TXT_CPFCNPJ.Text = objNota.Pessoa?.CNPJ_CPF;

            // Endereço da Pessoa:

            TXT_ENDERECOCLIENTE.Text = $"{objNota.Pessoa.EnderecoPrincipal()?.Endereco}, {objNota.Pessoa.EnderecoPrincipal()?.Numero}, BAIRRO: {objNota.Pessoa.EnderecoPrincipal()?.Bairro}".ToUpper();
            TXT_CIDADE.Text = $"{objNota.Pessoa.EnderecoPrincipal()?.Cidade}".ToUpper();
            TXT_CEP.Text = $"{objNota.Pessoa.EnderecoPrincipal()?.CEP}".ToUpper();
            TXT_TELEFONECLIENTE.Text = $"{objNota.Pessoa.TelefonePrincipal}";
            TXT_VENDEDOR.Text = $"{objNota.Vendedor.Pessoa.Nome}".ToUpper();

            foreach (var item in objNota.NotaItems)
            {
                DataRow dr = ovDS_Dados.Tables[0].NewRow();
                dr["IDPRODUTO"] = item.Produto.ID;
                dr["CODIGO"] = item.Produto.Codigo;
                dr["DESCRICAO"] = item.Produto.Nome;
                dr["UNIDADE"] = item.Unidade;

                dr["QUANTIDADE"] = item.Quantidade;
                dr["VALORUNITARIO"] = item.Valor;
                dr["VALORDESCONTO"] = item.PercDesconto == 0 ? 0 : item.Total - item.SubTotal;
                dr["VALORTOTAL"] = item.Total.ToString("n2");
                ovDS_Dados.Tables[0].Rows.Add(dr);
            }

            if (ItensCancelados != null)
                foreach (var item in ItensCancelados)
                {
                    DataRow dr = ovDS_Dados.Tables[0].NewRow();
                    dr["IDPRODUTO"] = item.Produto.ID;
                    dr["CODIGO"] = item.Produto.Codigo;
                    dr["DESCRICAO"] = item.Produto.Nome;
                    dr["UNIDADE"] = item.Unidade;

                    dr["QUANTIDADE"] = item.Quantidade;
                    dr["VALORUNITARIO"] = 0;
                    dr["VALORDESCONTO"] = 0;
                    dr["VALORTOTAL"] = "Cancelado";
                    dr["CANCELADO"] = true;
                    ovDS_Dados.Tables[0].Rows.Add(dr);
                }

            TXT_SUBTOTAL.Text = (objNota.TotalProduto + (objNota.NotaItems.Sum(n=> n.ValorDesconto)==0? 0: objNota.ValorDesconto)).ToString("c2");
            TXT_DESCONTO.Text = objNota.ValorDesconto.ToString("c2");
            TXT_TOTAL.Text = objNota.TotalNota.ToString("c2");
            lblTroco.Text = Troco.ToString("c2");
            lblTotalPago.Text = TotalPago.ToString("c2");

            TXT_DATAIMPRESSAO.Text = DateTime.Now.ToString();

            /* Parcelas */
            var List = new List<string>();
            foreach (var Parcela in objNota.NotaParcelas)
                List.Add($@"{Parcela.TipoDocumento.FormaPagamento.Nome} {Parcela.Valor.ToString("c2")}");

            TXT_FormaPagamento.Text = string.Join(System.Environment.NewLine, List);

            bool temPagamentosNaoAVista = objNota.NotaParcelas.Any(a => !a.BEntrada);

            if (objConfiguracao.ImprimirAssinaturaNaoDinheiro == 1 && temPagamentosNaoAVista)
            {
                xrLabel24.Visible = true;
                xrLine4.Visible = true;
            }
            else
            {
                xrLabel24.Visible = false;
                xrLine4.Visible = false;
            }
        }

        private void GerarPedido(Pedido objPedido)
        {
            TXT_EMPRESA.Text = objPedido.Filial.Nome.ToUpper();
            TXT_ENDERECO.Text = $"{objPedido.Filial.Endereco},{objPedido.Filial.Numero} {objPedido.Filial.Cidade.Nome}-{objPedido.Filial.Cidade.UF.Sigla}".ToUpper();
            TXT_TELEFONE.Text = $"CNPJ/CPF: {objPedido.Filial.CNPJ} FONE: {objPedido.Filial.Telefone}";
            TXT_INSCRICAOESTADUAL.Text = $"INSC.ESTADUAL: {objPedido.Filial.Inscricao}";

            var PedOrOrcamento = objPedido.TipoPedido.Tipo == TipoPedidoType.Orçamento ? "ORÇAMENTO" : "PEDIDO";
            TXT_NUMEROPEDIDO.Text = $"{PedOrOrcamento} {objPedido.Codigo}";
            txt_TipoPedido.Text = "TIPO: " + objPedido.TipoPedido.Nome.ToUpper();
            TXT_DATA.Text = objPedido.Dt.ToString();

            TXT_CLIENTE.Text = objPedido.Pessoa?.Nome;
            TXT_CPFCNPJ.Text = objPedido.Pessoa?.CNPJ_CPF;

            // Endereço da Pessoa:

            TXT_ENDERECOCLIENTE.Text = $"{objPedido.Pessoa.EnderecoPrincipal()?.Endereco}, {objPedido.Pessoa.EnderecoPrincipal()?.Numero}, BAIRRO: {objPedido.Pessoa.EnderecoPrincipal()?.Bairro}".ToUpper();
            TXT_CIDADE.Text = $"{objPedido.Pessoa.EnderecoPrincipal()?.Cidade}".ToUpper();
            TXT_CEP.Text = $"{objPedido.Pessoa.EnderecoPrincipal()?.CEP}".ToUpper();
            TXT_TELEFONECLIENTE.Text = $"{objPedido.Pessoa.TelefonePrincipal}";
            TXT_VENDEDOR.Text = $"{objPedido.Vendedor.Nome}".ToUpper();

            foreach (var item in objPedido.Items)
            {
                DataRow dr = ovDS_Dados.Tables[0].NewRow();
                dr["IDPRODUTO"] = item.Produto.ID;
                dr["CODIGO"] = item.Produto.Codigo;
                dr["DESCRICAO"] = item.Produto.Nome + GetObsItem(item);
                dr["UNIDADE"] = item.Unidade;
                dr["QUANTIDADE"] = item.Quantidade;
                dr["VALORUNITARIO"] = item.Valor;
                dr["VALORDESCONTO"] = item.PercDesconto == 0 ? 0 : item.Total - item.SubTotal;
                dr["VALORTOTAL"] = item.Total.ToString("n2");
                ovDS_Dados.Tables[0].Rows.Add(dr);
            }

            if (ItensCancelados != null)
                foreach (var item in ItensCancelados)
                {
                    DataRow dr = ovDS_Dados.Tables[0].NewRow();
                    dr["IDPRODUTO"] = item.Produto.ID;
                    dr["CODIGO"] = item.Produto.Codigo;
                    dr["DESCRICAO"] = item.Produto.Nome;
                    dr["UNIDADE"] = item.Unidade;

                    dr["QUANTIDADE"] = item.Quantidade;
                    dr["VALORUNITARIO"] = 0;
                    dr["VALORDESCONTO"] = 0;
                    dr["VALORTOTAL"] = "Cancelado";
                    dr["CANCELADO"] = true;
                    ovDS_Dados.Tables[0].Rows.Add(dr);
                }

            // Ajuste para carregar o valor do desconto quando o valor de desconto é setado por item
            if (objPedido.ValorDesconto == 0 && objPedido.Items.Sum(x => x.ValorDesconto) != 0)
                objPedido.ValorDesconto = objPedido.Items.Sum(x => x.ValorDesconto);

            TXT_SUBTOTAL.Text = ((objPedido.Items.Sum(p=> p.ValorDesconto) ==0? 0 : objPedido.ValorDesconto) + objPedido.TotalProduto).ToString("c2");
            TXT_DESCONTO.Text = objPedido.ValorDesconto.ToString("c2"); 
            TXT_TOTAL.Text = objPedido.TotalPedido.ToString("c2");
            lblTroco.Text = Troco.ToString("c2");
            lblTotalPago.Text = TotalPago.ToString("c2");

            TXT_DATAIMPRESSAO.Text = DateTime.Now.ToString();

            /* Parcelas */
            var List = new List<string>();
            var Parcelas = PedidoParcelaController.Instancia.GetParcelasPedido(objPedido);
            foreach (var Parcela in Parcelas)
                List.Add($@"{Parcela.TipoDocumento.FormaPagamento.Nome} {Parcela.Valor.ToString("c2")}");

            TXT_FormaPagamento.Text = string.Join(System.Environment.NewLine, List);
            bool temPagamentosNaoAVista = Parcelas.Any(a => !a.BEntrada);

            if(objConfiguracao.ImprimirAssinaturaNaoDinheiro ==1 && temPagamentosNaoAVista)
            {
                xrLabel24.Visible = true;
                xrLine4.Visible = true;
            }
            else
            {
                xrLabel24.Visible = false;
                xrLine4.Visible = false;
            }
        }
        
        private string GetObsItem(PedidoItem item)
        {
            if (objConfiguracao.ObservacaoNoItemPedido == 1 && !string.IsNullOrEmpty(item.AplicacaoCompra + item.InformacoesAdicionais))
            {
                string obs = (string.IsNullOrEmpty((item.AplicacaoCompra + item.InformacoesAdicionais)) ? "" : System.Environment.NewLine + (item.AplicacaoCompra + 
                    (string.IsNullOrEmpty(item.AplicacaoCompra) ? "" :
                    (string.IsNullOrEmpty(item.InformacoesAdicionais) ? "" : " - ")) + item.InformacoesAdicionais));

                return obs;
            }

            else
                return "";
        }
    }
}
