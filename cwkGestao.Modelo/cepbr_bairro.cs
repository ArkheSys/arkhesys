using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class cepbr_bairro : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string bairro { get; set; }
		public virtual cepbr_cidade id_cidade { get; set; }

    }
}
