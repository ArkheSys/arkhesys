using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0001 : Registro
    {
        /// <summary>
        /// ABERTURA DO BLOCO 0
        /// </summary>

        public string Header = "0001";

        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int indicadorMovimento;

 
    }
}
