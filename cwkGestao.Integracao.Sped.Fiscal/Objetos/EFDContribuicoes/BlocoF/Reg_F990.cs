using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoF
{
    [DelimitedRecord("|")]
    public class Reg_F990 : Registro
    {
        public string REG = "F990";
        public int QTD_LIN_F;
    }
}
