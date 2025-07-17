using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoD
{
    [DelimitedRecord("|")]
    public class Reg_D001 : Registro
    {
        public string REG = "D001";//  C  004*  -  S 
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_MOV;//C  001  -  S 
    }
}
