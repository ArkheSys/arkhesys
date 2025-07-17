using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ProdutoServico : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Produto Produto { get; set; }
		public virtual Servico Servico { get; set; }

    }
}
