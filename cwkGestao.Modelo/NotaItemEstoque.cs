using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class NotaItemEstoque : ModeloBase
	{
        public override int ID { get; set; }
		public virtual NotaItem NotaItem { get; set; }
        public virtual decimal CMVUnit { get; set; }
		public virtual decimal Quantidade { get; set; }
        public virtual Identificador Identificador { get; set; }
    }
}
