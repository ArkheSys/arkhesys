using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_Consolidacao_NF_Servicos : Bloco
    {
        public Reg_C600 ConsolidacaoNfSvcs { get; set; }
        public IList<Reg_C601> CompConsolidacaoSaidasPisPasep { get; set; }
        public IList<Reg_C605> CompConsolidacaoSaidasCofins { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
