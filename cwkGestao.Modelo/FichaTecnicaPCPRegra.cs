using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FichaTecnicaPCPRegra : ModeloBase
	{
        public override int ID { get; set; }
		public virtual int Codigo { get; set; }
		public virtual string Descricao { get; set; }
		public virtual FichaTecnicaPCP FichaTecnicaPCP { get; set; }

        public FichaTecnicaPCPRegra()
        {
        }

    }
}
