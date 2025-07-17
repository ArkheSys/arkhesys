using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    [InformacaoExibicao("Fatura")]
    public class pxyHistoricoFaturaCliente
    {
        public pxyHistoricoFaturaCliente(string grupo, DateTime referencia, int numeroFatura, int codigoDoc, DateTime vencimento
                                        , DateTime? dataPagamento, decimal valor, string cliente, decimal saldo,
                                            decimal totalBaixado,string numeroPiloto, string fantasia)
        {
            Grupo = grupo;
            Referencia = String.Format("{0:00}/{1:0000}", referencia.Month, referencia.Year);
            NumeroFatura = numeroFatura;
            CodigoDocumento = codigoDoc;
            Vencimento = vencimento;
            DataPagamento = dataPagamento;
            Valor = valor;
            Cliente = cliente;
            Saldo = saldo;
            TotalBaixado = totalBaixado;
            Dias = ((dataPagamento ?? DateTime.Now) - Vencimento).Days;
            NumeroPiloto = String.IsNullOrEmpty(numeroPiloto) ? "" : String.Format("({0:00}){1:0000}-{2:0000}",numeroPiloto.Remove(2),numeroPiloto.Substring(2).Remove(4),numeroPiloto.Substring(6));
            Fantasia = fantasia;
        }

        public virtual string Grupo { get; set; }
        public virtual string Referencia { get; set; }
        public virtual int NumeroFatura { get; set; }
        public virtual int CodigoDocumento { get; set; }
        public virtual DateTime Vencimento { get; set; }
        public virtual DateTime? DataPagamento { get; set; }
        public virtual int Dias { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual string Cliente { get; set; }
        public virtual decimal Saldo { get; set; }
        public virtual decimal TotalBaixado { get; set; }
        public virtual string NumeroPiloto { get; set; }
        public virtual string Fantasia { get; set; }
    }
}
