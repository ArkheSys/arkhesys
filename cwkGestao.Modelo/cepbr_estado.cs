using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class cepbr_estado : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string uf { get; set; }
		public virtual string estado { get; set; }
		public virtual string cod_ibge { get; set; }

    }
}
