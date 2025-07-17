using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace cwkGestao.Integracao.SISeCommerce.Modelo
{
    [Serializable]
    public class produto
    {
        [XmlIgnore()]
        public int Codigo { get; set; }
        [XmlElement(ElementName = "produto_codigo")]
        public string XmlInt
        {
            get { return Codigo.ToString(); }
            set { Codigo = Convert.ToInt32(value); }
        }

        [XmlElement(ElementName = "produto_nome")]
        public string Nome { get; set; }

        [XmlIgnore()]
        public decimal Quantidade { get; set; }
        [XmlElement(ElementName = "produto_quantidade")]
        public string XmlDecimal
        {
            get { return Quantidade.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Quantidade = 0;
                }
                else
                {
                    Quantidade = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal Preco { get; set; }
        [XmlElement(ElementName = "produto_preco")]
        public string XmlDecimal3
        {
            get { return Preco.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Preco = 0;
                }
                else
                {
                    Preco = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal PrecoCusto { get; set; }
        [XmlElement(ElementName = "produto_preco_custo")]
        public string XmlDecimal1
        {
            get { return PrecoCusto.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    PrecoCusto = 0;
                }
                else
                {
                    PrecoCusto = Convert.ToDecimal(value);
                }
            }
        }

        [XmlIgnore()]
        public decimal PrecoTotal { get; set; }
        [XmlElement(ElementName = "produto_preco_total")]
        public string XmlDecimal2
        {
            get { return PrecoTotal.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    PrecoTotal = 0;
                }
                else
                {
                    PrecoTotal = Convert.ToDecimal(value);
                }
            }
        }

        [XmlElement(ElementName = "produto_codigo_variacao")]
        public string CodigoVariacao { get; set; }

        [XmlElement(ElementName = "produto_referencia")]
        public string CodigoReferencia { get; set; }

        [XmlElement(ElementName = "produto_variacao_informacao")]
        public string InfoVariacao { get; set; }

        public produto()
        {
        }
    }
}
