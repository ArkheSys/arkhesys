using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyRequisicao
    {
        public int CodigoPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }
        public string Cliente { get; set; }
        public string Projeto { get; set; }
        public string Estado { get; set; }
        public string TipoPedido { get; set; }
    }
}
