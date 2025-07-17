using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyRequisicaoProdutoStatus
    {
        public int CodigoPedido { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotal { get; set; }
        public string Cliente { get; set; }
        public string Projeto { get; set; }
        public string Estado { get; set; }
        public decimal QuantidadeEntregue { get; set; }
        public decimal QuantidadeNaoEntregue { get; set; }
        public decimal Quantidade { get; set; }
        public object Fracao { get; set; }
        public string Produto { get; set; }
        public string CodigoProduto { get; set; }
        public string Unidade { get; set; }
        public string Status { get; set; }
    }
}
