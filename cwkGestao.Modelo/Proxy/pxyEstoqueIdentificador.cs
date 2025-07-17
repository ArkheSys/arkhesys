using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEstoqueIdentificador
    {
        public pxyEstoqueIdentificador(string identificador, decimal saldo, decimal custoUnitario, int idIdentificador)
        {
            Identificador = identificador;
            Saldo = saldo;
            CustoUnitario = custoUnitario;
            IdIdentificador = idIdentificador;
        }

        public string Identificador { get; set; }
        public decimal Saldo { get; set; }
        public decimal CustoUnitario { get; set; }
        public int IdIdentificador { get; set; }

        public decimal QuantidadeBaixada { get; set; }

        public decimal SaldoDisponivel { get { return Saldo - QuantidadeBaixada; } }
    }
}
