using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA
{
    [DelimitedRecord("|")]
    public class Reg_A120 : Registro
    {
        //header - Obrigatorio
        public string REG = "A120";
        //decimal 2 casas precisão - Obrigatorio
        public decimal VL_TOT_SERV;
        //decimal 2 casas precisão - Obrigatorio
        public decimal VL_BC_PIS;
        //decimal 2 casas precisão
        public decimal VL_PIS_IMP;
        // ddmmaaaa 8 caractere(s) - Obrigatorio
        public DateTime DT_PAG_PIS;
        //decimal 2 casas precisão - Obrigatorio
        public decimal VL_BC_COFINS;
        //decimal 2 casas precisão
        public decimal VL_COFINS_IMP;
        // ddmmaaaa 8 caractere(s)
        public DateTime DT_PAG_COFINS;
        // 1 caractere(s) - Obrigatorio
        public string LOC_EXE_SERV;
    }
}
