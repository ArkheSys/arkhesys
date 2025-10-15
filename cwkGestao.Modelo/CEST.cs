using System;
using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("CEST")]
    public class CEST : ModeloBase
    {
        public CEST()
        {
            // Constructor logic (if any) goes here
        }
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 10)]
        public virtual string Codigo { get; set; }

        [InformacaoExibicao(1, "Segmento", 2)]
        public virtual string Segmento { get; set; }

        [InformacaoExibicao(1, "NCM", 10)]
        public virtual string NCM { get; set; }

        [InformacaoExibicao(2, "Descrição", 300)]
        public virtual string Descricao { get; set; }

        public virtual IList<NCM> NCMs { get; set; } = new List<NCM>();

        public virtual IList<NCMCEST> NCMCESTs { get; set; }

        public override string ToString()
        {
            return $"{ID} - {Codigo} - {Descricao}";
        }
    }
}