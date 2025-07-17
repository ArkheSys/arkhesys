using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class MovCaixaMemoria
    {
        public DateTime Data { get; set; }
        public Decimal SaldoInicial { get; set; }
        public Decimal SaldoFinal { get; set; }
        public Decimal Entrada { get; set; }
        public decimal Saida { get; set; }
    }
}
