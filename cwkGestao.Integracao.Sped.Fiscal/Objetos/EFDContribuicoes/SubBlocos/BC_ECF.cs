using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_ECF : Bloco
    {
        public Reg_C400 EquipECF { get; set; }
        public IList<BC_ECF_RedZ> Reducoes_Z { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
