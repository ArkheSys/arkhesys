using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cheque")]
    public class Cheque : ModeloBase
    {
        public Cheque() 
        {
            ChequeHistorico = new List<ChequeHistorico>();

        }

        public enum StatusCheque
        {
            EmCaixa = 0,
            Depositado = 1,
            Devolvido = 2,
            Redepositado = 3,
            Pago = 4,
            Baixado = 5
        }

        [InformacaoExibicao(1, "Código", 50)]
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Banco", 80)]
        public virtual string Banco { get; set; }
        [InformacaoExibicao(2, "Agência", 150)]
        public virtual string Agencia { get; set; }
        [InformacaoExibicao(3, "CC", 120)]
        public virtual string ContaCorrente { get; set; }
        [InformacaoExibicao(4, "Número", 100)]
        public virtual string Numero { get; set; }
        [InformacaoExibicao(8, "Emitente", 380)]
        public virtual string Emitente { get; set; }
        [InformacaoExibicao(7, "CNPJ/CPF", 120)]
        public virtual string CNPJCPF_Emitente { get; set; }
        [InformacaoExibicao(5, "Valor", 150, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual Decimal Valor { get; set; }
        [InformacaoExibicao(9, "Ch. Terceiro", 100)]
        public virtual Boolean ChTerceiro { get; set; }
        [InformacaoExibicao(6, "Vencimento", 80, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Vencimento { get; set; }
        public virtual IList<ChequeHistorico> ChequeHistorico { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        [InformacaoExibicao(16, "Conta", 150)]
        public virtual Banco Conta { get; set; }
        public virtual IList<MovCaixa> MovCaixas { get; set; }
        public virtual StatusCheque Status{ get; set; }

        [InformacaoExibicao(10, "Status", 100)]
        public virtual string StatusLegivel
        {
            get
            {
                return StatusToString(this.Status);
            }
        }

        public static string StatusToString(StatusCheque status)
        {
            string statusLegivel = "";
            switch (status)
            {
                case StatusCheque.EmCaixa:
                    statusLegivel = "Em Caixa";
                    break;
                case StatusCheque.Depositado:
                    statusLegivel = "Depositado";
                    break;
                case StatusCheque.Devolvido:
                    statusLegivel = "Devolvido";
                    break;
                case StatusCheque.Redepositado:
                    statusLegivel = "Redepositado";
                    break;
                case StatusCheque.Pago:
                    statusLegivel = "Pago";
                    break;
                case StatusCheque.Baixado:
                    statusLegivel = "Baixado";
                    break;
            }
            return statusLegivel;
        }

        public override DateTime? IncData
        {
            get
            {
                return base.IncData;
            }
            set
            {
                base.IncData = value;
            }
        }

        public override string IncUsuario
        {
            get
            {
                return base.IncUsuario;
            }
            set
            {
                base.IncUsuario = value;
            }
        }

        public override DateTime? AltData
        {
            get
            {
                return base.AltData;
            }
            set
            {
                base.AltData = value;
            }
        }

        public override string AltUsuario
        {
            get
            {
                return base.AltUsuario;
            }
            set
            {
                base.AltUsuario = value;
            }
        }
    }
}
