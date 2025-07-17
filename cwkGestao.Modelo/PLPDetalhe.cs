using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class PLPDetalhe : ModeloBase
	{
        public override int ID { get; set; }
		public virtual PLP PLP { get; set; }
		public virtual Pedido Pedido { get; set; }
        public PLPDetalhe()
        {
        }
    }
}
