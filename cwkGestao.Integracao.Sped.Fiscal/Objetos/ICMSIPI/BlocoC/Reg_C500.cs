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
    public class Reg_C500 : Registro
    {
        public string REG = "C500";
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_OPER;
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_EMIT;
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_PART;//  Código do participante do fornecedor C  060  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", " ", "esquerda", "false")]
        public string COD_MOD;// C  002*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "0", "esquerda", "false")]
        public string COD_SIT;// N  002*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public string SER;// C  004  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public decimal SUB;// N  003  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "2", " ", "", "false")]
        public string COD_CONS;
        [FieldConverter(typeof(StringNumberCharConverter), "9", "0", "esquerda", "false")]
        public int NUM_DOC;// N  009  -  S 
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_DOC;// N  008*  -  S 
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_E_S;// N  008*  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DOC;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_FORN;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_SERV_NT;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_TERC;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DA;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS;// N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS_ST;// N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS_ST;// N  -  02  N 
        [FieldConverter(typeof(StringNumberCharConverter), "6", "", "", "false")]
        public string COD_INF;// C  006  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;// N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;// N  -  02  N 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int TP_LIGACAO;
        [FieldConverter(typeof(StringNumberCharConverter), "2", " ", "", "false")]
        public string COD_GRUPO_TENSAO;
    }
}
