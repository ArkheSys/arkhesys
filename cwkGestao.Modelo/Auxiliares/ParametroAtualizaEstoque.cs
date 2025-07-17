using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Auxiliares
{
    public struct ParametroEstoque
    {
        public Modelo.LocalEstoque localEstoque;
        public Modelo.Produto produto;
        public decimal? qtdDescontar_Fisico;
        public decimal? qtdAcrescentar_Fisico;

        public decimal? qtdEstoque_Pedido;
        public decimal? qtdCompra_Pedido;

        public decimal? custoUnitarioNotaItem { get; set; }

        public static ParametroEstoque GetNovoParametro(LocalEstoque localEstoque, Produto produto)
        {
            return new ParametroEstoque()
            {
                localEstoque = localEstoque,
                produto = produto
            };
        }
    }
}
