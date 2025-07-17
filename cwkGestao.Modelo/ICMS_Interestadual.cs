using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("ICMS Interestadual")]
    public class ICMS_Interestadual : IcmsBase, IComparable<ICMS_Interestadual>
    {
        public override int ID { get; set; }

        //[InformacaoExibicao(1, "Origem", 250)]
        public override UF UFOrigem { get; set; }

        //[InformacaoExibicao(2, "Destino", 250, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public override UF UFDestino { get; set; }

        public virtual int CompareTo(ICMS_Interestadual other)
        {
            return ID.CompareTo(other.ID);
        }
    }
}
