using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Fatura Ligação")]
    public class Tel_FaturaLigacao : ModeloBase
	{
        public Tel_FaturaLigacao()
        {
        }

        public override int ID { get; set; }
        [InformacaoExibicao(1, "Sequencia", 85)]
        public virtual int Sequencia { get; set; }
        [InformacaoExibicao(2, "Quantidade", 85)]
        public virtual int Quantidade { get; set; }
        [InformacaoExibicao(3, "Valor do Minuto", 85, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorMinuto { get; set; }
        [InformacaoExibicao(4, "Total", 85, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Total { get; set; }
        public virtual Tel_Ligacao Ligacao { get; set; }
        public virtual Tel_Fatura Fatura { get; set; }
        public virtual Tel_TipoLigacao TipoLigacao { get; set; }
    }
}
		
