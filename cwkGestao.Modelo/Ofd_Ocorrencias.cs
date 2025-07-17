using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Ocorr�ncias")]
    public class Ofd_Ocorrencias : ModeloBase
	{
        [InformacaoExibicao(0, "C�digo", 90)]
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Descri��o", 200)]
		public virtual string Descricao { get; set; }

    }
}
