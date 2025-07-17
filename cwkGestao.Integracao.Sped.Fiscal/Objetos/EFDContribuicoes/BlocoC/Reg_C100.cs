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
    public class Reg_C100 : Registro
    {
        public string REG = "C100";
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_OPER;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_EMIT;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_PART;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string COD_MOD;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "0", "esquerda", "false")]
        public string COD_SIT;
        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public string SER;
        [FieldConverter(typeof(StringNumberCharConverter), "9", "", "", "false")]
        public int NUM_DOC;
        [FieldConverter(typeof(StringNumberCharConverter), "44", "0", "Esquerda", "false")]
        public string CHV_NFE;
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime? DT_DOC;
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime? DT_E_S;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_DOC;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int? IND_PGTO;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_DESC;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_ABAT_NT;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_MERC;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int? IND_FRT;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_FRT;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_SEG;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_OUT_DA;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_BC_ICMS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_ICMS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_BC_ICMS_ST;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_ICMS_ST;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_IPI;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_PIS_ST;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal? VL_COFINS_ST;
    }
}
