using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Custo")]
    public class Custo : ModeloBase
	{
        public override int ID { get; set; }

        [InformacaoExibicao(0, "Código", 80, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(1, "Descricao", 230, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual String Descricao { get; set; }

        [InformacaoExibicao(2, "Valor", 120, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual Decimal Valor { get; set; }
		public virtual Nota Nota { get; set; }
    }
}
