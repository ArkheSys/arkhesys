using System;
using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("NCMCEST")]
    public class NCMCEST : ModeloBase
    {
        public override int ID { get; set; }

        public virtual string NCMCodigo { get; set; } // Campo para armazenar o texto livre (ex: "2106")

        public virtual CEST CEST { get; set; }
    }
}