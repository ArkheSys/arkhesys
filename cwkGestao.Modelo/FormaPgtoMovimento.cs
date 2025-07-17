using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Forma Pagamento Movimento")]
#pragma warning disable CS0659 // "FormaPgtoMovimento" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public class FormaPgtoMovimento : ModeloBase
#pragma warning restore CS0659 // "FormaPgtoMovimento" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    {
        public FormaPgtoMovimento()
        {
        }
        public FormaPgtoMovimento(FormaPgtoMovimento bas)
        {
            this.Agencia = bas.Agencia;
            this.Cheque = bas.Cheque;
            this.Codigo = bas.Codigo;
            this.Conta = bas.Conta;
            this.CpfCnpjEmitente = bas.CpfCnpjEmitente;
            this.DataCheque = bas.DataCheque;
            this.FormaPagamento = bas.FormaPagamento;
            this.Movimento = bas.Movimento;
            this.NomeBanco = bas.NomeBanco;
            this.NomeEmitente = bas.NomeEmitente;
            this.NumBanco = bas.NumBanco;
            this.Valor = bas.Valor;
            this.BandeiraCartao = bas.BandeiraCartao;
            this.Operadora = bas.Operadora;
            this.CAut = bas.CAut;
        }

        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual Movimento Movimento { get; set; }
        [InformacaoExibicao(0, "Descrição", 90)]
        public virtual FormaPagamento FormaPagamento { get; set; }
        [InformacaoExibicao(1, "Valor", 90, InformacaoExibicao.HAlinhamento.Direita,InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        [InformacaoExibicao(2, "Banco", 90)]
        public virtual string NomeBanco { get; set; }
        [InformacaoExibicao(3, "Nº Banco", 90)]
        public virtual string NumBanco { get; set; }
        [InformacaoExibicao(4, "Agência", 90)]
        public virtual string Agencia { get; set; }
        [InformacaoExibicao(5, "Conta", 90)]
        public virtual string Conta { get; set; }
        [InformacaoExibicao(6, "Cheque", 90)]
        public virtual string Cheque { get; set; }
        [InformacaoExibicao(8, "CPF/CNPJ", 90)]
        public virtual string CpfCnpjEmitente { get; set; }
        [InformacaoExibicao(7, "Emitente", 90)]
        public virtual string NomeEmitente { get; set; }
        public virtual DateTime DataCheque { get; set; }

        public virtual int? BandeiraCartao { get; set; }
        public virtual string Operadora { get; set; }
        public virtual string CAut { get; set; }

        public override string ToString()
        {
            return FormaPagamento.Tipo + " R$" + Valor;
        }


        public override bool Equals(object obj)
        {
            FormaPgtoMovimento fpm = (FormaPgtoMovimento) obj;
            bool igual = base.Equals(obj);

            igual &= this.Valor == fpm.Valor;
            igual &= this.FormaPagamento == fpm.FormaPagamento;
            igual &= this.Cheque == fpm.Cheque;

            return igual;
        }
    }
}
