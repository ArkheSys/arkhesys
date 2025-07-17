using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoA
{
    [DelimitedRecord("|")]
    public class Reg_A010 : Registro
    {
        //header - Obrigatorio
        public string REG = "A010";
        // 14 caractere(s) - Obrigatorio
        [FieldConverter(typeof(StringNumberCharConverter), "14", "0", "esquerda", "true")]
        public string CNPJ;
    }
}
