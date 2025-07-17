using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class SisECommerceConfiguracao : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Filial Filial { get; set; }
		public virtual TabelaPreco TabelaPreco { get; set; }
		public virtual Condicao Condicao { get; set; }
		public virtual Pessoa Vendedor { get; set; }
        public virtual TipoPedido TipoOrcamento { get; set; }
		public virtual string DirSincronia { get; set; }
		public virtual string DirPosSincronia { get; set; }

    }
}
