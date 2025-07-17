using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CotacaoProdutoPedidoItem : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual CotacaoProduto CotacaoProduto { get; set; }
        public virtual PedidoItem PedidoItem { get; set; }
        public virtual decimal Quantidade { get; set; }
    }
}
