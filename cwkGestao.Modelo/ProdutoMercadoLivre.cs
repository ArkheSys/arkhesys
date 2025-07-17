using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ProdutoMercadoLivre : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Produto Produto { get; set; }
		public virtual Int32 MercadoLivre { get; set; }
		public virtual string Descricao { get; set; }
		public virtual bool bAtivo { get; set; }

    }
}
