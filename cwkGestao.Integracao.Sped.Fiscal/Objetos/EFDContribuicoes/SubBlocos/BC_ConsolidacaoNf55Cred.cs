using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.SubBlocos
{
    public class BC_ConsolidacaoNf55Cred : Bloco
    {
        public Reg_C190 ConsolidacoesNFeCred { get; set; }
        public IList<Reg_C191> DetPisPasepCred { get; set; }
        public IList<Reg_C195> DetCofinsCred { get; set; }
        public IList<Reg_C199> CompDocImportacoes { get; set; }


        public override void GerarBloco()
        {
            throw new NotImplementedException();
        }
    }
}
