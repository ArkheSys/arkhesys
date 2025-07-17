using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Ocorrências")]
    public class Ofd_Ocorrencias : ModeloBase
	{
        [InformacaoExibicao(0, "Código", 90)]
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Descrição", 200)]
		public virtual string Descricao { get; set; }

    }
}
