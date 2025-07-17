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
    public class Reg_A100 : Registro
    {
        //header - Obrigatorio
        public string REG = "A100";
        // 1 caractere(s) - Obrigatorio
        public int IND_OPER;
        // 1 caractere(s) - Obrigatorio
        public int IND_EMIT;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_PART;
        public string COD_SIT;
        [FieldConverter(typeof(StringNumberCharConverter), "20", "", "", "false")]
        public string SER;
        [FieldConverter(typeof(StringNumberCharConverter), "20", "", "", "false")]
        public string SUB;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public int NUM_DOC;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string CHV_NFSE;
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_DOC;
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_EXE_SERV;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DOC;
        // 1 caractere(s) - Obrigatorio
        public int IND_PGTO;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS_RET;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS_RET;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ISS;
    }
}
