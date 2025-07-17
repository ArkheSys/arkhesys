using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEstoqueInventario
    {
        public pxyEstoqueInventario(int idProduto, decimal valor, decimal quantidade)
        {
            IDProduto = idProduto;
            Valor = valor;
            Quantidade = quantidade;
        }

        public int IDProduto { get; set; }
        public decimal Valor { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Total { get { return Valor * Quantidade; } }
    }
}
