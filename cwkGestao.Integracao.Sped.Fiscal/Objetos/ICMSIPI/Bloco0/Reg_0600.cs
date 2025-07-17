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
    public class Reg_0600 : Registro
    {
        /// <summary>
        /// Centro de Custos
        /// </summary>
        public string Header = "0600";

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime datainclusaoOuAlteracao;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public int codigoCentroDeCustos;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "false")]
        public string nomeCentroDeCustos;
    }
}
