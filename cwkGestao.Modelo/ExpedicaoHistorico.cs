using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ExpedicaoHistorico : ModeloBase
	{
        public override int ID { get; set; }
		public virtual ExpedicaoRastreamento ExpedicaoRastreamento { get; set; }
		public virtual StatusEntEventoCorreio StatusEntEventoCorreio { get; set; }
		public virtual DateTime DataEvento { get; set; }
    }
}
