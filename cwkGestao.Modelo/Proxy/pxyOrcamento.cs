using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyOrcamento
    {
        public pxyOrcamento(DateTime dt, int codigo,int numero, string cliente, decimal desconto, decimal totalopedido, string tipopedido, string vendedor, int status, string notafiscal)
        {
            Dt = dt;
            Codigo = codigo;
            Numero = numero;
            Cliente = cliente;
            Desconto = desconto;
            TotalPedido = totalopedido;
            Vendedor = vendedor;
            Status = status;
            NotaFiscal = notafiscal;
        }

        public DateTime Dt { get; set; }
        public int Codigo { get; set; }
        public int Numero { get; set; }
        public string Cliente { get; set; }
        public decimal Desconto { get; set; }
        public decimal TotalPedido { get; set; }
        public string TipoPedido { get; set; }
        public string Vendedor { get; set; }
        public int Status { get; set; }
        public string NotaFiscal { get; set; }
    }
}
