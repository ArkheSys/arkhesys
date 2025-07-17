using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class StatusContato : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string Descricao { get; set; }
		public virtual bool bFinalizado { get; set; }
        public virtual IList<ContatoDetalhe> ContatoDetalhes { get; set; }
    }
}
