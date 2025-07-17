using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class PessoaContabilista : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pessoa Pessoa { get; set; }
		public virtual string CRC { get; set; }
		public virtual string CNPJEscritorio { get; set; }

    }
}
