using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class VersaoEfdIcmsIpi : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string CodigoSped { get; set; }
		public virtual string CodigoVersao { get; set; }
		public virtual DateTime DtInicioValidade { get; set; }
		public virtual DateTime DtFimValidade { get; set; }

    }
}
