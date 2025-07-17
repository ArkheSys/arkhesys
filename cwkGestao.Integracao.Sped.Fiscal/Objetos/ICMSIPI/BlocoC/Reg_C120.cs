using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C120 : Registro
    {
        public string Header = "C120";
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int COD_DOC_IMP;
        [FieldConverter(typeof(StringNumberCharConverter), "12", "", "", "true")]
        public int NUM_DOC_IMP;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal PIS_IMP;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal COFINS_IMP;
        [FieldConverter(typeof(StringNumberCharConverter), "20", "", "", "true")]
        public string NUM_ACDRAW;
    }
}
