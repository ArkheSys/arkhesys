using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0015 : Registro
    {
        /// <summary>
        ///  DADOS DO CONTRIBUINTE SUBSTITUTO
        /// </summary>

        public string Header = "0015";

        [FieldConverter(typeof(StringNumberCharConverter), "2", "", "", "false")]
        public string uf_st;
        
        [FieldConverter(typeof(StringNumberCharConverter), "14", "", "", "true")]
        public string ie_st;


     
    }
}
