using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class LogAlteracaoParticipante : ModeloBase
	{
        public override int ID { get; set; }
		public virtual DateTime DataAlteracao { get; set; }
		public virtual string NomeCampoAlterado { get; set; }
		public virtual int NumeroCampoAlterado { get; set; }
		public virtual string ConteudoAnterior { get; set; }
		public virtual Pessoa Pessoa { get; set; }

    }
}
