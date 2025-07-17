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
    public class Reg_C505 : Registro
    {
        public string REG = "C505";//  C  004*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_COFINS;//  Código da Situação Tributária referente a COFINS  N  002* - S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ITEM;//  Valor total dos itens  N  -  02 S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string NAT_BC_CRED;// C  002*  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_COFINS;//  Valor da base de cálculo da COFINS  N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_COFINS;//  Alíquota da COFINS (em percentual)  N  008  04  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;//  Valor da COFINS  N  -  02  S 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;//  Código da conta analítica contábil debitada/creditada C 060 -  N 
    }
}
