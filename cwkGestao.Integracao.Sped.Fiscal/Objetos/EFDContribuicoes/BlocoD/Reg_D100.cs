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
    public class Reg_D100 : Registro
    {
        public string REG = "D100";//
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string IND_OPER;// C  001*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string IND_EMIT;//C  001*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_PART;//  Código do participante (campo 02 do Registro 0150). C 060 - S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string COD_MOD;//C  002*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string COD_SIT;// N  002*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public string SER;//  Série do documento fiscal  C  004  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "3", "", "", "false")]
        public string SUB;//  Subsérie do documento fiscal  C  003  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "9", "0", "esquerda", "true")]
        public int NUM_DOC;//  Número do documento fiscal  N  009  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "44", "", "", "false")]
        public string CHV_CTE;//  Chave do Conhecimento de Transporte Eletrônico  N  044*  -  N 
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_DOC;//  Data de referência/emissão dos documentos fiscais  N  008*  -  S 
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_A_P;//  Data da aquisição ou da prestação do serviço  N  008*  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int TP_CTe;// N  001*  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "44", "", "", "false")]
        public string CHV_CTE_REF;// N  044*  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DOC;//  Valor total do documento fiscal  N  -  02 S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_DESC;//  Valor total do desconto  N  -  02 N
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public string IND_FRT;// C  001*  -  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_SERV;//  Valor total da prestação de serviço  N  -  02 S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_ICMS;//  Valor da base de cálculo do ICMS  N  -  02 N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ICMS;//  Valor do ICMS  N  -  02 N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_NT;//  Valor não-tributado do ICMS  N  -  02 N 
        [FieldConverter(typeof(StringNumberCharConverter), "6", "", "", "false")]
        public string COD_INF;// C  006  -  N 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;//  Código da conta analítica contábil debitada/creditada C 060  -  N
    }
}
