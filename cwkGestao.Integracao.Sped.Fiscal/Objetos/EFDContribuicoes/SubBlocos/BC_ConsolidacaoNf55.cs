using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_ConsolidacaoNf55 : Bloco
    {
        public Reg_C180 ConsolidacaoNfVendas { get; set; }
        public IList<Reg_C181> DetPisPasepVendas { get; set; }
        public IList<Reg_C185> DetCofinsVendas { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
