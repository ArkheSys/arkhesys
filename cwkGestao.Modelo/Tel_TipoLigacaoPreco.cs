using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo Ligação Preço")]
    public class Tel_TipoLigacaoPreco : Tel_PrecoBase
    {
        public override int ID { get; set; }
        public override Tel_TipoLigacao TipoLigacao { get; set; }

        [InformacaoExibicao(2, "Área Inicial", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public override short Area_Inicial { get; set; }

        [InformacaoExibicao(3, "Área Final", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public override short Area_Final { get; set; }

        [InformacaoExibicao(4, "Prefixo Inicial", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public override int Prefixo_Inicial { get; set; }

        [InformacaoExibicao(5, "Prefixo Final", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public override int Prefixo_Final { get; set; }

        [InformacaoExibicao(6, "Range Inicial", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public override int Range_Inicial { get; set; }

        [InformacaoExibicao(7, "Range Final", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public override int Range_Final { get; set; }

        public override bool BDentro { get; set; }
        public override bool BFora { get; set; }

        [InformacaoExibicao(1, "Valor", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public override decimal Valor { get; set; }
    }
}
