using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FilaDav : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pedido Pedido { get; set; }



    }
}
