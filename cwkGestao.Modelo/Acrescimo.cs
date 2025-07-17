using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Acréscimo")]
	public class Acrescimo : ModeloBase
	{
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Nome", 100)]
        public virtual string Nome { get; set; }
        public virtual decimal PercMulta { get; set; }
        public virtual int DiasMulta { get; set; }
        public virtual decimal PercJuro { get; set; }
        public virtual int DiasJuro { get; set; }
        public virtual bool JurosMulta { get; set; }
        public virtual int DiasProtesto { get; set; }
        public virtual string InstrucaoCaixa1 { get; set; }
        public virtual string InstrucaoCaixa2 { get; set; }
        public virtual string InstrucaoCaixa3 { get; set; }
        public virtual string InstrucaoCaixa4 { get; set; }
        public virtual string InstrucaoCaixa5 { get; set; }
        public virtual string InstrucaoCaixa6 { get; set; }
        public virtual string InstrucaoCaixa7 { get; set; }
        public virtual int TipoJuro { get; set; }
        public virtual decimal VlrJuro { get; set; }
    }
}
		