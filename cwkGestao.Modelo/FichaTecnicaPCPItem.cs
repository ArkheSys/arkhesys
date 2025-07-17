using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FichaTecnicaPCPItem : ModeloBase
	{
        public override int ID { get; set; }
		public virtual decimal Quantidade { get; set; }
		public virtual decimal Aproveitamento { get; set; }
        public virtual DateTime DataVigor { get; set; }
		public virtual FichaTecnicaPCP FichaTecnicaPCP { get; set; }
		public virtual FichaTecnicaPCP FichaTecnicaPCPFilha { get; set; }
		public virtual Produto Produto { get; set; }

        public FichaTecnicaPCPItem()
        {
        }

    }
}
