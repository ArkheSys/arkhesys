using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class Contato : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pessoa Pessoa { get; set; }
		public virtual string NomeContato { get; set; }
        public virtual Cw_usuario Atendente { get; set; }

        public virtual string FoneContato { get; set; }
        public virtual IList<ContatoDetalhe> ContatoDetalhes { get; set; }

    }
}
