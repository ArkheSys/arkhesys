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
    public class Reg_D101 : Registro
    {
        public string REG = "D101";//  C  004*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_NAT_FRT;// C  001*  -  S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ITEM;//  Valor total dos itens  N  -  02  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_PIS;//  Código da Situação Tributária referente ao PIS/PASEP N 002* -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string NAT_BC_CRED;// C  002*  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_PIS;//  Valor da base de cálculo do PIS/PASEP  N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_PIS;//  Alíquota do PIS/PASEP (em percentual)  N  008  04  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_PIS;//  Valor do PIS/PASEP  N  -  02  N 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;//  Código da conta analítica contábil debitada/creditada C 060  -  N 
    }
}
