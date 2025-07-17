using cwkGestao.Integracao.Magento.RedfeetAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Text;
using cwkGestao.Integracao.Magento.DTO;
using System.Collections.Generic;

namespace cwkGestao.Integracao.Magento.Test
{

    [TestClass()]
    public class DeserializadorTest
    {
        [TestMethod()]
        public void Deserializar_JSON_Produto()
        {
            StringBuilder json = new StringBuilder();
            json.Append("[{\"entity_id\":\"199\",");
            json.Append("\"sku\":\"L2092 \",");
            json.Append("\"name\":\"RLC - 50S CHEVY TRUCK \",");
            json.Append("\"meta_title\":\"\",");
            json.Append("\"meta_description\":\"\",");
            json.Append("\"gift_message_available\":\"\",");
            json.Append("\"price\":\"12.0000\",");
            json.Append("\"special_price\":null,");
            json.Append("\"cost\":null,");
            json.Append("\"status\":\"1\",");
            json.Append("\"visibility\":\"4\",");
            json.Append("\"is_imported\":\"0\",");
            json.Append("\"volume_comprimento\":null,");
            json.Append("\"volume_altura\":null,");
            json.Append("\"volume_largura\":null,");
            json.Append("\"description\":\"RLC - 50S CHEVY TRUCK \",");
            json.Append("\"short_description\":\" \",");
            json.Append("\"meta_keyword\":\"\",");
            json.Append("\"special_from_date\":null,");
            json.Append("\"special_to_date\":null,");
            json.Append("\"news_from_date\":null,");
            json.Append("\"news_to_date\":null,");
            json.Append("\"custom_design_from\":null,");
            json.Append("\"custom_design_to\":null,");
            json.Append("\"media_gallery\":{\"images\":[],\"values\":[]},");
            json.Append("\"tier_price\":[],");
            json.Append("\"stock_item\":{\"item_id\":\"20\",\"product_id\":\"199\",\"stock_id\":\"1\",\"qty\":\"155900.0000\",\"min_qty\":\"0.0000\",\"use_config_min_qty\":\"1\",\"is_qty_decimal\":\"0\",\"backorders\":\"0\",\"use_config_backorders\":\"1\",\"min_sale_qty\":\"1.0000\",\"use_config_min_sale_qty\":\"1\",\"max_sale_qty\":\"0.0000\",\"use_config_max_sale_qty\":\"1\",\"is_in_stock\":\"1\",\"low_stock_date\":null,\"notify_stock_qty\":null,\"use_config_notify_stock_qty\":\"1\",\"manage_stock\":\"0\",\"use_config_manage_stock\":\"1\",\"stock_status_changed_automatically\":\"0\",\"use_config_qty_increments\":\"1\",\"qty_increments\":\"0.0000\",\"use_config_enable_qty_increments\":\"1\",\"enable_qty_increments\":\"0\",\"type_id\":\"simple\"},");
            json.Append("\"is_salable\":\"1\"");
            json.Append("}]");

            IList<Product> actual = Deserializador.Deserializar<Product>(json.ToString());
            Assert.AreEqual(1, actual.Count);
        }
    }
}
