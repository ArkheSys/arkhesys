using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class NotaItemPedidoItem : ModeloBase
    {
        public NotaItemPedidoItem()
        {
        }

        public NotaItemPedidoItem(NotaItem notaitem, PedidoItem pedidoitem)
        {
            NotaItem = notaitem;
            PedidoItem = pedidoitem;
        }

        public override int ID { get; set; }
        public virtual NotaItem NotaItem { get; set; }
        public virtual PedidoItem PedidoItem { get; set; }
    }
}
