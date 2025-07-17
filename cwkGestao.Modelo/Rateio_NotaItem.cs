using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Rateio Projeto")]
    public class Rateio_NotaItem : ModeloBase
    {
        public override int ID { get; set; }
        public virtual NotaItem NotaItem { get; set; }
        [InformacaoExibicao(0, "Projeto", 75, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual Projeto Projeto { get; set; }
        [InformacaoExibicao(1, "Valor",75, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal Valor { get; set; }        
        public virtual int Sequencia { get; set; }
    }
}
