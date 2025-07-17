using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class cepbr_cidade : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string cidade { get; set; }
		public virtual string uf { get; set; }
		public virtual string cod_ibge { get; set; }
		public virtual float area { get; set; }
		public virtual Cidade CidadeCwork { get; set; }

    }
}
