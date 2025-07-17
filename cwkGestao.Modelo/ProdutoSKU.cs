using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Produtos SKU")]
    public class ProdutoSKU : ModeloBase
	{
        public ProdutoSKU()
        {
        }

        public ProdutoSKU(int id, Produto produto, string sku)
        {
            ID = id;
            Produto = produto;
            SKU = sku;
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Produto", 300)]
		public virtual Produto Produto { get; set; }

        [InformacaoExibicao(0, "SKU", 100)]
		public virtual string SKU { get; set; }

    }
}
