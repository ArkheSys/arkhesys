using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelCustoPorNota
    {
        public pxyRelCustoPorNota() { }
        public pxyRelCustoPorNota(string nomeFilial, string nomeCliente, int numeroNota, string tipoMovimentacao, string descricao, decimal valor, decimal totalNota)
        {
            this.NomeFilial = nomeFilial;
            this.NomeCliente = nomeCliente;
            this.NumeroNota = numeroNota;
            this.TipoMovimentacao = tipoMovimentacao;
            this.Descricao = descricao;
            this.Valor = valor;
            this.TotalNota = totalNota;
        }
        public int NumeroNota { get; set; }

        public string NomeFilial { get; set; }

        public decimal TotalNota { get; set; }

        public string NomeCliente { get; set; }

        public string TipoMovimentacao { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public decimal ValorFrete { get; set; }

        public decimal ValorSeguro { get; set; }

        public decimal ValorICMS { get; set; }

        public decimal ValorIPI { get; set; }

        public decimal ValorPIS { get; set; }

        public decimal ValorCOFINS { get; set; }
    }
}
