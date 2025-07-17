using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Magento.WebService;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using System.Xml.Linq;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.Magento.Importacao
{
    public class Buscador
    {
        private readonly Comunicador comunicador;
        private readonly Action<string> contador;
        int count = 0;

        # region Estabelece conexão

        public Buscador(string usuario, string senha, Action<string> atualizadorContador)
        {
            comunicador = new Comunicador(usuario, senha);

            this.contador = atualizadorContador;
        }

        # endregion

        # region Importar produtos

        public IEnumerable<catalogProductReturnEntity> ImportarProdutos()
        {
            foreach (var produtosMagento in comunicador.RequisitarProdutos())
            {
                if (contador != null)
                    contador("Registros recebidos: " + ++count);

                yield return comunicador.RequisitarProduto(produtosMagento.product_id);
            }
        }

        public catalogProductReturnEntity ImportarProduto(string id)
        {
            return comunicador.RequisitarProduto(id);
        }

        public IEnumerable<catalogInventoryStockItemEntity> ImportarProdutoEstoque(string id)
        {
            return comunicador.RequisitarProdutoEstoque(id);
        }

        # endregion

        # region Importar clientes

        public IEnumerable<customerCustomerEntity> ImportarClientes()
        {
            foreach (var clientesMagento in comunicador.RequisitarClientes())
            {
                if (contador != null)
                    contador("Registros recebidos: " + ++count);

                yield return comunicador.RequisitarCliente(clientesMagento.customer_id);
            }
        }

        public customerCustomerEntity ImportarCliente(int id)
        {
            return comunicador.RequisitarCliente(id);
        }

        public customerAddressEntityItem[] ImportarClienteEndereco(int id)
        {
            return comunicador.RequisitarClienteEndereco(id);
        }

        # endregion

        # region Importar pedidos

        public IEnumerable<salesOrderEntity> ImportarPedidos()
        {
            foreach (var pedidosMagento in comunicador.RequisitarPedidos())
            {
                if (contador != null)
                    contador("Registros recebidos: " + ++count);

                if (DesconsiderarPedido(pedidosMagento))
                    continue;

                yield return comunicador.RequisitarPedido(pedidosMagento.increment_id);
            }
        }

        public salesOrderEntity ImportarPedido(string id)
        {
            return comunicador.RequisitarPedido(id);
        }

        public bool DesconsiderarPedido(salesOrderListEntity pedidoMagento)
        {
            var _pedidosMagentoCadastrados = PedidoController.Instancia.GetHashIdsIntegracao();
            var _pedidosMagentoPendentes = PedidoController.Instancia.GetHashIdsIntegracaoPendente();

            return String.IsNullOrEmpty(pedidoMagento.order_id) || _pedidosMagentoCadastrados.Contains(Convert.ToInt32(pedidoMagento.order_id)) || _pedidosMagentoPendentes.Contains(Convert.ToInt32(pedidoMagento.order_id)) || String.IsNullOrEmpty(pedidoMagento.customer_id);
        }

        # endregion
    }
}
