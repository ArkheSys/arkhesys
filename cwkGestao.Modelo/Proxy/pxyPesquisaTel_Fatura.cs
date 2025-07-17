using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    [InformacaoExibicao("Fatura")]
    public class pxyPesquisaTel_Fatura
    {
        public pxyPesquisaTel_Fatura(int codigoDoc, DateTime referencia, int codigoFatura, string numeroDoc,
                                     DateTime vencimento, decimal saldo, string situacao, decimal total,
                                     int idDoc, int idFatura)
        {
            CodigoDocumento = codigoDoc;
            Referencia = String.Format("{0:00}/{1:0000}", referencia.Month, referencia.Year);
            CodigoFatura = codigoFatura;
            NumeroDocumento = numeroDoc;
            Vencimento = vencimento;
            Saldo = saldo;
            Situacao = situacao;
            Total = total;
            IdDocumento = idDoc;
            IdFatura = idFatura;
        }

        public pxyPesquisaTel_Fatura(int codigoDoc, DateTime referencia, int codigoFatura, string numeroDoc,
                                    DateTime vencimento, decimal saldo, string situacao, decimal total,
                                    int idDoc, int idFatura, DateTime? DataEnvio, bool bEnviadoEmail)
        {
            CodigoDocumento = codigoDoc;
            Referencia = String.Format("{0:00}/{1:0000}", referencia.Month, referencia.Year);
            CodigoFatura = codigoFatura;
            NumeroDocumento = numeroDoc;
            Vencimento = vencimento;
            Saldo = saldo;
            Situacao = situacao;
            Total = total;
            IdDocumento = idDoc;
            IdFatura = idFatura;
            bEnviado = bEnviadoEmail;
            if (bEnviadoEmail == false)
            {
                dataEnvio = null;
            }
            else
            {
                dataEnvio = DataEnvio;
            }
        }

        public pxyPesquisaTel_Fatura(int codigoFatura, int codigoDoc, DateTime vencimento, DateTime referencia,
                                     decimal total, string grupo, string cliente, string fantasia, int idDoc, 
                                     int idFatura, DateTime? DataEnvio, bool bEnviadoEmail, string piloto, Tel_Cliente Cliente)
        {
            CodigoFatura = codigoFatura;
            CodigoDocumento = codigoDoc;
            Vencimento = vencimento;
            Referencia = String.Format("{0:00}/{1:0000}", referencia.Month, referencia.Year);
            Total = total;
            grupoCliente = grupo;
            clienteStr = cliente;
            Fantasia = fantasia;
            IdDocumento = idDoc;
            IdFatura = idFatura;
            bEnviado = bEnviadoEmail;
            if (bEnviadoEmail == false)
            {
                dataEnvio = null;
            }
            else
            {
                dataEnvio = DataEnvio;
            }
            Piloto = piloto;
            clienteCarregado = Cliente;
        }


        public virtual bool Selecionado { get; set; }
        public virtual int CodigoDocumento { get; set; }
        public virtual string Referencia { get; set; }
        public virtual int CodigoFatura { get; set; }
        public virtual string NumeroDocumento { get; set; }
        public virtual DateTime Vencimento { get; set; }
        public virtual Decimal Saldo { get; set; }
        public virtual string Situacao { get; set; }
        public virtual Decimal Total { get; set; }
        public virtual int IdDocumento { get; set; }
        public virtual int IdFatura { get; set; }
        public virtual DateTime? dataEnvio { get; set; }
        public virtual bool bEnviado { get; set; }
        public virtual string clienteStr { get; set; }
        public virtual string grupoCliente { get; set; }
        public virtual string Fantasia { get; set; }
        public virtual string Piloto { get; set; }
        public virtual Tel_Cliente clienteCarregado { get; set; }
    }
}
