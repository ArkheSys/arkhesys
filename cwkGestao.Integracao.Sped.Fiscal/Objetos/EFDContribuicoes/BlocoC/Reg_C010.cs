using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C010 : Registro
    {
        public string Header = "C010";
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string CNPJ;
        public int IND_ESCRI = 2;
    }
}
