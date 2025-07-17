using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C175 : Registro
    {
        public string REG = "C175";
        

        [FieldConverter(typeof(StringNumberCharConverter), "4", "0", "esquerda", "true")]
        public string CFOP;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_OPR;

        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;

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
        
        [FieldConverter(typeof(StringNumberCharConverter), "255", "", "", "false")]
        public string COD_CTA;

        [FieldConverter(typeof(StringNumberCharConverter), "255", "", "", "false")]
        public string INFO_COMPL;
    }
}