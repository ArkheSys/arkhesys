using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    [InformacaoExibicao("Diário de Obra")]
    public class Ofd_ObraDiario : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Ofd_Obra Obra { get; set; }
        [InformacaoExibicao(0, "Sequência", 150)]
		public virtual int Sequencia { get; set; }
        [InformacaoExibicao(1, "Descritivo", 400)]
		public virtual string Descritivo { get; set; }

    }
}
