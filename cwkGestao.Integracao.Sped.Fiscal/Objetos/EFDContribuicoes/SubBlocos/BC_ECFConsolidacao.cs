using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_ECFConsolidacao : Bloco
    {
        public Reg_C490 ConsolidacaoECF { get; set; }
        public IList<Reg_C491> DetConsolidacaoPisPasep { get; set; }
        public IList<Reg_C495> DetConsolidacaoCofins { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
