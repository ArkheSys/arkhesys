using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.ModeloProxy
{
    public class pxyPedido
    {
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public DateTime? Digitacao { get; set; }
        public StatusOrcamento Status { get; set; }
        public string Nome { get; set; }
        public string Fantasia { get; set; }
        public decimal? Total { get; set; }
        public string Vendedor { get; set; }
        public string Tipo { get; set; }
        public int ID { get; set; }
        public int? Numero { get; set; }
    }
}
