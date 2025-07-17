using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ProdutoFCI : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Produto Produto { get; set; }
		public virtual string FCI { get; set; }
		public virtual bool bAtivo { get; set; }

        public override string ToString()
        {
            return FCI;
        }

    }
}
