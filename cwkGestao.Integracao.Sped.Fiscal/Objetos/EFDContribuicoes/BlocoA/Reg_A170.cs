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
    public class Reg_A170 : Registro
    {
        //header - Obrigatorio
        public string REG = "A170";
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public int NUM_ITEM;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_ITEM;
        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "false")]
        public string DESCR_COMPL;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ITEM;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string NAT_BC_CRED;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string IND_ORIG_CRED;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CCUS;
    }
}
