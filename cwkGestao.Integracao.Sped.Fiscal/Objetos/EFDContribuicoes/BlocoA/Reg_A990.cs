using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA
{
    [DelimitedRecord("|")]
    public class Reg_A990 : Registro
    {
        //header - Obrigatorio
        public string REG = "A990";
        //inteiro - Obrigatorio
        public int QTD_LIN_A;
    }
}
