using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.WebService;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Magento.Exportacao
{
    public class ExportadorPedidoStatusEntrega : IExportador<Pedido>
    {
#pragma warning disable CS0169 // O campo "ExportadorPedidoStatusEntrega.pedidoMagento" nunca é usado
        private salesOrderEntity pedidoMagento;
#pragma warning restore CS0169 // O campo "ExportadorPedidoStatusEntrega.pedidoMagento" nunca é usado
        private Comunicador comunicador;

        public ExportadorPedidoStatusEntrega(Comunicador comunicador)
        {
            this.comunicador = comunicador;
        }

        public void Exportar(Pedido pedido)
        {
            comunicador.AtualizarEstoqueViaPedido(pedido);
        }

    }
}