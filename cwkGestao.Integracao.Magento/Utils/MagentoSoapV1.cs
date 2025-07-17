using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio;
using cwkGestao.Modelo;
using cwkGestao.Integracao.Magento.Magentov1;
using cwkGestao.Integracao.Magento.br.com.redfeet.cwork;
using System.Xml;
using System.Reflection;
using System.Collections;

namespace cwkGestao.Integracao.Magento.Utils
{
    public class MagentoSoapV1
    {
        MagentoConfiguracao ConfigMagento = MagentoConfiguracaoController.Instancia.GetConfiguracao();
        Magentov1.MagentoService Magentov1 = new Magentov1.MagentoService();
        private static salesOrderEntity PedidoV2 = new salesOrderEntity();
        

        private string login()
        {
            return Magentov1.login(ConfigMagento.Usuario, ConfigMagento.Senha);
        }

        public System.Xml.XmlNode[] ImportacaoCliente(string idCliente)
        {
            return ((System.Xml.XmlNode[])Magentov1.call(login(), "customer.info", idCliente));
        }

        public System.Xml.XmlNode[] RequisitarPedidoV1(string id)
        {
            return ((System.Xml.XmlNode[])Magentov1.call(login(), "sales_order.info", id));
        }

        #region MontarPedidoV1

        public salesOrderEntity MontarPedidoV1ParaV2(string id)
        {
            #region Comentário
            /*
             * Esse método pega a maioria dos dados do pedido que é retornado da,
             * API V1 do Magento onde é necessário um case para tirar os valores do nó,
             * do XML.
             */
            #endregion

            MagentoSoapV1 MagentoV1 = new MagentoSoapV1();

            var PedidoV1 = MagentoV1.RequisitarPedidoV1(id);

            foreach (var item in PedidoV1)
            {
                #region Dados PedidoV1
                switch (item.FirstChild.InnerText)
                {
                    case "state": PedidoV2.state = item.LastChild.InnerText; break;
                    case "status": PedidoV2.status = item.LastChild.InnerText; break;
                    case "shipping_description": PedidoV2.shipping_description = item.LastChild.InnerText; break;
                    case "customer_id": PedidoV2.customer_id = item.LastChild.InnerText; break;
                    case "base_discount_amount": PedidoV2.base_discount_amount = item.LastChild.InnerText; break;
                    case "base_grand_total": PedidoV2.base_grand_total = item.LastChild.InnerText; break;
                    case "base_shipping_amount": PedidoV2.base_shipping_amount = item.LastChild.InnerText; break;
                    case "base_subtotal": PedidoV2.base_subtotal = item.LastChild.InnerText; break;
                    case "base_tax_amount": PedidoV2.base_tax_amount = item.LastChild.InnerText; break;
                    case "base_to_global_rate": PedidoV2.base_to_global_rate = item.LastChild.InnerText; break;
                    case "base_to_order_rate": PedidoV2.base_to_order_rate = item.LastChild.InnerText; break;
                    case "base_total_invoiced": PedidoV2.base_total_invoiced = item.LastChild.InnerText; break;
                    case "base_total_paid": PedidoV2.base_total_paid = item.LastChild.InnerText; break;
                    case "discount_amount": PedidoV2.discount_amount = item.LastChild.InnerText; break;
                    case "grand_total": PedidoV2.grand_total = item.LastChild.InnerText; break;
                    case "shipping_amount": PedidoV2.shipping_amount = item.LastChild.InnerText; break;
                    case "store_to_base_rate": PedidoV2.store_to_base_rate = item.LastChild.InnerText; break;
                    case "store_to_order_rate": PedidoV2.store_to_order_rate = item.LastChild.InnerText; break;
                    case "subtotal": PedidoV2.subtotal = item.LastChild.InnerText; break;
                    case "tax_amount": PedidoV2.tax_amount = item.LastChild.InnerText; break;
                    case "total_invoiced": PedidoV2.total_invoiced = item.LastChild.InnerText; break;
                    case "total_paid": PedidoV2.total_paid = item.LastChild.InnerText; break;
                    case "total_qty_ordered": PedidoV2.total_qty_ordered = item.LastChild.InnerText; break;
                    case "customer_is_guest": PedidoV2.customer_is_guest = item.LastChild.InnerText; break;
                    case "customer_note_notify": PedidoV2.customer_note_notify = item.LastChild.InnerText; break;
                    case "billing_address_id": PedidoV2.billing_address_id = item.LastChild.InnerText; break;
                    case "customer_group_id": PedidoV2.customer_group_id = item.LastChild.InnerText; break;
                    case "email_sent": PedidoV2.email_sent = item.LastChild.InnerText; break;
                    case "quote_id": PedidoV2.quote_id = item.LastChild.InnerText; break;
                    case "shipping_address_id": PedidoV2.shipping_address_id = item.LastChild.InnerText; break;
                    case "weight": PedidoV2.weight = item.LastChild.InnerText; break;
                    case "increment_id": PedidoV2.increment_id = item.LastChild.InnerText; break;
                    case "applied_rule_ids": PedidoV2.applied_rule_ids = item.LastChild.InnerText; break;
                    case "base_currency_code": PedidoV2.base_currency_code = item.LastChild.InnerText; break;
                    case "customer_email": PedidoV2.customer_email = item.LastChild.InnerText; break;
                    case "customer_firstname": PedidoV2.customer_firstname = item.LastChild.InnerText; break;
                    case "customer_lastname": PedidoV2.customer_lastname = item.LastChild.InnerText; break;
                    case "global_currency_code": PedidoV2.global_currency_code = item.LastChild.InnerText; break;
                    case "order_currency_code": PedidoV2.order_currency_code = item.LastChild.InnerText; break;
                    case "shipping_method": PedidoV2.shipping_method = item.LastChild.InnerText; break;
                    case "store_currency_code": PedidoV2.store_currency_code = item.LastChild.InnerText; break;
                    case "store_name": PedidoV2.store_name = item.LastChild.InnerText; break;
                    case "created_at": PedidoV2.created_at = item.LastChild.InnerText; break;
                    case "updated_at": PedidoV2.updated_at = item.LastChild.InnerText; break;
                    case "order_id": PedidoV2.order_id = item.LastChild.InnerText; break;
                    case "parent_id": PedidoV2.parent_id = item.LastChild.InnerText; break;
                    case "address_id": PedidoV2.shipping_address_id = item.LastChild.InnerText; break;
                    default:
                        break;
                }

                #endregion;
            }

            return PegaArrayPedidoV1(PedidoV1);
                
        }

        private static salesOrderEntity ObtemEnderecoPedido(string xml)
        {
            #region Comentário
            /*
             * Nesse método leio o xml e armazeno em um Hashtable a,
             * chave e valor do xml, que no caso seria nome do campo,
             * e o valor do mesmo, no final é feito um método de reflexão,
             * atribuindo os valores do Hash no objeto.
             */
            #endregion

            string nomeNo = "";
            salesOrderAddressEntity objEndereco = new salesOrderAddressEntity();
            PropertyInfo[] propObjEndereco = objEndereco.GetType().GetProperties();
            Hashtable HashObjEndereco = new Hashtable();
            string key = "", value = "";
            
            var aux = xml.Replace("<key xsi:type=\"xsd:string\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">shipping_address</key>", "");

            XmlDocument xmldocument = new XmlDocument();
            xmldocument.LoadXml(aux);
            XmlNodeReader leitor = new XmlNodeReader(xmldocument);

            while (leitor.Read())
            {
                if (leitor.NodeType == XmlNodeType.EndElement || leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                if (leitor.NodeType == XmlNodeType.Element)
                {
                    nomeNo = leitor.LocalName;
                    continue;
                }

                if (nomeNo.ToLower() == "key" && key == "")
                {
                    key = leitor.Value;
                }
                else if (nomeNo.ToLower() == "value")
                {
                    value = leitor.Value;
                }
                else if (nomeNo.ToLower() == "key" && key != "")
                {
                    value = "";
                    HashObjEndereco.Add(key, value);
                    key = leitor.Value;
                }
                else
                {
                    HashObjEndereco.Add(key, value);
                    key = "";
                    value = "";
                }

                if (key != "" && value != "")
                {
                    HashObjEndereco.Add(key, value);
                    key = "";
                    value = "";
                }

            }

            foreach (PropertyInfo prop in propObjEndereco)
            {
                try
                {
                    prop.SetValue(objEndereco, HashObjEndereco[prop.Name], null);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            PedidoV2.shipping_address = objEndereco;
            HashObjEndereco.Clear();

            return PedidoV2;
        }

        private static salesOrderEntity PegaArrayPedidoV1(XmlNode[] _item)
        {
            #region Comentário
            /*
             * Devido o Magento Soap V1 retornar vários nós de xml,
             * ocorre de em um único nó vir um xml inteiro, no caso de itens,
             * e endereço faço um filtro pra quando ocorrer os casos,
             * ele busca em métodos separados os valores do mesmo.
             */
            #endregion

            salesOrderEntity Pedido = new salesOrderEntity();
            foreach (var item in _item)
            {
                switch (item.FirstChild.InnerText)
                {
                    case "items":
                        Pedido = ObterItemsPedido(item.InnerXml.ToString());
                        break;
                    case "shipping_address":
                        Pedido = ObtemEnderecoPedido(item.InnerXml.ToString());
                        break;
                }
            }

            return Pedido;
        }

        private static salesOrderEntity ObterItemsPedido(string xml)
        {
            #region Comentário
            /*
             * Nesse método pego os itens do pedido que vem também em um xml separado,
             * leio o xml e pego chave e valor que no caso são nome do campo e seu valor,
             * atribuo em um Hashtable e no final atribuo todo os dados do Hash no objeto.
             */
            #endregion

            string nomeNo = "";

            salesOrderItemEntity PedidoItemV2 = new salesOrderItemEntity();
            PropertyInfo[] propPedidoV2 = PedidoItemV2.GetType().GetProperties();
            Hashtable Hash = new Hashtable();
            IList<salesOrderItemEntity> PedidosItemV2 = new List<salesOrderItemEntity>();

            string key = "", value = "";

            var aux = xml.Replace("<key xsi:type=\"xsd:string\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">items</key>", "");

            XmlDocument _xml = new XmlDocument();
            _xml.LoadXml(aux);
            XmlNodeReader leitor = new XmlNodeReader(_xml);

            while (leitor.Read())
            {
                if (leitor.NodeType == XmlNodeType.EndElement || leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                if (leitor.NodeType == XmlNodeType.Element)
                {
                    nomeNo = leitor.LocalName;
                    continue;
                }

                if (nomeNo.ToLower() == "key" && key == "")
                {
                    key = leitor.Value;
                }
                else if (nomeNo.ToLower() == "value")
                {
                    value = leitor.Value;
                }
                else if (nomeNo.ToLower() == "key" && key != "")
                {
                    value = "";
                    Hash.Add(key, value);
                    key = leitor.Value;
                }
                else
                {
                    Hash.Add(key, value);
                    key = "";
                    value = "";
                }

                if (key != "" && value != "")
                {
                    Hash.Add(key, value);
                    key = "";
                    value = "";
                }

                if (Hash.Count == 78)
                {
                    foreach (PropertyInfo prop in propPedidoV2)
                    {
                        try
                        {
                            prop.SetValue(PedidoItemV2, Hash[prop.Name], null);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }

                    }
                    PedidosItemV2.Add(PedidoItemV2);
                    Hash.Clear();
                }

            }
            PedidoV2.items = PedidosItemV2.ToArray();

            return PedidoV2;
        }

        #endregion
    }
        
}
