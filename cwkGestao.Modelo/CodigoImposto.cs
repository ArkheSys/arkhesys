using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class CodigoImposto : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string CodCST { get; set; }
		public virtual string Descricao { get; set; }
		public virtual Imposto Imposto { get; set; }

        public override string ToString()
        {
            return Imposto.Nome + " - " + CodCST + " | " + Descricao;
        }

    }
}
