using System;
using System.Collections.Generic;

namespace cwkGestao.Integracao.Ecommerce.Entidades
{
    public class ProdutoApi
    {
        public string name { get; set; }
        public string regular_price { get; set; }
        
        public string sale_price { get; set; }
        public string date_on_sale_from { get; set; }
        public string date_on_sale_to { get; set; }

        public string stock_quantity { get; set; }
        public bool in_stock { get; set; }
        public string weight { get; set; }
        public DimensionProdutoApi dimensions { get; set; }
        public string sku { get; set; }
        public List<CategoryProdutoApi> categories { get; set; }
        public bool featured { get; set; }
        public List<ProdutoImagemApi> images { get; set; }
        public string short_description { get; set; }
        public List<MetaDataApi> meta_data { get; set; }
    }

    public class MetaDataApi
    {
        public string key { get; set; }
        public object value { get; set; }

    }

    public class ProdutoImagemApi
    {
        public string src { get; set; }
        public int position { get; set; }
        // Novos campos para envio do Produto
        public string date_created { get; set; }
        public string date_created_gmt { get; set; }
        public string date_modified { get; set; }
        public string date_modified_gmt { get; set; }
        public string name { get; set; }
        public string alt { get; set; }
    }

    public class ProdutoResponse
    {
        public string code { get; set; }
        public string message { get; set; }
    }
}
