using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class Ofd_PropostaOrcamentoServicos : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Servico Servico { get; set; }
        public virtual Ofd_Proposta Ofd_Proposta { get; set; }

    }
}
