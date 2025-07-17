using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class Imposto : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string Nome { get; set; }
        public virtual IList<CodigoImposto> CSTs { get; set; }

        public override string ToString()
        {
            return ID + " | " + Nome;
        }
    }
}
