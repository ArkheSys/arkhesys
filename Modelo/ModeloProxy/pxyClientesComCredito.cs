using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.ModeloProxy
{
    public class pxyClientesComCredito
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string CNPJ_CPF { get; set; }
        public decimal Saldo { get; set; }

        public string SaldoFormatado { get { return Saldo.ToString("c2"); } }

        public pxyClientesComCredito(int codigo, string nome, string cnpj_cpf, decimal saldo)
        {
            Codigo = codigo;
            Nome = nome;
            CNPJ_CPF = cnpj_cpf;
            Saldo = saldo;
        }
    }
}
