using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoC
{
    [DelimitedRecord("|")]
    public class Reg_C990 : Registro
    {
        public string Header = "C990";
        public int QTD_LIN_C;

    }
}
