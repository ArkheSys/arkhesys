using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Partilha Simples Nacional")]
    public class TabelaFaixaFatSimples : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(0, "Código", 100)]
        public virtual int Codigo { get; set; }
        public virtual int Tipo { get; set; }
        [InformacaoExibicao(1, "Faixa Inicial", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Faixa_I { get; set; }
        [InformacaoExibicao(2, "Faixa Final", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Faixa_F { get; set; }
        [InformacaoExibicao(3, "Alíquota %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Aliquota { get; set; }
        [InformacaoExibicao(4, "IRPJ %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal IRPJ { get; set; }
        [InformacaoExibicao(5, "CSLL %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal CSLL { get; set; }
        [InformacaoExibicao(6, "COFINS %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal COFINS { get; set; }
        [InformacaoExibicao(7, "PIS %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal PIS_PASEP { get; set; }
        [InformacaoExibicao(8, "CPP %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal CPP { get; set; }
        [InformacaoExibicao(9, "ICMS %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal ICMS { get; set; }
        [InformacaoExibicao(10, "IPI %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal IPI { get; set; }
        [InformacaoExibicao(11, "ISS %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal ISS { get; set; }
        [InformacaoExibicao(11, "Red BC %", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal PercReducaoBC { get; set; }

        

    }
}

