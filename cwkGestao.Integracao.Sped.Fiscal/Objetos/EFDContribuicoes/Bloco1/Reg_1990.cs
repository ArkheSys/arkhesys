using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.Bloco1
{
    [DelimitedRecord("|")]
    public class Reg_1990 : Registro
    {
        public string REG = "1990";
        public int QTD_LIN_1;
    }
}
