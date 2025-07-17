using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyPedidoItemTroca
    {
        public pxyPedidoItemTroca() 
        {
        }

        public pxyPedidoItemTroca(int idPedidoItem, string codProduto, string descricao, decimal valor, decimal qtdCompra)
        {
            IDPedidoItem = idPedidoItem;
            CodProduto = codProduto;
            Descricao = descricao;
            Valor = valor;
            QtdCompra = qtdCompra;
        }

        public virtual int IDPedidoItem { get; set; }
        public virtual string CodProduto { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual decimal QtdCompra { get; set; }
    }
}
