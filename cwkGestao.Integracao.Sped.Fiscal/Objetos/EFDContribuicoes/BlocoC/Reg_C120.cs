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
    public class Reg_C120 : Registro
    {
        public string Header = "C120";
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int COD_DOC_IMP;
        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "true")]
        public int NUM_DOC_IMP;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS_IMP;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS_IMP;
        [FieldConverter(typeof(StringNumberCharConverter), "20", "", "", "true")]
        public string NUM_ACDRAW;
    }
}
