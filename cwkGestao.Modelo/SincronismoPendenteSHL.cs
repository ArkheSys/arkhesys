using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class SincronismoPendenteSHL : ModeloBase
	{
        public virtual bool Selecionado { get; set; }
        public override int ID { get; set; }
		public virtual string Erro { get; set; }
		public virtual string TipoErro { get; set; }
        public virtual string Chave { get; set; }
        public virtual int CodigoErro { get; set; }
		public virtual int InterfaceSHL { get; set; }
        public virtual Pedido Pedido { get; set; }
		public virtual Nota Nota { get; set; }

    }
}
