using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    [InformacaoExibicao("Status de Atendimento")]
    public class StatusAtendimento : ModeloBase
	{
        public override int ID { get; set; }
        [InformacaoExibicao(1,"C�digo", 100)]
		public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Descri��o", 250)]
		public virtual string Descricao { get; set; }

        public override string ToString()
        {
            return Descricao;
        }

    }
}
