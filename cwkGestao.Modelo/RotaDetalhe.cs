using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class RotaDetalhe : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Rota Rota { get; set; }
        [InformacaoExibicao(1, "C�digo", 80)]
		public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Cliente", 80)]
		public virtual Pessoa Cliente { get; set; }
        [InformacaoExibicao(3, "Sequ�ncia", 50)]
		public virtual int Sequencia { get; set; }
        [InformacaoExibicao(4, "Situa��o", 50)]
		public virtual int Situacao { get; set; }
        [InformacaoExibicao(5, "Data", 80)]
		public virtual DateTime? Data { get; set; }

        [InformacaoExibicao(6, "Operador", 80)]
        public virtual Pessoa Operador { get; set; }

        [InformacaoExibicao(7, "Observa��o", 150)]
        public virtual string Observacao { get; set; }

    }
}
