using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoM;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_M : Bloco
    {
        public Reg_M001 Abertura { get; set; }
        public Reg_M990 Encerramento { get; set; }
        
        public Bloco_M()
        {
        }
        
        public override void GerarBloco()
        {
            Abertura = new Reg_M001();
            Abertura.IND_MOV = 1;
            Encerramento = new Reg_M990();
            Encerramento.QTD_LIN_M = GetContagemRegistros();
        }
    }
}