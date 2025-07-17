using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class NotaItemServicoPedidoItemServico : ModeloBase
	{
        public override int ID { get; set; }
		public virtual PedidoItemServico PedidoItemServico { get; set; }
		public virtual NotaItemServico NotaItemServico { get; set; }

    }
}
