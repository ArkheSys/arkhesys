using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("DadosSubstituto")]
    public class DadosSubstituto : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string UF_ST { get; set; }
		public virtual string IE_ST { get; set; }
		public virtual Filial Filial { get; set; }

    }
}
