using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C170 : Registro
    {
        public string REG = "C170";
        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public int NUM_ITEM;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_ITEM;
        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "false")]
        public string DESCR_COMPL;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal QTD;
        [FieldConverter(typeof(StringNumberCharConverter), "6", "", "", "false")]
        public string UNID;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ITEM;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_MOV;
        [FieldConverter(typeof(StringNumberCharConverter), "3", "0", "esquerda", "true")]
        public string CST_ICMS;
        [FieldConverter(typeof(StringNumberCharConverter), "4", "0", "esquerda", "true")]
        public string CFOP;
        [FieldConverter(typeof(StringNumberCharConverter), "10", "", "", "false")]
        public string COD_NAT;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_ICMS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS_ST;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_ST;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS_ST;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_APUR;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_IPI;
        [FieldConverter(typeof(StringNumberCharConverter), "3", "0", "esquerda", "false")]
        public string COD_ENQ;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_IPI;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_IPI;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_IPI;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_PIS;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? QUANT_BC_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? ALIQ_PIS_QUANT;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_COFINS;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? QUANT_BC_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? ALIQ_COFINS_QUANT;
        
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;
    }
}
