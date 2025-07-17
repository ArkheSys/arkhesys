using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA
{
    [DelimitedRecord("|")]
    public class Reg_A111 : Registro
    {
        //header - Obrigatorio
        public string REG = "A111";
        // 15 caractere(s) - Obrigatorio
        public string NUM_PROC;
        // 1 caractere(s) - Obrigatorio
        public string IND_PROC;
    }
}
