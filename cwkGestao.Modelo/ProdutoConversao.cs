using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ProdutoConversao : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Produto Produto { get; set; }
		public virtual ConversaoUnidade ConversaoUnidade { get; set; }

    }
}
