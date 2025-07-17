using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEntregaPedidoItem
    {
        public pxyEntregaPedidoItem(int id, string produtoNome, string unidade, decimal quantidade,
            decimal? quantidadeEntregue, decimal saldo)
        {
            ID = id;
            ProdutoNome = produtoNome;
            Unidade = unidade; 
            Quantidade = quantidade; 
            QuantidadeEntregue = quantidadeEntregue == null ? 0 : quantidadeEntregue; 
            Saldo = quantidadeEntregue == null ? (Quantidade - (Decimal)QuantidadeEntregue) : saldo;
        }

        public int ID { get; set; }
        public string ProdutoNome { get; set; }
        public string Unidade { get; set; }
        public decimal Quantidade { get; set; }
        public decimal? QuantidadeEntregue { get; set; }
        public decimal Saldo { get; set; }

      
    }
}
