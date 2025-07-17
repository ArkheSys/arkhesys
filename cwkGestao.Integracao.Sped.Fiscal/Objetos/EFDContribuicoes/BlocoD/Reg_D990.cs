using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoD
{
    [DelimitedRecord("|")]
    public class Reg_D990 : Registro
    {
        public string REG = "D990";
        public int QTD_LIN_D;
    }
}
