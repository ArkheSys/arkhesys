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
    public class Reg_0190 : Registro
    {
        /// <summary>
        ///  IDENTIFICAÇÃO DAS UNIDADES DE MEDIDA 
        /// </summary>
        
        public string Header = "0190";

        [FieldConverter(typeof(StringNumberCharConverter), "6", "", "", "true")]
        public string codigoDeIUnidadeDeMedida;

        public string descricaoDaUnidadeDeMedida;


    }
}
