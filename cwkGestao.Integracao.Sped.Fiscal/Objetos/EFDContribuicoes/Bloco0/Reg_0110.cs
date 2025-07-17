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
    public class Reg_0110 : Registro
    {
        /// <summary>
        ///  REGIMES DE APURAÇÃO DA CONTRIBUIÇÃO SOCIAL E DE APROPRIAÇÃO DE CRÉDITO 
        /// </summary>
        
        public string Header = "0110";

        [FieldConverter(typeof(StringNumberCharConverter), "01", "", "", "false")]
        public string codigoIndicadorIncidenciaTributaria;

        [FieldConverter(typeof(StringNumberCharConverter), "01", "", "", "false")]
        public string codigoIndicadorDeMetodoDeApropriacaoCreditosComuns;

        [FieldConverter(typeof(StringNumberCharConverter), "01", "", "", "false")]
        public string codigoIndicadorTipoContribuicaoApuradaPeriodo;

        [FieldConverter(typeof(StringNumberCharConverter), "01", "", "", "false")]
        public string codigoIndicadorCriterioEscrituracaoRegimeCumulativo;

    }
}
