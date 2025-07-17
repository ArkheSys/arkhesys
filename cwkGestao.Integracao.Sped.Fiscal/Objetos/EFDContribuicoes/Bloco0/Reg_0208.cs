using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Integracao.Sped.Fiscal.Interfaces;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Objetos.Bloco0
{
    [DelimitedRecord("|")]
    public class Reg_0208 : Registro
    {
        public string Header;
    }
}
