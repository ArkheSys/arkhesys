using System;
using cwkGestao.Modelo;
using System.Data;
using cwkGestao.Negocio;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using DevExpress.XtraReports.UI;

namespace Aplicacao.Modulos.Rel
{
    public partial class XtraPedidoPreVenda : DevExpress.XtraReports.UI.XtraReport
    {
        private Pedido pedidoSelecionado = null;
        private Configuracao objConfiguracao = null;
        private decimal Troco { get; set; }
        private decimal TotalPago { get; set; }
        public XtraPedidoPreVenda(Pedido pedido, decimal Troco)
        {
            InitializeComponent();
            this.Troco = Troco;
            this.TotalPago = TotalPago;
            pedidoSelecionado = pedido;
            objConfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();
            GerarPedido(pedido);
        }

        private void GerarPedido(Pedido objPedido)
        {
            TXT_Empresa.Text = objPedido.Filial.Nome.ToUpper();
            TXT_Telefone.Text = $"CNPJ/CPF:{objPedido.Filial.CNPJ} FONE: {objPedido.Filial.Telefone}";
            TXT_DATAIMPRESSAO.Text = DateTime.Now.ToString();
            TXT_Vendedor.Text = $"{objPedido.Vendedor.Nome}".ToUpper();
            TXT_Cliente.Text = objPedido.Pessoa?.Nome;
            ovTXT_Observacao.Text = objPedido.Observacao1;
            ovTXT_DataHoraEntrada.Text = objPedido.Dt.ToString("g");
            xrBarCode1.Text = objPedido.Codigo.ToString();
            ovTXT_Observacao.Text = objConfiguracao.ObservacaoPDV;

            var PedOrOrcamento = objPedido.TipoPedido.Tipo == TipoPedidoType.Orçamento ? "ORÇAMENTO" : "PEDIDO";

            foreach (var item in objPedido.Items)
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

            
            // Ajuste para carregar o valor do desconto quando o valor de desconto é setado por item
            if (objPedido.ValorDesconto == 0 && objPedido.Items.Sum(x => x.ValorDesconto) != 0)
                objPedido.ValorDesconto = objPedido.Items.Sum(x => x.ValorDesconto);

            TXT_SUBTOTAL.Text = (objPedido.ValorDesconto + objPedido.TotalProduto).ToString("c2");
            TXT_DESCONTO.Text = objPedido.ValorDesconto.ToString("c2");
            TXT_TOTAL.Text = objPedido.TotalPedido.ToString("c2");
            lblTroco.Text = Troco.ToString("c2");
            lblTotalPago.Text = TotalPago.ToString("c2");

            TXT_DATAIMPRESSAO.Text = DateTime.Now.ToString();

            /* Parcelas */
            var List = new List<string>();
            foreach (var Parcela in PedidoParcelaController.Instancia.GetParcelasPedido(objPedido))
                List.Add($@"{Parcela.TipoDocumento.FormaPagamento.Nome} {Parcela.Valor.ToString("c2")}");

            TXT_FormaPagamento.Text = string.Join(System.Environment.NewLine, List);
            TXT_Quantidade.Text = objPedido.Items.Sum(o=> o.Quantidade).ToString("n3");
        }
    }
}
