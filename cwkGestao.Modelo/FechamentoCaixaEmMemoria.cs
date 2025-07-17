using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class FechamentoCaixaEmMemoria
    {
        public DateTime Data { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Entrada { get; set; }
        public decimal Saida { get; set; }
        public decimal Saldo { get; set; }
        public decimal SaldoFinal { get; set; }
        public bool Fechado { get; set; }
        public string UsuarioFechamento { get; set; }
        public DateTime? DtFechamento { get; set; }
    }
}
