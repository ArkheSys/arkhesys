using System;
using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Segmento CEST")]
    public class CEST_Segmento : ModeloBase
    {
        public CEST_Segmento()
        {
            // Constructor logic (if any) goes here
        }

        [InformacaoExibicao(1, "Código", 10)]
        public virtual string Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 300)]
        public virtual string Descricao { get; set; }

        public virtual IList<CEST> CESTs { get; set; } = new List<CEST>();
    }
}