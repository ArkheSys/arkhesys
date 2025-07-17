using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.SISeCommerce
{
    public class PedidoItemSISeCText
    {
        public int CODIGO_PRODUTO { get; set; }
        public string PRODUTO { get; set; }
        public decimal PRECO_UNITARIO { get; set; }
        public decimal QUANTIDADE { get; set; }
        public decimal PRECO_TOTAL { get; set; }
        public decimal PESO_EM_KG { get; set; }
        public string OPCOES_PRODUTO { get; set; }
        public string COD_REFERENCIA { get; set; }

        public PedidoItemSISeCText()
        {
        }
    }
}
