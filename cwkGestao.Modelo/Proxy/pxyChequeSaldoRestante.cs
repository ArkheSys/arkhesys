using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyChequeSaldoRestante
    {
        public Cheque Cheque { get; set; }
        public Decimal SaldoRestanteEntrada { get; set; }
        public Decimal TotalUtilizadoEntrada { get; set; }
        public Decimal SaldoRestanteSaida { get; set; }
        public Decimal TotalUtilizadoSaida { get; set; }

        public pxyChequeSaldoRestante(Cheque c, Decimal totalUtilizadoEntrada, Decimal saldoRestanteEntrada, Decimal totalUtilizadoSaida, Decimal saldoRestanteSaida)
        {
            Cheque = c;
            SaldoRestanteEntrada = saldoRestanteEntrada;
            TotalUtilizadoEntrada = totalUtilizadoEntrada;
            SaldoRestanteSaida = saldoRestanteSaida;
            TotalUtilizadoSaida = totalUtilizadoSaida;
        }

        public pxyChequeSaldoRestante(int id, string banco, string agencia, string cc, string numeroCheque, 
            string emitente, string cnpjCpfEmitente, decimal valorCheque, bool chTerceiro,
            DateTime vencimento, Decimal totalUtilizadoEntrada, Decimal saldoRestanteEntrada,
            Decimal totalUtilizadoSaida, Decimal saldoRestanteSaida)
        {
            Cheque = new Cheque()
            {
                Agencia = agencia, 
                Banco = banco, 
                ChTerceiro = chTerceiro, 
                CNPJCPF_Emitente = cnpjCpfEmitente, 
                ContaCorrente = cc, 
                Emitente = emitente, 
                ID = id, 
                Numero = numeroCheque, 
                Valor = valorCheque, 
                Vencimento = vencimento
            };
            SaldoRestanteEntrada = saldoRestanteEntrada;
            TotalUtilizadoEntrada = totalUtilizadoEntrada;
            SaldoRestanteSaida = saldoRestanteSaida;
            TotalUtilizadoSaida = totalUtilizadoSaida;
        }
    }
}
