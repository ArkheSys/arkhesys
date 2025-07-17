using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Auxiliares
{
    public static class Util
    {
        public static decimal PorcentagemDe(decimal valorTotal, decimal valor)
        {
            if (valorTotal > 0)
            {
                return Math.Round(valor / valorTotal * 100, 4);
            }
            return 0;
        }

        public static decimal ValorDe(decimal valorTotal, decimal valorPerc)
        {
            if (valorTotal > 0)
            {
                return Math.Round(valorTotal * valorPerc / 100, 4);
            }
            return 0;
        }
    }
}
