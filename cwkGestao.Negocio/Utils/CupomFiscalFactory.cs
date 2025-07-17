using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Utils
{
    public class CupomFiscalFactory
    {
        public ICupomFiscal Build(string arquitetura)
        {
            if (arquitetura == "x86")
                return new CupomFiscal32Bits();
            else
                return new CupomFiscal64Bits();
        }
    }
}
