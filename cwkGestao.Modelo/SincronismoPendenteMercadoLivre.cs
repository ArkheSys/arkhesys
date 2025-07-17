using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class SincronismoPendenteMercadoLivre : ModeloBase
	{
        public override int ID { get; set; }
        public virtual bool Selecionado { get; set; }
		public virtual Int32 PedidoMercadoLivre { get; set; }
		public virtual string Erro { get; set; }
		public virtual Pessoa IdCliente { get; set; }

    }
}
