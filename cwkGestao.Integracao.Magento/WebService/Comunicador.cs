using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using System.Xml;
using System.Web;
using cwkGestao.Negocio;
using cwkGestao.Integracao.Magento.Utils;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using System.Collections;

namespace cwkGestao.Integracao.Magento.WebService
{
    public class Comunicador : IDisposable
    {
        private MagentoService magentoConnect;
        private readonly string session;

        public Comunicador(string _usuario, string _senha)
        {
            //cwkGestao.Integracao.Magento.Properties.Settings.Default.cwkGestao_Integracao_Magento_br_com_redfeet_cwork_MagentoService = "http://www.geologiabr.com/index.php/api/v2_soap/index/";
            //cwkGestao.Integracao.Magento.Properties.Settings.Default.Save();

            magentoConnect = new MagentoService();
            magentoConnect.EnableDecompression = true;

            session = magentoConnect.login(_usuario, _senha);
        }

        # region Importação SOAP <- Magento

        # region Importar produtos

        public catalogProductEntity[] RequisitarProdutos()
        {
            filters filter = new filters();

            return magentoConnect.catalogProductList(session, filter, null);
        }

        public catalogProductEntity[] RequisitarProdutos(string sku)
        {
            filters filter = new filters();
            complexFilter[] complexFilterArray = new complexFilter[1];
            complexFilter complexFilter = new complexFilter();
            associativeEntity associative = new associativeEntity();

            complexFilter.key = "sku";
            associative.key = "sku";
            associative.value = sku;
            complexFilter.value = associative;
            complexFilterArray[0] = complexFilter;
            filter.complex_filter = complexFilterArray;

            return magentoConnect.catalogProductList(session, filter, null);
        }

        public catalogProductReturnEntity RequisitarProduto(string id)
        {
            var _Produto = magentoConnect.catalogProductInfo(session, id, null, null, null);

            return _Produto;
        }

        public catalogInventoryStockItemEntity[] RequisitarProdutoEstoque(string id)
        {
            string[] item = new string[1];
            item[0] = id;
            return magentoConnect.catalogInventoryStockItemList(session, item);
        }

        # endregion

        # region Importar clientes

        public customerCustomerEntity[] RequisitarClientes()
        {
            filters filters = new filters();

            return magentoConnect.customerCustomerList(session, filters);
        }

        public customerCustomerEntity RequisitarCliente(int id)
        {
            var algo = new customerCustomerEntity();

            if(id != 0)
              algo = magentoConnect.customerCustomerInfo(session, id, null);

            return algo;
        }

        public customerAddressEntityItem[] RequisitarClienteEndereco(int id)
        {
            var algo = new customerAddressEntityItem[0];

            if(id != 0)
              algo = magentoConnect.customerAddressList(session, id);

            return algo;
        }

        # endregion

        # region Importar pedidos

        public salesOrderListEntity[] RequisitarPedidos()
        {
            var statusPedido = MagentoConfiguracaoController.Instancia.GetConfiguracao().StatusPedido;

            filters filter = new filters();
            complexFilter[] complexFilterArray = new complexFilter[1];
            complexFilter complexFilter = new complexFilter();
            associativeEntity associative = new associativeEntity();

            complexFilter.key = "status";
            associative.key = "status";

            associative.value = statusPedido; 
 

            complexFilter.value = associative;
            complexFilterArray[0] = complexFilter;
            filter.complex_filter = complexFilterArray;

            magentoConnect.EnableDecompression = true;
            return magentoConnect.salesOrderList(session, filter);
        }

        public salesOrderEntity RequisitarPedido(string id) 
        {
            MagentoSoapV1 objMagento = new MagentoSoapV1();
            return objMagento.MontarPedidoV1ParaV2(id);  
        }

        #endregion

       

        # region Exportação SOAP <- Magento

        # region Atualizar status do pedido

        public void AlterarStatusPedidoEntrega(int idItegragao)
        {
            string id = "1" + String.Format("{0:00000000}", idItegragao);
            salesOrderEntity magentoPedido = magentoConnect.salesOrderInfo(session, id);

            orderItemIdQty[] qtd = new orderItemIdQty[1];
            qtd[0] = new orderItemIdQty();
            qtd[0].order_item_id = Convert.ToInt32(magentoPedido.items[0].item_id);
            qtd[0].qty = magentoPedido.items[0].qty_ordered.ValorMagentoToDouble();

            magentoConnect.salesOrderShipmentCreate(session, id, qtd, "", 0, 0);
        }
        public void AlterarStatusPedidoFatura(int idItegragao)
        {
            string id = "1" + String.Format("{0:00000000}", idItegragao);
            salesOrderEntity magentoPedido = magentoConnect.salesOrderInfo(session, id);
            orderItemIdQty[] qtd = new orderItemIdQty[1];
            qtd[0] = new orderItemIdQty();
            qtd[0].order_item_id = Convert.ToInt32(magentoPedido.items[0].item_id);
            qtd[0].qty = magentoPedido.items[0].qty_ordered.ValorMagentoToDouble();

            magentoConnect.salesOrderInvoiceCreate(session, id, qtd, "fatura", "ti@cwork.com.br", "fatura");
        }

        public void AtualizarEstoqueViaPedido(Pedido pedido)
        {
            var ConfigMagento = MagentoConfiguracaoController.Instancia.GetConfiguracao();
            IList<pxyEstoque> _Estoque = new List<pxyEstoque>();
            pxyEstoque _aux = new pxyEstoque();

            foreach (var item in pedido.Items)
            {
                catalogInventoryStockItemUpdateEntity campo = new catalogInventoryStockItemUpdateEntity();
                _Estoque = ProdutoController.Instancia.GetListaEstoque(item.Produto.ID);

                foreach (var estoque in _Estoque)
                {
                    if (estoque.LocalEstoque.ToLower() == ConfigMagento.LocalEstoque.ToString().ToLower())
                        _aux = estoque;
                }

                var qtd = _aux.Saldo;

                campo.qty = Convert.ToString(qtd);

                IList<ProdutoSKU> listaSKU = ProdutoSKUController.Instancia.GetSKUPorProduto(item.Produto.ID);

                if (listaSKU.Count == 0)
                {
                    throw new Exception("Não encontrado SKU para os produtos em questão");
                }

                foreach (var id in listaSKU)
                {
                    magentoConnect.catalogInventoryStockItemUpdate(session, id.SKU, campo);
                }
            } 
        }

        # endregion

        # region Atualizar preço e estoque

        public bool AlterarPrecoProdutos(catalogProductReturnEntity produto, IList<ProdutoSKU> listaSKU)
        {
            foreach (var item in listaSKU)
            {
                catalogProductCreateEntity campo = new catalogProductCreateEntity();
                campo.price = produto.price;

                magentoConnect.catalogProductUpdate(session, item.SKU, campo, null, null);
            }

            if (listaSKU.Count() == 0)
                return false;

            return true;
        }

        public void ExportarNovoProduto(catalogProductReturnEntity produto, IList<ProdutoSKU> listaSKU)
        {
            foreach (var item in listaSKU)
            {
                catalogProductCreateEntity campo = new catalogProductCreateEntity();

                campo.additional_attributes.single_data = produto.additional_attributes;
                campo.categories = produto.categories;
                campo.category_ids = produto.category_ids;
                campo.custom_design = produto.custom_design;
                campo.description = produto.description;
                campo.gift_message_available = produto.gift_message_available;
                campo.has_options = produto.has_options;
                campo.meta_description = produto.meta_description;
                campo.meta_keyword = produto.meta_keyword;
                campo.meta_title = produto.meta_title;
                campo.name = produto.name;
                campo.options_container = produto.options_container;
                campo.price = produto.price;
                campo.short_description = produto.short_description;
                campo.special_from_date = produto.special_from_date;
                campo.special_price = produto.special_price;
                campo.special_to_date = produto.special_to_date;
                campo.status = produto.status;
                campo.tax_class_id = produto.tax_class_id;
                campo.tier_price = produto.tier_price;
                campo.url_key = produto.url_key;
                campo.url_path = produto.url_path;
                campo.visibility = produto.visibility;
                campo.website_ids = produto.website_ids;
                campo.websites = produto.websites;
                campo.weight = produto.weight;

                magentoConnect.catalogProductCreate(session, null, null, item.SKU, campo, null);
            }

        }

        public void AlterarProdutoEstoque(Nota nota)
        {
            try
            {
                foreach (var item in nota.NotaItems)
                {
                    if (nota.TipoNota.BAtualizaEstoque)
                    {
                        catalogInventoryStockItemUpdateEntity campo = new catalogInventoryStockItemUpdateEntity();
                        campo.qty = NotaItemController.Instancia.SaldoEstoqueLocalEstoque(nota.LocalEstoqueNota.ID, item.Produto.ID).ToString();

                        //string id = ProdutoController.Instancia.GetIDIntegracaoPorID(item.Produto.ID);
                        IList<ProdutoSKU> listaSKU = ProdutoSKUController.Instancia.GetSKUPorProduto(item.Produto.ID);

                        if (listaSKU.Count == 0)
                        {
                            throw new Exception("Não encontrado SKU para os produtos em questão");
                        }

                        foreach (var id in listaSKU)
                        {
                            magentoConnect.catalogInventoryStockItemUpdate(session, id.SKU, campo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarEstoqueBalanco(Balanco balanco)
        {
            try
            {
                foreach (var item in balanco.BalancoItems)
                {
                    catalogInventoryStockItemUpdateEntity campo = new catalogInventoryStockItemUpdateEntity();
                    campo.qty = item.Quantidade.ToString();

                    IList<ProdutoSKU> listaSKU = ProdutoSKUController.Instancia.GetSKUPorProduto(item.Produto.ID);

                    if (listaSKU.Count == 0)
                    {
                        throw new Exception("Não encontrado SKU para os produtos em questão");
                    }

                    foreach (var id in listaSKU)
                    {
                        magentoConnect.catalogInventoryStockItemUpdate(session, id.SKU, campo);
                    }
                }   
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        # endregion

        #region Exportação Código Rastreamento

        public void ExportarCodigoRastreamento(ExpedicaoRastreamento rastreamento)
        {
            try
            {
                var transpMagento = GetTransportadorasMagento(rastreamento.Pedido);
                string CodigoEntrega = CriarNovaEntrega(rastreamento.Pedido);

                if (rastreamento.IDRastreamento.CodigoEntregaMagento != CodigoEntrega)
                {
                    rastreamento.IDRastreamento.CodigoEntregaMagento = CodigoEntrega;
                    RastreamentoCorreioController.Instancia.Salvar(rastreamento.IDRastreamento, Acao.Alterar);

                    magentoConnect.salesOrderShipmentAddTrack(session, CodigoEntrega,
                    transpMagento.key, rastreamento.IDRastreamento.TipoServico.Descricao,
                    rastreamento.IDRastreamento.Letra + rastreamento.IDRastreamento.Numero + "BR");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        private associativeEntity GetTransportadorasMagento(Pedido pedido)
        {
           return magentoConnect.salesOrderShipmentGetCarriers(session, pedido.CodigoPedidoMagento.ToString()).LastOrDefault();
        }

        private string CriarNovaEntrega(Pedido pedido)
        {
            try
            {
                if (pedido.CodigoEntregaMagento == null || pedido.CodigoEntregaMagento == "")
                {
                    pedido.CodigoEntregaMagento = magentoConnect.salesOrderShipmentCreate(session, pedido.CodigoPedidoMagento.ToString(), null, "", 1, 1);
                    PedidoController.Instancia.Salvar(pedido, Acao.Alterar);
                }

                return pedido.CodigoEntregaMagento;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        # endregion

        public void Dispose()
        {
            magentoConnect.endSession(session);
        }
        # endregion
    }
}