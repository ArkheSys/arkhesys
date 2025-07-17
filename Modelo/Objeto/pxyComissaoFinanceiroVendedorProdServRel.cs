using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyComissaoFinanceiroVendedorProdServRel : pxyBase
    {
        public String Cliente { get; set; }
        public Decimal? ComissaoPct { get; set; }
        public Decimal? ComissaoValor { get; set; }
        public DateTime? Data { get; set; }
        public String DocFin { get; set; }
        public Int32 IDDocumento { get; set; }
        public Int32 IDNota { get; set; }
        public Int32? Nota { get; set; }
        public String Parcela { get; set; }
        public String TipoComissao { get; set; }
        public Decimal? TotalNota { get; set; }
        public Decimal Valor { get; set; }
        public Decimal ValorDocumento { get; set; }
        public String Vendedor { get; set; }



    }
}
