using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace cwkGestao.Integracao.SISeCommerce.Modelo
{
    [Serializable]
    public class pedido
    {
        [XmlIgnore()]
        public DateTime DtExportacao { get; set; }
        [XmlElement(ElementName = "data_hora_exportacao_xml")]
        public string XmlTime
        {
            get { return DtExportacao.ToString(); }
            set { DtExportacao = ConversorData((string)value); }
        }

        [XmlIgnore()]
        public int CodigoPedido { get; set; }
        [XmlElement(ElementName = "pedido_codigo")]
        public string XmlInt
        {
            get { return CodigoPedido.ToString(); }
            set { CodigoPedido = Convert.ToInt32(value); }
        }

        [XmlElement(ElementName = "pedido_status_codigo")]
        public string StatusCodigo { get; set; }

        [XmlElement(ElementName = "pedido_status")]
        public string PedidoStatus { get; set; }

        [XmlIgnore()]
        public DateTime Data { get; set; }
        [XmlElement(ElementName = "pedido_data_hora")]
        public string XmlTime2
        {
            get { return Data.ToString(); }
            set { Data = ConversorData((string)value); }
        }

        [XmlElement(ElementName = "pedido_canal_venda_codigo")]
        public string CodigoCanalVenda { get; set; }

        [XmlElement(ElementName = "cliente_codigo")]
        public string ClienteCodigo { get; set; }

        [XmlElement(ElementName = "cliente_email")]
        public string ClienteEmail { get; set; }

        [XmlElement(ElementName = "cliente_nome")]
        public string ClienteNome { get; set; }

        [XmlElement(ElementName = "cliente_endereco")]
        public string ClienteEndereco { get; set; }

        [XmlElement(ElementName = "cliente_numero")]
        public string ClienteEnderecoNumero { get; set; }

        [XmlElement(ElementName = "cliente_bairro")]
        public string ClienteBairro { get; set; }

        [XmlElement(ElementName = "cliente_cidade")]
        public string ClienteCidade { get; set; }

        [XmlElement(ElementName = "cliente_estado")]
        public string ClienteUF { get; set; }

        [XmlElement(ElementName = "cliente_cep")]
        public string ClienteCep { get; set; }

        [XmlElement(ElementName = "cliente_telefone_1")]
        public string ClienteTel1 { get; set; }

        [XmlElement(ElementName = "cliente_telefone_2")]
        public string ClienteTel2 { get; set; }

        [XmlElement(ElementName = "cliente_apelido")]
        public string ClienteApelido { get; set; }

        [XmlElement(ElementName = "cliente_tipo")]
        public string ClienteTipo { get; set; }

        [XmlElement(ElementName = "cliente_tipo_documento")]
        public string ClienteTipoDocumento { get; set; }

        [XmlElement(ElementName = "cliente_documento")]
        public string ClienteDocumento { get; set; }

        [XmlElement(ElementName = "cliente_ie_rg")]
        public string ClienteIeRg { get; set; }

        [XmlElement(ElementName = "cliente_ie_rg_documento")]
        public string ClienteIeRgDocumento { get; set; }

        [XmlElement(ElementName = "pedido_frete_codigo")]
        public string CodigoFrete { get; set; }

        [XmlElement(ElementName = "pedido_frete_forma")]
        public string FormaFrete { get; set; }

        [XmlIgnore()]
        public decimal ValorFrete { get; set; }
        [XmlElement(ElementName = "pedido_frete_valor")]
        public string XmlDecimal1
        {
            get { return ValorFrete.ToString(); }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    ValorFrete = 0;
                }
                else
                {
                    ValorFrete = Convert.ToDecimal(value);
                }
                }
        }

        [XmlIgnore()]
        public decimal ValorFreteReal { get; set; }
        [XmlElement(ElementName = "pedido_frete_valor_real")]
        public string XmlDecimal2
        {
            get { return ValorFreteReal.ToString(); }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    ValorFreteReal = 0;
                }
                else
                {
                    ValorFreteReal = Convert.ToDecimal(value);
                }    
            }
        }

        [XmlIgnore()]
        public decimal PesoFrete { get; set; }
        [XmlElement(ElementName = "pedido_frete_peso")]
        public string XmlDecimal3
        {
            get { return PesoFrete.ToString(); }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    PesoFrete = 0;
                }
                else
                {
                    PesoFrete = Convert.ToDecimal(value);
                }
                }
        
        }

        [XmlIgnore()]
        public decimal PesoFreteCubico { get; set; }
        [XmlElement(ElementName = "pedido_frete_peso_cubico")]
        public string XmlDecimal4
        {
            get { return PesoFreteCubico.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    PesoFreteCubico = 0;
                }
                else
                {
                    PesoFreteCubico = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public DateTime DtEnvioFrete { get; set; }
        [XmlElement(ElementName = "pedido_frete_data_envio")]
        public string XmlTime3
        {
            get { return DtEnvioFrete.ToString(); }
            set { DtEnvioFrete = ConversorData((string)value); }
        }

        [XmlElement(ElementName = "pedido_frete_codigo_rastreamento")]
        public string CodigoRastreamento { get; set; }

        [XmlElement(ElementName = "pedido_pagamento_codigo")]
        public string CodigoPagamento { get; set; }

        [XmlElement(ElementName = "pedido_pagamento_forma")]
        public string FormaPagamento { get; set; }

        [XmlIgnore()]
        public decimal ValorPagamento { get; set; }
        [XmlElement(ElementName = "pedido_pagamento_valor")]
        public string XmlDecimal5
        {
            get { return ValorPagamento.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    ValorPagamento = 0;
                }
                else
                {
                    ValorPagamento = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal TotalProduto { get; set; }
        [XmlElement(ElementName = "pedido_total_produto")]
        public string XmlDecimal6
        {
            get { return TotalProduto.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    TotalProduto = 0;
                }
                else
                {
                    TotalProduto = Convert.ToDecimal(value);
                }
            }
        }

        [XmlElement(ElementName = "pedido_total_desconto_acrescimo")]
        public string TotalDescontoAcrescimo { get; set; }

        [XmlIgnore()]
        public decimal TotalDescontoAcrescimoValor { get; set; }
        [XmlElement(ElementName = "pedido_total_desconto_acrescimo_valor")]
        public string XmlDecimal7
        {
            get { return TotalDescontoAcrescimoValor.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    TotalDescontoAcrescimoValor = 0;
                }
                else
                {
                    TotalDescontoAcrescimoValor = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal TotalFrete { get; set; }
        [XmlElement(ElementName = "pedido_total_frete")]
        public string XmlDecimal8
        {
            get { return TotalFrete.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    TotalFrete = 0;
                }
                else
                {
                    TotalFrete = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal TotalAcrescimoAdicional { get; set; }
        [XmlElement(ElementName = "pedido_total_acrescimo_adicional_valor")]
        public string XmlDecimal9
        {
            get { return TotalAcrescimoAdicional.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    TotalAcrescimoAdicional = 0;
                }
                else
                {
                    TotalAcrescimoAdicional = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal SubTotal { get; set; }
        [XmlElement(ElementName = "pedido_total_sub_total")]
        public string XmlDecimal10
        {
            get { return SubTotal.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    SubTotal = 0;
                }
                else
                {
                    SubTotal = Convert.ToDecimal(value);
                }
            }
        }

        [XmlElement(ElementName = "pedido_info_obs_cliente")]
        public string ObsCliente { get; set; }

        [XmlElement(ElementName = "pedido_info_obs_loja")]
        public string ObsLoja { get; set; }

        [XmlElement(ElementName = "pedido_info_adicional_2")]
        public string InfoAdicional2 { get; set; }

        [XmlElement(ElementName = "pedido_info_parceiro_codigo")]
        public string CodigoParceiro { get; set; }

        [XmlElement(ElementName = "pedido_info_quantidade_produtos")]
        public string QtdProdutos { get; set; }

        [XmlElement(ElementName = "pedido_info_quantidade_itens")]
        public string QtdItens { get; set; }

        [XmlElement(ElementName = "cliente_complemento")]
        public string ClienteComplemento { get; set; }

        [XmlElement(ElementName = "pedido_info_adicional_1")]
        public string InfoAdicional1 { get; set; }

        [XmlElement(ElementName = "pedido_info_cupom_codigo")]
        public string CodigoCupom { get; set; }

        [XmlElement(ElementName = "pedido_parcelas_cartao")]
        public string ParcelasCartao { get; set; }

        [XmlElement(ElementName = "produtos")]
        public produtos PedidoItens { get; set; }

        public pedido()
        {
        }

        private DateTime ConversorData(string data)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("pt-BR", true);
            string date = (string)data;
            if (!String.IsNullOrEmpty(date))
            {
                DateTime valor = DateTime.Parse(date, culture,
                    System.Globalization.DateTimeStyles.AssumeLocal);
                return valor;
            }
            return DateTime.Now;
        }
    }

    [Serializable]
    public class produtos
    {
        [XmlElement(ElementName = "produto")]
        public List<produto> Produtos { get; set; }

        public produtos()
        {
        }
    }
}
