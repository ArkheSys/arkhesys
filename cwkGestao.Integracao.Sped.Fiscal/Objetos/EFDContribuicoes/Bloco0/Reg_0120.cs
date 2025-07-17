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
    public class Reg_0120 : Registro
    {
        /// <summary>
        /// IDENTIFICAÇÃO DE PERÍODOS DISPENSADOS DA ESCRITURAÇÃO FISCAL DIGITAL DAS CONTRIBUIÇÕES 
        /// – EFD-CONTRIBUIÇÕES 
        /// </summary>
        
        public string Header = "0120";

        [FieldConverter(ConverterKind.Date, "MMyyyy")]
        public DateTime mesReferênciaAnoDaEscrituracaoDispensadaEntrega;

        [FieldConverter(typeof(StringNumberCharConverter), "90", "", "", "false")]
        public string informacaoComplementarDoRegistro;

    }
}
