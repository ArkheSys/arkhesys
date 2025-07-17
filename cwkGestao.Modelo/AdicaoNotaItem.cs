using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Adição")]
	public class AdicaoNotaItem : ModeloBase
	{
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Número Adição", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal I26_nAdicao { get; set; }
        [InformacaoExibicao(2, "Seq.", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal I27_nSeqAdic { get; set; }
        [InformacaoExibicao(3, "Fabricante", 100, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string I28_cFabricante { get; set; }
        [InformacaoExibicao(4, "Desconto DI", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal I29_vDescDI { get; set; }
        [InformacaoExibicao(5, "Num. Ped.", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string I30_xPed { get; set; }
        [InformacaoExibicao(6, "Seq. Ped.", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal I31_nItemPed { get; set; }

        public virtual NotaItem NotaItem { get; set; }
    }
}
		