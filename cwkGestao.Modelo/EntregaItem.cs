using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class EntregaItem : ModeloBase
	{      
        public override int ID { get; set; }
		public virtual int Sequencia { get; set; }
		public virtual decimal? QuantidadeEntrega { get; set; }
		public virtual decimal QuantidadeRestante { get; set; }
        public virtual decimal ValorQuantidadeEntrega { get; set; }
        public virtual decimal ValorQuantidadeRestante { get; set; }
		public virtual PedidoItem PedidoItem { get; set; }
		public virtual Entrega Entrega { get; set; }

    }
}
