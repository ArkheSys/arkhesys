using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_NF_02_NfVendaConsumidor : Bloco
    {
        public Reg_C380 NFC { get; set; }
        public IList<Reg_C381> DetConsolidacaoPisPasep { get; set; }
        public IList<Reg_C385> DetConsolidacaoCofins { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
