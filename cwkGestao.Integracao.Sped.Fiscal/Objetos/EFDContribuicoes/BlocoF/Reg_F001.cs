using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;
using cwkGestao.Integracao.Sped.Fiscal.Util.Converters;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.EFDContribuicoes.BlocoF
{
    [DelimitedRecord("|")]
    public class Reg_F001 : Registro
    {
        public string REG = "F001";
        [FieldConverter(typeof(StringNumberCharConverter), "1", "", "", "false")]
        public int IND_MOV;
    }
}
