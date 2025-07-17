using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C115 : Registro
    {
        public string REG = "C115";
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_CARGA;
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string CNPJ_COL;
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string IE_COL;
        [FieldConverter(typeof(StringNumberCharConverter), "11", "", "", "true")]
        public string CPF_COL;
        [FieldConverter(typeof(StringNumberCharConverter), "7", "", "", "false")]
        public string COD_MUN_COL;
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string CNPJ_ENTG;
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string IE_ENTG;
        [FieldConverter(typeof(StringNumberCharConverter), "11", "", "", "true")]
        public string CPF_ENTG;
        [FieldConverter(typeof(StringNumberCharConverter), "7", "", "", "false")]
        public string COD_MUN_ENTG;
    }
}
