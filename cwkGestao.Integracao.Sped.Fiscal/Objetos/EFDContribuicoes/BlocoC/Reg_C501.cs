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
    public class Reg_C501 : Registro
    {
        public string REG = "C501";
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_PIS;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ITEM;
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string NAT_BC_CRED;
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_PIS;//Valor da base de cálculo do PIS/PASEP  N  -  02  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_PIS;//Alíquota do PIS/PASEP (em percentual)  N  008  04  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;//Valor do PIS/PASEP  N  -  02  S 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;//Código da conta analítica contábil debitada/creditada  C  060  -  N 
    }
}
