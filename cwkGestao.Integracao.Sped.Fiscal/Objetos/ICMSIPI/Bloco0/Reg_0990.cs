using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.ICMSIPI.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0990 : Registro
    {
        /// <summary>
        /// Centro de Custos
        /// </summary>
        
        public string Header = "0990";

        public int QuantidadeTotalDeLinhasDoBloco0;
    }
}
