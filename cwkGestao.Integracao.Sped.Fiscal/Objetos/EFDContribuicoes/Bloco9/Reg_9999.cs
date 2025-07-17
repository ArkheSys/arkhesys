using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco9
{
    [DelimitedRecord("|")]
    public class Reg_9999 : Registro
    {
        public string REG = "9999";
        public int QTD_LIN;
    }
}
