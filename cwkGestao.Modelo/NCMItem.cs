using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Regra de Imposto")]
	public class NCMItem : IcmsBase
	{
        public override int ID { get; set; }
        public virtual NCM NCM { get; set; }
	}
}
		