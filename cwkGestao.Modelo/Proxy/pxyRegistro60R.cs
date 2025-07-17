using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRegistro60R
    {
        public pxyRegistro60R(string tipo, string subTipo, string referencia, string codigoProduto, string quantidade, string valorProduto, string baseCalculoIcms, string aliquitaProduto, string brancos)
        {
            Tipo                = tipo;         
            SubTipo             = subTipo;
            Referencia          = referencia;
            CodigoProduto       = codigoProduto;
            Quantidade          = quantidade;
            ValorProduto        = valorProduto;
            BaseCalculoIcms     = baseCalculoIcms;
            AliquitaProduto     = aliquitaProduto;
            Brancos             = brancos;
        }

        public string Tipo { get; set; }
        public string SubTipo { get; set; }
        public string Referencia { get; set; }
        public string CodigoProduto { get; set; }
        public string Quantidade { get; set; }
        public string ValorProduto { get; set; }
        public string BaseCalculoIcms { get; set; }
        public string AliquitaProduto { get; set; }
        public string Brancos { get; set; }
    }
}
