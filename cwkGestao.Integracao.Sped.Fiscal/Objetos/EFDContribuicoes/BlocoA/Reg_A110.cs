using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA
{
    [DelimitedRecord("|")]
    public class Reg_A110 : Registro
    {
        //header - Obrigatorio
        public string REG = "A110";
        // 6 caractere(s) - Obrigatorio
        public string COD_INF;
        // MAX 255 caractere(s)
        public string TXT_COMPL;
    }
}
