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
    public class Reg_C110 : Registro
    {
        public string REG = "C110";
        [FieldConverter(typeof(StringNumberCharConverter), "4", "", "", "false")]
        public string COD_INF;
        [FieldConverter(typeof(StringNumberCharConverter), "", "", "", "false")]
        public string TXT_COMPL;
    }
}
