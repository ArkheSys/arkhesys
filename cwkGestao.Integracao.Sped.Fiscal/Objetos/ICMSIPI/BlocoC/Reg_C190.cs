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
    public class Reg_C190 : Registro
    {
        public string REG = "C190";
        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public string CST_ICMS;// Código da Situação Tributária N 003* - O O
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public string CFOP;// N 004* - O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_ICMS;// Alíquota do ICMS N 006 02 OC OC
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_OPR;// N - 02 O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS;// N - 02 O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS;// N - 02 O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS_ST;// N - 02 O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS_ST;// N - 02 O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_RED_BC;// N - 02 O O
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_IPI;// N - 02 O O
        [FieldConverter(typeof(StringNumberCharConverter), "6", "", "", "false")]
        public string COD_OBS;// C 006 - OC OC
    }
}
