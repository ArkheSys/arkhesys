using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoD
{
    [DelimitedRecord("|")]
    public class Reg_D500 : Registro
    {
        public string REG = "D500";//  C  004*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string IND_OPER;// C  001*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string IND_EMIT;// C  001*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_PART;// C  060  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string COD_MOD;// C  002*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string COD_SIT;// N  002*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public string SER;//  Série do documento fiscal  C  004  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public string SUB;//  Subsérie do documento fiscal  N  003  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "9", "0", "esquerda", "true")]
        public int NUM_DOC;//  Número do documento fiscal  N  009  -  S 
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_DOC;//  Data da emissão do documento fiscal  N  008*  -  S 
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_A_P;//  Data da entrada (aquisição)  N  008*  -  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DOC;//  Valor total do documento fiscal  N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;//  Valor total do desconto  N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_SERV;//  Valor da prestação de serviços  N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_SERV_NT;//  Valor total dos serviços não-tributados pelo ICMS  N - 02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_TERC;//  Valores cobrados em nome de terceiros  N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DA;//  Valor de outras despesas indicadas no documento fiscal  N - 02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS;//  Valor da base de cálculo do ICMS  N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS;//  Valor do ICMS  N  -  02  N 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string COD_INF;// C  006  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;//  Valor do PIS/PASEP  N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;//  Valor da COFINS  N  -  02  N 
    }
}
