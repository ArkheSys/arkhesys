using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyComissaoVendedorRel : pxyBase
    {
        public String Vendedor { get; set; }
        public String Produto { get; set; }
        public String Cliente { get; set; }
        public DateTime Data { get; set; }
        public Decimal Valor { get; set; }
        public Decimal? ComissaoPct { get; set; }
        public Decimal? ComissaoValor { get; set; }
        public Decimal? ComissaoVendedor { get; set; }
        public Int32? Nota { get; set; }
        public Decimal Quantidade { get; set; }
        public Decimal ValorDocumento { get; set; }
        public Int32 DocFin { get; set; }
        public String Parcela { get; set; }
        public Decimal? DescGeral { get; set; }
        public Decimal? DescUnitario { get; set; }
        public DateTime? Hora { get; set; }
        public Decimal? ValorTotalNota { get; set; }
        public Decimal ComissaoPedido { get; set; }
        public Decimal PercPedido { get; set; }
    }
}
