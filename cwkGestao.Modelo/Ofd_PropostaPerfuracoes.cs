using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Perfuração")]
    public class Ofd_PropostaPerfuracoes : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Ofd_PropostaRevisaoPerfuracoes PropostaRevisao { get; set; }
        [InformacaoExibicao(0, "Seq", 80)]
        public virtual int Sequencia { get; set; }
        [InformacaoExibicao(1, "Estaca", 80)]
        public virtual string Estaca { get; set; }
        public virtual TipoDiametro Diametro { get; set; }
        [InformacaoExibicao(2, "Qtd", 100)]
        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(4, "Profundidade", 90)]
        public virtual decimal Profundidade { get; set; }
        [InformacaoExibicao(3, "Valor Uni.", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorUnitario { get; set; }
        [InformacaoExibicao(5, "Total", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Total { get { return Quantidade * Profundidade; } }

        [InformacaoExibicao(1, "Diâmetro", 80)]
        public virtual string DiametroStr
        {
            get
            {
                return Diametro.TipoDiametroToString();
            }
        }
    }
}
