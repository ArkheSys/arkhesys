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
    public class Reg_0205 : Registro
    {
        /// <summary>
        ///   ALTERAÇÃO DO ITEM 
        /// </summary>
        
        public string Header = "0205";

        public string descricaoAnteriorDoItem;

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime dataInicialUtilizacaoItem;

        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime dataFinalUtilizacaoItem;

        [FieldConverter(typeof(StringNumberCharConverter), "60", "", "", "true")]
        public string codigoAnteriorDoItemComRelacaoAUltimaInformacaoApresentada;
        


    }
}
