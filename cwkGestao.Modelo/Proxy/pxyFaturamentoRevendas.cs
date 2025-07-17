using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    /// <summary>
    /// Proxy para o relatório de faturamento das revendas
    /// </summary>
    public class pxyFaturamentoRevendas
    {
        public pxyFaturamentoRevendas(string nomeCliente, int codigoCliente, string nomeServico
            , int? codigo, decimal? valor, DateTime data, int codigoSistema, int numeroSerie, string nomeRevenda)
        {
            NomeCliente = nomeCliente;
            CodigoCliente = codigoCliente;
            NomeServico = nomeServico;
            Codigo = codigo;
            Valor = valor;
            Data = data.ToShortDateString();
            CodigoSistema = codigoSistema;
            NumeroSerie = numeroSerie;
            NomeRevenda = nomeRevenda;
            }

        public string NomeCliente { get; set; }
        public int CodigoCliente { get; set; }
        public string NomeServico { get; set; }
        public int? Codigo { get; set; }
        public decimal? Valor { get; set; }
        public string Data { get; set; }
        public int CodigoSistema { get; set; }
        public int NumeroSerie { get; set; }
        public string NomeRevenda { get; set; }
    }
}
