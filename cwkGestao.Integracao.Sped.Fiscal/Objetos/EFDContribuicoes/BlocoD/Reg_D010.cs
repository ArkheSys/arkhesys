using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoD
{
    [DelimitedRecord("|")]
    public class Reg_D010 : Registro
    {
        public string REG = "D010";
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string CNPJ;//  N  014*  -  S 
    }
}
