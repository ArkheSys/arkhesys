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
    public class Reg_D505 : Registro
    {
        public string REG = "D505";//  Texto fixo contendo "D505"  C  004* - S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string CST_COFINS;//  Código da ST referente a COFINS N 002* - S 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_ITEM;//  Valor Total dos Itens  N  -  02 S 
        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string NAT_BC_CRED;//  C  002*  -  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_BC_COFINS;//  Valor da base de cálculo da COFINS N  -  02  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal ALIQ_COFINS;//  Alíquota da COFINS (em percentual) N 008 04  N 
        [FieldConverter(ConverterKind.Decimal, ",")]
        public decimal VL_COFINS;//  Valor da COFINS  N  -  02  N 
        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string COD_CTA;// Código da conta analítica debitada/creditada C 060 - N 
    }
}
