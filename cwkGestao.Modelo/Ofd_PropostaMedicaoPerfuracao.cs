using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Medição Perfuração")]
    public class Ofd_PropostaMedicaoPerfuracao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Ofd_PropostaMedicao PropostaMedicao { get; set; }
        [InformacaoExibicao(0, "Seq", 70)]
        public virtual int Sequencia { get; set; }
        [InformacaoExibicao(1, "Estaca", 80)]
        public virtual string Estaca { get; set; }
        public virtual TipoDiametro Diametro { get; set; }
        [InformacaoExibicao(3, "Qtd", 90)]
        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(4, "Profundidade", 90)]
        public virtual decimal Profundidade { get; set; }
        [InformacaoExibicao(5, "Total", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Total { get { return Quantidade * Profundidade; } }

        [InformacaoExibicao(2, "Diâmetro", 80)]
        public virtual string DiametroStr
        {
            get
            {
                return Diametro.TipoDiametroToString();
            }
        }
    }
}
