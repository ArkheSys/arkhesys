using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco9
{
    [DelimitedRecord("|")]
    public class Reg_9900 : Registro
    {
        public string REG = "9900";
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public string REG_BLC;
        public int QTD_REG_BLC;
    }
}
