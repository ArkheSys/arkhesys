using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_ECF_RedZ : Bloco
    {
        public Reg_C405 ReducaoZ { get; set; }
        public IList<Reg_C481> ResumoDiarioPisPasep { get; set; }
        public IList<Reg_C485> ResumoDiarioCofins { get; set; }

        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
