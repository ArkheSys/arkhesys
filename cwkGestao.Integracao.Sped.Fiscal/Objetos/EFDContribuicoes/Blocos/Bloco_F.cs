using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoF;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Blocos
{
    public class Bloco_F : Bloco
    {
        public Reg_F001 Abertura { get; set; }
        public Reg_F990 Encerramento { get; set; }
        
        public Bloco_F()
        {
        }
        
        public override void GerarBloco()
        {
            Abertura = new Reg_F001();
            Abertura.IND_MOV = 1;
            Encerramento = new Reg_F990();
            Encerramento.QTD_LIN_F = GetContagemRegistros();
        }
    }
}