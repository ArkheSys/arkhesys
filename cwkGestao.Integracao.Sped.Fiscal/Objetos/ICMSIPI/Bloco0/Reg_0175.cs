using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0175 : Registro
    {
        /// <summary>
        /// ALTERAÇÃO DA TABELA DE CADASTRO DO PARTICIPANTE 
        /// </summary>
        
        public string Header = "0175";

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime DT_ALT;

        [FieldConverter(typeof(StringNumberCharConverter), "02", "", "", "false")]
        public int NR_CAMPO;

        [FieldConverter(typeof(StringNumberCharConverter), "100", "", "", "false")]
        public string CONT_ANT;

    }
}
