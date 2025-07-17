using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Preço")]
    public class Tel_ClientePreco : Tel_PrecoBase
	{
        public override int ID { get; set; }
        public virtual Tel_Cliente Cliente { get; set; }
    }
}
		