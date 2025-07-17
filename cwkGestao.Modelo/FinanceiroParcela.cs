using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class FinanceiroParcela : NotaParcela
    {
        public DateTime DtContabil { get; set; }
        public bool ParcelaClienteCartao { get; set; }
    }
}
