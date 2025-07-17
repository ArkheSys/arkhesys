using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyOrcamento : pxyBase
    {    
        public DateTime Dt { get; set; }
        public int Codigo { get; set; }
        public string Cliente { get; set; }
        public decimal Desconto { get; set; }
        public decimal TotalPedido { get; set; }
        public string TipoPedido { get; set; }
        public string Vendedor { get; set; }
        public Modelo.StatusOrcamento Status { get; set; }
        public string NotaFiscal { get; set; }
    }
}
