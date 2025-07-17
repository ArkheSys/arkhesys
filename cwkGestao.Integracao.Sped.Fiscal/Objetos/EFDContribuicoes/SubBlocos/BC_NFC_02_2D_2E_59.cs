using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_NFC_02_2D_2E_59 : Bloco
    {
        public Reg_C395 NFC { get; set; }
        public IList<Reg_C396> NFCItens { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
