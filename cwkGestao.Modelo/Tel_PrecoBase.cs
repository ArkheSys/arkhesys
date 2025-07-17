using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Tel_PrecoBase : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(7, "Tipo", 200)]
        public virtual Tel_TipoLigacao TipoLigacao { get; set; }
        [InformacaoExibicao(0, "Área Ini.", 70)]
        public virtual short Area_Inicial { get; set; }
        [InformacaoExibicao(1, "Área Fin.", 70)]
        public virtual short Area_Final { get; set; }
        [InformacaoExibicao(2, "Prefixo Ini.", 90)]
        public virtual int Prefixo_Inicial { get; set; }
        [InformacaoExibicao(3, "Prefixo Fin.", 90)]
        public virtual int Prefixo_Final { get; set; }
        [InformacaoExibicao(4, "Range Ini.", 100)]
        public virtual int Range_Inicial { get; set; }
        [InformacaoExibicao(5, "Range Fin.", 100)]
        public virtual int Range_Final { get; set; }
        public virtual bool BDentro { get; set; }
        public virtual bool BFora { get; set; }
        [InformacaoExibicao(6, "Valor", 120)]
        public virtual decimal Valor { get; set; }

        public virtual int Ordem
        {
            get
            {
                return (Area_Final - Area_Inicial)
                + (Prefixo_Final - Prefixo_Inicial)
                + (Range_Final - Range_Inicial);
            }
        }
    }
}
