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
    public partial class XtraPedidoEntregaBalcao : DevExpress.XtraReports.UI.XtraReport
    {
        private Pedido pedidoSelecionado = null;
        private Configuracao objConfiguracao = null;
        private decimal Troco { get; set; }
        private decimal TotalPago { get; set; }
        public XtraPedidoEntregaBalcao(Pedido pedido, decimal Troco)
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
            TXT_Vendedor.Text = $"{objPedido.Vendedor.Nome}".ToUpper();
            ovTXT_DataHoraEntrada.Text = objPedido.Dt.ToString("g");
            xrBarCode1.Text = objPedido.Codigo.ToString();

            var PedOrOrcamento = objPedido.TipoPedido.Tipo == TipoPedidoType.Orçamento ? "ORÇAMENTO" : "PEDIDO";

            foreach (var item in objPedido.Items)
            {
                DataRow dr = ovDS_Dados.Tables[0].NewRow();
                dr["IDPRODUTO"] = item.Produto.ID;
                dr["CODIGO"] = item.Produto.Codigo;
                dr["DESCRICAO"] = item.Produto.Nome;
                dr["LOCALIZACAO"] = item.Produto.Localizacao;
                dr["QUANTIDADE"] = item.Quantidade;
                ovDS_Dados.Tables[0].Rows.Add(dr);
            }

            
            // Ajuste para carregar o valor do desconto quando o valor de desconto é setado por item
            if (objPedido.ValorDesconto == 0 && objPedido.Items.Sum(x => x.ValorDesconto) != 0)
                objPedido.ValorDesconto = objPedido.Items.Sum(x => x.ValorDesconto);

            /* Parcelas */
            var List = new List<string>();
            foreach (var Parcela in PedidoParcelaController.Instancia.GetParcelasPedido(objPedido))
                List.Add($@"{Parcela.TipoDocumento.FormaPagamento.Nome} {Parcela.Valor.ToString("c2")}");

            TXT_Quantidade.Text = objPedido.Items.Count().ToString();
        }
    }
}
