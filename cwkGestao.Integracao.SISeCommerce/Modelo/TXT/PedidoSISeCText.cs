using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.SISeCommerce
{
    public class PedidoSISeCText
    {
        public Int32 PEDIDO { get; set; }
        public DateTime DATA { get; set; }
        public string LOCAL { get; set; }
        public string STATUS { get; set; }
        public Int32 CLIENTE_COD { get; set; }
        public string CLIENTE_NOME { get; set; }
        public string CLIENTE_TIPO { get; set; }
        public string CLIENTE_CPF_CNPJ { get; set; }
        public string CLIENTE_RG_IE { get; set; }
        public string CLIENTE_RAZAO_SOCIAL { get; set; }
        public string CLIENTE_ENDERECO { get; set; }
        public string CLIENTE_COMPLEMENTO { get; set; }
        public string CLIENTE_BAIRRO { get; set; }
        public string CLIENTE_CIDADE { get; set; }
        public string CLIENTE_UF { get; set; }
        public string CLIENTE_CEP { get; set; }
        public string CLIENTE_EMAIL { get; set; }
        public string CLIENTE_TELEFONE { get; set; }
        public string CLIENTE_CAMPO_LIVRE { get; set; }
        public Int32 PEDIDO_NUM_PRODUTOS { get; set; }
        public decimal PEDIDO_SUBTOTAL { get; set; }
        public decimal PEDIDO_FRETE { get; set; }
        public string PEDIDO_FRETE_TIPO { get; set; }
        public DateTime PEDIDO_ENVIO_DATA { get; set; }
        public string PEDIDO_ENVIO_CODIGO { get; set; }
        public decimal PEDIDO_ENVIO_VALOR_REAL { get; set; }
        public decimal PEDIDO_PESO { get; set; }
        public string PEDIDO_OBS_CLIENTE { get; set; }
        public string PEDIDO_CAMPO_LIVRE { get; set; }
        public decimal PEDIDO_VALOR_PRODUTOS { get; set; }
        public string PEDIDO_DA { get; set; }
        public decimal PEDIDO_DA_VALOR { get; set; }
        public decimal PEDIDO_DA_PAGAMENTO { get; set; }
        public string PEDIDO_FORMA_PAGAMENTO { get; set; }
        public DateTime PEDIDO_DATA_ENVIO { get; set; }
        public string PEDIDO_COD_RASTREAMENTO { get; set; }
        public string PEDIDO_INFO_AD_CLIENTE { get; set; }
        public string PEDIDO_INFO_AD_LOJA { get; set; }
        public string PEDIDO_APELIDO { get; set; }
        public IList<PedidoItemSISeCText> PEDIDO_ITENS { get; set; }

        public PedidoSISeCText()
        {
        }
    }
}
