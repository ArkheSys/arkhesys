using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco1;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_1 : Bloco
    {
        public Reg_1001 Abertura { get; set; }
        public Reg_1990 Encerramento { get; set; }
        
        public Bloco_1()
        {
        }
        
        public override void GerarBloco()
        {
            Abertura = new Reg_1001();
            Abertura.IND_MOV = 1;
            Encerramento = new Reg_1990();
            Encerramento.QTD_LIN_1 = GetContagemRegistros();
        }
    }
}